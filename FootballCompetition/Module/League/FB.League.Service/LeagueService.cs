using FB.League.Domain.Interface;
using FB.League.Domain.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infras.Common.CommonUltil;

namespace FB.League.Service
{
    public class LeagueService : ILeagueService
    {
        [Dependency]
        public ILeagueRepository LeagueRepository { get; set; }
        public IEnumerable<LeagueX> GetAllLeague(DatatableReq obj)
        {
            try
            {
                return LeagueRepository.GetAllLeague(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public LeagueX GetLeagueByID(int id)
        {
            try
            {
                return LeagueRepository.GetLeagueByID(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool CreateOneLeague(LeagueX league)
        {
            try
            {
                return LeagueRepository.CreateOneLeague(league);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public int InsertTeam(Team team)
        {
            try
            {
                return LeagueRepository.InsertTeam(team);
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        public IEnumerable<Team> GetAllTeam(DatatableReq obj)
        {
            try
            {
                return LeagueRepository.GetAllTeam(obj);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public bool InsertFootballer(Footballer fb)
        {
            try
            {
                return LeagueRepository.InsertFootballer(fb);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public int CreateOneRound(Round round)
        {
            try
            {
                return LeagueRepository.CreateOneRound(round);
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        public IEnumerable<Team> GetAllTeamByID(int leagueID)
        {
            try
            {
                return LeagueRepository.GetAllTeamByID(leagueID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int CreateOneCity(string cityName)
        {
            try
            {
                return LeagueRepository.CreateOneCity(cityName);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int CreateOneCountry(string countryName)
        {
            try
            {
                return LeagueRepository.CreateOneCountry(countryName);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int CreateOneStadium(Stadium stadium)
        {
            try
            {
                return LeagueRepository.CreateOneStadium(stadium);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public bool InsertVS(Vs vs)
        {
            try
            {
                return LeagueRepository.InsertVS(vs);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public IEnumerable<Round> GetAllRound(DatatableReq obj)
        {

            try
            {
                return LeagueRepository.GetAllRound(obj);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public IEnumerable<Vs> GetAllVs(DatatableReq obj)
        {
            try
            {
                var result= LeagueRepository.GetAllVs(obj);
                foreach (var item in result)
                {
                    item.vs_homeX = LeagueRepository.GetOneTeamByID(item.vs_home);
                    item.vs_guessX = LeagueRepository.GetOneTeamByID(item.vs_guess);
                    item.vs_homeName = item.vs_homeX.t_NAME;
                    item.vs_guessName = item.vs_guessX.t_NAME;
                    item.vs_stadiumName = LeagueRepository.GetOneStadiumByID(item.vs_stadium).s_name;
                }
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public IEnumerable<Vs> GetVsOfRound(DatatableReq obj)
        {
            try
            {
                var result = LeagueRepository.GetVsOfRound(obj);
                foreach (var item in result)
                {
                    item.vs_homeX = LeagueRepository.GetOneTeamByID(item.vs_home);
                    item.vs_guessX = LeagueRepository.GetOneTeamByID(item.vs_guess);
                    item.vs_homeName = item.vs_homeX.t_NAME;
                    item.vs_guessName = item.vs_guessX.t_NAME;
                    item.vs_stadiumName = LeagueRepository.GetOneStadiumByID(item.vs_stadium).s_name;
                }
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public bool UpdateScored(Vs vs)
        {
            try
            {
                vs.vs_goalDiff = vs.vs_homeScore - vs.vs_guessScore;
                vs.vs_goalTotal = vs.vs_homeScore + vs.vs_guessScore;
                return LeagueRepository.UpdateScored(vs);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public Stadium GetOneStadiumByID(int id)
        {
            try
            {
                return LeagueRepository.GetOneStadiumByID(id);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
