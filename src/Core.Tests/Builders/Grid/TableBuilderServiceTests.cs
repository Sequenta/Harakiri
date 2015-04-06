using System.Collections.Generic;
using System.Linq;
using Core.Builders.Grid;
using Core.Tests.Common;
using Xunit;

namespace Core.Tests.Builders.Grid
{
    namespace AssignmentsTests.Services
    {
        public class TableBuilderServiceTests
        {
            private readonly List<TestModel> simpleData;
            private readonly List<ComplexData> complexData;

            public TableBuilderServiceTests()
            {
                simpleData = new List<TestModel>
                {
                    new TestModel
                    {
                        First = "blahgh",
                        Second = "gfhfddh"
                    },
                    new TestModel
                    {
                        First = "blahgfhdfjhhgh",
                        Second = "gfhft45345ddh"
                    },
                    new TestModel
                    {
                        First = "bl34yhahgh",
                        Second = "gfhfh45h4ehddh"
                    }
                };

                complexData = new List<ComplexData>
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
            }

            [Fact]
            public void BoundingColumnsTest()
            {
                var grid = KendoBuilder<TestModel>.Grid();

                grid = grid.Columns(columns =>
                {
                    columns.Bound(column => column.First).Name("Second");
                    columns.Bound(column => column.Second);
                });

                Assert.Equal(2, grid.GridColumns.Count);
                Assert.True(grid.GridColumns.All(x => x.Title == "Second"));
            }

            [Fact]
            public void BasicMarkupTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                    .Name("test")
                    .Columns(columns =>
                    {
                        columns.Bound(column => column.First).Name("Second");
                        columns.Bound(column => column.Second);
                    })
                    .DataSource(datasource => datasource.Data(simpleData))
                    .Scrollable()
                    .Sortable()
                    .Groupable()
                    .Html();

                Assert.Equal(@"<div id=""test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#test"").kendoGrid({dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}]},groupable: {messages: {empty: 'Перетащите сюда колонку для группировки по ней'}},sortable: true,scrollable: true,columns: [{field: ""First"", title: ""Second"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script>", gridMarkup);
            }

            [Fact]
            public void RowTemplateTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                    .Name("test")
                    .Columns(columns =>
                    {
                        columns.Bound(column => column.First);
                        columns.Bound(column => column.Second);
                    })
                    .DataSource(datasource => datasource.Data(simpleData)).ClientRowTemplate("testTemplate", @"<script id=""testTemplate"" type=""text/x-kendo-tmpl""><tr><td><b> #:First# </b></td><td><span>#: First# #: Second# </span></td></tr></script>")
                    .Html();

                Assert.Equal(@"<div id=""test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#test"").kendoGrid({dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}]},rowTemplate: kendo.template($('#testTemplate').html()),columns: [{field: ""First"", title: ""First"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script><script id=""testTemplate"" type=""text/x-kendo-tmpl""><tr><td><b> #:First# </b></td><td><span>#: First# #: Second# </span></td></tr></script>", gridMarkup);
            }

            [Fact]
            public void ColumnTemplateTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                   .Name("test")
                   .Columns(columns =>
                   {
                       columns.Bound(column => column.First).Template("<b>#=First#</b>");
                       columns.Bound(column => column.Second);
                   })
                   .DataSource(datasource => datasource.Data(simpleData))
                   .Html();

                Assert.Equal(@"<div id=""test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#test"").kendoGrid({dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}]},columns: [{field: ""First"", title: ""First"", width: 110, groupable: true, sortable: true, template:""<b>#=First#</b>"", footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script>", gridMarkup);
            }

            [Fact]
            public void AggregateTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                    .Name("test")
                    .DataSource(datasource => datasource.Data(simpleData)
                                                        .Aggregates(aggregates =>
                                                        {
                                                            aggregates.Add(x => x.First).Count();
                                                            aggregates.Add(x => x.Second).Max();
                                                        })
                    );
                Assert.Equal(gridMarkup.GridDataSource.Aggregates.Count, 2);
                Assert.Equal(gridMarkup.GridDataSource.Aggregates.First().Field, "First");
                Assert.Equal(gridMarkup.GridDataSource.Aggregates.First().Function, "count");
            }

