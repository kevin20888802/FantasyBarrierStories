using UnityEditor;
using UnityEngine;
using FBS.Data;
using FBS.Main;

namespace FBS.Editor
{
    public class CharClassEditor : GameDataEditor
    {
        public GameDataCollection<CharClass> all_data;

        public Sprite ClassIcon;

        [MenuItem("遊戲資料/角色職業")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CharClassEditor), false, "角色職業", false);
        }

        public override void LoadData()
        {
            base.LoadData();
            all_data = DM.CharClasses;
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            if (GUILayout.Button("儲存"))
            {
                SaveData();
            }
            EditorGUILayout.BeginHorizontal();
            #region 資料清單
            WindowPos[0] = EditorGUILayout.BeginScrollView(WindowPos[0], GUILayout.Width((WindowSize.x - 15) * 0.25f), GUILayout.Height(WindowSize.y - 100));
            DataList(all_data, new Vector2((WindowSize.x - 15) * 0.25f, WindowSize.y - 100));
            EditorGUILayout.EndScrollView();
            #endregion
            #region 資料編輯
            WindowPos[1] = EditorGUILayout.BeginScrollView(WindowPos[1], GUILayout.Width((WindowSize.x) * 0.75f), GUILayout.Height(WindowSize.y - 100));
            all_data.GetData(dataIndex).name = EditorGUILayout.TextField("資料檔案名稱", all_data.GetData(dataIndex).name);
            GameDataAssetField<Sprite>(ref ClassIcon, ref all_data.GetData(dataIndex).Icon);
            EditorGUILayout.EndScrollView();
            #endregion
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        public override void AddData()
        {
            base.AddData();
            all_data.AddData(new CharClass("CharClass_" + all_data.DataCount()));
        }

        public override void DeleteData()
        {
            base.DeleteData();
            if (all_data.DataCount() > 0)
            {
                all_data.RemoveAt(dataIndex);
                dataIndex -= 1;
                if (dataIndex < 0)
                {
                    dataIndex = all_data.DataCount() - 1;
                }
            }
        }

        public override void SaveData()
        {
            base.SaveData();
            DataManager.SaveData<CharClass>(DataManager.DataPaths["CharClasses"], all_data);
        }
    }

}
