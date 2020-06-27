using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge;
using Codenation.Challenge.Exceptions;
using Source.Classes;


namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private Dictionary<long, Team> teams;
        private Dictionary<long, Player> players;


        public SoccerTeamsManager()
        {
            teams = new Dictionary<long, Team>();
            players = new Dictionary<long, Player>();

        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (teams.ContainsKey(id))
            {
                throw new UniqueIdentifierException();
            }
            else
            {
                var Team = new Team()
                {
                    Id = id,
                    Name = name,
                    CreateDate = createDate,
                    MainShirtColor = mainShirtColor,
                    SecondaryShirtColor = secondaryShirtColor,

                };

                teams.Add(id, Team);
            }
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (players.ContainsKey(id))
            {
                throw new UniqueIdentifierException();
            }
            else
            {
                var Player = new Player()
                {
                    Id = id,
                    TeamId = teamId,
                    Name = name,
                    BirthDate = birthDate,
                    SkillLevel = skillLevel,
                    Salary = salary,
                    Captain = false

                };

                players.Add(id, Player);
            }

        }

        public void SetCaptain(long playerId)
        {
            if (!players.ContainsKey(playerId))
            {
                throw new PlayerNotFoundException();
            }
            else
            {
                Player playerCaptain = players[playerId];
                long teamIdCaptain = playerCaptain.TeamId;
                RemoveCaptain(teamIdCaptain);
                playerCaptain.Captain = true;
                players[playerId] = playerCaptain;

            }
        }

        private void RemoveCaptain(long teamId)
        {
            foreach (var pair in players)
            {
                long key = pair.Key;
                Player player = pair.Value;

                if (player.TeamId == teamId)
                {
                    player.Captain = false;
                    players[key] = player;
                    break;
                }
            }
        }

        public long GetTeamCaptain(long teamId)
        {
            long idTeamCaptain = 0;

            if (!teams.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                foreach(var item in players)
                {
                    Player player = item.Value;

                    if(player.TeamId == teamId && player.Captain)
                    {
                        idTeamCaptain = player.TeamId;
                    }
                }
            }

            if (idTeamCaptain > 0)
            {
                return idTeamCaptain;
            }
            else
            {
                throw new CaptainNotFoundException();
            }
        }

        public string GetPlayerName(long playerId)
        {
            if (!players.ContainsKey(playerId))
            {
                throw new PlayerNotFoundException();
            }
            else
            {
                return players[playerId].Name.Trim();
            }
        }

        public string GetTeamName(long teamId)
        {
            if (!teams.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                return teams[teamId].Name.Trim();
            }
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!teams.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                IEnumerable<Player> enumPlayers = players.Values.Where(x => x.TeamId == teamId);
                List<long> playersList = new List<long>();

                foreach (Player player in enumPlayers)
                {
                    playersList.Add(player.Id);
                }

                playersList.Sort();

                return playersList;
            }
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!teams.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                int maxSkillLevel = players.Values.Where(x => x.TeamId == teamId).Max(x => x.SkillLevel);

                return players.Values.Where(x => x.TeamId == teamId)
                                        .Where(x => x.SkillLevel == maxSkillLevel)
                                        .Min(x => x.Id);
            }
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!teams.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                DateTime minBirthDate = players.Values.Where(x => x.TeamId == teamId)
                                                .Min(x => x.BirthDate);

                return players.Values.Where(x => x.TeamId == teamId)
                                        .Where(x => x.BirthDate == minBirthDate)
                                        .Min(x => x.Id);
            }
        }

        public List<long> GetTeams()
        {
            List<long> listTeams = new List<long>();

            if(teams.Count > 0)
            {
                foreach (Team team in teams.Values)
                {
                    listTeams.Add(team.Id);
                }

                listTeams.Sort();

                return listTeams;
            }
            else
            {
                return listTeams;
            }

        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!teams.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                decimal maxSalary = players.Values.Where(x => x.TeamId == teamId).Max(x => x.Salary);

                return players.Values.Where(x => x.TeamId == teamId)
                                        .Where(x => x.Salary == maxSalary)
                                        .Min(x => x.Id);
            }
        }

        public decimal GetPlayerSalary(long playerId)
        {
            if (!players.ContainsKey(playerId))
            {
                throw new PlayerNotFoundException();
            }
            else
            {
                return players[playerId].Salary;
            }
            
        }

        public List<long> GetTopPlayers(int top)
        {
            // busco pela ordem decrescente utilizando Linq
            IEnumerable<Player> enumPlayers = players.Values.OrderByDescending(x => x.SkillLevel).Take(top);

            List<long> playersList = new List<long>();

            foreach (Player player in enumPlayers)
            {
                playersList.Add(player.Id);
            }

            return playersList;
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            if (!teams.ContainsKey(teamId) || !teams.ContainsKey(visitorTeamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                Team teamHome = teams[teamId];
                Team teamVisitor = teams[visitorTeamId];

                if(teamHome.MainShirtColor.Trim() == teamVisitor.MainShirtColor.Trim())
                {
                    return teamVisitor.SecondaryShirtColor.Trim();
                }
                else
                {
                    return teamVisitor.MainShirtColor.Trim();
                }

            }
        }

    }
}
