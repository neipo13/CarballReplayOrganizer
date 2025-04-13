using CarballReplayOrganizer.ReplayParsing.Properties;
using RocketLeagueReplayParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.ReplayParsing
{
    public class ReplayParser
    {

        public static ReplayMeta? GetReplayMeta(string file, bool rankedOnly = true, bool mainModesOnly = true, bool debugOutput = false)
        {
            if(debugOutput) Console.Write($"{Path.GetFileNameWithoutExtension(file)} starting....   ");
            Replay? parsedReplay = null;
            //parse replay
            try
            {
                parsedReplay = RocketLeagueReplayParser.Replay.Deserialize(file);
            }
            catch (Exception e)
            {
                if (debugOutput) Console.WriteLine($"PARSE FAILED __ SKIPPING");
                return null;
            }
            var replayName = parsedReplay.Properties[ReplayPropertyConstants.REPLAY_NAME]?.Value.ToString();

            var replayPlayedDateStr = parsedReplay.Properties[ReplayPropertyConstants.DATE]?.Value.ToString();
            DateTime replayPlayedDate;
            var dateParsed = DateTime.TryParse(replayPlayedDateStr, out replayPlayedDate);
            if (!dateParsed) replayPlayedDate = File.GetCreationTime(file);

            string? mode = null;
            mode = GetModeFromReplayName(replayName);
            if (mode == null) mode = GetModeFromReplayPlayerCount(parsedReplay);

            // For now we simply output file info; you'll replace this stub with your parser.
            var replay = new ReplayMeta
            {
                Title = replayName ?? "NO NAME FOUND -----",
                Mode = mode ?? ReplayPropertyConstants.OUTPUT_UNKNOWN,
                Date = replayPlayedDate,
                FilePath = file,
                FileName = Path.GetFileNameWithoutExtension(file),
                IsRanked = IsRankedFromReplayName(replayName) ?? false,
            };
            if (rankedOnly && !replay.IsRanked)
            {
                if (debugOutput) Console.WriteLine("REJECTED UNRANKED");
                return null;
            }
            if (mainModesOnly && replay.Mode == ReplayPropertyConstants.OUTPUT_UNKNOWN)
            {
                if (debugOutput) Console.WriteLine("REJECTED NON-MAIN MODE");
                return null;
            }

            if (debugOutput) Console.WriteLine($"{replay.Title} {replay.Date.ToShortDateString()} {replay.Date.ToShortTimeString()} PARSED");

            //Console.WriteLine("----------");
            //foreach (var item in parsed.Properties.Keys)
            //{
            //    Console.WriteLine($"{item} {parsed.Properties[item].Name} {parsed.Properties[item].Value}");
            //}
            //Console.WriteLine("----------");

            return replay;
        }
        public static bool? IsRankedFromReplayName(string? replayName)
        {
            if (replayName == null) return null;

            if (replayName.Contains(ReplayPropertyConstants.RANKED)) return true;

            return false;
        }

        public static string? GetModeFromReplayName(string? replayName)
        {
            if (replayName == null) return null;

            if (replayName.Contains(ReplayPropertyConstants.ONES)) return ReplayPropertyConstants.OUTPUT_ONES;
            if (replayName.Contains(ReplayPropertyConstants.TWOS)) return ReplayPropertyConstants.OUTPUT_TWOS;
            if (replayName.Contains(ReplayPropertyConstants.THREES)) return ReplayPropertyConstants.OUTPUT_THREES;

            return ReplayPropertyConstants.OUTPUT_UNKNOWN;
        }

        public static string? GetModeFromReplayPlayerCount(Replay replay)
        {
            var players = GetPlayers(replay);
            var orangeCount = GetPlayerCountPerTeam(players, isOrange: true);
            var blueCount = GetPlayerCountPerTeam(players, isOrange: false);

            if (orangeCount != blueCount) return null;

            if (orangeCount == 1) return ReplayPropertyConstants.OUTPUT_ONES;
            if (orangeCount == 2) return ReplayPropertyConstants.OUTPUT_TWOS;
            if (orangeCount == 3) return ReplayPropertyConstants.OUTPUT_THREES;

            return null;
        }


        public static List<Player> GetPlayers(RocketLeagueReplayParser.Replay replay)
        {
            var players = new List<Player>();
            var playerWrappers = new List<PropertyWrapper>();
            foreach(var frame in replay.Frames)
            {
                // check for player actors
                if (frame.ActorStates.Any(a => a.Properties.Any(p => p.Value.PropertyName == PropertyNameConstants.PLAYER_NAME_ENGINE)))
                {
                    var states = frame.ActorStates.ToList();
                    foreach (var state in states)
                    {
                        var props = state.Properties;
                        var wrappers = props.Where(p => p.Value.PropertyName == PropertyNameConstants.PLAYER_NAME_ENGINE)
                            .Select(p => new PropertyWrapper() { Frame = frame, State = state, Property = p.Value, Type = PropertyType.Player })
                            .ToList();

                        playerWrappers.AddRange(wrappers);
                    }
                }
            }
            //get the player info from the player wrappers
            var playerNameGroups = playerWrappers.GroupBy(w => (string)w.Property.Data).ToList();
            foreach (var playerGroup in playerNameGroups)
            {
                var playerName = playerGroup.Key;
                var props = playerGroup.ToList();
                var player = new Player();
                player.PlayerName = playerName;
                player.ActorIDs = props.Select(p => p.State.Id).Distinct().ToList();

                players.Add(player);
            }

            var PlayerStats = (List<PropertyDictionary>)replay.Properties.FirstOrDefault(p => p.Key == "PlayerStats").Value.Value;
            foreach (var stat in PlayerStats)
            {
                //find the player by name field
                var name = (string)stat.FirstOrDefault(s => s.Key == PropertyNameConstants.PLAYER_NAME_PROPERTY).Value.Value;
                var team = (int)stat.FirstOrDefault(s => s.Key == PropertyNameConstants.PLAYER_TEAM_PROPERTY).Value.Value;

                var player = players.FirstOrDefault(p => p.PlayerName == name);
                if (player == null) continue;
                player.isOrange = team == 1;
            }
            return players;
        }

        public static int GetPlayerCountPerTeam(List<Player> players, bool isOrange)
        {
            return players.Where(p => p.isOrange == isOrange).Count();
        }
    }
}
