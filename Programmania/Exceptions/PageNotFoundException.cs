using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Exceptions
{
    public class PageNotFoundException : Exception
    {
        public PageNotFoundException() : base("Page not found") { } 
    }
}
