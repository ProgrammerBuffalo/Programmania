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
        public byte[] GetDocument(string full_path);

        public Models.SqlFileContext GetSqlFileContext(Guid guid);

        public Models.SqlFileContext AddEmptyDocument(string fileName);

        public bool FillDocumentContent(Models.SqlFileContext emptyFileContext, IFormFile formFile);

        public bool UpdateDocument(Guid guid, IFormFile formFile);

        public bool RemoveDocument(Guid guid);
    }
}
