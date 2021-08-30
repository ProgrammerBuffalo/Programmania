using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IFormFile GetDocument(string full_path)
        {
            FormFile formFile = null;
            
            using(FileStream fileStream = new FileStream(full_path, FileMode.Open, FileAccess.Read))
            {
                formFile = new FormFile(fileStream, 0, fileStream.Length, null, null);
            }

            return formFile;
        }

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
