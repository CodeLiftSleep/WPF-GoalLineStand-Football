'use strict';

angular.module('app.graphs').directive('dygraphsNoRollTimestamp', function (DygraphsDataDemo) {
    return {
        restrict: 'A',
        compile: function () {
            return {
                post: function (scope, element) {
                    new Dygraph(element[0], DygraphsDataDemo.data_temp, {
                        rollPeriod : 14,
                        showRoller : true,
                        customBars : true,
                        ylabel : 'Temperature (F)',
                        legend : 'always',
                        labelsDivStyles : {
                            'textAlign' : 'right'
                        },
                        showRangeSelector : true,
                        rangeSelectorHeight : 30,
                        rangeSelectorPlotStrokeColor : 'yellow',
                        rangeSelectorPlotFillColor : 'lightyellow'
                    });
                }
            }
        }
    }
});