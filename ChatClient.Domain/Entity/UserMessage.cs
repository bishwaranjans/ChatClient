using System;
using System.Runtime.Serialization;

namespace ChatClient.Domain.Entity
{
    /// <summary>
    /// Message entity
    /// </summary>
    [DataContract]
    public class UserMessage
    {
        #region Properties

        public string UniqueMessageId { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [DataMember]
        public string Content { get; private set; }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        [DataMember]
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [DataMember]
        public User User { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMessage" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="content">The content.</param>
        public UserMessage(User user, string content)
        {
            UniqueMessageId = Guid.NewGuid().ToString("N");
            User = user;
            Content = content;
            TimeStamp = DateTime.Now;
        }

        #endregion
    }
}
