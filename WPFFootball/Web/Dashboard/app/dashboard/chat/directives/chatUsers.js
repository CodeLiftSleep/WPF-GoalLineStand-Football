"use strict";

angular.module('app.chat').directive('chatUsers', function(ChatApi){
    return {
        restrict: 'E',
        replace: true,
        templateUrl: 'app/dashboard/chat/directives/chat-users.tpl.html',
        scope: true,
        link: function(scope, element){
            scope.open = false;
            scope.openToggle = function(){
                scope.open = !scope.open;
            };

            scope.chatUserFilter = '';

            ChatApi.initialized.then(function () {
                scope.chatUsers = ChatApi.users;
            });
        }
    }
});
