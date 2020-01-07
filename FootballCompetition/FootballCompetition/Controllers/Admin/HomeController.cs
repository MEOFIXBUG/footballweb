using FB.League.Domain.Interface;
using FB.League.Domain.Model;
using Microsoft.Practices.Unity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Infras.Common.CommonUltil;

namespace FootballCompetition.Controllers.Admin
{

    public class HomeController : Controller
    {

        // GET: Home
        [Dependency]
        public ILeagueService LeagueService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLeague(LeagueX leage, HttpPostedFileBase[] fileExcel)
        {
            //Ensure model state is valid

            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in fileExcel)
                {
                    //Checking file is available to save.  
                    if (file != null && file.ContentLength > 0)
                    {
                        //Xu ly
                        // thong tin team , footballer , 
                    }
                }
            }
            return RedirectToAction("DetailLeague", "Common", new { id = 1, area = "" }); //SCOPE_IDENTITY()
        }
        public ActionResult AddLeague(int? index)
        {
            if (index == null)
            {
                LeagueX model = new LeagueX();
                return View(model);
            }
            else
            {
                var model = LeagueService.GetLeagueByID(index.Value);
                return View(model);
            }
        }
        public ActionResult TeamListFile(int leagueID)
        {
            List<string> Name = new List<string>();
            //get team name
            if (TempData.ContainsKey("ERROR"))
            {
                ViewBag.ERROR = TempData["ERROR"].ToString();
            }
            //ViewBag.TeamList = Name;
            ViewBag.leagueID = leagueID;
            return View();
        }
        [HttpPost]
        public ActionResult TeamListFile(int leagueID, HttpPostedFileBase[] fileExcel)
        {
            var infoLeague = LeagueService.GetLeagueByID(leagueID);
            List<string> Name = new List<string>();
            foreach (HttpPostedFileBase file in fileExcel)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    //doc excel.
                    try
                    {

                        using (ExcelPackage package = new ExcelPackage(file.InputStream))
                        {
                            var workbook = package.Workbook;
                            var worksheet = workbook.Worksheets[1];
                            // get infoTeam from excel 
                            var newcollection = worksheet.ConvertSheetToObjects<Team>();
                            Team team = newcollection.FirstOrDefault();
                            team.t_leagueID = leagueID;
                            //Name.Add(team.t_NAME);
                            //insert team return scopeID
                            //set playing club and countryteam
                            //
                            //get footbaler from excel
                            var worksheet1 = workbook.Worksheets[2];
                            var newcollection1 = worksheet1.ConvertSheetToObjects<Footballer>();
                            var footbalerNumber = newcollection1.Count();
                            var over = newcollection1.Where(x => DateTime.Now.Year - x.fb_DOB.Year > infoLeague.l_AOR).Count();
                            if (over <= infoLeague.l_over)
                            {
                                if (footbalerNumber <= infoLeague.l_max && footbalerNumber >= infoLeague.l_min)
                                {
                                    team.t_city = LeagueService.CreateOneCity(team.t_cityName);
                                    team.t_country = LeagueService.CreateOneCountry(team.t_countryName);
                                    var stadium = new Stadium();
                                    stadium.s_name = team.t_stadiumName;
                                    stadium.s_city = team.t_city;
                                    stadium.s_country = team.t_country;
                                    team.t_stadium = LeagueService.CreateOneStadium(stadium);
                                    team.t_ID = LeagueService.InsertTeam(team);
                                    foreach (var item in newcollection1)
                                    {

                                        item.fb_teamID = team.t_ID;
                                        //insert item
                                        LeagueService.InsertFootballer(item);
                                    }
                                }
                                else
                                {
                                    ViewBag.TeamList = Name;
                                    ViewBag.Error = $"{footbalerNumber} is not in allowed arrange from {infoLeague.l_min} to {infoLeague.l_max}";
                                    TempData["ERROR"] = $"{team.t_NAME} : {footbalerNumber} is not in allowed arrange from {infoLeague.l_min} to {infoLeague.l_max}";
                                    return RedirectToAction("TeamListFile", new { leagueID });
                                }
                            }
                            else
                            {
                                ViewBag.TeamList = Name;
                                ViewBag.Error = $"There are more {over - infoLeague.l_over} overallowedAge footballers";
                                TempData["ERROR"] = $"{team.t_NAME} : There are more {over - infoLeague.l_over} overallowedAge footballers";
                                return RedirectToAction("TeamListFile", new { leagueID });
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //string filePath = Path.Combine(Server.MapPath("/UploadedFiles/"), Path.GetFileName(file.FileName));
                    //file.SaveAs(filePath);
                    //ghi data


                }

            }
            //ViewBag.TeamList = Name;
            return RedirectToAction("TeamListFile", new { leagueID });
        }
        [HttpPost]
        public JsonResult DeleteTeam(int[] idList)
        {
            return Json("");
        }
        [HttpPost]
        public ActionResult Generate(int leagueID)
        {
            try
            {
                var infoLeague = LeagueService.GetLeagueByID(leagueID);
                var allTeam = LeagueService.GetAllTeamByID(leagueID).ToList();
                if (allTeam.Count() == infoLeague.l_teamNumber)
                {
                    int totalRounds = (infoLeague.l_teamNumber - 1) * 2;
                    int halfSeason = totalRounds / 2;
                    int matchesPerRound = infoLeague.l_teamNumber / 2;
                    DateTime RoundstartAt = infoLeague.l_start;

                    int dateStep = (int)(infoLeague.l_end - infoLeague.l_start).TotalDays;
                    for (int round = 0; round < totalRounds; round++)
                    {
                        var rnd = new Round();
                        rnd.r_name = $"Round{round + 1}";
                        rnd.r_start = RoundstartAt;
                        rnd.r_end = RoundstartAt.AddDays(dateStep);
                        rnd.r_league = leagueID;
                        //create round return id
                        int idRound = LeagueService.CreateOneRound(rnd);
                        for (int match = 0; match < matchesPerRound; match++)
                        {
                            //set idround for match
                            //add match
                            int home;
                            int guess;
                            if (round < halfSeason)
                            {
                                home = (round + match) % (infoLeague.l_teamNumber - 1);
                                guess = (infoLeague.l_teamNumber - 1 - match + round) % (infoLeague.l_teamNumber - 1);
                            }
                            else
                            {
                                guess = (round + match) % (infoLeague.l_teamNumber - 1);
                                home = (infoLeague.l_teamNumber - 1 - match + round) % (infoLeague.l_teamNumber - 1);
                            }

                            var vs = new Vs();
                            vs.vs_home = allTeam[home];
                            vs.vs_guess = allTeam[guess];
                            vs.vs_round = idRound;
                            vs.vs_stadium = allTeam[home].t_stadium;
                            vs.vs_date = rnd.r_start.AddDays(match % dateStep).AddMinutes(60 * 17);
                            //insert.....
                            LeagueService.InsertVS(vs);
                        }
                        RoundstartAt = rnd.r_end.AddDays(1);
                    }
                    return Json(new { Result = "successfull" });
                }
                else
                {
                    return Json(new { Result = "failure", Mess = "Not Enough" });
                   
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "error" });
                
            }

        }
    }
}