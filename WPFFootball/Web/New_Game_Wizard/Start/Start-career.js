(function () {
    'use strict';
angular
.module('routerApp')
.controller('careerCtrl', function careerCtrl($scope, $stateParams) {

var vm = this;
vm.model = $stateParams.model;
console.log(vm.model);

vm.careerLeague = [
    {
        className: 'display-flex settings',
        fieldGroup: [
        {
            className: 'flex-1 leagueSettings',
            key: 'StartYear',
            type: 'input',
            defaultValue: 2016,
            templateOptions: {
            label: 'Start Year',
            type: 'numeric'
        }
        },
        {
            className: 'flex-1 animate-show',
            key: 'LeagueRules',
            type: 'nya-bootstrapSelect',
            defaultValue: 'NFL',
            templateOptions: {
                label: 'League Rules',
                labelProp: 'value',
                valueProp: 'value',
                options: [
                    {value: 'NFL', id: 'NFL'},
                    {value: 'CFL', id: 'CFL'},
                    {value: 'College', id: 'College'},
                    {value: 'Arena', id: 'Arena'}
                ]
            }
        },
        {
            className: 'flex-1 leagueSettings animate-show',
            key: 'FieldType',
            type: 'nya-bootstrapSelect',
            defaultValue: 'Modern NFL',
            templateOptions: {
                label: 'Field Type',
                labelProp: 'value',
                valueProp: 'value',
                options: [
                    {value: 'Modern NFL', id: 'FTMN'},
                    {value: 'Old NFL', id: 'FTON'},
                    {value: 'CFL', id: 'FTCFL'},
                    {value: 'College', id: 'FTCol'},
                    {value: 'Arena', id: 'FTAr'}
                ]
            }             
        },
               
        {
           //wrapper: 'panel',
            className: 'flex-1',
            key: 'NumTeams',
            type: 'input',
            defaultValue: 32,
            templateOptions: {
                label: 'Number of Teams',
                type: 'numeric',
                placeholder: 'Between 8-48 teams',
                min: 8,
                max: 48
            }
        },
        {
            className: 'flex-1',
            key: 'NumConf',
            type: 'nya-bootstrapSelect',
            defaultValue: '2',
            templateOptions: {
                label: 'Number of Conferences',
                labelProp: 'value',
                valueProp: 'value',
                options: [
                    {value: '2', id: 'conf2'},
                    {value: '0', id: 'conf0'},
                    {value: '1', id: 'conf1'},
                    {value: '3', id: 'conf3'},
                    {value: '4', id: 'conf4'},
                    {value: '5', id: 'conf5'},
                    {value: '6', id: 'conf6'}
                ]
            }
        },
        {
            className: 'flex-1',
            key: 'NumDiv',
            type: 'nya-bootstrapSelect',
            defaultValue: '8',
            templateOptions: {
                label: 'Number of Divisions',
                labelProp: 'value',
                valueProp: 'value',
                options: [
                    {value: '8', id: 'div8'},
                    {value: '0', id: 'div0'},
                    {value: '1', id: 'div1'},
                    {value: '2', id: 'div2'},
                    {value: '3', id: 'div3'},
                    {value: '4', id: 'div4'},
                    {value: '5', id: 'div5'},
                    {value: '6', id: 'div6'},
                    {value: '7', id: 'div7'},
                    {value: '9', id: 'div9'},
                    {value: '10', id: 'div10'},
                    {value: '11', id: 'div11'},
                    {value: '12', id: 'div12'},
                ]
            }
        }]
    },

        {
            className: 'display-flex settings',
            fieldGroup: [
                {
                    template: '<div class="row" style="padding-bottom: 40px;"/>'
                },
                {
                    key: 'StartFantDraft',
                    type: 'toggleSwitch',
                    className: 'col-md-2 pull-right',
                    templateOptions: {
                        label: 'Start with Fantasy Draft?',
                        toggle: 'Fant',
                        checked: false,
                    },
                    controller: function($scope) {
                        $scope.Update = function(ischecked) {
                        vm.model.FantDraftType = ischecked ? vm.model.FantDraftType : undefined; //removes type of fantasy draft if its unchecked
                        };
                    }
                },
                {
                    key: 'FantDraftType',
                    className: 'col-md-2 pull-left',
                    type: 'nya-bootstrapSelect',
                    defaultValue: 'Normal',
                    hideExpression: '!model.StartFantDraft || model.StartFantDraft === "No"',                    
                    templateOptions: {
                        label: 'Choose Draft Type',
                        labelProp: 'value',
                        valueProp: 'value',
                        options: [
                            {value: 'Normal', id: 'FDNorm'},
                            {value: 'Snake', id: 'FDSnake'},
                            {value: 'Random', id: 'FDRand'}
                        ]
                    }
                }]
        
        }
];
vm.careerGameday = [
    {
        className: 'display-flex settings',
        fieldGroup: [
            {
                key: 'RosterSize',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: '53',
                templateOptions: {
                    label: 'Regular Season Roster Size',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: '53', id: 'RS53'},
                        {value: '43', id: 'RS43'},
                        {value: '44', id: 'RS44'},
                        {value: '45', id: 'RS45'},
                        {value: '46', id: 'RS46'},
                        {value: '47', id: 'RS47'},
                        {value: '48', id: 'RS48'},
                        {value: '49', id: 'RS49'},
                        {value: '50', id: 'RS50'},
                        {value: '51', id: 'RS51'},
                        {value: '52', id: 'RS52'},
                        {value: '54', id: 'RS54'},
                        {value: '55', id: 'RS55'},
                        {value: '56', id: 'RS56'},
                        {value: '57', id: 'RS57'},
                        {value: '58', id: 'RS58'},
                        {value: '59', id: 'RS59'},
                        {value: '60', id: 'RS60'},
                        {value: '61', id: 'RS61'},
                        {value: '62', id: 'RS62'},
                        {value: '63', id: 'RS63'}
                    ]
                }
            },
            {
                key: 'Inactives',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: '7',
                templateOptions: {
                    label: 'Gameday Inactives',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: '7', id: 'GI7'},
                        {value: '0', id: 'GI0'},
                        {value: '1', id: 'GI1'},
                        {value: '2', id: 'GI2'},
                        {value: '3', id: 'GI3'},
                        {value: '4', id: 'GI4'},
                        {value: '5', id: 'GI5'},
                        {value: '6', id: 'GI6'},
                        {value: '8', id: 'GI8'},
                        {value: '9', id: 'GI9'},
                        {value: '10', id: 'GI10'},
                        {value: '11', id: 'GI11'},
                        {value: '12', id: 'GI12'},
                        {value: '13', id: 'GI13'},
                        {value: '14', id: 'GI14'},
                        {value: '15', id: 'GI15'}                       
                    ]
                }
            },
            {
                key: 'PracticeSquad',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: '8',
                templateOptions: {
                    label: 'Practice Squad Size',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                    {value: '8', id: 'PS8'},                     
                    {value: '0', id: 'PS0'},
                    {value: '1', id: 'PS1'},
                    {value: '2', id: 'PS2'},
                    {value: '3', id: 'PS3'},
                    {value: '4', id: 'PS4'},
                    {value: '5', id: 'PS5'},
                    {value: '6', id: 'PS6'},
                    {value: '7', id: 'PS7'},
                    {value: '9', id: 'PS9'},
                    {value: '10', id: 'PS10'},
                    {value: '11', id: 'PS11'},
                    {value: '12', id: 'PS12'},
                    ]          
                }
            },
            {
                key: 'Penalties',
                className: 'flex-1',
                type: 'vertSlider',
                defaultValue: 100,
                templateOptions: {
                    label: 'Penalty level',
                    sliderOptions: {
                        value: 100,
                        floor: 0,
                        ceil: 200,
                        vertical: false,
                        showTicks: false
                    }
                }
            },
            {
                key: 'QuarterLen',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: '15',
                templateOptions: {
                    label: 'Quarter Length',
                    options: [
                        {value: '15', id: 'QL15'},
                        {value: '5', id: 'QL5'},
                        {value: '6', id: 'QL6'},
                        {value: '7', id: 'QL7'},
                        {value: '8', id: 'QL8'},
                        {value: '9', id: 'QL9'},
                        {value: '10', id: 'QL10'},
                        {value: '11', id: 'QL11'},
                        {value: '12', id: 'QL12'},
                        {value: '13', id: 'QL13'}, 
                        {value: '14', id: 'QL14'},
                        {value: '16', id: 'QL16'},
                        {value: '17', id: 'QL17'},
                        {value: '18', id: 'QL18'},
                        {value: '19', id: 'QL19'},
                        {value: '20', id: 'QL20'}
                    ]
                }
            },
            {
                key: 'OTFormat',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: 'NFL',
                templateOptions: {
                    label: 'OT Format',
                    options: [
                        {value: 'NFL', id: 'OTNFL'},
                        {value: 'CFL', id: 'OTCFL'},
                        {value: 'College', id: 'OTCol'},
                        {value: 'Sudden Death', id: 'OTSud'},
                        {value: 'None', id: 'OTNone'}
                    ]
                }
            },
            {
                key: 'OTLength',
                className: 'flex-1',
                hideExpression: 'model.OTFormat.value === "None"',
                type: 'nya-bootstrapSelect',
                defaultValue: 'Quarter Length',
                templateOptions: {
                    label: 'OT Length',
                    options: [
                        {value: 'Quarter Length', id: 'OTLQL'},
                        {value: 'Possessions', id: 'OTLPos'},
                        {value: '5', id: 'OTL5'},
                        {value: '6', id: 'OTL6'},
                        {value: '7', id: 'OTL7'},
                        {value: '8', id: 'OTL8'},
                        {value: '9', id: 'OTL9'},
                        {value: '10', id: 'OTL10'},
                        {value: '11', id: 'OTL11'},
                        {value: '12', id: 'OTL12'},
                        {value: '13', id: 'OTL13'},
                        {value: '14', id: 'OTL14'}
                    ]
                }
            }
        ]
    }
];
vm.careerSchedule = [
    {
        className: 'display-flex settings',
        fieldGroup: [
            {
                className: 'flex-1',
                key: 'regSeasonGames',
                type: 'input',
                defaultValue: 16,
                templateOptions: {
                    label: 'Regular Season Games',
                    type: 'numeric',
                    placeholder: 'Between 8-20 Games',
                    min: 8,
                    max: 20
                }
            },
            {
                className: 'flex-1',
                key: 'preSeasonGames',
                type: 'nya-bootstrapSelect',
                defaultValue: '4',
                templateOptions: {
                    label: 'Pre-Season Games',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: '4', id: 'preSeas4'},
                        {value: '0', id: 'preSeas0'},
                        {value: '1', id: 'preSeas1'},
                        {value: '2', id: 'preSeas2'},
                        {value: '3', id: 'preSeas3'},
                        {value: '5', id: 'preSeas5'},
                        {value: '6', id: 'preSeas6'}
                    ]
                }
            },
            {
                className: 'flex-1',
                key: 'numByesPerTeam',
                type: 'nya-bootstrapSelect',
                defaultValue: '1',
                templateOptions: {
                    label: 'Byes Per Team',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: '1', id: 'byes1'},
                        {value: '0', id: 'byes0'},
                        {value: '2', id: 'byes2'},
                        {value: '3', id: 'byes3'}
                    ]

                }
            },
            {
                className: 'flex-1',
                key: 'playoffTeamsConf',
                type: 'nya-bootstrapSelect',
                defaultValue: '6',
                templateOptions: {
                    label: 'Playoff Teams per Conf',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: '6', id: 'play6'},
                        {value: '4', id: 'play4'},
                        {value: '5', id: 'play5'},
                        {value: '7', id: 'play7'},
                        {value: '8', id: 'play8'},
                        {value: '9', id: 'play9'},
                        {value: '10', id: 'play10'},
                        {value: '11', id: 'play11'},
                        {value: '12', id: 'play12'}
                    ]
                }
            },
            {
                className: 'flex-1',
                key: 'SchedType',
                type: 'nya-bootstrapSelect',
                defaultValue: 'NFL',
                templateOptions: {
                    label: 'Schedule Type',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: 'NFL', id: 'schedNFL'},
                        {value: 'Heavy Divisional', id: 'schedHD'},
                        {value: 'In Conference Only', id: 'schedICO'},
                        {value: 'Split', id: 'schedSplit'},
                        {value: 'Random', id: 'schedRand'}
                    ]
                }
            },
            {
                className: 'flex-1',
                key: 'seasonStart',
                type: 'nya-bootstrapSelect',
                defaultValue: 'September',
                templateOptions: {
                    label: 'Season Start Month',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: 'September', id: 'Sep'},
                        {value: 'January', id: 'Jan'},
                        {value: 'February', id: 'Feb'},
                        {value: 'March', id: 'Mar'},
                        {value: 'April', id: 'Apr'},
                        {value: 'May', id: 'May'},
                        {value: 'June', id: 'Jun'},
                        {value: 'July', id: 'Jul'},
                        {value: 'August', id: 'Aug'},
                        {value: 'October', id: 'Oct'},
                        {value: 'November', id: 'Nov'},
                        {value: 'December', id: 'Dec'}
                    ]
                }
            }
        ]
    },
    {
        template: '<div class="row">'
    },
    {
        className: 'display-flex settings',
        fieldGroup: [
            {        
                className: 'col-md-2 pull-right',
                key: 'schedIntGames',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Schedule International Games?',
                    checked: true            
                },
                controller: function($scope) {
                    $scope.Update = function(ischecked) {
                    $scope.model.IntGamesSeason = ischecked ? $scope.model.IntGamesSeason : undefined; //removes Internation Games if its unchecked
                    };
                }
            },
            {
            className: 'col-md-2 pull-left',
            key: 'IntGamesSeason',
            hideExpression: '!model.schedIntGames || model.schedIntGames === "No"',
            type: 'nya-bootstrapSelect',
            defaultValue: '6',
            templateOptions: {
                label: 'International Games per Year',
                labelProp: 'value',
                valueProp: 'value',
                options: [
                    {value: '6', id: 'int6'},
                    {value: '0', id: 'int0'},
                    {value: '1', id: 'int1'},
                    {value: '2', id: 'int2'},
                    {value: '3', id: 'int3'},
                    {value: '4', id: 'int4'},
                    {value: '5', id: 'int5'},
                    {value: '7', id: 'int7'},
                    {value: '8', id: 'int8'},
                    {value: '9', id: 'int9'},
                    {value: '10', id: 'int10'},
                    {value: '11', id: 'int11'},
                    {value: '12', id: 'int12'},
                ]    
            }
        }
        ]
    },

];

