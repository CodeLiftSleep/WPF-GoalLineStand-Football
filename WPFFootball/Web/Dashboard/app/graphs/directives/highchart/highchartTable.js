'use strict';

angular.module('app.graphs').directive('highchartTable', function (lazyScript) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            lazyScript.register('build/vendor.graphs.js').then(function(){
                element.highchartTable();
            })
        }
    }
});