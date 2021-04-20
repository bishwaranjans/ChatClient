#region Namespaces

using ChatClient.Domain.Entity;
using System.Collections.Concurrent;

#endregion

namespace ChatClient.Domain.SeedWork
{
    /// <summary>
    /// Contract for Subscriber
    /// </summary>
    public interface ISubscriber
    {
        #region Properties

        /// <summary>
        /// Gets or sets the received user messages.
        /// </summary>
        /// <value>
        /// The received user messages.
        /// </value>
        ConcurrentBag<UserMessage> ReceivedUserMessages { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        void Subscribe();

        /// <summary>
        /// Uns the subscribe.
        /// </summary>
        void UnSubscribe();

        #endregion
    }
}
