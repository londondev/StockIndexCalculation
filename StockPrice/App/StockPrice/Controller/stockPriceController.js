angular.module('app')
    .controller('stockPriceController', ['$scope', 'stockPriceFactory',
        function ($scope, stockPriceFactory) {
            //functions
            $scope.populateEndDates = populateEndDates;
            $scope.updateGraph = updateGraph;
            $scope.saveModel = saveModel;

            $scope.stockDataList = [];
            $scope.hasDataLoaded = false;
            $scope.pieChartData = [];
            $scope.plotChartData = {
                indexes: [],
                dates: []
            };
            

            

            function saveModel() {
                $scope.isLoading = true;
                stockPriceFactory.saveModel($scope.stockData).
                    then(function(response) {
                        $scope.stockDataList = response.dataTableData;
                        $scope.hasDataLoaded = true;
                        $scope.plotChartCoreData = response.plotChartData;
                        addOrderNumberToPlotData();
                        preparePlotChartData($scope.plotChartCoreData);
                        preparePieChartData(response.pieChartData);
                        $scope.isLoading = false;
                    });
            }

           function updateGraph() {
               filterCorePlotData();
               var firstDayIndexValue = $scope.plotChartFilteredData[0].Index;
               angular.forEach($scope.plotChartFilteredData, function (data) {
                   data.Index = data.Index * 100 / firstDayIndexValue;
               });
               $scope.plotChartData.indexes = [];
               $scope.plotChartData.dates = [];
               preparePlotChartData($scope.plotChartFilteredData);

               //Workaround, ng highchart does not update graph
               Highcharts.charts[0].series[0].remove();
               Highcharts.charts[0].addSeries({
                   data:$scope.plotChartData.indexes
               }, true);

           }

            function filterCorePlotData() {
                var startDateIndex = _.findWhere($scope.plotChartCoreData, { Date: $scope.startDate }).order;
                var endDateIndex = _.findWhere($scope.plotChartCoreData, { Date: $scope.endDate }).order;
                $scope.plotChartFilteredData = _.filter($scope.plotChartCoreData, function (data) {
                    return data.order >= startDateIndex && data.order <= endDateIndex;
                });
            }


            function preparePlotChartData(plotChartData) {
                angular.forEach(plotChartData, function (data) {
                    $scope.plotChartData.indexes.push(data.Index);
                    $scope.plotChartData.dates.push(data.Date);
                });
            }

            function addOrderNumberToPlotData() {
                var i = 0;
                angular.forEach($scope.plotChartCoreData, function (data) {
                    data.order = i++;
                });
            }

            function preparePieChartData(pieChartData) {
                angular.forEach(pieChartData, function(data) {
                    $scope.pieChartData.push({ name: data.StockId, y: data.Weight });
                });
            }

            function populateEndDates() {
                var startDateIndex = _.findWhere($scope.plotChartCoreData, { Date: $scope.startDate }).order;
                var filteredData = _.filter($scope.plotChartCoreData, function (data) {
                    return data.order > startDateIndex;
                });

                $scope.endDates = [];
                angular.forEach(filteredData, function (data) {
                    $scope.endDates.push(data.Date);
                });
               
            }

            $scope.highchartsIndex = {
                options: {
                    chart: {
                        type: 'line'
                    }
                },
                yAxis: {
                    title: {
                        text: 'Index'
                    }
                },
                xAxis: [{
                    categories: $scope.plotChartData.dates
                }],
                series: [{
                    data: $scope.plotChartData.indexes
                }],
                title: {
                    text: 'Index '
                },
                loading: false
            }

            $scope.highchartsPie = {
                options: {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    }
                },
                title: {
                    text: 'Top 5 Weighted Stocks'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.5f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Stocks',
                    colorByPoint: true,
                    data: $scope.pieChartData
                }]
            }

        }]);