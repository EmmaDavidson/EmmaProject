using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TreasureHuntDesktopApplication.FullClient.Project_Utilities
{
    public static class Validation
    {
        public static bool IsNullOrWhiteSpace(String stringToCheck)
        {
                if (String.IsNullOrWhiteSpace(stringToCheck))
                {
                    return true;
                }
            
            return false;
        }

        public static bool IsNullOrEmpty(String stringToCheck)
        {

            if (String.IsNullOrEmpty(stringToCheck))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidLength(String stringToCheck, int maxLength)
        {
            if(stringToCheck != null)
            {
                if (stringToCheck.Length <= maxLength)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidCharacters(String stringToCheck)
        {
            if (stringToCheck != null)
            {
                if (Regex.IsMatch(stringToCheck, @"^[a-zA-Z ]+$"))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
