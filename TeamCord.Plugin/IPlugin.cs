using TeamCord.Core;

namespace TeamCord.Plugin
{
    internal interface IPlugin
    {
        /// <summary>
        /// Settings of the plugin
        /// </summary>
        SettingsModel Settings { get; }

        /// <summary>
        /// Plugin name
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Current plugin version
        /// </summary>
        string PluginVersion { get; }

        /// <summary>
        /// Teamspeak API Version of the plugin
        /// </summary>
        int ApiVersion { get; }

        /// <summary>
        /// Plugin author
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Plugin description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Unique plugin ID
        /// </summary>
        string PluginID { get; set; }

        /// <summary>
        /// Initialization function
        /// </summary>
        /// <returns> 0 for success, 1 for failure</returns>
        int Init();

        /// <summary>
        /// Shutdown function
        /// </summary>
        void Shutdown();
    }
}