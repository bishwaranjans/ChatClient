#region Namespaces

using ChatClient.Domain.Entity;
using System.IO;
using System.Runtime.Serialization.Json;

#endregion

namespace ChatClient.Domain.SeedWork
{
    /// <summary>
    /// Utility class for serialization
    /// </summary>
    public class Serialization
    {
        #region Methods

        /// <summary>
        /// Jsons the deserializer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static object JsonDeserializer(byte[] buffer)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(UserMessage));
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                return serializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Jsons the serializer.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte[] JsonSerializer(object obj)
        {
            if (obj == null)
                return null;

            var serializer = new DataContractJsonSerializer(typeof(UserMessage));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return stream.ToArray();
            }
        }

        #endregion
    }
}
