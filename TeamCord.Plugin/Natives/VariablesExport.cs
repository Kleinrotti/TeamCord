namespace TeamCord.Plugin.Natives
{
    public struct VariablesExport
    {
        public VariablesExport(int exportCount = 64)
        {
            items = new VariablesExportItem[exportCount];
        }

        public VariablesExportItem[] items;
    }
}