namespace ChatClient.Domain.SeedWork
{
    public interface ISubscriber
    {
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
