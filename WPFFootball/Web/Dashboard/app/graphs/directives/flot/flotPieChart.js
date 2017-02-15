"use strict";

angular.module('app.graphs').directive('flotPieChart', function(){
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="chart"></div>',
        scope: {
            data: '='
        },
        link: function(scope, element){
            $.plot(element, scope.data, {
                series : {
                    pie : {
                        show : true,
                        innerRadius : 0.5,
                        radius : 1,
                        label : {
                            show : false,
                            radius : 2 / 3,
                            formatter : function(label, series) {
                                return '<div style="font-size:11px;text-align:center;padding:4px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
                            },
                            threshold : 0.1
                        }
                    }
                },
                legend : {
                    show : true,
                    noColumns : 1, // number of colums in legend table
                    labelFormatter : null, // fn: string -> string
                    labelBoxBorderColor : "#000", // border color for the little label boxes
                    container : null, // container (as jQuery object) to put legend in, null means default on top of graph
                    position : "ne", // position of default legend container within plot
                    margin : [5, 10], // distance from grid edge to default legend container within plot
                    backgroundColor : "#efefef", // null means auto-detect
                    backgroundOpacity : 1 // set to 0 to avoid background
                },
                grid : {
                    hoverable : true,
                    clickable : true
                },
            });

        }
    }
});
