using Clinic.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.Utilities
{
    public static class InjectionValidator
    {
        /// <summary>
        /// Throws SqlInjectionException if not valid
        /// </summary>
        /// <param name="fields"></param>
        public static void Validate(List<Field> fields)
        {
            var errorMessages = new List<ErrorMessage>();

            foreach (var field in fields)
            {
                var protectedWords = "";

                if (field.Value.ToLower().Contains("insert"))
                {
                    protectedWords += "INSERT, ";
                }
                if (field.Value.ToLower().Contains("drop table"))
                {
                    protectedWords += "DROP, ";
                }
                if (field.Value.ToLower().Contains("select"))
                {
                    protectedWords += "SELECT, ";
                }
                if (field.Value.ToLower().Contains("delete"))
                {
                    protectedWords += "DELETE, ";
                }
                if (field.Value.ToLower().Contains("update"))
                {
                    protectedWords += "UPDATE, ";
                }
                if (field.Value.ToLower().Contains("create"))
                {
                    protectedWords += "CREATE, ";
                }

                if (protectedWords.Length > 0)
                {
                    errorMessages.Add(new ErrorMessage(field.Name, "Prohibited keywords: " + protectedWords));
                }
            }

            if (errorMessages.Count > 0)
            {
                throw new CustomException(errorMessages);
            }
        }
    }
}