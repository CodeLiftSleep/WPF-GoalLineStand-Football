"use strict";

angular.module('app.graphs').directive('flotFillChart', function(){
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="chart"></div>',
        scope: {
            data: '='
        },
        link: function(scope, element){

            $.plot(element, scope.data, {

                xaxis : {
                    tickDecimals : 0
                },

                yaxis : {
                    tickFormatter : function(v) {
                        return v + " cm";
                    }
                }

            });
        }
    }
})