using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Services
{
    public class FileService : IFileService
    {
        public Guid AddDocument(MemoryStream memoryStream, DbContext dbContext)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDocument(Guid guid, DbContext dbContext)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDocument(Guid guid, MemoryStream memoryStream, DbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
