"use strict";

angular.module('app.graphs').directive('morrisLineGraphC', function(){
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="chart no-padding"></div>',
        link: function(scope, element){

            var day_data = [{
                "elapsed" : "I",
                "value" : 34
            }, {
                "elapsed" : "II",
                "value" : 24
            }, {
                "elapsed" : "III",
                "value" : 3
            }, {
                "elapsed" : "IV",
                "value" : 12
            }, {
                "elapsed" : "V",
                "value" : 13
            }, {
                "elapsed" : "VI",
                "value" : 22
            }, {
                "elapsed" : "VII",
                "value" : 5
            }, {
                "elapsed" : "VIII",
                "value" : 26
            }, {
                "elapsed" : "IX",
                "value" : 12
            }, {
                "elapsed" : "X",
                "value" : 19
            }];
            Morris.Line({
                element : element,
                data : day_data,
                xkey : 'elapsed',
                ykeys : ['value'],
                labels : ['value'],
                parseTime : false
            });

        }
    }
});