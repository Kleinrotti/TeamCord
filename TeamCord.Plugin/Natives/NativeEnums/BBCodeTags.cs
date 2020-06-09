namespace TeamCord.Plugin.Natives
{
    public enum BBCodeTags
    {
        BBCodeTag_B = 0x00000001,
        BBCodeTag_I = 0x00000002,
        BBCodeTag_U = 0x00000004,
        BBCodeTag_S = 0x00000008,
        BBCodeTag_SUP = 0x00000010,
        BBCodeTag_SUB = 0x00000020,
        BBCodeTag_COLOR = 0x00000040,
        BBCodeTag_SIZE = 0x00000080,
        BBCodeTag_group_text = 0x000000FF,

        BBCodeTag_LEFT = 0x00001000,
        BBCodeTag_RIGHT = 0x00002000,
        BBCodeTag_CENTER = 0x00004000,
        BBCodeTag_group_align = 0x00007000,

        BBCodeTag_URL = 0x00010000,
        BBCodeTag_IMAGE = 0x00020000,
        BBCodeTag_HR = 0x00040000,

        BBCodeTag_LIST = 0x00100000,
        BBCodeTag_LISTITEM = 0x00200000,
        BBCodeTag_group_list = 0x00300000,

        BBCodeTag_TABLE = 0x00400000,
        BBCodeTag_TR = 0x00800000,
        BBCodeTag_TH = 0x01000000,
        BBCodeTag_TD = 0x02000000,
        BBCodeTag_group_table = 0x03C00000,

        BBCodeTag_def_simple = BBCodeTag_B | BBCodeTag_I | BBCodeTag_U | BBCodeTag_S | BBCodeTag_SUP | BBCodeTag_SUB | BBCodeTag_COLOR | BBCodeTag_URL,
        BBCodeTag_def_simple_Img = BBCodeTag_def_simple | BBCodeTag_IMAGE,
        BBCodeTag_def_extended = BBCodeTag_group_text | BBCodeTag_group_align | BBCodeTag_URL | BBCodeTag_IMAGE | BBCodeTag_HR | BBCodeTag_group_list | BBCodeTag_group_table,
    }
}