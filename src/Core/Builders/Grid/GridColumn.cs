namespace Core.Builders.Grid
{
    public class GridColumn
    {
        public string Field { get; private set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public string Template { get; set; }
        public string FooterTemplate { get; set; }
        public bool Groupable { get; set; }
        public bool Sortable { get; set; }
        public bool Hidden { get; set; }
        public string GroupFooterTemplate { get; set; }
        public string GroupHeaderTemplate { get; set; }

        public GridColumn(string field)
        {
            Field = field;
            Title = Field;
            Width = 110;
            Groupable = true;
            Sortable = true;
            Hidden = false;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Template))
            {
                return "{" + string.Format(@"field: ""{0}"", title: ""{1}"", width: {2}, groupable: {3}, sortable: {4}, footerTemplate:""{5}"", groupFooterTemplate:""{6}"", groupHeaderTemplate:""{7}"", hidden: {8}", Field, Title, Width, Groupable.ToString().ToLower(), Sortable.ToString().ToLower(), FooterTemplate, GroupFooterTemplate, GroupHeaderTemplate, Hidden.ToString().ToLower()) + "}";
            }
            return "{" + string.Format(@"field: ""{0}"", title: ""{1}"", width: {2}, groupable: {3}, sortable: {4}, template:""{5}"", footerTemplate:""{6}"", groupFooterTemplate:""{7}"", groupHeaderTemplate:""{8}"", hidden: {9}", Field, Title, Width, Groupable.ToString().ToLower(), Sortable.ToString().ToLower(), Template, FooterTemplate, GroupFooterTemplate, GroupHeaderTemplate, Hidden.ToString().ToLower()) + "}";
        }

        public void SetFieldAsCountable()
        {
            Field = Field + ".length";
        }
    }
}