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
            param.Add("@cityName", team.t_cityName);
            param.Add("@countryName", team.t_countryName);
            param.Add("@offical_color", team.t_offical_color);
            param.Add("@second_color", team.t_second_color);
            param.Add("@leagueID", team.t_leagueID);
          
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
    }
}
