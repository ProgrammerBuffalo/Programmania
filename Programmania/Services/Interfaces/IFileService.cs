using Microsoft.AspNetCore.Http;
using System;

namespace Programmania.Services.Interfaces
{
    public interface IFileService
    {
        public byte[] GetDocument(string full_path);

        public Models.SqlFileContext GetSqlFileContext(Guid guid);

        public Models.SqlFileContext AddEmptyDocument(string fileName);

        public bool FillDocumentContent(Models.SqlFileContext emptyFileContext, IFormFile formFile);

        public bool FillDocumentContent(Models.SqlFileContext emptyFileContext, string content);

        public bool UpdateDocument(Guid guid, IFormFile formFile);

        public bool UpdateDocument(Guid guid, string content);

        public bool RemoveDocument(Guid guid);
    }
}
