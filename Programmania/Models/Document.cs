using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programmania.Models
{
    public class Document
    {
        [Column("stream_id")]
        public Guid StreamId { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        [NotMapped]
        public byte[] Transaction_Context { get; set; }
    }
}
