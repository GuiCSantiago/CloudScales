using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Models.Exceptions
{
    public class RequisicaoException : ApplicationException
    {
        public RequisicaoException(string message) : base(message)
        {
        }
    }
}
