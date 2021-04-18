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
        #region Methods

        void Publish(UserMessage message);

        #endregion
    }
}
