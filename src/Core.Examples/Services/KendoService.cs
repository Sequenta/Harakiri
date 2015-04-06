using System.Collections.Generic;
using Core.Examples.Entities;

namespace Core.Examples.Services
{
    public static class KendoService
    {
        public static string GetKendoGrid()
        {
            var data = GetTestData();
            var markup = KendoBuilder<ComplexData>.Grid()
                  .Name("test")
                  .DataSource(datasource => datasource.Data(data))
                  .Columns(columns =>
                  {
                      columns.Bound(column => column.Responsible);
                      columns.Bound(column => column.Tasks).Count();
                  });
            return markup.Html();
        }

        private static IEnumerable<ComplexData> GetTestData()
        {
            var complexData = new List<ComplexData>
            {
                new ComplexData
                {
                    Responsible = "First",
                    Tasks = new List<TestModel>
                    {
                        new TestModel
                        {
                            First = "11",
                            Second = "21"
                        },
                        new TestModel
                        {
                            First = "12",
                            Second = "22"
                        },
                         new TestModel
                        {
                            First = "12",
                            Second = "22"
                        },
                         new TestModel
                        {
                            First = "12",
                            Second = "22"
                        }
                    }
                },
                new ComplexData
                {
                    Responsible = "Second",
                    Tasks = new List<TestModel>
                    {
                        new TestModel
                        {
                            First = "11",
                            Second = "21"
                        },
                        new TestModel
                        {
                            First = "12",
                            Second = "22"
                        }
                    }
                }
            };
            return complexData;
        }
    }
}