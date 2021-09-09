using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Programmania.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Services
{
    public class FileService : IFileService
    {

        private DAL.ProgrammaniaDBContext dbContext;

        public FileService(DAL.ProgrammaniaDBContext context)
        {
            this.dbContext = context;
        }

        public byte[] GetDocument(string full_path)
        {
            return File.ReadAllBytes(full_path);
        }

        public SqlFileContext GetSqlFileContext(Guid guid)
        {
            return getSqlFileContext(guid);
        }

        public SqlFileContext AddEmptyDocument(string fileName)
        {
            using (SqlCommand sqlCommand = (SqlCommand)dbContext.Database.GetDbConnection().CreateCommand())
            {
                if (dbContext.Database.CurrentTransaction != null)
                {
                    sqlCommand.Transaction = (SqlTransaction)dbContext.Database.CurrentTransaction.GetDbTransaction();
                    sqlCommand.CommandText = @"INSERT INTO DataFT(file_stream, name)                                                                                  
                                               OUTPUT INSERTED.stream_id, GET_FILESTREAM_TRANSACTION_CONTEXT(), INSERTED.file_stream.PathName()
                                               VALUES(CAST('' as varbinary(MAX)), @name)";

                    sqlCommand.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = fileName;

                    SqlFileContext emptyFileContext = new SqlFileContext();

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            emptyFileContext.StreamId = sqlDataReader.GetSqlGuid(0).Value;
                            emptyFileContext.TransactionContext = sqlDataReader.GetSqlBinary(1).Value;
                            emptyFileContext.Path = sqlDataReader.GetSqlString(2).Value;
                        }
                    }
                    return emptyFileContext;
                }
            }
            return null;
        }

        public bool FillDocumentContent(Models.SqlFileContext emptyFileContext, IFormFile formFile)
        {
            using (SqlFileStream sqlFileStream = new SqlFileStream(emptyFileContext.Path, emptyFileContext.TransactionContext, FileAccess.Write))
            {
                using (Stream stream = formFile.OpenReadStream())
                {
                    stream.CopyTo(sqlFileStream);
                }
            }
            return true;
        }


        public bool RemoveDocument(Guid guid)
        {
            dbContext.Database.ExecuteSqlRaw(@"DELETE FROM DataFT WHERE stream_id = {0}", guid);
            return true;
        }

        public bool UpdateDocument(Guid guid, IFormFile formFile)
        {
            var fileContext = getSqlFileContext(guid);
            using (SqlFileStream sqlFileStream = new SqlFileStream(fileContext.Path, fileContext.TransactionContext, FileAccess.Write))
            {
                using (Stream stream = formFile.OpenReadStream())
                {
                    stream.CopyTo(sqlFileStream);
                }
            }
            return true;
        }

        private SqlFileContext getSqlFileContext(Guid guid)
        {
            var doc = dbContext.Documents
                .FromSqlRaw("SELECT *, GET_FILESTREAM_TRANSACTION_CONTEXT() FROM DataFT WHERE stream_id = {0}", guid).FirstOrDefault();
            return new SqlFileContext { Path = doc.Path, StreamId = doc.StreamId, TransactionContext = doc.Transaction_Context };
        }
    }
}
