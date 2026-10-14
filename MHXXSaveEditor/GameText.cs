namespace MHXXSaveEditor
{
    /// <summary>
    /// Selects display names for game data (items, skills).
    /// Chinese names come from MHGU v1.4.0 chS tables.
    /// </summary>
    static class GameText
    {
        public static bool UseChinese = true;

        public static string[] ItemNames
        {
            get { return UseChinese ? Data.GameConstantsZH.ItemNameList : Data.GameConstants.ItemNameList; }
        }

        public static string[] SkillNames
        {
            get { return UseChinese ? Data.GameConstantsZH.SkillNames : Data.GameConstants.SkillNames; }
        }
    }
}
