using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Domain.Accounts.Entities;

namespace TextEditor.Domain.Accounts.DomainServices.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsExistCardId(string cartId);
        Task RegisterAsync(string cartId, string pw, string address, string gender, string name);
        Task<Account> LoginAsync(string username, string password);
    }
}
