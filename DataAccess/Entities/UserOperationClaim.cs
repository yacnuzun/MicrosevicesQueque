using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
