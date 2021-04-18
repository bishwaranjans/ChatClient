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

        #endregion
    }
}
