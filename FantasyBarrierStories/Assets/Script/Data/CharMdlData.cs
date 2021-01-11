using System;
using System.Collections.Generic;

namespace FBS.Data
{
    /// <summary>
    /// 角色模型資料
    /// </summary>
    public class CharMdlData
    {
        /// <summary>
        /// 角色形狀的數值，例如胖瘦、臉部寬度、眼睛大小等...。
        /// 如果有新的ShapeKey，Dictionary會自動新增。
        /// 
        /// 使用方法:
        /// ShapeValues[ShapekeyName] = 某數值
        /// </summary>
        private Dictionary<string, float> ShapeValues; 
        /// <summary>
        /// 角色的各個配件之路徑加檔名，讀取用。
        /// </summary>
        private string[] PartNames;

        public CharMdlData()
        {
            // 總共有幾個配件依照CharPartType的數量來決定
            PartNames = new string[Enum.GetNames(typeof(CharPartType)).Length];  
            ShapeValues = new Dictionary<string, float>();
        }
    }
}
