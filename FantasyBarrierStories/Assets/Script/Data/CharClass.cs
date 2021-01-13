namespace FBS.Data
{
    public class CharClass : GameData
    {

        /// <summary>
        /// 職業基礎服裝名稱。
        /// </summary>
        private string BasePartName;

        public CharClass(string i_name) : base(i_name)
        {
        }

        public CharPart GetBasePart()
        {
            return DataManager.instance.CharParts.GetData(BasePartName);
        }
    }
}
