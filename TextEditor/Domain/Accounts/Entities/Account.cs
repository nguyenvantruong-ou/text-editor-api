using System;
using System.Collections.Generic;
using TextEditor.Domain.Editors.Entities;

namespace TextEditor.Domain.Accounts.Entities
{
    public partial class Account
    {
        public Account()
        {
            Libraries = new HashSet<Library>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IdCard { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int RoleId { get; set; }
        public int? Status { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime? DateCreated { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Library> Libraries { get; set; }
    }
}
