using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Domain.Accounts.DomainServices.Interfaces;
using TextEditor.Domain.Accounts.Entities;
using TextEditor.Domain.Accounts.Repositories;

namespace TextEditor.Domain.Accounts.DomainServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accRepo) 
        { 
            _accountRepository = accRepo;
        }

        public async Task<bool> IsExistCardId(string cartId)
        {
            if (cartId.Length < 9 || cartId.Length > 12)
                throw new Exception("Bad Request");
            return await _accountRepository.IsExistCardId(cartId);
        }

        public async Task<Account> LoginAsync(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                throw new Exception("Bad Request");
            return await _accountRepository.LoginAsync(username, password);
        }

        public async Task RegisterAsync(string cartId, string pw, string address, string gender, string name)
        {
            if (String.IsNullOrEmpty(cartId) || String.IsNullOrEmpty(pw) ||
               String.IsNullOrEmpty(address) || String.IsNullOrEmpty(gender) || String.IsNullOrEmpty(name))
                throw new Exception("Bad Request");
            Account acc = new Account();
            acc.IdCard = cartId;
            acc.Password= pw;
            acc.Name = name;
            acc.Address = address;
            acc.Gender = gender;
            acc.Status = 1;
            acc.RoleId = 1;
            acc.DateCreated = DateTime.UtcNow;

            await _accountRepository.AddEntityAsync(acc);
        }
    }
}
