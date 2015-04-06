using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Builders.Grid
{
    public class GridDataSourceBuilder<T>
    {
        private KendoGridBuilder<T> container;

        public GridDataSourceBuilder(KendoGridBuilder<T> gridBuilder)
        {
            container = gridBuilder;
        }

        public GridDataSourceBuilder<T> Data(IEnumerable<T> data)
        {
            container.GridDataSource = new GridDataSource<T>(data);
            return this;
        }

        public GridDataSourceBuilder<T> Aggregates(Action<DataSourceAggregateDescriptorFactory<T>> aggregates)
        {
            var dataSourceAggregateDescriptorFactory = new DataSourceAggregateDescriptorFactory<T>(container.GridDataSource);
            aggregates(dataSourceAggregateDescriptorFactory);
            return this;
        }

        public GridDataSourceBuilder<T> Group<TValue>(Expression<Func<T, TValue>> expression)
        {
            container.GridDataSource.InitialGroupFields.Add(new DataSourceGroupField { Name = expression.GetName() });
            return this;
        }
    }
}