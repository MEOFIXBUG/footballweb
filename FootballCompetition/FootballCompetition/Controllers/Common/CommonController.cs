using FB.League.Domain.Interface;
using FB.League.Domain.Model;
using Infras.Common;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Infras.Common.CommonUltil;

namespace FootballCompetition.Controllers.Common
{
    public class CommonController : Controller
    {
        // GET: Common
        [Dependency]
        public ILeagueService LeagueService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllLeague()
        {
            DatatableReq datatableReq = GetDatatableReq(Request);
           

            IEnumerable<LeagueX> leagues = LeagueService.GetAllLeague(datatableReq);

            dynamic result = new JObject();

            dynamic data = new JArray();
            if (leagues != null)
            {
                foreach (var league in leagues)
                {
                    datatableReq.total = league.Total;
                    data.Add(JObject.FromObject(league));
                }
            }

            dynamic meta = new JObject();
            meta["page"] = datatableReq.page;
            meta["pages"] = datatableReq.total / datatableReq.perpage;
            meta["perpage"] = datatableReq.perpage;
            meta["total"] = datatableReq.total;
            meta["sort"] = datatableReq.sort;
            meta["field"] = datatableReq.cField;

            result["meta"] = meta;
            result["data"] = data;

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult DetailLeague(int id)
        {
            var result = LeagueService.GetLeagueByID(id);
            ViewBag.Admin = true;
            return View(result);
        }

        public ActionResult GetAllTeam(int leagueID)
        {
            DatatableReq datatableReq = GetDatatableReq(Request);
            datatableReq.Id = leagueID;
            IEnumerable <Team> teams = LeagueService.GetAllTeam(datatableReq);

            dynamic result = new JObject();

            dynamic data = new JArray();
            if (teams != null)
            {
                foreach (var team in teams)
                {
                    datatableReq.total = team.Total;
                    team.t_matches = team.t_playedMatch;
                    //team.t_Point =3*team.t_won + team.t_draw;
                    team.t_Diff = team.t_goal - team.t_goalLost;
                    data.Add(JObject.FromObject(team));
                }
            }

            dynamic meta = new JObject();
            meta["page"] = datatableReq.page;
            meta["pages"] = datatableReq.total / datatableReq.perpage;
            meta["perpage"] = datatableReq.perpage;
            meta["total"] = datatableReq.total;
            meta["sort"] = datatableReq.sort;
            meta["field"] = datatableReq.cField;

            result["meta"] = meta;
            result["data"] = data;

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        public ActionResult GetAllRound(int leagueID)
        {
            DatatableReq datatableReq = GetDatatableReq(Request);
            datatableReq.Id = leagueID;
            IEnumerable<Round> rounds = LeagueService.GetAllRound(datatableReq);

            dynamic result = new JObject();

            dynamic data = new JArray();
            if (rounds != null)
            {
                foreach (var round in rounds)
                {
                    datatableReq.total = round.Total;
                    //team.t_matches = team.t_won + team.t_lost + team.t_draw;
                    //team.t_Point = 3 * team.t_won + team.t_draw;
                    //team.t_Diff = team.t_goal - team.t_goalLost;
                    data.Add(JObject.FromObject(round));
                }
            }

            dynamic meta = new JObject();
            meta["page"] = datatableReq.page;
            meta["pages"] = datatableReq.total / datatableReq.perpage;
            meta["perpage"] = datatableReq.perpage;
            meta["total"] = datatableReq.total;
            meta["sort"] = datatableReq.sort;
            meta["field"] = datatableReq.cField;

            result["meta"] = meta;
            result["data"] = data;

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        public ActionResult GetAllVs(int leagueID)
        {
            DatatableReq datatableReq = GetDatatableReq(Request);
            datatableReq.Id = leagueID;
            IEnumerable<Vs> vs_es = LeagueService.GetAllVs(datatableReq);

            dynamic result = new JObject();

            dynamic data = new JArray();
            if (vs_es != null)
            {
                foreach (var vs in vs_es)
                {
                    datatableReq.total = vs.Total;
                    //team.t_matches = team.t_won + team.t_lost + team.t_draw;
                    //team.t_Point = 3 * team.t_won + team.t_draw;
                    //team.t_Diff = team.t_goal - team.t_goalLost;
                    data.Add(JObject.FromObject(vs));
                }
            }

            dynamic meta = new JObject();
            meta["page"] = datatableReq.page;
            meta["pages"] = datatableReq.total / datatableReq.perpage;
            meta["perpage"] = datatableReq.perpage;
            meta["total"] = datatableReq.total;
            meta["sort"] = datatableReq.sort;
            meta["field"] = datatableReq.cField;

            result["meta"] = meta;
            result["data"] = data;

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        public ActionResult GetVsOfRound(int RoundID)
        {
            DatatableReq datatableReq = GetDatatableReq(Request);
            datatableReq.Id = RoundID;
            IEnumerable<Vs> vs_es = LeagueService.GetVsOfRound(datatableReq);

            dynamic result = new JObject();

            dynamic data = new JArray();
            if (vs_es != null)
            {
                foreach (var vs in vs_es)
                {
                    datatableReq.total = vs.Total;
                    //team.t_matches = team.t_won + team.t_lost + team.t_draw;
                    //team.t_Point = 3 * team.t_won + team.t_draw;
                    //team.t_Diff = team.t_goal - team.t_goalLost;
                    data.Add(JObject.FromObject(vs));
                }
            }

            dynamic meta = new JObject();
            meta["page"] = datatableReq.page;
            meta["pages"] = datatableReq.total / datatableReq.perpage;
            meta["perpage"] = datatableReq.perpage;
            meta["total"] = datatableReq.total;
            meta["sort"] = datatableReq.sort;
            meta["field"] = datatableReq.cField;

            result["meta"] = meta;
            result["data"] = data;

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}