using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface IUserRoleBusiness
    {
        Task<IEnumerable<UserRole>> GetUserRole(int? id);
        Task<bool> SaveUserRoleAsync(UserRole userRole);
        Task<bool> DeleteUserRoleAsync(int id);
    }
    public class UserRoleBusiness(IRepositoryUserRole repositoryUserRole) : IUserRoleBusiness
    {
        public async Task<IEnumerable<UserRole>> GetUserRole(int? id)
        {
            return id == null
                ? await repositoryUserRole.ReadAsync()
                : [await repositoryUserRole.FindAsync((int)id)];
        }

        public async Task<bool> SaveUserRoleAsync(UserRole userRole)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12
            return await repositoryUserRole.UpdateAsync(userRole);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteUserRoleAsync(int id)
        {
            var userRole = await repositoryUserRole.FindAsync(id);
            return await repositoryUserRole.DeleteAsync(userRole);
        }
    }

    
}
