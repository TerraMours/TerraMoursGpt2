namespace TerraMours.Domains.LoginDomain.Contracts.Res
{
    /// <summary>
    /// 下拉框参数模型
    /// </summary>
    public class KeyValueRes
    {
        /// <summary>
        /// 标签
        /// </summary>
        public long Key { get; set; }
        /// <summary>
        /// 显示值
        /// </summary>
        public string Value { get; set; }

        public KeyValueRes(long key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
