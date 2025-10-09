using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW3.Data.Models;

namespace PAW3.Data.Repositories
{
    public interface IRepositoryNotification
    {
        Task<bool> UpsertAsync(Notification entity, bool isUpdating);
        Task<bool> CreateAsync(Notification entity);
        Task<bool> DeleteAsync(Notification entity);
        Task<IEnumerable<Notification>> ReadAsync();
        Task<Notification> FindAsync(int id);
        Task<bool> UpdateAsync(Notification entity);
        Task<bool> UpdateManyAsync(IEnumerable<Notification> entities);
        Task<bool> ExistsAsync(Notification entity);
        Task<bool> CheckBeforeSavingAsync(Notification entity);
    }

    public class RepositoryNotification : RepositoryBase<Notification>, IRepositoryNotification
    {
        public async new Task<bool> ExistsAsync(Notification entity)
        {
            return await DbContext.Notifications.AnyAsync(x => x.Id == entity.Id);
        }

        public async Task<bool> CheckBeforeSavingAsync(Notification entity)
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
