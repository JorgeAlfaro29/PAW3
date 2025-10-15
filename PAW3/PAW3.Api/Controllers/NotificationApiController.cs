using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationApiController(INotificationBusiness notificationBusiness) : ControllerBase
    {
        // GET: api/<NotificationApiController>
        [HttpGet]
        public async Task<IEnumerable<Notification>> Get()
        {
            return await notificationBusiness.GetNotification(id: null);
        }

        // GET api/<NotificationApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotificationApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Notification notification)
        {
            var result = await notificationBusiness.SaveNotificationAsync(notification);
            return result ? Ok(notification) : BadRequest("No se pudo guardar la notificacion.");
        }

        // PUT api/<NotificationApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Notification notification)
        {
            if (id != notification.Id)
                return BadRequest("El ID no coincide.");

            var result = await notificationBusiness.SaveNotificationAsync(notification);
            return result ? Ok(notification) : BadRequest("No se pudo actualizar la notificacion.");
        }

        // DELETE api/<NotificationApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await notificationBusiness.DeleteNotificationAsync(id);
            return result ? Ok($"Notificacion {id} eliminada correctamente.") : BadRequest("No se pudo eliminar la notificacion.");
        }
    }
}