            [Fact]
            public void AggregateMarkupTest()
            {
                var data = new GridDataSource<TestModel>(simpleData);
                data.Aggregates.Add(new DataSourceAggregate
                {
                    Field = "First",
                    Function = "sum"
                });
                data.Aggregates.Add(new DataSourceAggregate
                {
                    Field = "Second",
                    Function = "max"
                });

                var actual = data.GetDataSourceMarkup();
                Assert.Equal(@"dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}],aggregate:[{field: ""First"", aggregate: ""sum""},{field: ""Second"", aggregate: ""max""}]}", actual);
            }

            [Fact]
            public void GridWithAggregatesMarkupTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                   .Name("test")
                   .DataSource(datasource => datasource.Data(simpleData)
                                                       .Aggregates(aggregates =>
                                                       {
                                                           aggregates.Add(x => x.First).Sum();
                                                           aggregates.Add(x => x.Second).Max();
                                                       }))
                   .Columns(columns =>
                   {
                       columns.Bound(column => column.First);
                       columns.Bound(column => column.Second);
                   });

                var actual = gridMarkup.Html();
                Assert.Equal(@"<div id=""test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#test"").kendoGrid({dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}],aggregate:[{field: ""First"", aggregate: ""sum""},{field: ""Second"", aggregate: ""max""}]},columns: [{field: ""First"", title: ""First"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script>", actual);
            }

            [Fact]
            public void ColumnCountFunctionTest()
            {
                var gridMarkup = KendoBuilder<ComplexData>.Grid()
                  .Name("test")
                  .DataSource(datasource => datasource.Data(complexData))
                  .Columns(columns =>
                  {
                      columns.Bound(column => column.Responsible);
                      columns.Bound(column => column.Tasks).Count();
                  });

                var actual = gridMarkup.Html();
                Assert.Equal(@"<div id=""test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#test"").kendoGrid({dataSource:{data:[{""Responsible"":""First"",""Tasks"":[{""First"":""11"",""Second"":""21""},{""First"":""12"",""Second"":""22""},{""First"":""12"",""Second"":""22""},{""First"":""12"",""Second"":""22""}]},{""Responsible"":""Second"",""Tasks"":[{""First"":""11"",""Second"":""21""},{""First"":""12"",""Second"":""22""}]}]},columns: [{field: ""Responsible"", title: ""Responsible"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Tasks.length"", title: ""Tasks"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script>", actual);
            }

