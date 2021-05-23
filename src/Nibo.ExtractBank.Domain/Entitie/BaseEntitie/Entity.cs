using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.ExtractBank.Domain.Entitie.BaseEntitie
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
