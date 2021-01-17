using UnityEngine;

namespace FBS.Data
{
    public class CharClass : GameData
    {
        /// <summary>
        /// 職業圖示
        /// </summary>
        public GDAssetPath<Sprite> Icon;
        /// <summary>
        /// 職業基礎服裝名稱。
        /// </summary>
        public string BasePartName;

        public CharClass(string i_name) : base(i_name)
        {
            BasePartName = "";
        }

        /// <summary>
        /// 取得該職業的基本服裝模型。
        /// </summary>
        /// <returns></returns>
        public CharPart GetBasePart()
        {
            return DataManager.instance.CharParts.GetData(BasePartName);
        }
    }
}
