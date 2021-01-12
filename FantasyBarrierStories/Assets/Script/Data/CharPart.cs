namespace FBS.Data
{

    /// <summary>
    /// 角色配件
    /// </summary>
    public class CharPart : GameData
    {
       
        /// <summary>
        /// 配件名稱
        /// </summary>
        private string PartName;
        /// <summary>
        /// 配件介紹
        /// </summary>
        private string PartDescription;
        /// <summary>
        /// 配件實際模型的路徑加上檔案名稱，讀取用。
        /// </summary>
        private string MdlPath;

        public CharPart(string i_name) : base(i_name)
        {
            
        }

        public CharPart(string i_name, string i_des, string i_mdlPath) : base(i_name)
        {
            PartName = i_name;
            PartDescription = i_des;
            MdlPath = i_mdlPath;
        }
    }
}
