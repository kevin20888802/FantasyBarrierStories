using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBS.Main
{
    /// <summary>
    /// 主要系統，例如音量。
    /// </summary>
    public class SystemManager : MonoBehaviour
    {
        public static SystemManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 離開遊戲
        /// </summary>
        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit(0);
            #endif
        }
    }
}