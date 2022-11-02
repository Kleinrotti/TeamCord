namespace TeamCord.Plugin.Natives
{
    public enum SecuritySaltOptions
    {
        SECURITY_SALT_CHECK_NICKNAME = 1, /* put nickname into security hash */
        SECURITY_SALT_CHECK_META_DATA = 2  /* put (game)meta data into security hash */
    }
}