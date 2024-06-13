using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieManager.Utils
{
    public static class ValidationUtils
    {
        public static bool IsValidEmail(string email) => Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        public static bool IsValidUrl(string url)
        {
            string pattern = @"^(https?://)[^\s/$.?#].[^\s]*$";

            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }
    }
}
