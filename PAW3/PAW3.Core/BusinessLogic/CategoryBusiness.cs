using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;
using PAW3.Models.DTOs;

namespace PAW3.Core.BusinessLogic
{
    public interface ICategoryBusiness
    {
        /// <summary>
        /// Obtiene todas las categorias
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetCategory(int? id);
        /// <summary>
        /// Guarda las categorias de manera async
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<bool> SaveCategoryAsync(Category category);
        /// <summary>
        /// Elimina las categorias
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<bool> DeleteCategoryAsync(int id);


    }


    public class CategoryBusiness(IRepositoryCategory repositoryCategory) : ICategoryBusiness
    {
        /// </inheritdoc>
        public async Task<IEnumerable<Category>> GetCategory(int? id)
        {
            return id == null
                ? await repositoryCategory.ReadAsync()
                : [await repositoryCategory.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveCategoryAsync(Category category)
        {

            category.LastModified = DateTime.Now;
            category.ModifiedBy = "Jorge";

            return await repositoryCategory.CheckBeforeSavingAsync(category);
            //return await repositoryCategory.UpdateAsync(category);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await repositoryCategory.FindAsync(id);
            return await repositoryCategory.DeleteAsync(category);
        }


    }

} 
