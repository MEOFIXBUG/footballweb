using Dapper;
using FB.League.Domain.Interface;
using FB.League.Domain.Model;
using Infras.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infras.Common.CommonUltil;

namespace FB.League.Repository
{
    public class LeagueRepository : ILeagueRepository
    {
       public IEnumerable<LeagueX> GetAllLeague(DatatableReq obj)
       {
            var param = new DynamicParameters();
            param.Add("@SearchTerm", obj.search);
            param.Add("@SortColumn", obj.cField);
            param.Add("@SortOrder", obj.sort);
            param.Add("@PageNumber", obj.page);
            param.Add("@PageSize", obj.perpage);
            return DALHelpers.QueryByStored<LeagueX>("usp_League_GetAll", param);
       }
        public IEnumerable<Team> GetAllTeam(DatatableReq obj)
        {
            var param = new DynamicParameters();
            param.Add("@LeagueID", obj.Id);
            param.Add("@SearchTerm", obj.search);
            param.Add("@SortColumn", obj.cField);
            param.Add("@SortOrder", obj.sort);
            param.Add("@PageNumber", obj.page);
            param.Add("@PageSize", obj.perpage);
            return DALHelpers.QueryByStored<Team>("usp_Team_GetAll", param);
        }
        public LeagueX GetLeagueByID(int id)
        {
            var param = new DynamicParameters();
            param.Add("@ID", id);
            return DALHelpers.QueryByStored<LeagueX>("usp_League_GetByID", param).FirstOrDefault();
        }
        public bool CreateOneLeague( LeagueX league)
        {
            var param = new DynamicParameters();
            //param.Add("@ID", league.l_AOR);
            //param.Add("@ID", league.l_ID);
            param.Add("@startAt", league.l_start);
            param.Add("@endAt", league.l_end);
            param.Add("@name", league.l_name);
            param.Add("@teamNumber", league.l_teamNumber);
            param.Add("@descript", league.l_descript);
            param.Add("@for", league.l_for);
            param.Add("@celebrateBY", league.l_celebrateBY);
            return DALHelpers.ExecuteByStored("usp_League_CreateOne", param) > 0;
        }
        public int InsertTeam(Team team)
        {
            var param = new DynamicParameters();
            //param.Add("@ID", league.l_AOR);
            //param.Add("@ID", league.l_ID);
            param.Add("@teamName", team.t_NAME);
            param.Add("@teamType", team.t_type);
            param.Add("@city", team.t_city);
            param.Add("@country", team.t_country);
            param.Add("@offical_color", team.t_offical_color);
            param.Add("@second_color", team.t_second_color);
            param.Add("@leagueID", team.t_leagueID);
            param.Add("@stadium", team.t_stadium);
            return DALHelpers.QueryByStored<int>("InsertTeam", param).FirstOrDefault();
        }

        public bool InsertFootballer(Footballer fb)
        {
            var param = new DynamicParameters();
            param.Add("@fbName", fb.fb_name);
            param.Add("@fbDOB", fb.fb_DOB);
            param.Add("@fbPosition", fb.fb_position);
            param.Add("@fb_jersey_no1", fb.fb_jersey_no1);
            param.Add("@fb_jersey_no2", fb.fb_jersey_no2);
            param.Add("@fb_teamID", fb.fb_teamID);
            return DALHelpers.ExecuteByStored("usp_FBler_Insertfbler", param) > 0;
        }
        public int CreateOneRound(Round round)
        {
            var param = new DynamicParameters();
            //param.Add("@ID", league.l_AOR);
            //param.Add("@ID", league.l_ID);
            param.Add("@RoundName", round.r_name);
            param.Add("@RoundstartAt", round.r_start);
            param.Add("@RoundendAt", round.r_end);
            param.Add("@leagueID", round.r_league);
            return DALHelpers.QueryByStored<int>("usp_Round_CreateOne", param).FirstOrDefault();
        }
        public IEnumerable<Team> GetAllTeamByID(int leagueID)
        {
            var param = new DynamicParameters();
            param.Add("@ID", leagueID);
            return DALHelpers.QueryByStored<Team>("usp_Team_GetByLeagueID", param);
        }
        public int CreateOneCity(string ciyName)
        {
            var param = new DynamicParameters();
            //param.Add("@ID", league.l_AOR);
            //param.Add("@ID", league.l_ID);
            param.Add("@cityName", ciyName);
           
            return DALHelpers.QueryByStored<int>("usp_City_CreateOne", param).FirstOrDefault();
        }
        public int CreateOneCountry(string countryName)
        {
            var param = new DynamicParameters();
            //param.Add("@ID", league.l_AOR);
            //param.Add("@ID", league.l_ID);
            param.Add("@countryName", countryName);

            return DALHelpers.QueryByStored<int>("usp_Country_CreateOne", param).FirstOrDefault();
        }
        public int CreateOneStadium(Stadium stadium)
        {
            var param = new DynamicParameters();
            //param.Add("*@ID", league.l_AOR);
            //param.Add("@ID", league.l_ID);
            param.Add("@s_name", stadium.s_name);
            param.Add("@s_city", stadium.s_city);
            param.Add("@s_country", stadium.s_country);
            return DALHelpers.QueryByStored<int>("usp_Stadium_CreateOne", param).FirstOrDefault();
        }

