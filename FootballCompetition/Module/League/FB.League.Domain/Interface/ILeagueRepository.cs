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
        int CreateOneRound(Round round);
        IEnumerable<Team> GetAllTeamByID(int leagueID);
        int CreateOneCity(string ciyName);

        int CreateOneCountry(string countryName);

        int CreateOneStadium(Stadium stadium);
        bool InsertVS(Vs vs);
        IEnumerable<Round> GetAllRound(DatatableReq obj);
        IEnumerable<Vs> GetAllVs(DatatableReq obj);
        Team GetOneTeamByID(int id);
        IEnumerable<Vs> GetVsOfRound(DatatableReq obj);
        bool UpdateScored(Vs vs);
       Stadium GetOneStadiumByID(int id);
    }
}
