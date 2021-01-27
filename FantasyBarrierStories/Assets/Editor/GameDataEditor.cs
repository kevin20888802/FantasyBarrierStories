using FBS.Data;
using FBS.Main;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FBS.Editor
{
    /// <summary>
    /// 遊戲資料編輯器的基本類型
    /// </summary>
    public class GameDataEditor : EditorWindow
    {
        public DataManager DM;

        /// <summary>
        /// 目前選擇的資料是第幾個
        /// </summary>
        public int dataIndex;

        /// <summary>
        /// 編輯器視窗大小
        /// </summary>
        public static Vector2 WindowSize = new Vector2(800, 800);
        /// <summary>
        /// 編輯器子視窗滾動位置空間
        /// </summary>
        public Vector2[] WindowPos = new Vector2[10];

        public GameDataEditor()
        {
        }

        public void OnEnable()
        {
            if (DataManager.instance != null)
            {
                DM = DataManager.instance;
            }
            else
            {
                DM = new DataManager(DataMode.Editor);
            }
            LoadData();
        }

        /// <summary>
        /// 顯示遊戲資料選擇清單
        /// </summary>
        /// <typeparam name="T">遊戲資料類型</typeparam>
        /// <param name="i_data">資料清單來源</param>
        /// <param name="i_size">視窗大小</param>
        public void DataList<T>(GameDataCollection<T> i_data, Vector2 i_size) where T : GameData
        {
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("新增",GUILayout.Width(i_size.x - 15)))
            {
                AddData();
            }
            if (GUILayout.Button("刪除", GUILayout.Width(i_size.x - 15)))
            {
                DeleteData();
            }
            List<string> dataNames = new List<string>();
            int _dataCount = i_data.DataCount();
            for (int i = 0; i < _dataCount; i++)
            {
                dataNames.Add(i_data.GetData(i).name);
            }
            dataIndex = GUILayout.SelectionGrid(dataIndex, dataNames.ToArray(), 1, GUILayout.Width(i_size.x - 15));
            EditorGUILayout.EndVertical();
        }
        /// <summary>
        /// 遊戲資源的物件選擇欄位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i_obj"></param>
        /// <param name="i_dataPath"></param>
        public void GameDataAssetField<T>(ref T i_obj,ref GDAssetPath<T> i_dataPath) where T : Object
        {
            if (i_dataPath != null && !string.IsNullOrEmpty(i_dataPath.AssetDBPath))
            {
                i_obj = AssetDatabase.LoadAssetAtPath<T>(i_dataPath.AssetDBPath);
            }
            else
            {
                i_obj = null;
            }
            i_obj = (T)EditorGUILayout.ObjectField(GUIContent.none, i_obj, typeof(T), true);
            if (i_obj != null)
            {
                if (i_dataPath == null)
                {
                    i_dataPath = new GDAssetPath<T>();
                }
                i_dataPath.AssetDBPath = AssetDatabase.GetAssetPath(i_obj);
                i_dataPath.AssetName = Path.GetFileName(i_dataPath.AssetDBPath);
            }
        }
        /// <summary>
        /// 從資料清單新增遊戲資料
        /// </summary>
        public virtual void AddData()
        {
        }
        /// <summary>
        /// 從資料清單刪除遊戲資料
        /// </summary>
        public virtual void DeleteData()
        {
        }
        /// <summary>
        /// 將遊戲資料儲存到檔案
        /// </summary>
        public virtual void SaveData()
        {
        }
        /// <summary>
        /// 讀取遊戲資料
        /// </summary>
        public virtual void LoadData()
        {
        }
    }

}
