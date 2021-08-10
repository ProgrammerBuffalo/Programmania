using Microsoft.EntityFrameworkCore;
using System;

namespace Programmania.Services
{
    interface IFileService
    {
        public Guid AddDocument(System.IO.MemoryStream memoryStream, DbContext dbContext);

        public bool UpdateDocument(Guid guid, System.IO.MemoryStream memoryStream, DbContext dbContext);

        public bool RemoveDocument(Guid guid, DbContext dbContext);
    }
}
