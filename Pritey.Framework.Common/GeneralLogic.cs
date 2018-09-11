using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pritey.Framework.Common
{
    public class GeneralLogic
    {
        public static string GenerateDateFormat(string Date)
        {
            string WholeString = string.Empty;
            if (Date.Contains("/"))
            {
                string[] SplitString = Date.Split('/');
                WholeString = SplitString[1] + "/" + SplitString[0] + "/" + SplitString[2];
            }
            else
            {
                string[] SplitString = Date.Split('-');
                WholeString = SplitString[1] + "-" + SplitString[0] + "-" + SplitString[2].Split(' ')[0];
            }
            return WholeString;
        }
    }
}
