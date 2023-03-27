using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.SharedKernel.MD5
{
    public interface IHash
    {
        string GetHash(string s);
    }
}
