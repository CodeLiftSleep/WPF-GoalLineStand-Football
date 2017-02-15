"use strict";

angular.module('app.inbox').directive('messageLabels', function (InboxConfig) {
    return {
        replace: true,
        restrict: 'AE',
        link: function (scope, element) {

            if (scope.message.labels && scope.message.labels.length) {
                InboxConfig.success(function (config) {
                    var html = _.map(scope.message.labels, function (label) {
                        return '<span class="label bg-color-'+config.labels[label].color +'">' + config.labels[label].name + '</span>';
                    }).join('');
                    element.replaceWith(html);
                });

            } else {
                element.replaceWith('');
            }
        }
    }
});