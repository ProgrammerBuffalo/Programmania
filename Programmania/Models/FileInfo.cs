namespace Programmania.Models
{
    public class FileInfo
    {
        private byte[] file;
        private string exntension;

        public FileInfo()
        {

        }

        public FileInfo(byte[] file, string exntension)
        {
            File = file;
            Extension = exntension;
        }

        public byte[] File { get => file; set => file = value; }
        public string Extension { get => exntension; set => exntension = value; }
    }
}
