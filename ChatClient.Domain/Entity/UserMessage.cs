using System;

namespace ChatClient.Domain.Entity
{
    /// <summary>
    /// Message entity
    /// </summary>
    public class UserMessage
    {
        #region Properties

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
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
            User = user;
            Content = content;
            TimeStamp = DateTime.Now;
        }

        #endregion
    }
}
