using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogFileCodeTest
{
    /// <summary>
    /// Custom Comparer for IPAddresses
    /// </summary>
    public class IPComparison : System.Collections.Generic.IComparer<IPAddress>
    {
        public int Compare(IPAddress x, IPAddress y)
        {
            return BitConverter.ToUInt32(x.GetAddressBytes().Reverse().ToArray(), 0)
            .CompareTo(BitConverter.ToUInt32(y.GetAddressBytes().Reverse().ToArray(), 0));
        }
    }
}
