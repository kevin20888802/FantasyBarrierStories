using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBS.Main
{
    /// <summary>
    /// 玩家系統，例如玩家遊戲中狀態。
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}
