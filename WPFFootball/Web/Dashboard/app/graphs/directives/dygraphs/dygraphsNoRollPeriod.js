'use strict';

angular.module('app.graphs').directive('dygraphsNoRollPeriod', function (DygraphsDataDemo) {
    return {
        restrict: 'A',
        compile: function () {
            return {
                post: function (scope, element) {
                    new Dygraph(element[0], DygraphsDataDemo.data_temp, {
                        customBars: true,
                        title: 'Daily Temperatures in New York vs. San Francisco',
                        ylabel: 'Temperature (F)',
                        legend: 'always',
                        labelsDivStyles: {
                            'textAlign': 'right'
                        },
                        showRangeSelector: true
                    });
                }
            }
        }
    }
});