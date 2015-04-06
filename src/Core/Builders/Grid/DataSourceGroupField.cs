using System.Collections.Generic;

namespace Core.Builders.Grid
{
    public class DataSourceGroupField
    {
        public string Name { get; set; }
        public List<DataSourceAggregate> Aggregates { get; set; }

        public override string ToString()
        {
            if (Aggregates == null) 
                return "{" + string.Format(@"field: ""{0}""", Name) + "}";

            var aggregates = string.Join(",", Aggregates);
            return "{" + string.Format(@"field: ""{0}"", aggregates:[{1}]", Name, aggregates) + "}";
        }
    }
}