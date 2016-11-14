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
        public static T GetParams<T>(HttpContext context)
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
    }
}