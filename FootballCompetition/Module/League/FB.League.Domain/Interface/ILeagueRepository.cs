using FB.League.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infras.Common.CommonUltil;

namespace FB.League.Domain.Interface
{
    public interface ILeagueRepository
    {
        IEnumerable<LeagueX> GetAllLeague(DatatableReq obj);
        LeagueX GetLeagueByID(int id);
        bool CreateOneLeague(LeagueX league);
        int InsertTeam(Team team);
        IEnumerable<Team> GetAllTeam(DatatableReq obj);
        bool InsertFootballer(Footballer fb);
    }
}
