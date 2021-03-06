#region Namespaces

using System.Runtime.Serialization;

#endregion

namespace ChatClient.Domain.Entity
{
    /// <summary>
    /// User entity
    /// </summary>
    [DataContract]
    public class User
    {
        #region Properties

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [DataMember]
        public string UserName { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public User(string userName)
        {
            UserName = userName;
        }

        #endregion
    }
}
