'use strict';

angular.module('app.ui').directive('smartJquiMenu', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {

            element.menu();
        }
    }
});