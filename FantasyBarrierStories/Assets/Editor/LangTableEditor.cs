using FBS.Data;
using FBS.Main;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AssetDataBaseHelper;
using Newtonsoft.Json;

namespace FBS.Editor
{
    /// <summary>
    /// 語言字詞表編輯器
    /// </summary>
    public class LangTableEditor : GameDataEditor
    {
        private LanguageType _oldLang;
        public LanguageType Lang;

        /// <summary>
        /// 修改依賴用的英文語言檔案
        /// </summary>
        public TextAsset ENFile;
        /// <summary>
        /// 修改依賴用的英文語言字詞表
        /// </summary>
        public Dictionary<string, string> ENTable;

        /// <summary>
        /// 所選語言的語言檔案
        /// </summary>
        public TextAsset EditFile;
        /// <summary>
        /// 所選語言的語言字詞表
        /// </summary>
        public Dictionary<string, string> EditTable;

        /// <summary>
        /// 目前頁數
        /// </summary>
        int currPage = 1;

        // 最底下的即將加入的新值
        string preAddKey = "";

        [MenuItem("遊戲資料/語言")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(LangTableEditor), false, "語言", false);
        }
        public void OnGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            if (GUILayout.Button("儲存"))
            {
                SaveData();
            }

            EditorGUILayout.BeginHorizontal();
            #region 語言清單
            WindowPos[0] = EditorGUILayout.BeginScrollView(WindowPos[0], GUILayout.Width((WindowSize.x - 15) * 0.25f), GUILayout.Height(WindowSize.y - 100));
            EditorGUILayout.BeginVertical();
            dataIndex = GUILayout.SelectionGrid(dataIndex, Enum.GetNames(typeof(LanguageType)), 1, GUILayout.Width((WindowSize.x - 15) * 0.25f - 15));
            Lang = (LanguageType)dataIndex;
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            #endregion

            if (Lang != _oldLang | ENTable == null | EditTable == null)
            {
                LoadFile();
                _oldLang = Lang;
            }

            #region 語言編輯
            WindowPos[1] = EditorGUILayout.BeginScrollView(WindowPos[1], GUILayout.Width((WindowSize.x) * 0.75f), GUILayout.Height(WindowSize.y - 100));
            try
            {
                string[] theKeys = new string[ENTable.Keys.Count];
                ENTable.Keys.CopyTo(theKeys, 0);
                foreach (string key in theKeys)
                {
                    ENTable[key] = key;
                    if (!EditTable.ContainsKey(key))
                    {
                        EditTable.Add(key, key);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key");
            EditorGUILayout.LabelField("ENValue",GUILayout.Width(50));
            EditorGUILayout.LabelField("Value");
            GUILayout.Button("-");
            EditorGUILayout.EndHorizontal();
            try
            {
                int currCount = 0;
                string[] theKeys = new string[EditTable.Keys.Count];
                EditTable.Keys.CopyTo(theKeys, 0);
                foreach (string key in theKeys)
                {
                    currCount += 1;
                    if (currCount > ((currPage - 1) * 20) && currCount <= (currPage * 20))
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(key);
                        EditorGUILayout.LabelField(ENTable[key], GUILayout.Width(50));
                        EditTable[key] = EditorGUILayout.TextField(EditTable[key]);
                        if (GUILayout.Button("x"))
                        {
                            ENTable.Remove(key);
                            EditTable.Remove(key);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            EditorGUILayout.BeginHorizontal();
            preAddKey = EditorGUILayout.TextField(preAddKey);
            EditorGUILayout.LabelField("\t--\t", GUILayout.Width(50));
            EditorGUILayout.LabelField("\t--\t");
            if (GUILayout.Button("+"))
            {
                ENTable.Add(preAddKey, preAddKey);
                EditTable.Add(preAddKey, preAddKey);
                preAddKey = "";
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("<"))
            {
                if (currPage > 1)
                {
                    currPage -= 1;
                }
            }
            if (GUILayout.Button(">"))
            {
                if (currPage < EditTable.Keys.Count / 20)
                {
                    currPage += 1;
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
            #endregion
            EditorGUILayout.EndVertical();

        }

        private void Awake()
        {
            FileExistCheck();
        }

        public void FileExistCheck()
        {
            string _realPath = Application.dataPath + "/Resources/GameData/Languages";
            string[] langNames = Enum.GetNames(typeof(LanguageType));
            //  如果語言檔案不存在就創一個
            for (int i = 0; i < langNames.Length; i++)
            {
                string _fileName = _realPath + "/" + "Lang_" + langNames[i] + ".json";
                if (!Directory.Exists(_realPath))
                {
                    Directory.CreateDirectory(_realPath);
                }
                if (!File.Exists(_fileName))
                {
                    File.Create(_fileName).Close();
                    File.WriteAllText(_fileName, JsonConvert.SerializeObject(new Dictionary<string, string>()));
                }
            }
        }

        public override void SaveData()
        {
            base.SaveData();
            string _realPath = Application.dataPath + "/Resources/GameData/Languages";
            //Debug.Log(JsonConvert.SerializeObject(ENTable));
            File.WriteAllText(_realPath + "/" + "Lang_" + "EN" + ".json", JsonConvert.SerializeObject(ENTable));
            File.WriteAllText(_realPath + "/" + "Lang_" + Lang.ToString() + ".json", JsonConvert.SerializeObject(EditTable));
            AssetDatabase.Refresh();
        }

        public void LoadFile()
        {
            FileExistCheck();
            ENFile = AssetDataBaseHelper.LoadAssetAtPath<TextAsset>("Assets/Resources/" + "GameData/Languages/Lang_EN.json");
            EditFile = AssetDataBaseHelper.LoadAssetAtPath<TextAsset>("Assets/Resources/" + "GameData/Languages/Lang_" + Lang.ToString() + ".json");
            Debug.Log(Lang.ToString());
            ENTable = JsonConvert.DeserializeObject<Dictionary<string, string>>(ENFile.text);
            EditTable = JsonConvert.DeserializeObject<Dictionary<string, string>>(EditFile.text);
            if (ENTable == null)
            {
                ENTable = new Dictionary<string, string>();
            }
            if (EditTable == null)
            {
                EditTable = new Dictionary<string, string>();
            }
        }

    }
}
