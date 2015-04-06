using System;
using System.Linq.Expressions;

namespace Core.Builders.Grid
{
    public class DataSourceAggregateDescriptorFactory<TModel>
    {
        public GridDataSource<TModel> GridDataSource { get; set; }

        public DataSourceAggregateDescriptorFactory(GridDataSource<TModel> dataSource)
        {
            GridDataSource = dataSource;
        }

        public AggregateFactory<TModel> Add<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var aggregateFactory = new AggregateFactory<TModel>(expression.GetName(),this);
            return aggregateFactory;
        }
    }
}