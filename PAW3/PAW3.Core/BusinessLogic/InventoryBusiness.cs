using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface IInventoryBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Inventory>> GetInventory(int? id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Task<bool> SaveInventoryAsync(Inventory inventory);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteInventoryAsync(int id);

    }
    public class InventoryBusiness(IRepositoryInventory repositoryInventory) : IInventoryBusiness
    {
        public async Task<IEnumerable<Inventory>> GetInventory(int? id)
        {
            return id == null
                ? await repositoryInventory.ReadAsync()
                : [await repositoryInventory.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveInventoryAsync(Inventory inventory)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12

            inventory.LastUpdated = DateTime.Now;
            inventory.ModifiedBy = "admin";
            inventory.DateAdded = DateTime.Now;

            return await repositoryInventory.CheckBeforeSavingAsync(inventory);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var inventory = await repositoryInventory.FindAsync(id);
            return await repositoryInventory.DeleteAsync(inventory);
        }

    }

}
