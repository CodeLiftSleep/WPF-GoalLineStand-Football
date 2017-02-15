'use strict';

angular.module('app.calendar').controller('CalendarCtrl', function ($scope, $log, CalendarEvent) {


    // Events scope
    $scope.events = [];

    // Unassigned events scope
    $scope.eventsExternal = [
        {
            title: "Office Meeting",
            description: "Currently busy",
            className: "bg-color-darken txt-color-white",
            icon: "fa-time"
        },
        {
            title: "Lunch Break",
            description: "No Description",
            className: "bg-color-blue txt-color-white",
            icon: "fa-pie"
        },
        {
            title: "URGENT",
            description: "urgent tasks",
            className: "bg-color-red txt-color-white",
            icon: "fa-alert"
        }
    ];


    // Queriing our events from CalendarEvent resource...
    // Scope update will automatically update the calendar
    CalendarEvent.query().$promise.then(function (events) {
        $scope.events = events;
    });


    $scope.newEvent = {};

    $scope.addEvent = function() {

        $log.log("Adding new event:", $scope.newEvent);

        var newEventDefaults = {
            title: "Untitled Event",
            description: "no description",
            className: "bg-color-darken txt-color-white",
            icon: "fa-info"
        };


        $scope.newEvent = angular.extend(newEventDefaults, $scope.newEvent);

        $scope.eventsExternal.unshift($scope.newEvent);

        $scope.newEvent = {};

        // $log.log("New events now:", $scope.eventsExternal);

    };


});
