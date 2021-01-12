using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using FBS.Data;
using UnityEditor;
using UnityEngine.AssetDataBaseHelper;
using System.IO;

public class DataManager
{
    public static DataManager instance;
    public static DataMode dataMode;
    public static Dictionary<string, string> DataPaths;

    /// <summary>
    /// 所有角色職業
    /// </summary>
    public List<CharClass> CharClasses;

    public DataManager(DataMode i_mode)
    {
        if (instance == null)
        {
            DataPaths = new Dictionary<string, string>()
            {
                {"CharClasses", "GameData/CharClasses/"}
            };
            dataMode = i_mode;
            Init();
        }
    }

    /// <summary>
    /// 資料系統初始化。
    /// </summary>
    public void Init()
    {
        CharClasses = LoadData<CharClass>(DataPaths["CharClasses"]);
    }

    /// <summary>
    /// 編輯時使用的儲存資料，只能於編輯時使用。
    /// </summary>
    /// <typeparam name="T">資料類型，必須是遊戲資料GameData類。</typeparam>
    /// <param name="i_path">資料夾路徑。</param>
    /// <param name="i_data">資料類型清單，必須是遊戲資料GameData類。</param>
    public static void SaveData<T>(string i_path, List<T> i_data) where T : GameData
    {
        if (dataMode == DataMode.Editor)
        {
            string _realPath = Application.dataPath + "/Resources/" + i_path.Replace("Assets", "");
            for (int i = 0; i < i_data.Count; i++)
            {
                string _fileName = _realPath + "/" + i_data[i].name + ".json";
                if (!Directory.Exists(_realPath))
                {
                    Directory.CreateDirectory(_realPath);
                }
                if (!File.Exists(_fileName))
                {
                    File.Create(_fileName).Close();
                }
                File.WriteAllText(_fileName,JsonConvert.SerializeObject(i_data[i]));
                Debug.Log("Saving Data:" + _fileName);
            }
        }
    }

    /// <summary>
    /// 讀取資料。
    /// </summary>
    /// <typeparam name="T">資料類型，必須是遊戲資料GameData類。</typeparam>
    /// <param name="i_path">資料夾路徑。</param>
    /// <returns></returns>
    public static List<T> LoadData<T>(string i_path) where T : GameData
    {
        List<T> o_data = new List<T>();
        TextAsset[] _loadedDatas;
        if (dataMode == DataMode.InGame)
        {
            _loadedDatas = Resources.LoadAll<TextAsset>(i_path);
        }
        else
        {
            _loadedDatas = AssetDataBaseHelper.LoadAllAssetAtPath<TextAsset>("Assets/Resources/" + i_path);
            Debug.Log(_loadedDatas.Length);
        }
        for (int i = 0; i < _loadedDatas.Length; i++)
        {
            o_data.Add(JsonConvert.DeserializeObject<T>(_loadedDatas[i].text));
        }
        return o_data;
    }
}
