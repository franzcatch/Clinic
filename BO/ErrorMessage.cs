using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class ErrorMessage
    {
        public ErrorMessage(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }
        public string Message { get; set; }
    }
}