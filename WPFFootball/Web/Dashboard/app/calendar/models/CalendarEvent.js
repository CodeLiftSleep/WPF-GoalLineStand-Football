
"use strict";

angular.module('app.calendar').factory('CalendarEvent', function($resource, APP_CONFIG){
    return $resource( APP_CONFIG.apiRootUrl + '/events.json', {_id:'@id'})
});