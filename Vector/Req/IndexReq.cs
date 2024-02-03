namespace Terramours_Gpt_Vector.Req
{
    public record IndexUpsertReq : BaseReq
    {
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string? ThirdPartId { get; set; }
    }
    public record IndexUpdateReq : BaseReq
    {
        public int IndexId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string? Name { get; init; }
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string? ThirdPartId { get; set; }
    }
    public record IndexQueryReq : BaseReq
    {
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string? ThirdPartId { get; set; }
    }
    public record IndexDeleteReq : BaseReq
    {
        public int IndexId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
    }
}
