using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface IRoleBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetRole(int? id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> SaveRoleAsync(Role role);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteRoleAsync(int id);

    }
    public class RoleBusiness(IRepositoryRole repositoryRole) : IRoleBusiness
    {
        public async Task<IEnumerable<Role>> GetRole(int? id)
        {
            return id == null
                ? await repositoryRole.ReadAsync()
                : [await repositoryRole.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveRoleAsync(Role role)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12
            return await repositoryRole.CheckBeforeSavingAsync(role);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await repositoryRole.FindAsync(id);
            return await repositoryRole.DeleteAsync(role);
        }
    }

    
}
