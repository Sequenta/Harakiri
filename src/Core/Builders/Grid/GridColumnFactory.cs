using System;
using System.Linq.Expressions;

namespace Core.Builders.Grid
{
    public class GridColumnFactory<T>
    {
        private KendoGridBuilder<T> container;
        public GridColumnFactory(KendoGridBuilder<T> gridBuilder)
        {
            container = gridBuilder;
        }

        public virtual GridBoundColumnBuilder Bound<TValue>(Expression<Func<T, TValue>> expression)
        {
            var gridBoundColumn = new GridColumn(expression.GetName());
            container.GridColumns.Add(gridBoundColumn);
            return new GridBoundColumnBuilder(gridBoundColumn);
        }
    }
}