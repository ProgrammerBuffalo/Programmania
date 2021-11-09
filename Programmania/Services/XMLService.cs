using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Programmania.DAL;
using Programmania.Models;
using Programmania.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Programmania.Services
{
    public class XMLService : IXMLService
    {
        private ProgrammaniaDBContext dbContext;

        public XMLService(ProgrammaniaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateXDeclaration(SqlFileContext emptyFileContext)
        {
            using (SqlFileStream sqlFileStream = new SqlFileStream(emptyFileContext.Path, emptyFileContext.TransactionContext, FileAccess.Write))
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDocument.CreateElement("history");
                xmlDocument.AppendChild(root);
                xmlDocument.Save(sqlFileStream);
            }
            return true;
        }

        public bool AddNode(Reward reward, Guid guid)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var doc = dbContext.Documents.FromSqlRaw("SELECT *, GET_FILESTREAM_TRANSACTION_CONTEXT() " +
                           "as Transaction_Context FROM DocumentsView WHERE stream_id = '{0}'", guid).FirstOrDefault();

                if (doc == null)
                    return false;

                Type type = typeof(Reward);
                var properties = type.GetProperties();

                using (SqlFileStream sqlFileStream = new SqlFileStream(doc.Path, doc.Transaction_Context, FileAccess.Write))
                {
                    XDocument xDocument = XDocument.Load(sqlFileStream);

                    XElement xElement = new XElement(type.Name.ToLower());
                    foreach (var property in properties)
                    {
                        xElement.Add(new XElement(property.CustomAttributes.First()
                                            .NamedArguments.First().TypedValue.Value.ToString(), property.GetValue(reward)));
                    }

                    xDocument.Root.AddFirst(xElement);
                    xDocument.Save(sqlFileStream);
                }
            }
            return true;
        }

        public ICollection<Reward> GetNodes(int count, string fullPath)
        {
            List<Reward> rewards = new List<Reward>();
            Type type = typeof(Reward);
            var properties = type.GetProperties();

            using (XmlReader xmlReader = XmlReader.Create(fullPath))
            {
                xmlReader.MoveToContent();
                if (XmlNodeType.Element == xmlReader.NodeType)
                {
                    XElement xElement = XNode.ReadFrom(xmlReader) as XElement;
                    Reward reward = new Reward();

                    foreach (var property in properties)
                    {
                        property.SetValue(reward, Convert.ChangeType(xElement.Element(property.CustomAttributes
                            .First().NamedArguments.First().TypedValue.Value.ToString()).Value, property.PropertyType), null);
                    }

                    rewards.Add(reward);
                }
            }
            return rewards;
        }

        public ICollection<Reward> GetNodes(int count, int offset, string fullPath)
        {
            throw new NotImplementedException();
        }
    }
}
