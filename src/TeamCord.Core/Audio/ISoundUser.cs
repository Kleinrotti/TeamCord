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
        /// UserVolume object
        /// </summary>
        Task<UserVolume> GetUserVolume();
    }
}