using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaoYaoYao.Helpers
{
    public class DataCheckVerifyHelper
    {
        public bool IsHandset(string phone)
        {
            return Regex.IsMatch(phone, @"^09[0-9]{8}$");
        }
    }
}
