using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Models.Exceptions
{
    public class SqlConcurrencyException : ApplicationException
    {
        public SqlConcurrencyException(string message) : base(message)
        {
        }
    }
}
