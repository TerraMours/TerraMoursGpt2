﻿namespace Terramours_Gpt_Vector.Res
{
    public class IndexUpsertRes
    {
        /// <summary>
        /// name
        /// </summary>
        public string? Name { get; init; }
        /// <summary>
        /// key
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string? ThirdPartId { get; set; }
    }
    public class IndexUpdateRes
    {
        public int IndexId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string ThirdPartId { get; set; }
    }
    public class IndexQueryRes
    {
        public int IndexId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string ThirdPartId { get; set; }
    }
    
}
