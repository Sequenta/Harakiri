using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Core.Builders.Grid
{
    public class GridDataSource<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public ICollection<DataSourceAggregate> Aggregates { get; private set; }
        public ICollection<DataSourceGroupField> InitialGroupFields { get; set; }

        public GridDataSource(IEnumerable<T> data)
        {
            Data = data;
            Aggregates = new List<DataSourceAggregate>();
            InitialGroupFields = new List<DataSourceGroupField>();
        }

        public string GetDataSourceMarkup()
        {
            var data = JsonConvert.SerializeObject(Data);
            var result = new StringBuilder("dataSource:{");
            result.Append(string.Format(@"data:{0}", data));
            if (Aggregates.Any())
            {
                var aggregates = "aggregate:[";
                aggregates += string.Join(",", Aggregates);
                aggregates += "]";
                result.Append("," + aggregates);
            }
            if (InitialGroupFields.Any())
            {
                AddAggregates();
                var groupFileds = "group:[";
                groupFileds += string.Join(",", InitialGroupFields);
                groupFileds += "]";
                result.Append("," + groupFileds);
            }
            return result.Append("}").ToString();
        }

        private void AddAggregates()
        {
            if (!Aggregates.Any()) return;

            var aggregates = Aggregates.ToList();
            foreach (var dataSourceGroupField in InitialGroupFields)
            {
                dataSourceGroupField.Aggregates = aggregates;
            }
        }
    }
}