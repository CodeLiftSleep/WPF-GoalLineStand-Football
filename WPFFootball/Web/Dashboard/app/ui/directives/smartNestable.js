'use strict';

angular.module('app.ui').directive('smartNestable', function () {
    return {
        restrict: 'A',
        scope: {
            group: '@',
            output: '='
        },
        link: function (scope, element, attributes) {
            var options = {};
            if(scope.group){
                options.group = scope.group;
            }
            element.nestable(options);
            if(attributes.output){
                element.on('change', function(){
                    scope.$apply(function(){
                        scope.output = element.nestable('serialize');
                    });
                });
                scope.output = element.nestable('serialize');
            }

        }
    }
});