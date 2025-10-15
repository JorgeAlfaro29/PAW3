using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface ITaskBusiness
    {
        Task<IEnumerable<Tasks>> GetTask(int? id);
        Task<bool> SaveTaskAsync(Tasks task);
        Task<bool> DeleteTaskAsync(int id);
    }
    public class TaskBusiness(IRepositoryTask repositorytask) : ITaskBusiness
    {
        /// </inheritdoc>
        public async Task<IEnumerable<Tasks>> GetTask(int? id)
        {
            return id == null
                ? await repositorytask.ReadAsync()
                : [await repositorytask.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveTaskAsync(Tasks task)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12

            task.DueDate = DateTime.Now;
            task.Status = "Activo";
            task.CreatedAt = DateTime.Now;
            task.LastModified = DateTime.Now;
            task.ModifiedBy = "admin";
            return await repositorytask.UpdateAsync(task);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await repositorytask.FindAsync(id);
            return await repositorytask.DeleteAsync(task);
        }
    }

    
}
