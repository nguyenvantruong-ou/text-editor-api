using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.SharedKernel.MD5;
using System.Security.Cryptography;

namespace TextEditor.Infrastructure.Hash
{
    public class MD5Util : IHash
    {
        public string GetHash(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                return Convert.ToHexString(hashValue);
            }
        }
    }
}
