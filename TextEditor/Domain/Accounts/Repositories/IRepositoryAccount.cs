using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Domain.Accounts.Entities;
using TextEditor.Domain.Interfaces;

namespace TextEditor.Domain.Accounts.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<bool> IsExistCardId(string cartId);
        Task<Account> LoginAsync(string username, string password);
    }
}