vm.careerUser = [
{
    className: 'display-flex settings',
    fieldGroup: [
  
        {
            key: 'UserFired',
            className: 'flex-1',
            type: 'toggleSwitch',
            templateOptions: {
                label: 'Can user be fired?',
                toggle: 'Fired',
                checked: true
            },
        },
        {
            key: 'ReqRooney',
            className: 'flex-1',
            type: 'toggleSwitch',
            hideExpression: '!model.UserFired',
            templateOptions: {
               label: 'Require Rooney Rule?',
               toggle: 'Rooney',
               checked: true
            }
        },
        {
            key: 'CoachContractGuar',
            className: 'flex-2',
            type: 'toggleSwitch',
            hideExpression: '!model.UserFired',
            templateOptions: {
                label: 'Coaches Contracts Guaranteed?',
                toggle:'CoachGuar',
                checked: true
            },
        },
        {
            key: 'AllowExpansion',
            className: 'flex-1',
            type: 'toggleSwitch',
            templateOptions: {
                label: 'Allow Expansion?',
                toggle: 'Exp',
                checked: true
            }
        },
        {
            key: 'AllowRelocation',
            className: 'flex-1',
            type: 'toggleSwitch',
            templateOptions: {
                label: 'Allow Relocation?',
                toggle: 'Rel',
                checked: true
            },
        },
        {
            key: 'UseFinancials',
            className: 'flex-1',
            type: 'toggleSwitch',
            templateOptions: {
                label: 'Use Financials?',
                toggle: 'Fin',
                checked: true
            },
        },
        {
            key: 'AllowFA',
            className: 'flex-1',
            type: 'toggleSwitch',
            templateOptions: {
                label: 'Allow Free Agency?',
                toggle: 'FA',
                checked: true
            },
        },
        {
            key: 'AllowCollegeDraft',
            className:'flex-1',
            type: 'toggleSwitch',
            templateOptions: {
                label: 'Allow College Draft?',
                toggle: 'ColDraft',
                checked: true
            }
        }
    ]
}
];

});
})();