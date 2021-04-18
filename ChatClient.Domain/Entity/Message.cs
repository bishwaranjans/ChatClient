namespace ChatClient.Domain.Entity
{
    /// <summary>
    /// Message entity
    /// </summary>
    public class Message
    {
        #region Properties

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Message(string content)
        {
            Content = content;
        }

        #endregion
    }
}
