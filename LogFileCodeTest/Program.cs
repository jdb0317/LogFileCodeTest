using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileCodeTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = new LogParser();
            var requests = parser.Parse();
            parser.WriteToFile(requests);
        }
        
    }
}
