using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Builders.Grid
{
    public class KendoGridBuilder<T>
    {
        private string GridName { get; set; }
        private bool IsScrollable { get; set; }
        private bool IsGroupable { get; set; }
        private bool IsSortable { get; set; }
        private bool IsResizable { get; set; }
        private int GridHeight { get; set; }

        public GridDataSource<T> GridDataSource { get; set; }
        public RowTemplate RowTemplate { get; private set; }
        public List<GridColumn> GridColumns { get; set; }
        
        public KendoGridBuilder()
        {
            GridColumns = new List<GridColumn>();
            GridHeight = 550;
        }

        public KendoGridBuilder<T> Name(string name)
        {
            GridName = name;
            return this;
        }

        public KendoGridBuilder<T> Scrollable()
        {
            IsScrollable = true;
            return this;
        }

        public KendoGridBuilder<T> Groupable()
        {
            IsGroupable = true;
            return this;
        }

        public KendoGridBuilder<T> Sortable()
        {
            IsSortable = true;
            return this;
        }

        public KendoGridBuilder<T> Height(int height)
        {
            GridHeight = height;
            return this;
        }

        public KendoGridBuilder<T> Resizable()
        {
            IsResizable = true;
            return this;
        }

        public KendoGridBuilder<T> Columns(Action<GridColumnFactory<T>> configurator)
        {
            var gridColumnFactory = new GridColumnFactory<T>(this);
            configurator(gridColumnFactory);
            return this;
        }

        public KendoGridBuilder<T> DataSource(Action<GridDataSourceBuilder<T>> configurator)
        {
            var gridDataSourceBuilder = new GridDataSourceBuilder<T>(this);
            configurator(gridDataSourceBuilder);
            return this;
        }

        public KendoGridBuilder<T> ClientRowTemplate(string id,string template)
        {
            RowTemplate = new RowTemplate
            {
                Id = id,
                Template = template
            };
            return this;
        }


        public string Html()
        {
            var options = new List<string>();

            var gridMarkupBuilder = new StringBuilder(string.Format(@"<div id=""{0}"" class=""k-grid k-widget"" style=""width: auto;""></div>" + "<script>" + @"$(""#{0}"")", GridName ?? "grid") + ".kendoGrid({");

            options.Add(GridDataSource.GetDataSourceMarkup());

            if (IsGroupable)
            {
                options.Add("groupable: {messages: {empty: 'Перетащите сюда колонку для группировки по ней'}}");
            }
            if (IsSortable)
            {
                options.Add("sortable: true");
            }
            if (IsScrollable)
            {
                options.Add("scrollable: true");
            }
            if (IsResizable)
            {
                options.Add("resizable: true");
            }
            if (RowTemplate != null)
            {
                options.Add(string.Format("rowTemplate: kendo.template($('#{0}').html())",RowTemplate.Id));
            }
            
            var columnsMarkupBuilder = new StringBuilder();
            columnsMarkupBuilder.Append("columns: [");
            columnsMarkupBuilder.Append(string.Join(",",GridColumns));
            columnsMarkupBuilder.Append("]");
            options.Add(columnsMarkupBuilder.ToString());

            gridMarkupBuilder.Append(string.Join(",", options));
            gridMarkupBuilder.Append("});");
            gridMarkupBuilder.Append("</script>");

            if (RowTemplate != null)
            {
                gridMarkupBuilder.Append(RowTemplate.Template);
            }

            return gridMarkupBuilder.ToString();
        }
    }
}