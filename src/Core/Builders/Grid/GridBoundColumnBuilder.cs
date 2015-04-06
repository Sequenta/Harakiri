namespace Core.Builders.Grid
{
    public class GridBoundColumnBuilder
    {
        private GridColumn Column { get; set; }

        public GridBoundColumnBuilder(GridColumn column)
        {
            Column = column;
        }

        public GridBoundColumnBuilder Name(string name)
        {
            Column.Title = name;
            return this;
        }

        public GridBoundColumnBuilder Width(int width)
        {
            Column.Width = width;
            return this;
        }

        public GridBoundColumnBuilder Template(string template)
        {
            Column.Template = template;
            return this;
        }

        public GridBoundColumnBuilder FooterTemplate(string template)
        {
            Column.FooterTemplate = template;
            return this;
        }

        public GridBoundColumnBuilder GroupFooterTemplate(string template)
        {
            Column.GroupFooterTemplate = template;
            return this;
        }

        public GridBoundColumnBuilder GroupHeaderTemplate(string template)
        {
            Column.GroupHeaderTemplate = template;
            return this;
        }


        public GridBoundColumnBuilder Count()
        {
            Column.SetFieldAsCountable();
            return this;
        }

        public GridBoundColumnBuilder Sortable(bool isSortable)
        {
            Column.Sortable = isSortable;
            return this;
        }

        public GridBoundColumnBuilder Groupable(bool isGroupable)
        {
            Column.Groupable = isGroupable;
            return this;
        }

        public GridBoundColumnBuilder Hidden(bool isHidden)
        {
            Column.Hidden = isHidden;
            return this;
        }
    }
}