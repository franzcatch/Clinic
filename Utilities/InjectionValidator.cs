using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.Utilities
{
    public static class InjectionValidator
    {
        public static bool Validate(string input)
        {
            var protectedWords = "";

            if (input.ToLower().Contains("insert"))
            {
                protectedWords += "INSERT, ";
            }
            if (input.ToLower().Contains("drop table"))
            {
                protectedWords += "DROP, ";
            }
            if (input.ToLower().Contains("select"))
            {
                protectedWords += "SELECT, ";
            }
            if (input.ToLower().Contains("delete"))
            {
                protectedWords += "DELETE, ";
            }
            if (input.ToLower().Contains("update"))
            {
                protectedWords += "UPDATE, ";
            }
            if (input.ToLower().Contains("create"))
            {
                protectedWords += "CREATE, ";
            }

            if (protectedWords.Length > 0)
            {
                throw new SqlInjectionException("Error: A value entered contains a protected keyword: " + protectedWords);
            }

            return true;
        }
    }
}