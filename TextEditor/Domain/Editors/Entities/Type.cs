using System;
using System.Collections.Generic;

namespace TextEditor.Domain.Editors.Entities
{
    public partial class Type
    {
        public Type()
        {
            Layouts = new HashSet<Layout>();
            Libraries = new HashSet<Library>();
        }

        public int Id { get; set; }
        public string Type1 { get; set; } = null!;

        public virtual ICollection<Layout> Layouts { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
    }
}
