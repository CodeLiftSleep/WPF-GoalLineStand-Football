"use strict";

angular.module('app.widgets', ['ui.router'])


.config(function ($stateProvider) {

    $stateProvider
        .state('app.widgets', {
            url: '/widgets',
            data: {
                title: 'Widgets'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/widgets/views/widgets-demo.html'

                }
            }

        })

});
