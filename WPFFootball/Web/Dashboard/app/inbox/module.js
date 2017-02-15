'use strict';

angular.module('app.inbox', [
    'ui.router',
    'ngResource'
])
.config(function ($stateProvider) {
    
    $stateProvider
        .state('app.inbox', {
            url: '/inbox',
            data: {
                title: 'Inbox'
            },
            views: {
                content: {
                    templateUrl: 'app/inbox/views/inbox-layout.html',
                    controller: function ($scope, config) {
                        $scope.config = config.data;
                        $scope.deleteSelected = function(){
                            $scope.$broadcast('$inboxDeleteMessages')
                        }
                    },
                    controllerAs: 'inboxCtrl',
                    resolve: {
                        config: function (InboxConfig) {
                            return InboxConfig;
                        }
                    }
                }
            }

        })
        .state('app.inbox.folder', {
            url: '/:folder',
            views: {
                inbox: {
                    templateUrl: 'app/inbox/views/inbox-folder.html',
                    controller: function ($scope, messages, $stateParams) {
                        $scope.$parent.selectedFolder = _.find($scope.$parent.config.folders, {key: $stateParams.folder});
                        $scope.messages = messages;

                        $scope.$on('$inboxDeleteMessages', function(event){
                            angular.forEach($scope.messages, function(message, idx){
                                if(message.selected){
                                    message.$delete(function(){
                                        $scope.messages.splice(idx, 1);
                                    })
                                }
                            });
                        });

                    },
                    resolve: {
                        messages: function (InboxMessage, $stateParams) {
                            return InboxMessage.query({folder: $stateParams.folder});
                        }
                    }
                }
            }
        })
        .state('app.inbox.folder.detail', {
            url: '/detail/:message',
            views: {
                "inbox@app.inbox": {
                    templateUrl: 'app/inbox/views/inbox-detail.html',
                    controller: function ($scope, message) {
                        $scope.message = message;
                    },
                    resolve: {
                        message: function (InboxMessage, $stateParams) {
                            return InboxMessage.get({id: $stateParams.message})
                        }
                    }
                }
            }
        })
        .state('app.inbox.folder.replay', {
            url: '/replay/:message',
            views: {
                "inbox@app.inbox": {
                    templateUrl: 'app/inbox/views/inbox-replay.html',
                    controller: function ($scope, $timeout, $state, replayTo) {
                        $scope.replayTo = replayTo;
                        $scope.sending = false;
                        $scope.send = function(){
                            $scope.sending = true;
                            $timeout(function(){
                                $state.go('app.inbox.folder')
                            }, 1000);
                        }
                    },
                    controllerAs: 'replayCtrl',
                    resolve: {
                        replayTo: function (InboxMessage, $stateParams) {
                            return InboxMessage.get({id: $stateParams.message})
                        }
                    }
                }
            }
        })
        .state('app.inbox.folder.compose', {
            url: '/compose',
            views: {
                "inbox@app.inbox": {
                    templateUrl: 'app/inbox/views/inbox-compose.html',
                    controller: function ($scope, $timeout, $state) {
                        $scope.sending = false;
                        $scope.send = function(){
                            $scope.sending = true;
                            $timeout(function(){
                                $state.go('app.inbox.folder')
                            }, 1000);
                        }
                    },
                    controllerAs: 'composeCtrl'
                }
            }
        });
});