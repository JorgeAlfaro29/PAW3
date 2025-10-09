using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW3.Data.Models;

namespace PAW3.Data.Repositories
{
    public interface IRepositoryRole
    {
        Task<bool> UpsertAsync(Role entity, bool isUpdating);
        Task<bool> CreateAsync(Role entity);
        Task<bool> DeleteAsync(Role entity);
        Task<IEnumerable<Role>> ReadAsync();
        Task<Role> FindAsync(int id);
        Task<bool> UpdateAsync(Role entity);
        Task<bool> UpdateManyAsync(IEnumerable<Role> entities);
        Task<bool> ExistsAsync(Role entity);
        Task<bool> CheckBeforeSavingAsync(Role entity);
    }
    public class RepositoryRole : RepositoryBase<Role>, IRepositoryRole
    {
        public async new Task<bool> ExistsAsync(Role entity)
        {
            return await DbContext.Roles.AnyAsync(x => x.RoleId == entity.RoleId);
        }

        public async Task<bool> CheckBeforeSavingAsync(Role entity)
        {
            var exists = await ExistsAsync(entity);
            if (exists)
            {
                // algo mas 
            }

            return await UpsertAsync(entity, exists);
        }
    }


}
