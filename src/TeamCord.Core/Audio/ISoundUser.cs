using System.Threading.Tasks;

namespace TeamCord.Core
{
    /// <summary>
    /// Interface which declares a user with audio data
    /// </summary>
    internal interface ISoundUser
    {
        /// <summary>
        /// Volume of the user, 1.0 is 100%
        /// </summary>
        float Volume { get; set; }

        /// <summary>
        /// User volume object
        /// </summary>
        /// <returns></returns>
        Task<UserVolume> GetUserVolume();

        /// <summary>
        /// Get the username/nickname of the discord user.
        /// </summary>
        /// <returns></returns>
        Task<string> GetNickname();
    }
}