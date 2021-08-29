using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Programmania.Services
{
    public interface IFileService
    { 
        public IFormFile GetDocument(string full_path);

        public Guid AddDocument(System.IO.MemoryStream memoryStream, DbContext dbContext);

        public bool UpdateDocument(Guid guid, System.IO.MemoryStream memoryStream, DbContext dbContext);

        public bool RemoveDocument(Guid guid, DbContext dbContext);
    }
}
