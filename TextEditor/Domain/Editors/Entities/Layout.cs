using System;
using System.Collections.Generic;

namespace TextEditor.Domain.Editors.Entities
{
    public partial class Layout
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int TypeId { get; set; }

        public virtual Type Type { get; set; } = null!;
    }
}
