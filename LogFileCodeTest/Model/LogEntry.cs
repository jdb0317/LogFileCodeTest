using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileCodeTest.Model
{
    public class LogEntry
    {
        public string Date_Time { get; set; }
        public string C_ip { get; set; }
        public string Cs_username { get; set; }
        public string S_sitename { get; set; }
        public string S_computername { get; set; }
        public string S_ip { get; set; }
        public string S_port { get; set; }
        public string Cs_method { get; set; }
        public string Cs_uri_stem { get; set; }
        public string Cs_uri_query { get; set; }
        public string Sc_status { get; set; }
        public string Sc_win32_status { get; set; }
        public string Sc_bytes { get; set; }
        public string Cs_bytes { get; set; }
        public string Time_taken { get; set; }
        public string Cs_version { get; set; }
        public string Cs_host { get; set; }
        public string Cs_User_Agent { get; set; }
        public string Cs_Cookie { get; set; }
        public string Cs_Referer { get; set; }
    }
}
