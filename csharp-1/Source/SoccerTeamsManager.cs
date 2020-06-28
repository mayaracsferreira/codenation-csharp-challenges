using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Exceptions;
using Source;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {

        Dictionary<long, Team> Teams = new Dictionary<long, Team>();
        Dictionary<long, Player> Players = new Dictionary<long, Player>();
        Dictionary<long, long> TeamCaptain = new Dictionary<long, long>();

        public SoccerTeamsManager()
        {
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (GetTeams().Contains(id))
            {
                throw new UniqueIdentifierException();
            }
            else
            {
                Teams.Add(id, new Team(id, name, createDate, mainShirtColor, secondaryShirtColor));
            }
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (Players.ContainsKey(id))
            {
                throw new UniqueIdentifierException();
            }
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }
            Players.Add(id, new Player(id, teamId, name, birthDate, skillLevel, salary));
        }

        public void SetCaptain(long playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new PlayerNotFoundException();
            }

            var teamId = GetTeamByPlayerId(playerId);
            //Se time ja tiver um capitao atualiza o valor
            if (TeamCaptain.ContainsKey(teamId))
            {
                TeamCaptain[teamId] = playerId;
            }
            //Senao adiciona o time e o jogador
            else
            {
                TeamCaptain.Add(teamId, playerId);
            }
        }

        public long GetTeamCaptain(long teamId)
        {
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }
            if (!TeamCaptain.ContainsKey(teamId))
            {
                throw new CaptainNotFoundException();
            }

            return TeamCaptain[teamId];
        }

        public string GetPlayerName(long playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new PlayerNotFoundException();
            }

            return Players[playerId].Name;
        }

        public string GetTeamName(long teamId)
        {
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }

            return Teams[teamId].Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }

            return Players.Values.Where(x => x.TeamID == teamId).Select(_ => _.ID).OrderBy(_ => _).ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }

            return Players.Values.Where(x => x.TeamID == teamId).OrderByDescending(_ => _.SkillLevel).Select(_ => _.ID).First();
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }

            return Players.Values.Where(x => x.TeamID == teamId).OrderBy(_ => _.BirthDate).Select(_ => _.ID).FirstOrDefault();
        }

        public List<long> GetTeams()
        {
            return Teams.Keys.OrderBy(_ => _).ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!GetTeams().Contains(teamId))
            {
                throw new TeamNotFoundException();
            }

            return Players.Values.Where(x => x.TeamID == teamId).OrderByDescending(_ => _.Salary).Select(_ => _.ID).First();
        }

        public decimal GetPlayerSalary(long playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new PlayerNotFoundException();
            }

            return Players[playerId].Salary;
        }

        public List<long> GetTopPlayers(int top)
        {
            return Players.Values.OrderByDescending(_ => _.SkillLevel).Select(_ => _.ID).Take(top).ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            if (!GetTeams().Contains(teamId) || !GetTeams().Contains(visitorTeamId))
            {
                throw new TeamNotFoundException();
            }

            var localTeam = Teams[teamId];
            var visitorTeam = Teams[visitorTeamId];

            if (localTeam.MainShirtColor.Equals(visitorTeam.MainShirtColor))
            {
                return visitorTeam.SecondaryShirtColor;
            }
            else
            {
                return visitorTeam.MainShirtColor;
            }
        }

        //Metodos fora do escopo do contrato
        public long GetTeamByPlayerId(long playerId)
        {
            return Players[playerId].TeamID;
        }

        public bool PlayerExists(long playerId)
        {
            if (Players.ContainsKey(playerId))
            {
                return true;
            }
            return false;
        }

    }
}
