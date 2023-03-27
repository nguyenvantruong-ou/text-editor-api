using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Domain
{
    public interface IUnitOfWork<TContext>
    {
        Task CompleteAsync();
    }
}
