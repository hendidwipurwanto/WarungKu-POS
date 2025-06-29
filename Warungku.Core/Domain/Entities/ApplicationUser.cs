using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warungku.Core.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // you  may put here for another properties
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public DateTime? LastLogin { get; set; }
        public int? StatusId { get; set; }
        public int? RoleId { get; set; }
    }
}
