using System;
using System.Collections.Generic;

namespace Programmania.Services.Interfaces
{
    public interface IXMLService
    {
        public bool CreateXDeclaration(Models.SqlFileContext emptyFileContext);

        public bool AddNode(Models.Reward reward, Guid guid);

        public ICollection<Models.Reward> GetNodes(int offset, string fullPath);

    }
}