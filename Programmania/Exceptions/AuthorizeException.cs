using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string message) : base(message) { }
    }
}
