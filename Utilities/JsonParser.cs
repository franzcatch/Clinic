using Clinic.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Clinic.Utilities
{
    public static class JsonParser
    {
        public static T FromJson<T>(HttpContext context)
        {
            var jsonString = String.Empty;

            var stream = context.Request.InputStream;
            stream.Position = 0;
            using (var inputStream = new StreamReader(stream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            return new JavaScriptSerializer().Deserialize<T>(jsonString);
        }

        public static string ToJson(Object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static string ExceptionToJson (Exception ex)
        {
            List<ErrorMessage> errorMessages;

            if (ex.GetType() == typeof(CustomException))
            {
                errorMessages = ((CustomException)ex).ErrorMessages;
            }
            else
            {
                errorMessages = new List<ErrorMessage>();
                errorMessages.Add(new ErrorMessage("", ex.Message));
            }
            
            return "{\"errorMessages\":" + new JavaScriptSerializer().Serialize(errorMessages) + "}";
        }
    }
}