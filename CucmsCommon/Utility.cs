using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAMCommon
{
    public static class Utility
    {
        public static string FormatAmdName(string fName, string lName, string mName)
        {
            string returnName;
            if (string.IsNullOrEmpty(mName))
            {
                returnName = lName + "," + fName;
            }
            else
            {
                returnName = lName + "," + fName + " " + mName;
            }
            return returnName;
        }
    }
}
