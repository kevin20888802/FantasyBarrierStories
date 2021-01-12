using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBS.Data
{
    /// <summary>
    /// 遊戲資料基本，只用於資料類，如有與Runtime相關動作，則請勿使用。
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// 資料名稱
        /// </summary>
        public string name;

        public GameData(string i_name)
        {
            name = i_name;
        }
    }
}
