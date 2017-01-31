(function () {
    'use strict';
angular
.module('routerApp')
.controller('career2Ctrl', function career2Ctrl($scope, $stateParams) {

var vm = this;
vm.model = $stateParams.model;
console.log(vm.model);

vm.careerExpansion = [
    {
        className: 'display-flex settings expansion',
        //hideExpression: '!model.AllowExpansion || model.AllowExpansion === "No"',
        fieldGroup: [
            {
                key: 'Expansion Fee',
                className: 'flex-1',
                type: 'input',
                defaultValue: '200,000,000',
                templateOptions: {
                    label: 'Expansion Fee',
                    type: 'text',         
                }
            },
            {
                key: 'MinMetroPop',
                className: 'flex-1',
                type: 'input',
                defaultValue: '1,000,000',
                templateOptions: {
                    label: 'Minimum Metro Population',
                    type: 'text',
                }
            },
            {
                key: 'MultTeamsLgMkt',
                className: 'flex-1',
                defaultValue: 'No',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Allow Multiple Teams in Large Markets?',
                    toggle: 'MultTeams',
                    checked: true
                }, 
            },
            {
                key: 'AllowIntTeams',
                className: 'flex-1',
                defaultValue: 'Yes',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Allow International Teams?',
                    toggle: 'AllowInt',
                    checked: false
                },
            },
            {
                key: 'OwnerAppExp',
                className: 'flex-1',
                type: 'vertSlider',
                defaultValue: 75,
                templateOptions: {
                    label: 'Owner Approval % Required',
                    sliderOptions: {
                        value: 100,
                        floor: 0,
                        ceil: 100,
                        vertical: false,
                        showTicks: false,
                        translate: function(value) {
                            return value + '%';
                        }
                    }
                }
            }
        ]
    }
];

vm.careerRelocation = [
    {
        className: 'display-flex settings relocation',
        //hideExpression: '!model.AllowRelocation || model.AllowRelocation === "No"',
        fieldGroup: [
            {
                key: 'RelocationFee',
                className: 'flex-1',
                type: 'input',
                defaultValue: '200,000,000',
                templateOptions: {
                    label: 'Relocation Fee',
                    type: 'text'
                }
            },
            {
                key: 'RelMinMetroPop',
                className: 'flex-1',
                type: 'input',
                defaultValue: '1,000,000',
                templateOptions: {
                    label: 'Minimum Metro Population',
                    type: 'text'
                }
            },
            {
                key: 'AllowIntRel',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Allow International Relocation?',
                    toggle: 'intRel',
                    checked: false
                }
            },
            {
                key: 'RequireOwnerApp',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Require Owner Approval?',
                    toggle: 'reqOwnApp',
                    checked: true
                }
            },
            {
                key: 'PercentToRel',
                className: 'flex-1',
                type: 'vertSlider',
                hideExpression: '!model.RequireOwnerApp',
                defaultValue: 75,
                templateOptions: {
                    label: 'Owner Approval % Required',
                    sliderOptions: {
                        floor: 0,
                        value: 75,
                        ceil: 100,
                        vertical: false,
                        showTicks: false,
                        translate: function(value) {
                            return value + '%';
                        }
                    }
                }
            }
        ]
    }
];

