(function () {
    'use strict';
    angular
    .module('routerApp')
    .controller('mainCtrl', function ($scope, $stateParams) {
        var vm = this;
        vm.model = $stateParams.model;
        console.log(vm.model);
    });
})();