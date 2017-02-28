(function () {
    'use strict';
    angular
    .module('routerApp')
    .controller('quickStartCtrl', function quickStartCtrl($scope, $stateParams) {
        var vm = this;
        window.GamePlay.StartGame(Math.floor(Math.random() * (32 - 1 + 1) + 1), Math.floor(Math.random() * (32 - 1 + 1) + 1));
    });
})()