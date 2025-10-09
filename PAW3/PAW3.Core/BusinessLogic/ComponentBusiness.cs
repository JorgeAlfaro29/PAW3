using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface IComponentRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Component>> GetComponent(int? id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        Task<bool> SaveComponentAsync(Component component);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteComponentAsync(int id);

    }
    public class ComponentBusiness(IRepositoryComponent repositoryComponent): IComponentRepository
    {
        public async Task<IEnumerable<Component>> GetComponent(int? id)
        {
            return id == null
                ? await repositoryComponent.ReadAsync()
                : [await repositoryComponent.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveComponentAsync(Component component)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12
            return await repositoryComponent.CheckBeforeSavingAsync(component);
            //return await repositoryComponent.UpdateAsync(component);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteComponentAsync(int id)
        {
            var category = await repositoryComponent.FindAsync(id);
            return await repositoryComponent.DeleteAsync(category);
        }
    }

    
}
