'use strict';

angular.module('app.ui').directive('smartJquiAutocomplete', function () {
    return {
        restrict: 'A',
        scope: {
            'source': '='
        },
        link: function (scope, element, attributes) {

            element.autocomplete({
                source: scope.source
            });
        }
    }
});