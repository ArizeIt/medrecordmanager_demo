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

        public static  void ParseCptCode(string originalcode,  out string code,  out int quantity, out string modifier)
        {
            code = string.Empty;
            quantity = 0;
            modifier = "N/A";

            if (!string.IsNullOrEmpty(originalcode))
            {
                if (!originalcode.Contains(','))
                {
                     code = originalcode;
                }
                else
                {
                    var result = originalcode.Split(',');

                    if (result.Count() == 2)
                    {
                        if (result[0].Length == 5)
                        {
                            modifier = result[1];
                            code = result[0];
                        }
                        if (result[1].Length == 5)
                        {
                            quantity = short.Parse(result[0]);
                            code = result[1];
                        }
                    }

                    if (result.Count() == 3)
                    {
                        quantity = short.Parse(result[0]);
                        code = result[1];
                        modifier = result[2];
                    }
                }
            }
        }

       public static void Method(out int answer, out string message, out string stillNull)
        {
            answer = 44;
            message = "I've been returned";
            stillNull = null;
        }
    }
}
