using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface IUserActionBusiness
    {
        Task<IEnumerable<UserAction>> GetUserAction(int? id);
        Task<bool> SaveUserAsync(UserAction category);

        Task<bool> DeleteUserAsync(int id);

    }

    public class UserActionBusiness(IRepositoryUserAction repositoryUserAction) : IUserActionBusiness
    {
        public async Task<IEnumerable<UserAction>> GetUserAction(int? id)
        {
            return id == null
                ? await repositoryUserAction.ReadAsync()
                : [await repositoryUserAction.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveUserAsync(UserAction category)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12
            return await repositoryUserAction.UpdateAsync(category);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteUserAsync(int id)
        {
            var userAction = await repositoryUserAction.FindAsync(id);
            return await repositoryUserAction.DeleteAsync(userAction);
        }
    }

    
}
