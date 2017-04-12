(function () {
    'use strict';
    angular
    .module('routerApp')
    .controller('editorCtrl', function editorCtrl($scope, $state) {
        var vm = this;
        vm.model = {
            id: []
        };
        vm.fields = [
            {
                key: 'sideOfBall',
                type: 'radio',
                wrapper: 'panel',
                templateOptions: {
                    label: 'Side of Ball',
                    inline: true,
                    labelProp: 'value',
                    valueProp: 'id',
                    options: [{ value: 'Offense', id: 'O' }, { value: 'Defense', id: 'D"' }]
                }
            },
            { //offense
                hideExpression: '!model.sideOfBall || model.sideOfBall === "Defense"',
                wrapper: 'panel',
                templateOptions: {
                    label: 'Offensive Situation Editor'
                },
                fieldGroup: [
                    {
                        template: '<b><u>Choose Offensive Philosophies: {{model.offPhilosophy}}</u></b>'
                    },
                    {
                        //className: 'col-md-12',
                        key: 'offPhilosophy',
                        type: 'multiCheck',
                        templateOptions: {
                            //label: 'Offensive Philosophy',
                            options: [{ value: 'Bal Pass', id: 'bp' }, { value: 'Bal Run', id: 'br' }, { value: 'Smashmouth', id: 'sm' }, { value: 'Spread Bal', id: 'spb' }, { value: 'Spread Pass', id: 'spp' }, { value: 'Spread Run', id: 'spr' },
                                { value: 'Vert Pass', id: 'vp' }, { value: 'WC Bal', id: 'wcb' }, { value: 'WC Pass', id: 'wcp' }, { value: 'WC Run', id: 'wcr' }],
                            //description: 'Choose which offensive philosophies to make changes to'
                        }
                    },
                    {
                        template: '<b uib-tooltip="Choose what quarters are included in this situation." tooltip-placement="top-left"><u>Choose Quarter(s): {{model.quarter}}</u></b>'
                    },
                    {
                        className: 'col-md-6',
                        key: 'quarter',
                        type: 'multiCheck',
                        templateOptions: {
                            options: [{ value: 'Q1', id: 'q1' }, { value: 'Q2', id: 'q2' }, { value: 'Q3', id: 'q3' }, { value: 'Q4', id: 'q4' }, { value: 'OT', id: 'q5' }]
                        }
                    },
                    {
                        template: '<b uib-tooltip="Choose what downs are included in this situation." >Choose Down(s): {{model.down}}</b>'
                    },
                    {
                        className: 'col-md-6',
                        key: 'down',
                        type: 'multiCheck',
                        templateOptions: {
                            options: [{ value: 'D1', id: 'd1' }, { value: 'D2', id: 'd2' }, { value: 'D3', id: 'd3' }, { value: 'D4', id: 'd4' }]
                        }
                    },
                    {
                        template: `<b uib-tooltip="Choose the distance needed for a first down. Each one means it is between the one in front of it and the one you are selecting.  For instance, if you choose \'<=2 Yards\', this means a distance of between 1-2 yards." tooltip-placement="top-left">
                        <u>Choose Distances: {{model.distance}}</u></b>`
                    },
                    {
                        key: 'distance',
                        type: 'multiCheck',
                        templateOptions: {
                            options: [
                                { value: '<=.5 Yards', id: 'Y0' }, { value: '<=1 Yards', id: 'Y1' }, { value: '<=2 Yards', id: 'Y2' }, { value: '<=3 Yards', id: 'Y3' }, { value: '<=4 Yards', id: 'Y4' }, { value: '<=5 Yards', id: 'Y5' },
                                { value: '<=6 Yards', id: 'Y6' }, { value: '<=7 Yards', id: 'Y7' }, { value: '<=8 Yards', id: 'Y8' }, { value: '<=9 Yards', id: 'Y9' }, { value: '<=10 Yards', id: 'Y10' }, { value: '<=11 Yards', id: 'Y11' },
                                { value: '<=12 Yards', id: 'Y12' }, { value: '<=13 Yards', id: 'Y13' }, { value: '<=14 Yards', id: 'Y14' }, { value: '<=15 Yards', id: 'Y15' }, { value: '<=16-20 Yards', id: 'Y16' }, { value: '20+ Yards', id: 'Y17' }
                            ]
                        }
                    },
                    {
                        template: '<b uib-tooltip="Select the yardlines you want these options to be valid for" tooltip-placement="top-left"><u>Choose Yardlines: {{model.yardlines}}</u></b>'
                    },
                    {
                        key: 'yardlines',
                        type: 'multiCheck',
                        templateOptions: {
                            options: [{ value: 'Own 0-5', id: 'Own0' }, { value: 'Own 5-10', id: 'Own5' }, { value: 'Own 10-15', id: 'Own10' }, { value: 'Own 15-20', id: 'Own15' }, { value: 'Own 20-30', id: 'Own20' }, { value: 'Own 30-40', id: 'Own30' },
                                { value: 'Own 40-50', id: 'Own40' }, { value: 'Opp 50-40', id: 'Opp40' }, { value: 'Opp 40-30', id: 'Opp30' }, { value: 'Opp 30-20', id: 'Opp20' }, { value: 'Opp 20-15', id: 'Opp15' }, { value: 'Opp 10-15', id: 'Opp10' }, { value: 'Opp 5-10', id: 'Opp5' },
                                { value: 'Opp 0-5', id: 'Opp0' }
                            ]
                        }
                    },
                    {
                        template: '<b uib-tooltip="Select the time range(s) these options should apply to.  Each minute starts at that minute and runs to 1 second before the next minute.  For instance <= 14m means 14:00 to 13:01" tooltip-placement="top-left"><u>Choose Time Remaining: {{model.time}}</u></b>'
                    },
                    {
                        key: 'time',
                        type: 'multiCheck',
                        templateOptions: {
                            options: [{ value: '<= 15m', id: 'a' }, { value: '<= 14m', id: 'b' }, { value: '<= 13m', id: 'c' }, { value: '<= 12m', id: 'd' }, { value: '<= 11m', id: 'e' }, { value: '<= 10m', id: 'f' }, { value: '<= 9m', id: 'g' }, { value: '<= 8m', id: 'b' }, { value: '<= 7m', id: 'c' }, { value: '<= 6m', id: 'd' }, { value: '<= 5m', id: 'e' }, { value: '<= 4m', id: 'f' },
                            { value: '<= 3m', id: 'h' }, { value: '<= 2m', id: 'i' }, { value: ' <= 90s', id: 'j' }, { value: '<= 60s', id: 'k' }, { value: '<= 30s', id: 'l' }, { value: '<= 10s', id: 'm' }
                            ]
                        }
                    },
                    {
                        template: `<div uib-tooltip="Formations go by name and the 'personnel numbering group' is in parenthesis.  The first digit tells you the number of RBs in the formation, the second digit the number of TEs, add them
                        together and subtract from 5 and this is how many WRs are in the formation." tooltip-placement="top"><b><u>Choose the formations: {{model.formation}}</u></b></div>`
                    },
                    {
                        key: 'formation',
                        type: 'multiCheck',
                        templateOptions: {
                            options: [
                                { value: 'Royal(00)', id: 'R00' }, { value: 'Kings(01)', id: 'K01' }, { value: 'Joker(02)', id: 'J02' }, { value: 'Jet(10)', id: 'J10' }, { value: 'Posse(11)', id: 'P11' }, { value: 'Ace(12)', id: 'A12' }, { value: 'Heavy(13)', id: 'H13' },
                                { value: 'Houston(20)', id: 'H20' }, { value: 'Regular(21)', id: 'R21' }, { value: 'Tank(22)', id: 'T22' }, { value: 'Jumbo(23)', id: 'J23' }, { value: 'Wildcat(31)', id: 'W31' }, { value: 'Club(32)', id: 'C32' }
                            ]
                        }
                    },
                    {
                        key: 'runPlaysTotal',
                        template: `<div uib-tooltip="The total of this percentage must add up to 100 combined for ALL run/pass plays" class="row"><b><u>Running Plays(Percentage Weight):</u></b>
                                        <b>{{model.runLE + model.runLT + model.runMid + model.runRT + model.runRE}}<b>
                                   </div>`
                    },
                    {
                        className: 'col-md-12',
                        fieldGroup: [
                            {
                                key: 'runLE',
                                className: 'col-md-2',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Left End'
                                }
                            },
                            {
                                key: 'runLT',
                                className: 'col-md-2',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Left Tackle'
                                }
                            },
                            {
                                key: 'runMid',
                                className: 'col-md-2',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Middle'
                                }
                            },
                            {
                                key: 'runRT',
                                className: 'col-md-2',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Right Tackle'
                                }
                            },
                            {
                                key: 'runRE',
                                className: 'col-md-2',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Right End'
                                }
                            },
                        ]
                    },
                    {
                        key: 'passPlaysTotal',
                        template: `<div class="row" uib-tooltip="The total percentage for runs and passes MUST add up to 100!" tooltip-placement="top-left"><b><u>Passing Plays(Percentage Weight):</u></b>
                                   <b>{{model.passBLOS + model.passSh + model.passMed + model.passLg}}<b>
                                   </div>`
                    },
                    {
                        className: 'col-md-12',
                        fieldGroup: [
                            {
                                className: 'col-md-2',
                                key: 'passBLOS',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Pass Behind LOS'
                                }
                            },
                            {
                                className: 'col-md-2',
                                key: 'passSh',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Pass Short'
                                }
                            },
                            {
                                className: 'col-md-2',
                                key: 'passMed',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Pass Medium'
                                }
                            },
                            {
                                className: 'col-md-2',
                                key: 'passLg',
                                type: 'input',
                                templateOptions: {
                                    type: 'number',
                                    min: 0,
                                    max: 100,
                                    label: 'Pass Long'
                                }
                            }
                        ]
                    },
                    {
                        key: 'totalPlays',
                        template: `<div class="row" ng-class="(model.runLE +model.runLT +model.runMid +model.runRT +model.runRE + model.passBLOS +model.passSh +model.passMed +model.passLg) === 100 ? 'green' : 'red'"><b><u>Total Plays(Percentage Weight) Total Run % + Total Pass % MUST equal 100:</u></b>
                               <b>{{model.runLE +model.runLT +model.runMid +model.runRT +model.runRE + model.passBLOS +model.passSh +model.passMed +model.passLg}}<b>
                                   </div><span class="btn btn-primary btn-lg pull-right" ng-click="Update()">Save Situation(s)</span>`,
                        controller: function ($scope) {
                            $scope.Update = function () { //update the situations. Need to cycle through each item and save them individually to a unique id in the DB if it doesn't exist.
                                var model = $scope.model;

                                angular.forEach(model.offPhilosophy, function (value, key) {
                                    model.op = value === true ? key : model.op;
                                    angular.forEach(model.quarter, function (value, key) {
                                        model.quar = value === true ? key : model.op;
                                        angular.forEach(model.down, function (value, key) {
                                            model.dwn = value === true ? key : model.op;
                                            angular.forEach(model.distance, function (value, key) {
                                                model.dist = value === true ? key : model.op;
                                                angular.forEach(model.yardlines, function (value, key) {
                                                    model.yard = value === true ? key : model.op;
                                                    angular.forEach(model.time, function (value, key) {
                                                        model.tm = value === true ? key : model.op;
                                                        angular.forEach(model.formation, function (value, key) {
                                                            model.form = value === true ? key : model.op;
                                                            model.id.push(model.op + model.quar + model.dwn + model.dist + model.yard + model.tm + model.form);
                                                        });
                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                                //We have the id's to update, loop through them and add them/update the DB
                                angular.forEach(model.id, function (value) {
                                });

                                console.log(model.id);
                            };
                        }
                    }
                ]
            }
        ]
    })
})();