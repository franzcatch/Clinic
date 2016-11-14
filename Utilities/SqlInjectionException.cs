using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.Utilities
{
    public class SqlInjectionException : Exception
    {
        public SqlInjectionException(string message) : base(message)
        {

        }
    }
}