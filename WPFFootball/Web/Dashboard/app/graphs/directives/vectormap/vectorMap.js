'use strict';

angular.module('app.graphs').directive('vectorMap', function () {
    return {
        restrict: 'EA',
        scope: {
            mapData: '='
        },
        link: function (scope, element, attributes) {
            var data = scope.mapData;

            element.vectorMap({
                map: 'world_mill_en',
                backgroundColor: '#fff',
                regionStyle: {
                    initial: {
                        fill: '#c4c4c4'
                    },
                    hover: {
                        "fill-opacity": 1
                    }
                },
                series: {
                    regions: [
                        {
                            values: data,
                            scale: ['#85a8b6', '#4d7686'],
                            normalizeFunction: 'polynomial'
                        }
                    ]
                },
                onRegionLabelShow: function (e, el, code) {
                    if (typeof data[code] == 'undefined') {
                        e.preventDefault();
                    } else {
                        var countrylbl = data[code];
                        el.html(el.html() + ': ' + countrylbl + ' visits');
                    }
                }
            });

            element.on('$destroy', function(){
                element.children('.jvectormap-container').data('mapObject').remove();
            })
        }
    }
});