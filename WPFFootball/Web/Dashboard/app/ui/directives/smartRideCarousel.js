'use strict';

angular.module('app.ui').directive('smartRideCarousel', function () {
    return {
        restrict: 'A',
        compile: function (tElement, tAttributes) {
            tElement.removeAttr('smart-ride-carousel data-smart-ride-carousel');
            tElement.carousel(tElement.data());
        }
    }
});