vm.careerColDraft = [
    {
        className: 'display-flex settings',
        //hideExpression: '!model.AllowCollegeDraft',
        fieldGroup: [
            {
                key: 'DraftRounds',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: '7',
                templateOptions: {
                    label: 'Draft Rounds',
                    options: [
                        {value: '7', id: 'DR7'},
                        {value: '3', id: 'DR3'},
                        {value: '4', id: 'DR4'},
                        {value: '5', id: 'DR5'},
                        {value: '6', id: 'DR6'},
                        {value: '8', id: 'DR8'},
                        {value: '9', id: 'DR9'},
                        {value: '10', id: 'DR10'},
                        {value: '11', id: 'DR11'},
                        {value: '12', id: 'DR12'},
                        {value: '13', id: 'DR13'},
                        {value: '14', id: 'DR14'}
                    ]
                }
            },
            {
                key: 'DraftLottery',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Draft Lottery for 1st pick',
                    toggle: 'DraftLot',
                    checked: false
                }
            },
            {
                key: 'NumTeamsLottery',
                className: 'flex-1',
                hideExpression: '!model.DraftLottery',
                type: 'nya-bootstrapSelect',
                defaultValue: '16',
                templateOptions: {
                    label: '# of Teams in Draft Lottery',
                    options: [
                        {value: '16', id: 'DLot16'},
                        {value: '2', id: 'DLot2'},
                        {value: '3', id: 'DLot3'},
                        {value: '4', id: 'DLot4'},
                        {value: '5', id: 'DLot5'},
                        {value: '6', id: 'DLot6'},
                        {value: '7', id: 'DLot7'},
                        {value: '8', id: 'DLot8'},
                        {value: '9', id: 'DLot9'},
                        {value: '10', id: 'DLot10'},
                        {value: '11', id: 'DLot11'},
                        {value: '12', id: 'DLot12'},
                        {value: '13', id: 'DLot13'},
                        {value: '14', id: 'DLot14'},
                        {value: '15', id: 'Dlot15'}
                    ]
                }
            },
            {
                key: 'ColDraftType',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: 'Normal',
                templateOptions: {
                    label: 'Draft Type',
                    options: [
                        {value: 'Normal', id: 'ColDRN'},
                        {value: 'Snake', id: 'ColDRS'},
                        {value: 'Random', id: 'ColDRR'}
                    ]
                }
            },
            {
                key: 'SupplementalDraft',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Have Supplemental Draft?',
                    toggle: 'SupDraft',
                    checked: true
                }
            },
            {
                key: 'CompensDraftPicks',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Give Compensatory Draft Picks?',
                    toggle: 'CompDraft',
                    checked: true
                }
            },
            {
                key: 'DraftPickTrades',
                className: 'flex-1',
                type: 'toggleSwitch', 
                templateOptions: {
                    label: 'Allow Draft Picks to be traded',
                    toggle: 'PickTrade',
                    checked: true
                }
            }
        ]
    }
];

