#region Namespaces

using ChatClient.Domain.Entity;
using System;

#endregion

namespace ChatClient.Domain.SeedWork
{
    /// <summary>
    /// Contract for Publisher
    /// </summary>
    public interface IPublisher
    {
        #region Events

        /// <summary>
        /// Occurs when [handler].
        /// </summary>
        event EventHandler<Message> Handler;

        #endregion

        #region Methods

        /// <summary>
        /// Publishes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        void Publish(string content);

        #endregion
    }
}
