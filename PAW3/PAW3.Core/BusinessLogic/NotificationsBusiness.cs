using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Data.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic
{
    public interface INotificationBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotification(int? id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task<bool> SaveNotificationAsync(Notification notification);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteNotificationAsync(int id);


    }
    public class NotificationsBusiness(IRepositoryNotification repositoryNotification) : INotificationBusiness
    {
        /// </inheritdoc>
        public async Task<IEnumerable<Notification>> GetNotification(int? id)
        {
            return id == null
                ? await repositoryNotification.ReadAsync()
                : [await repositoryNotification.FindAsync((int)id)];
        }

        /// </inheritdoc>
        public async Task<bool> SaveNotificationAsync(Notification notification)
        {
            // que tengan mas de 5 quantity
            // sabado o domingo solo puedo salvar de 8 a 12

            notification.CreatedAt = DateTime.Now;


            return await repositoryNotification.UpdateAsync(notification);
        }

        /// </inheritdoc>
        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await repositoryNotification.FindAsync(id);
            return await repositoryNotification.DeleteAsync(notification);
        }
    }

    
}
