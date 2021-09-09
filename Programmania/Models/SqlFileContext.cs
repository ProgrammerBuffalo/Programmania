using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    public class SqlFileContext
    {
        public Guid StreamId { get; set; }

        public string Path { get; set; }

        public byte[] TransactionContext { get; set; }
    }
}
