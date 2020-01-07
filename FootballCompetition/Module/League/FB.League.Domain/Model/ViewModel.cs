using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.League.Domain.Model
{
    public class Footballer
    {
        public int fb_ID { get; set; }
        public DateTime fb_DOB { get; set; }
        public string fb_name { get; set; }
        public int fb_playing_club { get; set; }
        public string fb_position { get; set; }
        public int fb_jersey_no1 { get; set; }
        public int fb_jersey_no2 { get; set; }
        public int fb_country_team { get; set; }
        public int fb_teamID { get; set; }
    }
    public class Vs
    {
        public int vs_ID { get; set; }
        public int vs_round { get; set; }
        //public int vs_home { get; set; }
        public Team vs_home { get; set; }
        //public int vs_guess { get; set; }
        public Team vs_guess { get; set; }
        public int vs_stadium { get; set; }
        public string vs_decidedBy { get; set; }
        public int vs_referee { get; set; }
        public string vs_result { get; set; }
        public int vs_goalTotal { get; set; }
        public int vs_goalDiff { get; set; }
        public int vs_league { get; set; }
        public DateTime vs_date { get; set; }
    }
    public class Stadium
    {
        public int s_ID { get; set; }
        public string s_name { get; set; }
        public int s_city { get; set; }
        public int s_country { get; set; }
    }
    public class Round
    {
        public int r_ID { get; set; }
        public string r_name { get; set; }
        public DateTime r_start { get; set; }
        public DateTime r_end { get; set; }
        public int r_league { get; set; }
        public IEnumerable<Vs> Vs_es { get; set; }
    }
    public class Rank
    {

    }
    public class Team
    {
        public int Total { get; set; }
        public int t_ID { get; set; }
        public DateTime t_DOF { get; set; }
        public string t_NAME { get; set; }
        public int t_playedMatch { get; set; }
        public int t_won { get; set; }
        public int t_draw { get; set; }
        public int t_lost { get; set; }
        public int t_goal { get; set; }
        public int t_type { get; set; }
        public int t_city { get; set; }
        public int t_country { get; set; }
        public string t_cityName { get; set; }
        public string t_countryName { get; set; }
        public string t_offical_color { get; set; }
        public string t_second_color { get; set; }
        public IEnumerable<Footballer> footballers { get; set; }
        public string t_template { get; set; }
        public int t_leagueID { get; set; }
        public int t_stadium { get; set; }
        public string t_stadiumName { get; set; }
    }
    public class LeagueX
    {
        public int Total { get; set; }
        public int l_ID { get; set; }
        public string l_name { get; set; }
        public int l_AOR { get; set; }
        public DateTime l_start { get; set; }
        public DateTime l_end { get; set; }
        public int l_teamNumber { get; set; }
        public string l_descript { get; set; }
        public string l_celebrateBY { get; set; }
        public int l_for { get; set; } //type team
        public IEnumerable<Round> l_rounds { get; set; }
        public IEnumerable<Rank> l_Rank { get; set; }
        public int l_max { get; set; }
        public int l_min { get; set; }
        public int l_over { get; set; }
    }
}
