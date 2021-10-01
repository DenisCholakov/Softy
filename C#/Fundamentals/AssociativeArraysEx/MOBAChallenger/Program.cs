using System;
using System.Linq;
using System.Collections.Generic;

namespace MOBAChallenger
{
    class Program
    {
        static void Main(string[] args)
        {
            var playersRanking = new Dictionary<string, Dictionary<string, int>>();
            string input = Console.ReadLine();

            while (input != "Season end")
            {
                if (input.Contains("->"))
                {
                    string[] playerInfo = input.Split(" -> ");
                    string player = playerInfo[0];
                    string position = playerInfo[1];
                    int skill = int.Parse(playerInfo[2]);
                    if (!playersRanking.ContainsKey(player))
                    {
                        playersRanking.Add(player, new Dictionary<string, int>());
                        playersRanking[player].Add(position, skill);
                    }
                    else
                    {
                        if (!playersRanking[player].ContainsKey(position))
                        {
                            playersRanking[player].Add(position, skill);
                        }
                        else
                        {
                            if (playersRanking[player][position] < skill)
                            {
                                playersRanking[player][position] = skill;
                            }
                        }
                    }
                }
                else
                {
                    string[] players = input.Split(" vs ");
                    if (playersRanking.ContainsKey(players[0]) && playersRanking.ContainsKey(players[1]))
                    {
                        var player1 = playersRanking[players[0]];
                        var player2 = playersRanking[players[1]];

                        foreach (var pair in player1)
                        {
                            if (player2.ContainsKey(pair.Key))
                            {
                                int skillPlayer1 = player1.Sum(x => x.Value);
                                int skillPlayer2 = player2.Sum(x => x.Value);
                                if (skillPlayer1 < skillPlayer2)
                                {
                                    playersRanking.Remove(players[0]);
                                }
                                else if (skillPlayer1 > skillPlayer2)
                                {
                                    playersRanking.Remove(players[1]);
                                }
                            }
                        }
                    }
                }
                input = Console.ReadLine();
            }

            foreach (var player in playersRanking.OrderByDescending(x => x.Value.Sum(x => x.Value)).ThenBy(x => x.Key))
            {
                System.Console.WriteLine($"{player.Key}: {player.Value.Sum(x => x.Value)} skill");
                foreach (var pair in player.Value.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    System.Console.WriteLine($"- {pair.Key} <::> {pair.Value}");
                }
            }
        }
    }
}
