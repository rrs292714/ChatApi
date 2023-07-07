using System.Text;

namespace ChatApi.Interface
{
    public interface IHash
    {
        public string Hash(string message,string Salt);
        public string OriginalMessage(string Hash,string Salt, Encoding encoding);
    }
}
