using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface IUserBusiness
    {
        Task<IEnumerable<User>> GetUser(int? id);
        Task<bool> SaveUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);

    }
    public class UserBusiness(IRepositoryUser repositoryUser) : IUserBusiness
    {
        /// </inheritdoc>
        public async Task<IEnumerable<User>> GetUser(int? id)
        {
            return id == null
                ? await repositoryUser.ReadAsync()
                : [await repositoryUser.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveUserAsync(User user)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12
            return await repositoryUser.UpdateAsync(user);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await repositoryUser.FindAsync(id);
            return await repositoryUser.DeleteAsync(user);
        }
    }

    
}
