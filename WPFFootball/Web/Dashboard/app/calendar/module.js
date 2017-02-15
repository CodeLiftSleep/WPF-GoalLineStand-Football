

"use strict";


angular
.module('app.calendar', ['ngResource','ui.router'])
.config(function ($stateProvider) {

    $stateProvider
        .state('app.calendar', {
            url: '/calendar',
            views: {
                content: {
                    templateUrl: 'app/calendar/views/calendar.tpl.html'
                }
            },
            data:{
                title: 'Calendar'
            }
        });
});


