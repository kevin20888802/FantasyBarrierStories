using System.Collections.Generic;

namespace FBS.Data
{
    /// <summary>
    /// 遊戲資料集合，附帶取得、新增及刪除資料功能。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GameDataCollection<T> where T : GameData
    {
        /// <summary>
        /// 資料真正存放位置。
        /// </summary>
        private List<T> data;

        public GameDataCollection()
        {
            data = new List<T>();
        }

        public GameDataCollection(List<T> i_data)
        {
            data = i_data;
        }

        /// <summary>
        /// 由資料名稱取得資料。
        /// </summary>
        /// <param name="i_name">資料名稱</param>
        /// <returns></returns>
        public T GetData(string i_name)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].name == i_name)
                {
                    return data[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 由資料順序取得資料。
        /// </summary>
        /// <param name="i_index">順序</param>
        /// <returns></returns>
        public T GetData(int i_index)
        {
            return data[i_index];
        }

        /// <summary>
        /// 新增資料。
        /// </summary>
        /// <param name="i_data">資料</param>
        public void AddData(T i_data)
        {
            data.Add(i_data);
        }

        /// <summary>
        /// 移除指定順序的資料。
        /// </summary>
        /// <param name="i_index">順序</param>
        public void RemoveAt(int i_index)
        {
            data.RemoveAt(i_index);
        }

        /// <summary>
        /// 資料數量。
        /// </summary>
        /// <returns></returns>
        public int DataCount()
        {
            return data.Count;
        }
    }

}
