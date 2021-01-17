using UnityEngine;

namespace FBS.Data
{
    /// <summary>
    /// 遊戲資料的資源路徑，
    /// 如果跟圖片或模型相關的，
    /// 需要分以AssetDataBase和Assetbundle存取方式的資源的路徑。
    /// </summary>
    /// <typeparam name="T">Unity的Object</typeparam>
    public class GDAssetPath<T> where T : Object
    {
        /// <summary>
        /// 資源檔案名稱
        /// </summary>
        public string AssetName;
        /// <summary>
        /// 資源在AssetDataBase的路徑
        /// </summary>
        public string AssetDBPath;
        /// <summary>
        /// 資源被包裝的Assetbundle的名稱
        /// </summary>
        public string AssetBundleName;
    }
}
