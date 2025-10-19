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

    /*
     Patron de Diseño
     Interfaz separada

     Segun la inventigacion realiza la interfaz separada habla de que cada interfaz tiene una responsabilidad unica, en donde se definen metodos de una clase y esa interfaz puede ser llamada
     en otra clase para utilizar los metodos necesarios, como por ejemplo categoryBusiness que implementa una interfaz de IRepositoryCategory que tiene ya consultas a base de datos en el repository
     o mapeador de datos y se utliza en esta clase para ser utilizada con la logica cargada en la interfaz de la clase de RepositoryCategory. De esta manera se pueden combinar varias interfaces en una misma clase
     de otra clase para asi la combinacion de las mismas haga  un metodo mas completo y con mayor funcionalidad. Como es el caso de "GetCategory", que utiliza dos metodos de la interfaz de IRepositoryCategory que son
     "ReadAsync" y "FindAsync" para obtener los datos de la base de datos segun la logica que se aplique en el metodo y de esa manera en la API se puede obtener los datos referentes a las categorias existentes. De iual
    forma en esta clase se creo una interfaz para que se llama ICategoryBusiness que esta contiene ya la logica con interfaces de otras clases y solo se hace un llamado desde la API.

     */

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
