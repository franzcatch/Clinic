using Clinic.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.Utilities
{
    public class CustomException : Exception
    {
        public CustomException() {

        }

        public CustomException(List<ErrorMessage> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public List<ErrorMessage> ErrorMessages { get; set; }
    }
}