        public bool InsertVS(Vs vs)
        {
            var param = new DynamicParameters();
            param.Add("@vs_round", vs.vs_round);
            param.Add("@vs_league", vs.vs_league);
            param.Add("@vs_home", vs.vs_homeX.t_ID);
            param.Add("@vs_guess", vs.vs_guessX.t_ID);
            param.Add("@vs_date", vs.vs_date);
            param.Add("@vs_stadium", vs.vs_stadium);
            return DALHelpers.ExecuteByStored("usp_Vs_CreateOne", param) > 0;
        }
        public IEnumerable<Round> GetAllRound(DatatableReq obj)
        {
            var param = new DynamicParameters();
            param.Add("@LeagueID", obj.Id);
            param.Add("@SearchTerm", obj.search);
            param.Add("@SortColumn", obj.cField);
            param.Add("@SortOrder", obj.sort);
            param.Add("@PageNumber", obj.page);
            param.Add("@PageSize", obj.perpage);
            return DALHelpers.QueryByStored<Round>("usp_Round_GetAll", param);
        }
        public IEnumerable<Vs> GetAllVs(DatatableReq obj)
        {
            var param = new DynamicParameters();
            param.Add("@LeagueID", obj.Id);
            param.Add("@SearchTerm", obj.search);
            param.Add("@SortColumn", obj.cField);
            param.Add("@SortOrder", obj.sort);
            param.Add("@PageNumber", obj.page);
            param.Add("@PageSize", obj.perpage);
            var a= DALHelpers.QueryByStored<Vs>("usp_Vs_GetAll", param);
            return a;
        }
        public Team GetOneTeamByID(int id)
        {
            var param = new DynamicParameters();
            param.Add("@ID", id);
            return DALHelpers.QueryByStored<Team>("usp_Team_GetOneByID", param).FirstOrDefault();
        }
        public IEnumerable<Vs> GetVsOfRound(DatatableReq obj)
        {
            var param = new DynamicParameters();
            param.Add("@roundID", obj.Id);
            param.Add("@SearchTerm", obj.search);
            param.Add("@SortColumn", obj.cField);
            param.Add("@SortOrder", obj.sort);
            param.Add("@PageNumber", obj.page);
            param.Add("@PageSize", obj.perpage);
            var a = DALHelpers.QueryByStored<Vs>("usp_Vs_GetAllOfRound", param);
            return a;
        }
        public bool UpdateScored(Vs vs)
        {
            var param = new DynamicParameters();
            param.Add("@vsID", vs.vs_ID);
            param.Add("@vsGoalTotal", vs.vs_goalTotal);
            param.Add("@vsGoalDiff", vs.vs_goalDiff);
            var a = DALHelpers.ExecuteByStored("usp_Vs_updateScored", param)>0;
            return a;
        }
        public Stadium GetOneStadiumByID(int id)
        {
            var param = new DynamicParameters();
            param.Add("@ID", id);
            return DALHelpers.QueryByStored<Stadium>("usp_Stadium_GetOneByID", param).FirstOrDefault();
        }
    }
}
