using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
    {
        /*
        // GET: api/<NotificationServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NotificationServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<NotificationServiceLocatorController>
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] NotificationDTO notification)
        {

            var service = await serviceMapper.GetServiceAsync<NotificationDTO>("notifications");

            if (service is INotificationService notificationService)
            {
                var created = await notificationService.CreateAsync(notification);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/<NotificationServiceLocatorController>/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] NotificationDTO notification)
        {
            var service = await serviceMapper.GetServiceAsync<NotificationDTO>(name);

            if (service is INotificationService notificationService)
            {
                if (id != notification.Id)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await notificationService.UpdateAsync(notification);

                if (updated)
                    return Ok($"Notification {id} actualizada correctamente.");

                return BadRequest("No se pudo actualizar la notification.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/<NotificationServiceLocatorController>/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<NotificationDTO>(name);

            if (service is INotificationService notificationService)
            {
                var deleted = await notificationService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Notification {id} eliminada correctamente.");

                return BadRequest("No se pudo eliminar la notification.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}
