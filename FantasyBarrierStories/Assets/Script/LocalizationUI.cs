using UnityEngine;
using UnityEngine.UI;

namespace FBS.Main
{
    public class LocalizationUI : MonoBehaviour
    {
        public Text Text;

        public void Awake()
        {
            // 如果沒有UI文字則嘗試尋找
            if (Text == null)
            {
                // 如果本身Gameobject有UI文字就帶入
                if (GetComponent<Text>()) 
                {
                    Text = GetComponent<Text>();
                }
            }

            // 如果UI文字存在且字詞表有詞
            if (Text != null && LangManager.instance.StringTable.ContainsKey(Text.text))
            {
                // 替換字串為所選語言之字串
                Text.text = LangManager.instance.StringTable[Text.text];
            }
        }
    }
}
