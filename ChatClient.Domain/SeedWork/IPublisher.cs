#region Namespaces

using ChatClient.Domain.Entity;

#endregion

namespace ChatClient.Domain.SeedWork
{
    /// <summary>
    /// Contract for Publisher
    /// </summary>
    public interface IPublisher
    {
        #region Methods

        /// <summary>
        /// Publishes the message on the NATS subject.
        /// </summary>
        /// <param name="userMessage">The user Message.</param>    
        public void Publish(UserMessage userMessage);

        #endregion
    }
}
