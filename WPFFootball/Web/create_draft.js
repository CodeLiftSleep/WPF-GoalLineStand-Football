(function () {
    'use strict'
    angular
    .module('routerApp')
    .controller('draftCtrl', function draftCtrl() {
        var vm = this;
        var draftClass;

        vm.CreateDraftClass = function () {
            draftClass = window.CreateDraft.CreatePlayers(3000);
        };
    });
})();