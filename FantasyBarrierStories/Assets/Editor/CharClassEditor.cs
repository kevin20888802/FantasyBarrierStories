using UnityEditor;
using System.Collections.Generic;
using FBS.Data;
using UnityEngine;
using UnityEditorInternal;

public class CharClassEditor : GameDataEditor
{
    public List<CharClass> all_data;

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
        WindowPos[0] = EditorGUILayout.BeginScrollView(WindowPos[0], GUILayout.Width((WindowSize.x - 15) * 0.25f), GUILayout.Height(WindowSize.y - 100));
        EditorGUILayout.BeginVertical();
        DataList(all_data,new Vector2((WindowSize.x - 15) * 0.25f, WindowSize.y - 100));
        EditorGUILayout.EndVertical();
        WindowPos[1] = EditorGUILayout.BeginScrollView(WindowPos[1], GUILayout.Width((WindowSize.x) * 0.75f), GUILayout.Height(WindowSize.y - 100));
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    public override void AddData()
    {
        base.AddData();
        all_data.Add(new CharClass("CharClass_" + all_data.Count));
    }

    public override void DeleteData()
    {
        base.DeleteData();
        if (all_data.Count > 0)
        {
            all_data.RemoveAt(dataIndex);
            dataIndex = dataIndex - 1;
            if (dataIndex < 0)
            {
                dataIndex = all_data.Count - 1;
            }
        }
    }

    public override void SaveData()
    {
        base.SaveData();
        DataManager.SaveData<CharClass>(DataManager.DataPaths["CharClasses"], all_data);
    }
}
