"use strict";

angular.module('app.graphs').directive('morrisLineGraphB', function(){
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="chart no-padding"></div>',
        link: function(scope, element){

            var day_data = [{
                "period" : "2012-10-01",
                "licensed" : 3407
            }, {
                "period" : "2012-09-30",
                "sorned" : 0
            }, {
                "period" : "2012-09-29",
                "sorned" : 618
            }, {
                "period" : "2012-09-20",
                "licensed" : 3246,
                "sorned" : 661
            }, {
                "period" : "2012-09-19",
                "licensed" : 3257,
                "sorned" : null
            }, {
                "period" : "2012-09-18",
                "licensed" : 3248,
                "other" : 1000
            }, {
                "period" : "2012-09-17",
                "sorned" : 0
            }, {
                "period" : "2012-09-16",
                "sorned" : 0
            }, {
                "period" : "2012-09-15",
                "licensed" : 3201,
                "sorned" : 656
            }, {
                "period" : "2012-09-10",
                "licensed" : 3215
            }];
            Morris.Line({
                element : element,
                data : day_data,
                xkey : 'period',
                ykeys : ['licensed', 'sorned', 'other'],
                labels : ['Licensed', 'SORN', 'Other'],
                /* custom label formatting with `xLabelFormat` */
                xLabelFormat : function(d) {
                    return (d.getMonth() + 1) + '/' + d.getDate() + '/' + d.getFullYear();
                },
                /* setting `xLabels` is recommended when using xLabelFormat */
                xLabels : 'day'
            });

        }
    }
});