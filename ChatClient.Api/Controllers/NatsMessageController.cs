using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NatsMessageController : ControllerBase
    {
        private readonly ILogger<NatsMessageController> _logger;    
        private readonly IPublisher _publisher;
        private readonly ISubscriber _subscriber;

        public NatsMessageController(ILogger<NatsMessageController> logger, IPublisher publisher, ISubscriber subscriber)
        {
            _logger = logger;          
            _publisher = publisher;
            _subscriber = subscriber;
        }

        [HttpGet]
        public IEnumerable<UserMessage> GetNatsMessages()
        {
            var list = new List<UserMessage>()
           {
               new UserMessage(new User("Bish"),"Test")
           };

            return list;
        }
    }
}
