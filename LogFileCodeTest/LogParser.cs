using LogFileCodeTest.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogFileCodeTest
{
    internal class LogParser
    {
        private readonly string FilePath;
        private readonly string ReportFilePath;
        private readonly string IPExclusionFilter;
        private readonly string PortFilter;
        private readonly string MethodFilter;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogParser()
        {
            FilePath = ConfigurationManager.AppSettings[Constants.LogFilePathSettingsKey].ToString();
            ReportFilePath = ConfigurationManager.AppSettings[Constants.ReportFilePathSettingsKey].ToString();
            IPExclusionFilter = ConfigurationManager.AppSettings[Constants.IPExclusionFilterSettingsKey].ToString();
            PortFilter = ConfigurationManager.AppSettings[Constants.PortFilterSettingsKey].ToString();
            MethodFilter = ConfigurationManager.AppSettings[Constants.RequestMethodFilterSettingsKey].ToString();
        }

        /// <summary>
        /// Parse file and return results
        /// </summary>
        public IEnumerable<Tuple<int, IPAddress>> Parse()
        {
            var requestCollection = File.ReadLines(FilePath)
                .Where(line => !line.StartsWith("#"))
                .Select(line => CreateLogEntry(line))
                .Where(request => !request.C_ip.StartsWith(IPExclusionFilter)
                    && request.S_port == PortFilter
                    && request.Cs_method.ToLower() == MethodFilter)
                .GroupBy(request => request.C_ip)
                .Select(grp => new Tuple<int, IPAddress>(
                    grp.Count(),
                    IPAddress.Parse(grp.Key)))
                .OrderByDescending(t => t.Item1)
                    .ThenByDescending(t => t.Item2, new IPComparison());

            return requestCollection;
        }

        /// <summary>
        /// Convert a single log entry string into a LogEntry
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private LogEntry CreateLogEntry(string line)
        {
            var parts = line.Split(' ');
            return new LogEntry()
            {
                C_ip = parts[2] ?? string.Empty,
                S_port = parts[7] ?? string.Empty,
                Cs_method = parts[8] ?? string.Empty
            };
        }

        /// <summary>
        /// Writes output to text destination
        /// </summary>
        /// <param name="requests"></param>
        public void WriteToFile(IEnumerable<Tuple<int, IPAddress>> requests)
        {
            var sb = new StringBuilder();
            foreach(var req in requests)
            {
                sb.AppendLine($"{req.Item1}, \"{req.Item2.ToString()}\"");
            }

            File.WriteAllText(ReportFilePath, sb.ToString());
        }
    }


}
