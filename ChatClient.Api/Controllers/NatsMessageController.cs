#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ChatClient.Api.Controllers
{
    /// <summary>
    /// NatsMessageController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class NatsMessageController : ControllerBase
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<NatsMessageController> _logger;
        private readonly IPublisher _publisher;
        private readonly ISubscriber _subscriber;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NatsMessageController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="publisher">The publisher.</param>
        /// <param name="subscriber">The subscriber.</param>
        public NatsMessageController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<NatsMessageController> logger, IPublisher publisher, ISubscriber subscriber)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _publisher = publisher;
            _subscriber = subscriber;

            _subscriber.Subscribe(_configuration.GetValue<bool>("IsPersistReceivedMessage"));
        }

        #endregion

        #region CRUD

        /// <summary>
        /// HTTPGET : Get all the received messages
        /// </summary>
        /// <returns>Returns list of received messages by the client.</returns>
        [HttpGet]
        public IEnumerable<string> NatsMessage()
        {
            var userMessages = _subscriber.ReceivedUserMessages;

            return userMessages.Select(s=>s.Content);
        }

        /// <summary>
        /// HTTPPOST: Post the message to NATS subject.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        [HttpPost]
        public string NatsMessage(string content)
        {
            var userMessage = new UserMessage(new User(_httpContextAccessor.HttpContext.User.Identity.Name), content);
            _publisher.Publish(userMessage);

            return $"Published message:{content} by user:{_httpContextAccessor.HttpContext.User.Identity.Name} to NATS subject:{_configuration.GetValue<string>("NATSSubject")}";
        }

        #endregion
    }
}