            [Fact]
            public void ColumnAdditionalAttributesTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                .Name("test")
                .Columns(columns =>
                {
                    columns.Bound(column => column.First).Groupable(false);
                    columns.Bound(column => column.Second).Sortable(false);
                })
                .DataSource(dataSource => dataSource.Data(simpleData));
                var actual = gridMarkup.Html();
                Assert.Equal(@"<div id=""test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#test"").kendoGrid({dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}]},columns: [{field: ""First"", title: ""First"", width: 110, groupable: false, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: false, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script>", actual);
            }

            [Fact]
            public void DataSourceGroupTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid().Columns(columns =>
                {
                    columns.Bound(column => column.First).Groupable(false);
                    columns.Bound(column => column.Second).Sortable(false);
                })
               .DataSource(dataSource => dataSource.Data(simpleData)
                                                   .Group(x => x.First)
                                                   .Group(x => x.Second));
                var actual = gridMarkup.Html();
                Assert.Equal(@"<div id=""grid"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#grid"").kendoGrid({dataSource:{data:[{""First"":""blahgh"",""Second"":""gfhfddh""},{""First"":""blahgfhdfjhhgh"",""Second"":""gfhft45345ddh""},{""First"":""bl34yhahgh"",""Second"":""gfhfh45h4ehddh""}],group:[{field: ""First""},{field: ""Second""}]},columns: [{field: ""First"", title: ""First"", width: 110, groupable: false, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: false, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:"""", hidden: false}]});</script>", actual);
            }

            [Fact]
            public void CorrectNamingTest()
            {
                var gridMarkup = KendoBuilder<TestModel>.Grid()
                    .Name("fisrtName")
                    .Columns(columns =>
                    {
                        columns.Bound(column => column.First).Groupable(false);
                        columns.Bound(column => column.Second).Sortable(false);
                    })
                    .DataSource(datasource => datasource.Data(simpleData))
                    .Html();
                Assert.Contains(@"id=""fisrtName""", gridMarkup);
                Assert.Contains(@"$(""#fisrtName"")", gridMarkup);
            }

            [Fact]
            public void GroupFieldWithAggregateMarkupTest()
            {
                var markup = new DataSourceGroupField
                {
                    Name = "Test",
                    Aggregates = new List<DataSourceAggregate>
                {
                    new DataSourceAggregate {Field = "Agr1", Function = "min"},
                    new DataSourceAggregate {Field = "Agr2", Function = "max"},
                    new DataSourceAggregate {Field = "Agr3", Function = "sum"}
                }
                }.ToString();
                Assert.Equal(@"{field: ""Test"", aggregates:[{field: ""Agr1"", aggregate: ""min""},{field: ""Agr2"", aggregate: ""max""},{field: ""Agr3"", aggregate: ""sum""}]}", markup);
            }

            [Fact]
            public void AggregateForGroupingTest()
            {
                var data = new List<NumericModel>
            {
                new NumericModel
                {
                    First = 1,
                    Second = 2,
                    Third = 3
                },
                new NumericModel
                {
                    First = 4,
                    Second = 5,
                    Third = 6
                },
                new NumericModel
                {
                    First = 7,
                    Second = 8,
                    Third = 9
                }
            };

            var actual = KendoBuilder<NumericModel>.Grid()
                .Name("Test")
                .Columns(columns =>
                {
                    columns.Bound(x => x.First).GroupHeaderTemplate("Count: #=count#");
                    columns.Bound(x => x.Second).GroupFooterTemplate("Min: #=min#");
                    columns.Bound(x => x.Third).GroupFooterTemplate("Max: #=max#");
                })
                .DataSource(dataSource => dataSource.Data(data)
                                                    .Group(groups => groups.First)
                                                    .Aggregates(aggregates =>
                                                    {
                                                        aggregates.Add(field => field.First).Count();
                                                        aggregates.Add(field => field.Second).Min();
                                                        aggregates.Add(field => field.Third).Max();
                                                    }))
                .Html();
            Assert.Equal(@"<div id=""Test"" class=""k-grid k-widget"" style=""width: auto;""></div><script>$(""#Test"").kendoGrid({dataSource:{data:[{""First"":1,""Second"":2,""Third"":3},{""First"":4,""Second"":5,""Third"":6},{""First"":7,""Second"":8,""Third"":9}],aggregate:[{field: ""First"", aggregate: ""count""},{field: ""Second"", aggregate: ""min""},{field: ""Third"", aggregate: ""max""}],group:[{field: ""First"", aggregates:[{field: ""First"", aggregate: ""count""},{field: ""Second"", aggregate: ""min""},{field: ""Third"", aggregate: ""max""}]}]},columns: [{field: ""First"", title: ""First"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:"""", groupHeaderTemplate:""Count: #=count#"", hidden: false},{field: ""Second"", title: ""Second"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:""Min: #=min#"", groupHeaderTemplate:"""", hidden: false},{field: ""Third"", title: ""Third"", width: 110, groupable: true, sortable: true, footerTemplate:"""", groupFooterTemplate:""Max: #=max#"", groupHeaderTemplate:"""", hidden: false}]});</script>", actual);
            }
        }
    }
}