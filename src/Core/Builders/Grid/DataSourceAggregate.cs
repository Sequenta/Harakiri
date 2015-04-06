namespace Core.Builders.Grid
{
    public class DataSourceAggregate
    {
        public string Field { get; set; }
        public string Function { get; set; }

        public override string ToString()
        {
            return "{" + string.Format(@"field: ""{0}"", aggregate: ""{1}""", Field, Function) + "}";
        }
    }
}