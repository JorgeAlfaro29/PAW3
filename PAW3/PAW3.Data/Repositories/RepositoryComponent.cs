using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW3.Data.Models;

namespace PAW3.Data.Repositories
{
    public interface IRepositoryComponent
    {
        Task<bool> UpsertAsync(Component entity, bool isUpdating);
        Task<bool> CreateAsync(Component entity);
        Task<bool> DeleteAsync(Component entity);
        Task<IEnumerable<Component>> ReadAsync();
        Task<Component> FindAsync(int id);
        Task<bool> UpdateAsync(Component entity);
        Task<bool> UpdateManyAsync(IEnumerable<Component> entities);
        Task<bool> ExistsAsync(Component entity);
        Task<bool> CheckBeforeSavingAsync(Component entity);
    }

    public class RepositoryComponent: RepositoryBase<Component>, IRepositoryComponent
    {
        public async new Task<bool> ExistsAsync(Component entity)
        {
            return await DbContext.Components.AnyAsync(x => x.Id == entity.Id);
        }

        public async Task<bool> CheckBeforeSavingAsync(Component entity)
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
