using System;

namespace Labs.Feedback.API.Model
{
    public class EntityBase
    {
        public EntityBase()
        {
            Ident = Guid.NewGuid();
        }
        public Guid Ident { get; set; }
    }
}