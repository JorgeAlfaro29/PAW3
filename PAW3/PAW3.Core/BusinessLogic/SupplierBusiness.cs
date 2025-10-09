using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface ISupplierBusiness
    {
        Task<IEnumerable<Supplier>> GetSupplier(int? id);
        Task<bool> SaveSupplierAsync(Supplier supplier);
        Task<bool> DeleteSupplierAsync(int id);


    }
    public class SupplierBusiness(IRepositorySupplier repositorySupplier) : ISupplierBusiness
    {
        public async Task<IEnumerable<Supplier>> GetSupplier(int? id)
        {
            return id == null
                ? await repositorySupplier.ReadAsync()
                : [await repositorySupplier.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveSupplierAsync(Supplier supplier)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12
            return await repositorySupplier.UpdateAsync(supplier);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await repositorySupplier.FindAsync(id);
            return await repositorySupplier.DeleteAsync(supplier);
        }
    }

    
}
