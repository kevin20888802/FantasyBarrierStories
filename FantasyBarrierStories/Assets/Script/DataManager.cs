using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using FBS.Data;

public class DataManager
{
    public static Dictionary<string, string> DataPaths;

    /// <summary>
    /// 所有角色職業
    /// </summary>
    public List<CharClass> CharClasses;

    public DataManager()
    {
        DataPaths = new Dictionary<string, string>()
        {
            {"CharClasses", "GameData/CharClasses/"}
        };
    }

    public static List<CharClass> LoadCharClasses()
    {
        List<CharClass> o_data = new List<CharClass>();
        TextAsset[] _loadedDatas = Resources.LoadAll<TextAsset>(DataPaths["CharClasses"]);
        for (int i = 0; i < _loadedDatas.Length; i++)
        {
            o_data.Add(JsonConvert.DeserializeObject<CharClass>(_loadedDatas[i].text));
        }
        return o_data;
    }
}
