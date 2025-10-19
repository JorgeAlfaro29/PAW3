using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW3.Data.Models;

namespace PAW3.Data.Repositories
{

    //Patron de Diseño 
    //Mapeador de datos

    /*
    **Patron de Diseño** 
    **Mapeador de datos**
    
    Segun la investigacion que realice note que el repository hace la funcion de un intermediario entre la capa de datos y la capa de negocio
    es decir la capa de datos que en este caso es "Category" solo tiene los datos referentes a la base de datos y el repository es el encargado de hacer las consultas a la base de datos
    asi como se utilizan los metodos desde el repositoryBase, en este caso entity se encarga de hacer los metodos para poder utilizarlo pero de igual manera se pueden crear para hacer los metodos de 
    crete, read, update, delete y demas que se necesiten. Y se aplica una logica para obtener los datos, asi como el ejemplo del ExistsAsync que verifica si existe un datos con un id, pero para hacer
    eso se aplica una logica y se conoce si existe o no el dato. Luego ese metodo se aplica dentro de otros metodos como el CheckBeforeSavingAsync y ya cuando todo fue verificado se utiliza dentro de otro metodo
    en la capa de CategoryBusiness y de ahi pasa a la API. De esta manera si existe algun error en consultas o si se desea modificar se accede a la capa de repository o mapeador de datos y se hacen las 
    actualizaciones necesarias aplicando el principio de SOlID de responsabilidad unica.
    */

    public interface IRepositoryCategory
    {
        Task<bool> UpsertAsync(Category entity, bool isUpdating);
        Task<bool> CreateAsync(Category entity);
        Task<bool> DeleteAsync(Category entity);
        Task<IEnumerable<Category>> ReadAsync();
        Task<Category> FindAsync(int id);
        Task<bool> UpdateAsync(Category entity);
        Task<bool> UpdateManyAsync(IEnumerable<Category> entities);
        Task<bool> ExistsAsync(Category entity);
        Task<bool> CheckBeforeSavingAsync(Category entity);
        
    }

    public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
    {
        public async new Task<bool> ExistsAsync(Category entity)
        {
            return await DbContext.Categories.AnyAsync(x => x.CategoryId == entity.CategoryId);
        }

        public async Task<bool> CheckBeforeSavingAsync(Category entity)
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
