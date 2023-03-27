using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Domain.Accounts.Entities;
using TextEditor.Domain.Accounts.Repositories;

namespace TextEditor.Infrastructure.Datas.Accounts
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(TextEditorContext context) : base(context)
        {

        }

        public Task AddEntityAsync(Account entity)
        {
            DbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetEntityByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Account> GetEntityByName(string name)
        {
            return DbSet.Where(s => s.IdCard.Contains(name));
        }

        public Task<bool> IsExistCardId(string cartId)
        {
            var acc = DbSet.FirstOrDefault(s => s.IdCard.Equals(cartId));
            return Task.FromResult(acc != null ? true : false);
        }

        public Task<Account> LoginAsync(string username, string password)
        {
            var account = DbSet.Include(s => s.Role).FirstOrDefault(s => s.IdCard.Equals(username) && s.Password.Equals(password)
                                                    && s.Status == 1);
            return Task.FromResult(account);
        }

        public Task UpdateEntityAsync(Account req)
        {
            throw new NotImplementedException();
        }
    }
}
