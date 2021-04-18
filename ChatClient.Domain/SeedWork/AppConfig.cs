namespace ChatClient.Domain.SeedWork
{
    /// <summary>
    /// AppConfig Model
    /// </summary>
    public class AppConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the nats server.
        /// </summary>
        /// <value>
        /// The nats server.
        /// </value>
        public string NATSServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the nats subject.
        /// </summary>
        /// <value>
        /// The nats subject.
        /// </value>
        public string NATSSubject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is persist received message.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is persist received message; otherwise, <c>false</c>.
        /// </value>
        public bool IsPersistReceivedMessage { get; set; }

        #endregion
    }
}
