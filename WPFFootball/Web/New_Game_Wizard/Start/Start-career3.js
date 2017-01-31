(function () {
    'use strict';
angular
.module('routerApp')
.controller('career3Ctrl', function career3Ctrl($scope, $stateParams) {
    
var vm = this;
vm.model = $stateParams.model;
console.log(vm.model);

vm.careerOwner = [
    {
        className: 'display-flex settings owner',
        fieldGroup: [
            {
                key: 'AvgTicketPrice',
                className: 'col-md-2',
                defaultValue: '85.00',
                type: 'input',
                templateOptions: {
                    label: 'Average Ticket Price',
                    type: 'text',
                }
            },
            {
                key: 'AvgLuxuryBoxPrice',
                className: 'flex-1',
                defaultValue: '35,000',
                type: 'input',
                templateOptions: {
                    label: 'Ave Luxury Box Price',
                    type: 'text'
                }
            },
            {
                key: 'HomeTeamGateRev',
                className: 'flex-1',
                type: 'vertSlider',
                templateOptions: {
                    label: 'Home Team Gate Revenue %',
                    sliderOptions: {
                        value: 60,
                        floor: 0,
                        ceil: 100
                    }
                }
            },
            {
                key: 'ShareLuxBoxRev',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Share Luxury Box Rev?',
                    toggle: 'LuxBox',
                    checked: false
                }
            },
            {
                key: 'ShareMerchRev',
                className: 'flex-1',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Share Merchandise Revenue?',
                    toggle: 'MerchRev',
                    checked: true
                }
            }
        ]
    }
];

vm.careerPlayerCon = [
    {
        className: 'display-flex playerContract',
        fieldGroup: [
            {
                key: 'YearsInLeague',
                defaultValue: 'Rookie',
                className: 'col-md-2',
                type: 'nya-bootstrapSelect',
                templateOptions: {
                    label: 'Years Experience',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: 'Rookie', id: 'RookieYIL'},
                        {value: '1st Year', id: '1YIL'},
                        {value: '2nd Year', id: '2YIL'},
                        {value: '3rd Year', id: '3YIL'},
                        {value: '4th Year', id: '4YIL'},
                        {value: '5th Year', id: '5YIL'},
                        {value: '6th Year', id: '6YIL'},
                        {value: '7th Year', id: '7YIL'},
                        {value: '8th Year', id: '8YIL'},
                        {value: '9th Year', id: '9YIL'},
                        {value: '10th Year', id: '10YIL'},
                        {value: '11th Year+', id: '11YIL'}
                    ]
                }

            },
            {
                key: 'RookMinSalary',
                defaultValue: 450000,
                className: 'col-md-2',
                hideExpression: 'model.YearsInLeague != "Rookie"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Rookie Min Salary'
                }
            },
            {
                key: '1stYearSalary',
                className: 'col-md-2',
                defaultValue: 565000,
                hideExpression: 'model.YearsInLeague != "1st Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '1st Year Min Salary'
                }
            },
            {
                key: '2ndYearSalary',
                className: 'col-md-2',
                defaultValue: 600000,
                hideExpression: 'model.YearsInLeague != "2nd Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '2nd Year Min Salary'
                }
            },
            {
                key: '3rdYearSalary',
                className: 'col-md-2',
                defaultValue: 675000,
                hideExpression: 'model.YearsInLeague != "3rd Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '3rd Year Min Salary'
                }
            },
            {
                key: '4thYearSalary',
                className: 'col-md-2',
                defaultValue: 760000,
                hideExpression: 'model.YearsInLeague != "4th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '4th Year Min Salary'
                }
            },
            {
                key: '5thYearSalary',
                className: 'col-md-2',
                defaultValue: 760000,
                hideExpression: 'model.YearsInLeague != "5th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '5th Year Min Salary'
                }
            },
                {
                key: '6thYearSalary',
                className: 'col-md-2',
                defaultValue: 760000,
                hideExpression: 'model.YearsInLeague != "6th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '6th Year Min Salary'
                }
            },
            {
                key: '7thYearSalary',
                className: 'col-md-2',
                defaultValue: 885000,
                hideExpression: 'model.YearsInLeague != "7th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '7th Year Min Salary'
                }
            },
                {
                key: '8thYearSalary',
                className: 'col-md-2',
                defaultValue: 885000,
                hideExpression: 'model.YearsInLeague != "8th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '8th Year Min Salary'
                }
            },
            {
                key: '9thYearSalary',
                className: 'col-md-2',
                defaultValue: 885000,
                hideExpression: 'model.YearsInLeague != "9th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '9th Year Min Salary'
                }
            },
            {
                key: '10thYearSalary',
                className: 'col-md-2',
                defaultValue: 985000,
                hideExpression: 'model.YearsInLeague != "10th Year"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '10th Year Min Salary'
                }
            },
            {
                key: '11thYearSalary',
                className: 'col-md-2',
                defaultValue: 985000,
                hideExpression: 'model.YearsInLeague != "11th Year+"',
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '11th Year+ Min Salary'
                }
            },
            {
                key: 'VetMinAtLowerSal',
                className: 'col-md-2',
                type: 'toggleSwitch',
                templateOptions: {
                    label: 'Veteran has lower cap #?',
                    toggle: 'VetMin',
                    checked: true
                }
            },
            {
                key: 'VetYearsToBeEligible',
                className: 'col-md-2',
                hideExpression: '!model.VetMinAtLowerSal',
                type: 'nya-bootstrapSelect',
                defaultValue: 6,
                templateOptions: {
                    label: '# of years to be elgible for vet min',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: 6, id: '6VY'},
                        {value: 1, id: '1VY'},
                        {value: 2, id: '2VY'},
                        {value: 3, id: '3VY'},
                        {value: 4, id: '4VY'},
                        {value: 5, id: '5VY'},
                        {value: 7, id: '7VY'},
                        {value: 8, id: '8VY'},
                        {value: 9, id: '9VY'},
                        {value: 10, id: '10VY'},
                        {value: 11, id: '11VY'}
                    ]
                }
            },
            {
                key: 'VetMinBecomes',
                className: 'col-md-2',
                hideExpression: '!model.VetMinAtLowerSal',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    label: 'How Much Vet Min Counts vs. Cap',
                    type: 'number'
                }
            },
        ]
    },
    {
        className: 'display-flex',
        fieldGroup: [
            {
                key: 'PositionContract',
                className: 'col-md-2',
                defaultValue: 'QB',
                type: 'nya-bootstrapSelect',
                templateOptions: {
                    label: 'Contract for:',
                    labelProp: 'value',
                    valueProp: 'value',
                    options: [
                        {value: 'QB', id: 'QBPC'},
                        {value: 'RB', id: 'RBPC'},
                        {value: 'FB', id: 'FBPC'},
                        {value: 'WR', id: 'WRPC'},
                        {value: 'TE', id: 'TEPC'},
                        {value: 'LT', id: 'LTPC'},
                        {value: 'G', id: 'GPC'},
                        {value: 'C', id: 'CPC'},
                        {value: 'RT', id: 'RTPC'},
                        {value: 'DE', id: 'DEPC'},
                        {value: 'DT', id: 'DTPC'},
                        {value: 'OLB', id: 'OLBPC'},
                        {value: 'ILB', id: 'ILBPC'},
                        {value: 'CB', id: 'CBPB'},
                        {value: 'FS', id: 'FSPC'},
                        {value: 'SS', id: 'SSPC'},
                        {value: 'K', id: 'KPC'},
                        {value: 'P', id: 'PPC'},
                        {value: 'LS', id: 'LSPC'}
                    ]
                }
            },
            {
                key: 'QBFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "QB"',
                defaultValue: 22000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise QB Salary'
                }
            },
            {
                key: 'QBVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "QB"',
                defaultValue: 18000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good QB Salary'
                }
            },
            {
                key: 'QBGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "QB"',
                defaultValue: 14000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good QB Salary'
                }
            },
            {
                key: 'QBAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "QB"',
                defaultValue: 10000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average QB Salary'
                }
            },
            {
                key: 'QBBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "QB"',
                defaultValue: 6500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup QB Salary'
                }
            },
            {
                key: 'QB3rdSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "QB"',
                defaultValue: 1000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '3rd QB Salary'
                }
            },
            
            {
                key: 'RBFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RB"',
                defaultValue: 10000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise RB Salary'
                }
            },
            {
                key: 'RBVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RB"',
                defaultValue: 7000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good RB Salary'
                }
            },
            {
                key: 'RBGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RB"',
                defaultValue: 4500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good RB Salary'
                }
            },
            {
                key: 'RBAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RB"',
                defaultValue: 3000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average RB Salary'
                }
            },
            {
                key: 'RBBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RB"',
                defaultValue: 1500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup RB Salary'
                }
            },
            {
                key: 'RBDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RB"',
                defaultValue: 850000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth RB Salary'
                }
            },
            {
                key: 'FBFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FB"',
                defaultValue: 2500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise FB Salary'
                }
            },
            {
                key: 'FBVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FB"',
                defaultValue: 1650000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good FB Salary'
                }
            },
            {
                key: 'FBGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FB"',
                defaultValue: 750000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good FB Salary'
                }
            },
            {
                key: 'FBAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FB"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average FB Salary'
                }
            },
            {
                key: 'FBBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FB"',
                defaultValue: 500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup FB Salary'
                }
            },
            {
                key: 'FBDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FB"',
                defaultValue: 450000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth FB Salary'
                }
            },
            {
                key: 'WRFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "WR"',
                defaultValue: 15000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise WR Salary'
                }
            },
            {
                key: 'WRVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "WR"',
                defaultValue: 11000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good WR Salary'
                }
            },
            {
                key: 'WRGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "WR"',
                defaultValue: 8500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good WR Salary'
                }
            },
            {
                key: 'WRAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "WR"',
                defaultValue: 5000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average WR Salary'
                }
            },
            {
                key: 'WRBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "WR"',
                defaultValue: 2500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup WR Salary'
                }
            },
            {
                key: 'WRDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "WR"',
                defaultValue: 1250000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: '3rd WR Salary'
                }
            },
            {
                key: 'TEFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "TE"',
                defaultValue: 10000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise TE Salary'
                }
            },
            {
                key: 'TEVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "TE"',
                defaultValue: 7500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good TE Salary'
                }
            },
            {
                key: 'TEGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "TE"',
                defaultValue: 5500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good TE Salary'
                }
            },
            {
                key: 'TEAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "TE"',
                defaultValue: 3500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average TE Salary'
                }
            },
            {
                key: 'TEBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "TE"',
                defaultValue: 2000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup TE Salary'
                }
            },
            {
                key: 'TEDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "TE"',
                defaultValue: 840000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth TE Salary'
                }
            },
            {
                key: 'LTFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LT"',
                defaultValue: 13000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise LT Salary'
                }
            },
            {
                key: 'LTVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LT"',
                defaultValue: 10000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good LT Salary'
                }
            },
            {
                key: 'LTGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LT"',
                defaultValue: 7500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good LT Salary'
                }
            },
            {
                key: 'LTAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LT"',
                defaultValue: 3500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average LT Salary'
                }
            },
            {
                key: 'LTBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LT"',
                defaultValue: 1500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup LT Salary'
                }
            },
            {
                key: 'LTDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LT"',
                defaultValue: 550000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth LT Salary'
                }
            },
            {
                key: 'CFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "C"',
                defaultValue: 9400000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise C Salary'
                }
            },
            {
                key: 'CVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "C"',
                defaultValue: 6500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good C Salary'
                }
            },
            {
                key: 'CGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "C"',
                defaultValue: 2800000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good C Salary'
                }
            },
            {
                key: 'CAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "C"',
                defaultValue: 1500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average C Salary'
                }
            },
            {
                key: 'CBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "C"',
                defaultValue: 700000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup C Salary'
                }
            },
            {
                key: 'CDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "C"',
                defaultValue: 550000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth C Salary'
                }
            },
            {
                key: 'GFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "G"',
                defaultValue: 11700000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise G Salary'
                }
            },
            {
                key: 'GVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "G"',
                defaultValue: 7500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good G Salary'
                }
            },
            {
                key: 'GGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "G"',
                defaultValue: 4850000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good G Salary'
                }
            },
            {
                key: 'GAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "G"',
                defaultValue: 2750000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average G Salary'
                }
            },
            {
                key: 'GBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "G"',
                defaultValue: 1650000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup G Salary'
                }
            },
            {
                key: 'GDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "G"',
                defaultValue: 1500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth G Salary'
                }
            },
            {
                key: 'RTFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RT"',
                defaultValue: 7000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise RT Salary'
                }
            },
            {
                key: 'RTVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RT"',
                defaultValue: 5500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good RT Salary'
                }
            },
            {
                key: 'RTGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RT"',
                defaultValue: 3500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good RT Salary'
                }
            },
            {
                key: 'RTAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RT"',
                defaultValue: 2100000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average RT Salary'
                }
            },
            {
                key: 'RTBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RT"',
                defaultValue: 700000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup RT Salary'
                }
            },
            {
                key: 'RTDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "RT"',
                defaultValue: 1500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth RT Salary'
                }
            },
            {
                key: 'DEFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DE"',
                defaultValue: 17000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise DE Salary'
                }
            },
            {
                key: 'DEVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DE"',
                defaultValue: 10000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good DE Salary'
                }
            },
            {
                key: 'DEGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DE"',
                defaultValue: 7500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good DE Salary'
                }
            },
            {
                key: 'DEAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DE"',
                defaultValue: 5000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average DE Salary'
                }
            },
            {
                key: 'DEBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DE"',
                defaultValue: 2000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup DE Salary'
                }
            },
            {
                key: 'DEDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DE"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth DE Salary'
                }
            },
            {
                key: 'DTFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DT"',
                defaultValue: 19000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise DT Salary'
                }
            },
            {
                key: 'DTVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DT"',
                defaultValue: 10000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good DT Salary'
                }
            },
            {
                key: 'DTGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DT"',
                defaultValue: 4500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good DT Salary'
                }
            },
            {
                key: 'DTAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DT"',
                defaultValue: 3000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average DT Salary'
                }
            },
            {
                key: 'DTBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DT"',
                defaultValue: 1675000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup DT Salary'
                }
            },
            {
                key: 'DTDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "DT"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth DT Salary'
                }
            },
            {
                key: 'OLBFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "OLB"',
                defaultValue: 19000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise OLB Salary'
                }
            },
            {
                key: 'OLBVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "OLB"',
                defaultValue: 9000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good OLB Salary'
                }
            },
            {
                key: 'OLBGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "OLB"',
                defaultValue: 6500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good OLB Salary'
                }
            },
            {
                key: 'OLBAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "OLB"',
                defaultValue: 4750000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average OLB Salary'
                }
            },
            {
                key: 'OLBBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "OLB"',
                defaultValue: 2000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup OLB Salary'
                }
            },
            {
                key: 'OLBDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "OLB"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth OLB Salary'
                }
            },
            {
                key: 'ILBFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "ILB"',
                defaultValue: 13000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise ILB Salary'
                }
            },
            {
                key: 'ILBVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "ILB"',
                defaultValue: 7250000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good ILB Salary'
                }
            },
            {
                key: 'ILBGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "ILB"',
                defaultValue: 4000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good ILB Salary'
                }
            },
            {
                key: 'ILBAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "ILB"',
                defaultValue: 2250000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average ILB Salary'
                }
            },
            {
                key: 'ILBBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "ILB"',
                defaultValue: 1250000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup ILB Salary'
                }
            },
            {
                key: 'ILBDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "ILB"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth ILB Salary'
                }
            },
            {
                key: 'CBFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "CB"',
                defaultValue: 15000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise CB Salary'
                }
            },
            {
                key: 'CBVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "CB"',
                defaultValue: 10275000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good CB Salary'
                }
            },
            {
                key: 'CBGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "CB"',
                defaultValue: 5750000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good CB Salary'
                }
            },
            {
                key: 'CBAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "CB"',
                defaultValue: 2500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average CB Salary'
                }
            },
            {
                key: 'CBBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "CB"',
                defaultValue: 1500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup CB Salary'
                }
            },
            {
                key: 'CBDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "CB"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth CB Salary'
                }
            },
            {
                key: 'FSFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FS"',
                defaultValue: 12500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise FS Salary'
                }
            },
            {
                key: 'FSVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FS"',
                defaultValue: 7000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good FS Salary'
                }
            },
            {
                key: 'FSGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FS"',
                defaultValue: 3000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good FS Salary'
                }
            },
            {
                key: 'FSAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FS"',
                defaultValue: 2000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average FS Salary'
                }
            },
            {
                key: 'FSBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FS"',
                defaultValue: 1000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup FS Salary'
                }
            },
            {
                key: 'FSDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "FS"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth FS Salary'
                }
            },
            {
                key: 'SSFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "SS"',
                defaultValue: 7000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise SS Salary'
                }
            },
            {
                key: 'SSVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "SS"',
                defaultValue: 5000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good SS Salary'
                }
            },
            {
                key: 'SSGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "SS"',
                defaultValue: 1750000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good SS Salary'
                }
            },
            {
                key: 'SSAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "SS"',
                defaultValue: 900000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average SS Salary'
                }
            },
            {
                key: 'SSBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "SS"',
                defaultValue: 675000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup SS Salary'
                }
            },
            {
                key: 'SSDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "SS"',
                defaultValue: 550000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth SS Salary'
                }
            },
            {
                key: 'KFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "K"',
                defaultValue: 4300000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise K Salary'
                }
            },
            {
                key: 'KVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "K"',
                defaultValue: 3000000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good K Salary'
                }
            },
            {
                key: 'KGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "K"',
                defaultValue: 900000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good K Salary'
                }
            },
            {
                key: 'KAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "K"',
                defaultValue: 540000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average K Salary'
                }
            },
            {
                key: 'KBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "K"',
                defaultValue: 500000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup K Salary'
                }
            },
            {
                key: 'KDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "K"',
                defaultValue: 450000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth K Salary'
                }
            },
            {
                key: 'PFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "P"',
                defaultValue: 3750000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise P Salary'
                }
            },
            {
                key: 'PVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "P"',
                defaultValue: 2900000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good P Salary'
                }
            },
            {
                key: 'PGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "P"',
                defaultValue: 1700000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good P Salary'
                }
            },
            {
                key: 'PAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "P"',
                defaultValue: 600000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average P Salary'
                }
            },
            {
                key: 'PBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "P"',
                defaultValue: 525000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup P Salary'
                }
            },
            {
                key: 'PDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "P"',
                defaultValue: 450000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth P Salary'
                }
            },
            {
                key: 'LSFranSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LS"',
                defaultValue: 1150000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Franchise LS Salary'
                }
            },
            {
                key: 'LSVeryGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LS"',
                defaultValue: 1065000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Very Good LS Salary'
                }
            },
            {
                key: 'LSGoodSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LS"',
                defaultValue: 885000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Good LS Salary'
                }
            },
            {
                key: 'LSAverageSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LS"',
                defaultValue: 620000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Average LS Salary'
                }
            },
            {
                key: 'LSBackupSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "LS"',
                defaultValue: 480000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Backup LS Salary'
                }
            },
            {
                key: 'LSDepthSal',
                className: 'col-md-2',
                hideExpression: 'model.PositionContract != "Ls"',
                defaultValue: 450000,
                type: 'input',
                templateOptions: {
                    type: 'number',
                    label: 'Depth LS Salary'
                }
            },
        ]
    }
    
];
});
})();