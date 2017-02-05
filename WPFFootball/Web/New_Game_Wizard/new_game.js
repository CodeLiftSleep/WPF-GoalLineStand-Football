(function () {
    'use strict';
    angular
        .module('routerApp')
        .controller('newGameCtrl', function newGameCtrl($scope, $state) {
            var vm = this;
            vm.model = {};

            var myTeam = JSON.parse(window.Lookup.GetTeam('Carolina'));
            var getGrades = JSON.parse(window.Lookup.GetDraftPlayers(7.75));
            var getOwner = JSON.parse(window.Lookup.GetOwner('Carolina'));
            var getDraftPlayer = JSON.parse(window.Lookup.GetDraftPlayer(299));
            var getDraftCollege = JSON.parse(window.Lookup.GetDraftPlayers('Clemson'));
            var getDraftColAndPos = JSON.parse(window.Lookup.GetDraftPlayers('Alabama', 'RB'));
            var getDraftRegion = JSON.parse(window.Lookup.GetDraftRegion('South'));
            var getDraftRegionAndPos = JSON.parse(window.Lookup.GetDraftRegion('South', 'DE'))
            var getDraftPosition = JSON.parse(window.Lookup.GetDraftPosition("QB"));
            var getDraftRange = JSON.parse(window.Lookup.GetDraftPlayers(6.95, 7.43));
            var getPersonByAttr = JSON.parse(window.Lookup.GetPersonByAttr('PersonnelDT', 'Vigilant', 75, 90))

            //var Draft =  JSON.parse(window.Draft.GetDraftClass()).splice(3000, 2999);

            console.log("Team:");
            console.log(myTeam);
            console.log("Grade over 7.75:");
            console.log(getGrades);
            console.log("Owner:");
            console.log(getOwner);
            console.log("Player by DraftId:");
            console.log(getDraftPlayer);
            console.log("Players by Draft Position:");
            console.log(getDraftPosition);
            console.log("Players by Draft College:");
            console.log(getDraftCollege);
            console.log("Players by Draft College AND Position:");
            console.log(getDraftColAndPos);
            console.log("Players by Draft Region:");
            console.log(getDraftRegion);
            console.log("Players by Draft RegionAndPos:");
            console.log(getDraftRegionAndPos);
            console.log("Players with Draft Grade between 6.95 and 7.43");
            console.log(getDraftRange);
            console.log("Personnel People with Vigilance attribute between 75 and 90");
            console.log(getPersonByAttr);
            vm.fields = [
                {
                    className: 'newGame animated bounceInRight',
                    key: 'selectLeagueType',
                    wrapper: 'panel',
                    type: 'leagueType',
                    templateOptions: {
                        label: 'League Creation'
                    },
                    controller: function ($scope) { //update panel titles for the next menu here based on what button is clicked
                        $scope.Update = function () {
                            //removes the enter animation and adds the exit animation
                            angular.element('.newGame').removeClass('bounceInRight').addClass('bounceOutLeft');
                        };
                    },
                }
            ];
        });
})();