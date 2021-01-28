using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FBS.Main
{
    /// <summary>
    /// 起始目錄系統
    /// </summary>
    public class StartManager : MonoBehaviour
    {
        /// <summary>
        /// 開始按鈕
        /// </summary>
        public void EnterGame()
        {
        
        }

        /// <summary>
        /// 選項按鈕
        /// </summary>
        public void OpenSettings()
        {
        
        }

        /// <summary>
        /// 離開按鈕
        /// </summary>
        public void Exit()
        {
            SystemManager.instance.ExitGame();
        }
    }
}
