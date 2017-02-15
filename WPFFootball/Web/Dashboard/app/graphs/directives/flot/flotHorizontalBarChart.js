"use strict";

angular.module('app.graphs').directive('flotHorizontalBarChart', function(FlotConfig){
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="chart"></div>',
        scope: {
            data: '='
        },
        link: function(scope, element){
            $.plot(element, scope.data, {
                colors : [FlotConfig.chartSecond, FlotConfig.chartFourth, "#666", "#BBB"],
                grid : {
                    show : true,
                    hoverable : true,
                    clickable : true,
                    tickColor : FlotConfig.chartBorderColor,
                    borderWidth : 0,
                    borderColor : FlotConfig.chartBorderColor
                },
                legend : true,
                tooltip : true,
                tooltipOpts : {
                    content : "<b>%x</b> = <span>%y</span>",
                    defaultTheme : false
                }
            });
        }
    }
});