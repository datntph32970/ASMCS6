namespace AppAPI.Services.BaseServices.Common
{
    public class SearchBase
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public string SortColumn { get; set; } = "CreatedDate";
        public string SortOrder { get; set; } = "desc";

    }
}
