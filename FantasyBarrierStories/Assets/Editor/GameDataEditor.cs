using FBS.Data;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataEditor : EditorWindow
{
    public DataManager DM;

    public int dataIndex;

    public static Vector2 WindowSize = new Vector2(800, 800);
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

    public void DataList<T>(GameDataCollection<T> i_data, Vector2 i_size) where T : GameData
    {
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
    }
    public virtual void AddData()
    {
    }
    public virtual void DeleteData()
    {
    }
    public virtual void SaveData()
    {
    }
    public virtual void LoadData()
    {
    }
}
