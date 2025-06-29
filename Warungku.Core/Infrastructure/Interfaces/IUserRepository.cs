using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Domain.Entities;

namespace Warungku.Core.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByUsername(string username);
    }
}
