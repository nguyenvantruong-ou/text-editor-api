using System;
using System.Collections.Generic;
using TextEditor.Domain.Accounts.Entities;

namespace TextEditor.Domain.Editors.Entities
{
    public partial class Library
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? Status { get; set; }
        public int AccountId { get; set; }
        public int TypeId { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Type Type { get; set; } = null!;
    }
}
