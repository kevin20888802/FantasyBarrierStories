using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace FBS.Main
{
    /// <summary>
    /// 語言系統
    /// </summary>
    public class LangManager : MonoBehaviour
    {
        public static LangManager instance;

        /// <summary>
        /// 目前所選語言
        /// </summary>
        public LanguageType Lang;
        /// <summary>
        /// 字詞表
        /// </summary>
        public Dictionary<string, string> StringTable;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void Start()
        {
            LoadLangTable();
        }

        /// <summary>
        /// 依照所選語言讀取字詞表
        /// </summary>
        public void LoadLangTable()
        {
            TextAsset LangFile = Resources.Load<TextAsset>("GameData/Languages/" + "Lang_" + Lang.ToString());
            // 如果有語言檔案就載入字詞表
            if (LangFile != null)
            {
                StringTable = JsonConvert.DeserializeObject<Dictionary<string, string>>(LangFile.text);
                Debug.Log("Loaded Language:" + LangFile.name);
            }
        }
    }
}
