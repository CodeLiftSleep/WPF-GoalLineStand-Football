"use strict";

angular.module('app.inbox').factory('InboxConfig', function($http, APP_CONFIG){
    return $http.get(APP_CONFIG.apiRootUrl + '/inbox.json');
})