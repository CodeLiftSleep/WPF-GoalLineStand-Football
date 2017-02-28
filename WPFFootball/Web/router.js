//<!--GLOBAL ANGULAR-->
(function () {
    'use strict';
    angular
        .module('routerApp', ['ui.router', 'formly', 'formlyBootstrap', 'ngAnimate', 'ui.bootstrap', 'ngMessages',
            'ui.grid', 'restangular', 'formly_templates', 'nya.bootstrap.select',
            'rzModule', 'ui.mask', 'angular-3d-carousel', 'ui.grid.autoResize'])

        .service('DB', function ($q) {
            this.load = {
                isLoading: false,
                data: []
            };

            this.setIsLoading = function (value) {
                this.load.isLoading = value;
            };

            this.setData = function (data) {
                this.load.data = data;
            };

            this.getNumEnding = function (number) {
                var defer = $q.defer();
                var num = number.toString();
                var result = '';
                switch (num[num.length - 1]) { //gets the last digit of the number
                    case '1': result = 'st'; break;
                    case '2': result = 'nd'; break;
                    case '3': result = 'rd'; break;
                    default: result = 'th';
                }
                defer.resolve(result);
                return result;
            };
        })

        .service('dataService', ['$timeout', '$q', function ($timeout, $q) {
            //var fs = window.fs;
            var sql = window.SQL;

            define(function (require) {
                //fs = require('fs');
                sql = require('sql');
            });

            this.getData = function () {
                var defer = $q.defer();
                $timeout(function () {
                    var DB = [];
                    //DB.Teams = window.Teams;

                    defer.resolve(DB);
                }, 0);
                return defer.promise;
            };
        }])

    .run(function (DB, dataService) {
        DB.setIsLoading(true),
            dataService.getData(),
        //DB.setData(data);
            DB.setIsLoading(false);
    })

    .controller('loadCtrl', ["$scope", "DB", function loadCtrl($scope, DB) {
        $scope.appState = DB.load;
        //$scope.Teams = DBTeams; // still not getting the Data....
        //console.log(DBTeams);
    }])

    .config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/home');

        $stateProvider

            // HOME STATES AND NESTED VIEWS ========================================
            .state('home', {
                url: '/home',
                templateUrl: 'home.html',
                //resolve: { //attempting to ensure data loads before it finishes getting the page, still not working
                //DBTeams: function () {
                //return window.Teams;
                //}
                //}
            })
            //START GAME--MAIN
            .state('Start', {
                url: '/start',
                templateUrl: 'New_Game_Wizard/new_game.html',
                controller: 'newGameCtrl',
                controllerAs: 'vm'
            })
            //nested Start states--url will be Start/profile
            .state('Start.career', {
                url: '/career',
                pararms: { model: null },
                templateUrl: 'New_Game_Wizard/Start/Start-career.html',
                controller: 'careerCtrl',
                controllerAs: 'vm'
            })

            .state('Start.career2', {
                url: '/career2',
                params: { model: null },
                templateUrl: 'New_Game_Wizard/Start/Start-career2.html',
                controller: 'career2Ctrl',
                controllerAs: 'vm'
            })

            .state('Start.career3', {
                url: '/career3',
                params: { model: null },
                templateUrl: 'New_Game_Wizard/Start/Start-career3.html',
                controller: 'career3Ctrl',
                controllerAs: 'vm'
            })

            //Team Selection screen
            .state('Start.teamSelect', {
                url: '/teamSelect',
                params: { model: null },
                templateUrl: 'New_Game_Wizard/Start/Start-teamSelect.html',
                controller: 'teamSelectCtrl',
                controllerAs: 'vm'
            })

            .state('Start.single', {
                parent: 'Start',
                url: '/single',
                templateUrl: 'New_Game_Wizard/Start/Start-single.html'
            })

            .state('Start.quick', {
                //parent: 'Start',
                url: '/quick',
                templateUrl: 'New_Game_Wizard/Start/Start-quick.html',
                controller: 'quickStartCtrl',
                controllerAs: 'vm'
            })

            .state('Start.situation', {
                parent: 'Start',
                url: '/situation',
                templateUrl: 'New_Game_Wizard/Start/Start-situation.html'
            })

            .state('Load', {
                url: '/load',
                templateUrl: 'test.html',
                controller: 'testCtrl'
            })

            .state('Exit', {
                url: '/exit',
                templateUrl: 'exit_game.html'
            })

            .state('Dashboard', {
                url: '/dashboard',
                params: { model: null },
                templateUrl: 'Dashboard/index.html',
                controller: 'mainCtrl',
                controllerAs: 'vm',
                //lazyLoad: () => System.import('/Dashboard/index.html')
            })

            // ABOUT PAGE AND MULTIPLE NAMED VIEWS =================================
            .state('settings', {
                // we'll get to this in a bit
            })

            .state('menu', {
            });
    }])

    .run(['$rootScope', '$state', '$stateParams',
      function ($rootScope, $state, $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;
      }]);
})();