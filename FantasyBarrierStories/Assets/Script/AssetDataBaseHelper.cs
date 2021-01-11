using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.AssetDataBaseHelper
{
    public static class AssetDataBaseHelper
    {
        /// <summary>
        /// 讀取在資料夾下的全部資料。
        /// 從整個專案資料夾開始為相對路徑。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i_path"></param>
        /// <returns></returns>
        public static T[] LoadAllAssetAtPath<T>(string i_path) where T : UnityEngine.Object
        {
            // 實際路徑
            string _realPath = Application.dataPath + i_path.Replace("Assets", "");

            // 先尋找檔案
            List<string> _allDir = new List<string>();
            List<T> _assetList = new List<T>();

            // 有存在路徑在開找
            if (Directory.Exists(_realPath))
            {
#if UNITY_EDITOR
                GetDirectorys(_realPath, ref _allDir);
                for (int i = 0; i < _allDir.Count; i++)
                {
                    string[] _nameList = Directory.GetFiles(_allDir[i]);
                    // 設清單然後把符合類型的丟進去
                    //Debug.Log("[AssetDataBaseHelper]:Find " + _nameList.Length + " Files");
                    for (int j = 0; j < _nameList.Length; j++)
                    {
                        if (!_nameList[j].EndsWith(".meta"))
                        {
                            string _relatePath = "Assets" + _nameList[j].Replace(Application.dataPath, "");
                            //Debug.Log("[AssetDataBaseHelper]File(" + i.ToString() + "):" + _relatePath);
                            //Debug.Log("[AssetDataBaseHelper]Relate Dir(" + i.ToString() + "):" + _relatePath);
                            if ((T)AssetDatabase.LoadAssetAtPath(_relatePath, typeof(T)) != null)
                            {
                                _assetList.Add((T)AssetDatabase.LoadAssetAtPath(_relatePath, typeof(T)));
                            }
                        }
                    }
                }
#endif
            }

            // 清單轉陣列
            T[] _assetArr = new T[_assetList.Count];
            for (int i = 0; i < _assetList.Count; i++)
            {
                _assetArr[i] = _assetList[i];
            }

            return _assetArr;
        }

        public static T LoadAssetAtPath<T>(string i_path) where T :UnityEngine.Object
        {
#if UNITY_EDITOR
            return (T)AssetDatabase.LoadAssetAtPath(i_path, typeof(T));
#else
            return null;
#endif
        }

        public static void GetDirectorys(string i_path,  ref List<string> out_paths)
        {
            out_paths.Add(i_path);
            //Debug.Log("[AssetDataBaseHelper]Searching Directory:" + i_path);
            string[] all_paths = Directory.GetDirectories(i_path);
            for(int i = 0 ;i < all_paths.Length; i++)
            {
                if(!out_paths.Contains(all_paths[i]))
                {
                    GetDirectorys(all_paths[i], ref out_paths);
                }
            }
        }
    }
}
