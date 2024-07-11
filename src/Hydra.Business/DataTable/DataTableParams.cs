namespace Hydra.Presentation.Web.Models.DataTable
{
    public class DataTableParams
    {
        public int Draw { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
        public SearchValue Search { get; set; }
        public ColumnValue[]? Columns { get; set; }
        public OrderValue[]? Order { get; set; }

        public class SearchValue
        {
            public string Value { get; set; }
        }

        public class ColumnValue
        {
            public string Name { get; set; }
        }

        public class OrderValue
        {
            public int Column { get; set; }
            public string Dir { get; set; }
        }
    }
}
