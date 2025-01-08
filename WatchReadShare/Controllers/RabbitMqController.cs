using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Persistence.RabbitMQ;

namespace WatchReadShare.API.Controllers
{
    public class RabbitMqController(RabbitMqProducer rabbitMqProducer) : CustomBaseController
    {
        [HttpPost("send")]
        public IActionResult SendMessage(string message)
        {
            rabbitMqProducer.PublishMessage("testQueue", message);
            return Ok("Mesaj kuyruğa gönderildi.");
        }
    }
}
