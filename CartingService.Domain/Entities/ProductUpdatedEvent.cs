using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    public record ProductUpdatedEvent
    {
        public int ProductId { get; init; }
        public string NewName { get; init; }
        public decimal NewPrice { get; init; }
    }
}
