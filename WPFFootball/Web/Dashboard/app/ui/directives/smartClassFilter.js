'use strict';

angular.module('app.ui').directive('smartClassFilter', function () {
    return {
        restrict: 'A',
        scope: {
            model: '=',
            displayElements: '@',
            filterElements: '@'
        },
        link: function (scope, element) {
            scope.$watch('model', function (model) {
                if (angular.isString(model)) {
                    var search = model.trim();
                    if (search) {
                        angular.element(scope.displayElements, element).hide();

                        angular.element(scope.filterElements, element)
                            .filter(function () {
                                var r = new RegExp(search, 'i');
                                return r.test($(this).attr('class') + $(this).attr('alt'))
                            })
                            .closest(scope.displayElements).show();
                    } else {
                        angular.element(scope.displayElements, element).show();
                    }
                }
            })
        }
    }
});