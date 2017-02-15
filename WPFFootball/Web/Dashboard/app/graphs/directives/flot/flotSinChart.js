"use strict";

angular.module('app.graphs').directive('flotSinChart', function (FlotConfig) {
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="chart"></div>',
        scope: {
            data: '='
        },
        link: function (scope, element) {

            var plot = $.plot(element, scope.data, {
                series: {
                    lines: {
                        show: true
                    },
                    points: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true,
                    tickColor: FlotConfig.chartBorderColor,
                    borderWidth: 0,
                    borderColor: FlotConfig.chartBorderColor
                },
                tooltip: true,
                tooltipOpts: {
                    //content : "Value <b>$x</b> Value <span>$y</span>",
                    defaultTheme: false
                },
                colors: [FlotConfig.chartSecond, FlotConfig.chartFourth],
                yaxis: {
                    min: -1.1,
                    max: 1.1
                },
                xaxis: {
                    min: 0,
                    max: 15
                }
            });

            element.on("plotclick", function (event, pos, item) {
                if (item) {
                    $("#clickdata").text("You clicked point " + item.dataIndex + " in " + item.series.label + ".");
                    plot.highlight(item.series, item.datapoint);
                }
            });
        }
    }
});