vm.careerFinancials = [
    {
        className: 'display-flex settings finance',
        hideExpression: '!model.UseFinancials',
        fieldGroup: [
            {
                key: 'SalaryCapType',
                className: 'flex-1',
                type: 'nya-bootstrapSelect',
                defaultValue: 'Hard Cap',
                templateOptions: {
                    label: 'Salary Cap Type',
                    options: [
                        {value: 'Hard Cap', id: 'SCTHC'},
                        {value: 'Soft Cap', id: 'SCTSC'},
                        {value: 'Luxury Tax', id: 'SCTLT'},
                        {value: 'Soft Cap and Luxury Tax', id: 'SCTSL'}
                    ]
                }
            },
            {
                key: 'VarCap',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Cap based on revenues?',
                    toggle: 'capRev',
                    checked: true
                }
            },
            {
                key: 'FixedCap',
                className: 'flex-1',
                type: 'input',
                defaultValue: '155,000,000',
                hideExpression: '!model.VapCap',
                templateOptions: {
                    label: 'Salary Cap Amount',
                    type: 'text'
                }
            },
            {
                key: 'CapCarryOver',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Cap CarryOver?',
                    toggle: 'CapCO',
                    checked: true
                }
            },
            {
                key: 'UseRookiePool',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Rookie Cap Pool?',
                    toggle: 'RookieCP',
                    checked: true
                }
            },
            { //float values are not working for soem reason even though they do in examples
                key: 'RookiePoolAmount',
                className: 'flex-1',
                type: 'vertSlider',
                defaultValue: 6.00,
                hideExpression: '!model.UseRookiePool',
                templateOptions: {
                    label: 'Rookie Pool % of Cap',
                    sliderOptions: {
                    value: 6.00,
                    floor: 0.00,
                    ceil: 10.00,
                    step: 0.25,
                    precision: 2,
                    vertical: false,
                    showTicks: false,
                    translate: function(value) {
                            return value + '%';
                        }
                    }
         
                    
                }
            },
            {
                key: 'SalaryFloor',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Use Salary Floor?',
                    toggle: 'salFloor',
                    checked: true
                }
            },
            {
                key: 'SalFloorPer',
                className: 'flex-1',
                defaultValue: 89,
                hideExpression: '!model.SalaryFloor',
                type: 'vertSlider',
                templateOptions: {
                    label: 'Floor as Percent of Cap',
                    sliderOptions: {
                        value: 89,
                        floor: 50,
                        ceil: 98,
                        vertical: false,
                        showTicks: false
                    }
                }
            },
            {
                key: 'LuxuryTaxPer',
                hideExpression: function($viewValue, $modelValue, $scope) {
                    if($scope.model.SalaryCapType === 'Luxury Tax' || $scope.model.SalaryCapType === 'Soft Cap and Luxury Tax') {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                className: 'flex-1',
                type: 'vertSlider',
                defaultValue: 100,
                templateOptions: {
                    label: '% of cap when tax starts',
                    sliderOptions: {
                        value: 100,
                        floor: 101,
                        ceil: 200,
                        vertical: false,
                        showTicks: false
                    }
                }
            },
            {
                key: 'LuxuryTaxAmt',
                className: 'flex-1',
                hideExpression: '!model.LuxuryTaxPer',
                type: 'input',
                templateOptions: {
                    type: 'numeric',
                    label: 'Amount per dollar over',
                    placeholder: '1.50',

                }
            },
        ]
    },

    {
        template: '<div class="row" style="padding-bottom: 10px;"/>'
    },
    {
        className: 'display-flex settings',
        fieldGroup: [
            {
                key: 'FranchiseTag',
                type: 'toggleSwitch',
                className: 'flex-1',
                templateOptions: {
                    label: 'Use Franchise Tag?',
                    toggle: 'FranTag',
                    checked: true
                }
            },
            {
                key: 'NonExclFranTag',
                type: 'toggleSwitch',
                hideExpression: '!model.FranchiseTag',
                className: 'flex-1',
                templateOptions: {
                    label: 'Have Non-Excl. Franchise Tag?',
                    toggle: 'NonExcFranTag',
                    checked: true
                }
            },
            {
                key: 'FranTagBasedWhatPer',
                className: 'flex-1',
                defaultValue: 5,
                hideExpression: '!model.FranchiseTag',
                type: 'vertSlider',
                templateOptions: {
                    label: 'Based on Top % at position',
                    sliderOptions: {
                        value: 5,
                        floor: 1,
                        ceil: 15,
                        step: 0.25,
                        precision: 2
                    }
                }
            },
            {
                key: 'FranMinRaise',
                className: 'flex-1',
                defaultValue: 120,
                hideExpression: '!model.FranchiseTag',
                type: 'vertSlider',
                templateOptions: {
                    label: 'Min. Over Last Year Salary',
                    sliderOptions: {
                        value: 120,
                        floor: 100,
                        ceil: 150,
                        translate: function(value) {
                            return value + '%';
                        }
                    }
                }
            },
            {
                key: 'FranYearsPlayer',
                hideExpression: '!model.FranchiseTag',
                className: 'flex-1',
                defaultValue: 2,
                type: 'vertSlider',
                templateOptions: {
                    label: '# Years Can Tag Same Player',
                    sliderOptions: {
                        value: 2,
                        floor: 1,
                        ceil: 6
                    }
                }
            },
            {
                key: 'TransitionTag',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Use Transition Tag?',
                    toggle: 'TransTag',
                    checked: true
                }
            },
            {
                key: 'TransTagPerc',
                className: 'flex-1',
                defaultValue: 10,
                hideExpression: '!model.TransitionTag',
                type: 'vertSlider',
                templateOptions: {
                    label: 'Based on Top % at position',
                    sliderOptions: {
                        value: 10,
                        floor: 1,
                        ceil: 20,
                        step: 0.25,
                        precision: 2
                    }
                }
            },
            {
                key: 'TeamFirstRef',
                className: 'flex-1',
                hideExpression: '!model.TransitionTag',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Team has right of 1st refusal?',
                    toggle: 'TransFR',
                    checked: true
                }
            }
        ]
    }
];
});
})();