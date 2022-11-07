using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.Helpers.Utilities
{
    public class UtilityHelper
    {
        /// <summary>
        /// Gets if there is an active internet connection
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
