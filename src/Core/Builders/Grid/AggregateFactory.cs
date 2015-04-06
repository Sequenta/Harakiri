namespace Core.Builders.Grid
{
    public class AggregateFactory<T>
    {
        private DataSourceAggregateDescriptorFactory<T> container;
        public string AggregateField { get; set; }

        public AggregateFactory(string field, DataSourceAggregateDescriptorFactory<T> aggregateDescriptorFactory)
        {
            AggregateField = field;
            container = aggregateDescriptorFactory;
        }

        public AggregateFactory<T> Count()
        {
            container.GridDataSource.Aggregates.Add(new DataSourceAggregate
            {
                Field = AggregateField,
                Function = "count"
            });
            return this;
        }

        public AggregateFactory<T> Sum()
        {
            container.GridDataSource.Aggregates.Add(new DataSourceAggregate
            {
                Field = AggregateField,
                Function = "sum"
            });
            return this;
        }

        public AggregateFactory<T> Average()
        {
            container.GridDataSource.Aggregates.Add(new DataSourceAggregate
            {
                Field = AggregateField,
                Function = "average"
            });
            return this;
        }

        public AggregateFactory<T> Min()
        {
            container.GridDataSource.Aggregates.Add(new DataSourceAggregate
            {
                Field = AggregateField,
                Function = "min"
            });
            return this;
        }

        public AggregateFactory<T> Max()
        {
            container.GridDataSource.Aggregates.Add(new DataSourceAggregate
            {
                Field = AggregateField,
                Function = "max"
            });
            return this;
        } 
    }
}