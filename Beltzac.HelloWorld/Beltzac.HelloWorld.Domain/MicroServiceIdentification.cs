using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    class MicroServiceIdentification : IMicroServiceIdentification
    {
        public MicroServiceIdentification(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
