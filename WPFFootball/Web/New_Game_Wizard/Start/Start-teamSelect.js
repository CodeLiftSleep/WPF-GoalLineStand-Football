(function () {
'use strict';

angular
.module('routerApp')
.controller('teamSelectCtrl', ['$scope', '$stateParams', '$uibModal', 'DB', 'uiGridConstants',
function teamSelectCtrl ($scope, $stateParams, $uibModal, DB, uiGridConstants) {

    //DB is the database service object holding all the tables
    var vm = this;
    $scope.teamSelected = 'Arizona Cardinals'; //starts out as default team
    $scope.teamId = 29; //default ID for Arizona
    vm.model = $stateParams.model; //retrieves the model passed in by the state
    
    //vm.model.teamSelected = vm.teamSelected;
    //vm.model.teamId = vm.teamId;
    
//opens the roster of this team, loaded via sqlite;
    vm.ViewRoster = function(teamSelected, teamId) {
        var roster = [];

        var modalInstance = $uibModal.open({
            backdrop: 'static',
            template: `     
                            <div class="modal-header" ng-style="teamPri">
                                <h3 class="modal-title" id="modal-title">{{teamSelected}} Roster</h3>
                            </div>
                            <div class="modal-body" id="modal-body" ng-style="teamSec">
                                <div id="grid1" ui-grid="gridOptions" class="grid"></div>
                            </div>
                            <div class="modal-footer" ng-style="teamTer">
                                <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
                            </div>
                       `,
            controller: function ($scope, $uibModalInstance, $interval, $filter) {
                //var grid;
                $scope.teamSelected = teamSelected;
                $scope.team = $filter('filter')(DB.load.data.Teams, {TeamID: teamId}, true)[0];
                $scope.teamPri = {'background-color': $scope.team.MainColor, 'color': $scope.team.TrimColor};
                $scope.teamSec = {'background-color': $scope.team.SecondaryColor};
                $scope.teamTer = {'background-color': $scope.team.TrimColor};

                $scope.gridOptions = {
                    onRegisterApi: function(gridApi) {
                        $scope.gridApi = gridApi;
                        
                        $interval(function () {
                            $scope.gridApi.core.handleWindowResize();
                        }, 500, 10);
                    },
                    enableSorting: true,
                    columnDefs: [
                        {name: 'FName', displayName: 'First Name', width: '15%'},
                        {name: 'LName', displayName: 'Last Name', width: '15%'},
                        {name: 'College', width: '15%'},
                        {name: 'Age', width: '6%'},
                        {name: 'Height', width: '6%'},
                        {name: 'Weight', width: '7%'},
                        {name: 'ArmLength', displayName: 'Arm Length', width: '7%'},
                        {name: 'HandSize', displayName: 'Hand Size', width: '7%'},
                        {name: 'Pos', width: '6%'},
                        {name: 'PosType', displayName: 'Position Type', width: '15%'}
                    ]
                };
                $scope.ok = function () {
                    roster = undefined;
                    $scope.team = undefined;
                    $uibModalInstance.close();
                };          
                $scope.gridOptions.data = roster;
            },
            resolve: {
               roster: function() {
                   return angular.forEach(DB.load.data.RosterPlayers, function(value, key) {
                    if(value.TeamID === teamId) 
                        roster.push({FName: value.FName, LName: value.LName, College: value.College, Age: value.Age,
                        Height: value.Height, Weight: value.Weight, ArmLength: value. ArmLength, HandSize: value.HandLength,
                        Pos: value.Pos, PosType: value.PosType});                
                });
               },
            
            },
            size: 'lg'  
        });

        modalInstance.result.then(function() {
            $scope.team = undefined;
        });
    };
    vm.TeamInfo = function(teamSelected, teamId) {    
        var coaches = [];
        var personnel = [];  
           
        var modalInstance = $uibModal.open({
            template: `<div class="modal-header" ng-style="teamPri">
                          <h3 class="modal-title" id="modal-title">
                          {{teamSelected}} Team Information:  {{record}}  <span style="margin-left: 10px;">{{team.DivStanding}}{{teamPlace}} in the {{team.ConfName}} {{team.DivName}}</span></h3>
                          <span style="margin-left: 10px;"><b>Offense(Rank):</b> {{teamOffYds | number: 2}} yds/gm({{teamOffRank}})  <b>Pass:</b> {{passOffYds | number: 2}} yds/gm
                          yds/gm({{passOffRank}}) <b>Rush:</b> {{rushOffYds | number: 2}} yds/gm({{rushOffRank}}) <b>Pts:</b> {{teamPointsOff | number: 2}} pts/gm({{pointsOffRank}}) 
                          <span style="margin-left: 10px;"><b>Defense(Rank):</b> {{teamDefYds | number: 2}} yds/gm({{teamDefRank}})  <b>Pass:</b> 
                          {{passDefYds | number: 2}} yds/gm({{passDefRank}})  <b>Rush:</b> {{rushDefYds | number: 2}} yds/gm({{rushDefRank}})   <b>Pts: </b>{{teamPointsDef | number: 2}}
                          pts/gm({{pointsDefRank}})</span></span>
                       </div>
                        <div class="modal-body" id="modal-body" ng-style="teamSec">
                            <uib-tabset active="active">
                                <uib-tab index="0" heading="Team">
                                <div class="row">
                                    <div class="col-md-3">                                    
                                        <div><img style="height: 250px; width: 400px;" src="{{team.StadiumPic}}" alt=""/></div>
                                    </div>
                                    <div class="col-md-3 pull-left">
                                        <p ng-style="textColor" style="font-size: 20px;"> {{team.StadiumName}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;"> {{team.City}}, {{team.State}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Capacity: {{team.StadiumCapacity}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Attendance: {{(team.AvgAttendance/team.StadiumCapacity)*100 | number: 2}}% </p>
                                        <hr/>
                                        <p ng-style="textColor" style="font-size: 20px;">Upcoming Draft</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Positon: {{team.DraftPosition}}{{draftPos}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Biggest Need: QB</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Deepest Position: CB</p>
                                    </div>
                                    <div class="col-md-3 pull-left">
                                        <p ng-style="textColor" style="font-size: 20px;">Financials</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Salary Cap: \${{salaryCap | number}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Remaining: \${{maxCap - salaryCap | number}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Dead Cap: \${{deadCap | number}}</p>
                                        <hr/>
                                        <p ng-style="textColor" style="font-size: 20px;">Revenue</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Team Value: \${{teamValue | number}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Team Income: \${{revenue | number}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">Avg Ticket Price: \${{avgTicket | number: 2}}</p>                                    
                                    </div>
                                    <div class="col-md-3 pull-left">
                                        <p ng-style="textColor" style="font-size: 20px;">3 Highest Paid Players</p>
                                        <p ng-style="textColor" style="font-size: 16px;">{{highPlay1}} \${{highPlay1Sal | number}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">{{highPlay2}}: \${{highPlay2Sal | number}}</p>
                                        <p ng-style="textColor" style="font-size: 16px;">{{highPlay3}}: \${{highPlay3Sal | number}}</p>
                                        <hr/>
                                        <p ng-style="textColor" style="font-size: 20px;">3 Highest Rated Players</p>
                                        <p ng-style="textColor" style="font-size: 16px;">{{highPlay4}} {{highPlay4Rat | number}} Overall Rating</p>
                                        <p ng-style="textColor" style="font-size: 16px;">{{highPlay5}}: {{highPlay5Rat | number}} Overall Rating</p>
                                        <p ng-style="textColor" style="font-size: 16px;">{{highPlay6}}: {{highPlay6Rat | number}} Overall Rating</p>                                   
                                    </div>
                                </div>                                    
                                    </uib-tab>
                                    <uib-tab index="1" heading="Coaches" >
                                    <div class="row">
                                        <div class="col-md-12 pull-left">
                                            <div id="grid1" ui-grid="gridOptions1" class="grid"></div>
                                        </div>
                                    </div>
                                    </uib-tab>
                                    <uib-tab index="2" heading="Front Office" >
                                        <div class="row">
                                            <div class="col-md-12 pull-left">
                                                <div id="grid2" ui-grid="gridOptions2" class="grid"></div>
                                            </div>
                                        </div>
                                    </uib-tab>
                                </uib-tabset>
                                
                            </div>
                            <div class="modal-footer" ng-style="teamTer">
                                <span class="pull-left" style="font-size: 20px"><b>Owner:</b> {{owner.FName}} {{owner.LName}} <b>Age:</b> {{owner.Age}}  <b>Reputation:</b> {{ownerRep}}  <b>Finances:</b>  {{ownerFin}}</span>
                                <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
                            </div>
                       `,
            resolve: {
                coaches : coaches.push(_.filter(DB.load.data.Coaches, {'TeamID': teamId})),
                personnel : personnel.push(_.filter(DB.load.data.Personnel, {'TeamID': teamId}))

            },
            controller: function ($scope, $uibModalInstance, $interval, $filter, uiGridConstants) {
               


                $scope.teamSelected = teamSelected;
                $scope.team = $filter('filter')(DB.load.data.Teams, {TeamID: teamId}, true)[0];
                $scope.teamPri = {'background-color': $scope.team.MainColor, 'color': $scope.team.TrimColor};
                $scope.teamSec = {'background-color': $scope.team.SecondaryColor};
                $scope.teamTer = {'background-color': $scope.team.TrimColor};
                $scope.textColor = {'color' : $scope.team.TrimColor, 'font-weight': 'bold', 'margin-left': '20px' };
                $scope.teamPlace = DB.getNumEnding($scope.team.DivStanding);
                
                //Owner info in the footer
                $scope.owner = _.filter(DB.load.data.Owners, {'TeamID': teamId})[0];
                $scope.ownerRep = (function() {
                    var rep;

                    if($scope.owner.Reputation < 10) rep = "Very Poor";
                    else if ($scope.owner.Reputation < 25) rep ="Poor";
                    else if($scope.owner.Reputation < 50) rep = "Average";
                    else if($scope.owner.Reputation < 75) rep = "Good";
                    else if($scope.owner.Reputation < 90) rep = "Very Good";
                    else rep = "Outstanding";

                    return rep;
                })();

                $scope.ownerFin = (function() {
                    var fin;

                    if($scope.owner.PersonalWealth < 10) fin = "Struggling";
                    else if($scope.owner.PersonalWealth < 25) fin = "Lower Tier";
                    else if($scope.owner.PersonalWealth < 50) fin = "Middle of the Pack";
                    else if($scope.owner.PersonalWealth < 75) fin = "Above Average";
                    else if($scope.owner.PersonalWealth < 90) fin = "Wealthy";
                    else fin = "Banks ask to borrow money";

                    return fin;
                })();
                ///////////////////////////TEAM INFO SCREEN///////////////////////////////////////////////////////
                $scope.record = $scope.team.Wins + '-' + $scope.team.Losses;
                $scope.record += $scope.team.Ties > 0 ? '-' + $scope.team.Ties : '';
                $scope.teamOffYds = $filter('filter')(DB.load.data.TeamOffense, {TeamID: teamId}, true)[0].TotalYards / 16;
                $scope.teamOffRank = _.findIndex(DB.load.data.TeamOffense, {'TeamID': teamId}) + 1; //lodash function
                $scope.passOff = _.orderBy(DB.load.data.TeamOffense, ['PassingYards'], ['desc']);
                $scope.passOffYds = _.filter(DB.load.data.TeamOffense, {TeamID: teamId})[0].PassingYards / 16;
                $scope.passOffRank = _.findIndex($scope.passOff, {'TeamID': teamId}) + 1;
                $scope.rushOff = _.orderBy(DB.load.data.TeamOffense, ['RushingYards'], ['desc']);
                $scope.rushOffYds = _.filter(DB.load.data.TeamOffense, {'TeamID': teamId})[0].RushingYards / 16;
                $scope.rushOffRank = _.findIndex($scope.rushOff, {'TeamID': teamId}) + 1;
                $scope.pointsOff = _.orderBy(DB.load.data.TeamOffense, ['PointsFor'], ['desc']);
                $scope.teamPointsOff = _.filter(DB.load.data.TeamOffense, {'TeamID': teamId})[0].PointsFor / 16;
                $scope.pointsOffRank = _.findIndex($scope.pointsOff, {'TeamID': teamId}) + 1;
                $scope.teamDefYds = $filter('filter')(DB.load.data.TeamDefense, {TeamID: teamId}, true)[0].TotalYards / 16;
                $scope.teamDefRank = _.findIndex(DB.load.data.TeamDefense, {'TeamID': teamId}) + 1; //Lodash function
                $scope.passDef = _.orderBy(DB.load.data.TeamDefense, ['PassingYards'], ['asc']);
                $scope.passDefYds = _.filter(DB.load.data.TeamDefense, {TeamID: teamId})[0].PassingYards / 16;
                $scope.passDefRank = _.findIndex($scope.passDef, {'TeamID': teamId}) + 1;
                $scope.rushDef = _.orderBy(DB.load.data.TeamDefense, ['RushingYards'], ['asc']);
                $scope.rushDefYds = _.filter(DB.load.data.TeamDefense, {'TeamID': teamId})[0].RushingYards / 16;
                $scope.rushDefRank = _.findIndex($scope.rushDef, {'TeamID': teamId}) + 1;
                $scope.pointsDef = _.orderBy(DB.load.data.TeamDefense, ['PointsAgainst'], ['asc']);
                $scope.teamPointsDef = _.filter(DB.load.data.TeamDefense, {'TeamID': teamId})[0].PointsAllowed / 16;
                $scope.pointsDefRank = _.findIndex($scope.pointsDef, {'TeamID': teamId}) + 1;
                $scope.draftPos = DB.getNumEnding($scope.team.DraftPosition);
                $scope.roster = _.filter(DB.load.data.RosterPlayers, {'TeamID': teamId});
                $scope.salaryCap = _.random(133000000, 155000000);
                $scope.maxCap = 156000000;
                $scope.deadCap = _.random(800000, 12000000);
                $scope.teamValue = _.random(1500000000, 4200000000);
                $scope.revenue = _.random(300000000, 550000000);
                $scope.avgTicket = _.random(61.36, 130.75);
                $scope.highPlay1 = _.random(0, $scope.roster.length - 1);
                $scope.highPlay1 = $scope.roster[$scope.highPlay1].FName + ' ' + $scope.roster[$scope.highPlay1].LName + ' ' + $scope.roster[$scope.highPlay1].Pos;
                $scope.highPlay1Sal = _.random(17000000, 22000000);
                $scope.highPlay2 = _.random(0, $scope.roster.length - 1);
                $scope.highPlay2 = $scope.roster[$scope.highPlay2].FName + ' ' + $scope.roster[$scope.highPlay2].LName + ' ' + $scope.roster[$scope.highPlay2].Pos;
                $scope.highPlay2Sal = _.random(14000000, 16900000);
                $scope.highPlay3 = _.random(0, $scope.roster.length - 1);
                $scope.highPlay3 = $scope.roster[$scope.highPlay3].FName + ' ' + $scope.roster[$scope.highPlay3].LName + ' ' + $scope.roster[$scope.highPlay3].Pos;
                $scope.highPlay3Sal = _.random(10000000, 13900000);
                $scope.highPlay4 = _.random(0, $scope.roster.length - 1);
                $scope.highPlay4 = $scope.roster[$scope.highPlay4].FName + ' ' + $scope.roster[$scope.highPlay4].LName + ' ' + $scope.roster[$scope.highPlay4].Pos;
                $scope.highPlay4Rat = _.random(89, 99);
                $scope.highPlay5 = _.random(0, $scope.roster.length - 1);
                $scope.highPlay5 = $scope.roster[$scope.highPlay5].FName + ' ' + $scope.roster[$scope.highPlay5].LName + ' ' + $scope.roster[$scope.highPlay5].Pos;
                $scope.highPlay5Rat = _.random(85, $scope.highPlay4Rat - 1);
                $scope.highPlay6 = _.random(0, $scope.roster.length - 1);
                $scope.highPlay6 = $scope.roster[$scope.highPlay6].FName + ' ' + $scope.roster[$scope.highPlay6].LName + ' ' + $scope.roster[$scope.highPlay6].Pos;
                $scope.highPlay6Rat = _.random(80, $scope.highPlay5Rat - 1);
                //TODO: Calculate ways to grade roster players
                 /*getDraftPos($scope.roster);
               function getDraftPos(roster) { //get biggest weakness by checking player grades at each position and taking the average
                    var QB, RB, WR, TE, LT, LG, C, RG, RT, DE, DT, OLB, ILB, CB, FS, SS;
                    _.each($scope.roster, function(value) {
                        
                        switch(value.Pos) { //get the 
                            case 'QB': QB.value += value.AverageGrade; QB.count++; break;
                            case 'RB': RB += value.AverageGrade; break;
                            case 'WR': WR += value.AverageGrade; break;
                            case 'TE': TE += value.AverageGrade; break;
                            case 'LT': LT += value.AverageGrade; break;
                            case 'LG': LG += value.AverageGrade; break;
                            case 'C': C += value.AverageGrade; break;
                            case 'RG': RG += value.AverageGrade; break;
                            case 'RT': RT += value.AverageGrade; break;
                            case 'DE': DE += value.AverageGrade; break;
                            case 'DT': DT += value.AverageGrade; break;
                            case 'OLB': OLB += value.AverageGrade; break;
                            case 'ILB': ILB += value.AverageGrade; break;
                            case 'CB': CB += value.AverageGrade; break;
                            case 'FS': FS += value.AverageGrade; break;
                            case 'SS': SS += value.AverageGrade; break;
                        }

                    });

                };*/
                ////////////////////////////////////COACHES SCREEN/////////////////////////////////////////////////////////
                $scope.Coach = function(grid, row) {
                    return _.round((row.entity.CoachQB + row.entity.CoachRB + row.entity.CoachWR + row.entity.CoachTE + row.entity.CoachOL + 
                    row.entity.CoachDL + row.entity.CoachLB + row.entity.CoachDB + row.entity.CoachST) / 9);
                };
                
                $scope.Calc = function(grid, row) {
                                value = ((row.entity.JudgingQB + row.entity.JudgingRB + row.entity.JudgingWR + row.entity.JudgingTE + row.entity.JudgingOL + 
                                row.entity.JudgingDL + row.entity.JudgingLB + row.entity.JudgingCB + row.entity.JudgingSF) / 9);
                                return _.round(value);
                                };
                $scope.Dev = function(grid, row) {                   
                    value = ((row.entity.DevQB + row.entity.DevRB + row.entity.DevWR + row.entity.DevTE + row.entity.DevOL + row.entity.DevDL + 
                    row.entity.DevLB + row.entity.DevCB + row.entity.DevSF) / 9);
                    return _.round(value);
                };

                $scope.Train = function(grid, row) {
                    return _.round((row.entity.LowerBodyTrain + row.entity.UpperBodyTrain + row.entity.CoreTrain + row.entity.PreventInjuryTrain + row.entity.StaminaTrain) / 5);
                };
                $scope.headCoach = _.find($scope.coaches, {'CoachType': 1});
                var value;
            

                    $scope.gridOptions1 = {
                        onRegisterApi: function(gridApi) {
                            $scope.gridApi = gridApi;
                            $interval(function () {
                                $scope.gridApi.core.handleWindowResize();
                            }, 1000, 45);
                        },
                        enableSorting: true,
                        columnDefs: [
                            {name: 'FName', displayName: 'First Name', width: '7%'},
                            {name: 'LName', displayName: 'Last Name', width: '7%'},
                            {name: 'Age', width: '4%'},
                            {name: 'Experience', displayName: 'Exp', width: '4%', cellTooltip: 'Years of NFL coaching experience'},
                            {name: 'CoachType', visible: false, sort: {direction: uiGridConstants.ASC, priority: 0}},
                            {name: 'CoachTypeStr', displayName: 'Position', width: '12%'},
                            {name: 'SideOfBall', displayName: 'Spec.', width: '7%', cellTooltip: 'Side of ball the coach is most familiar with.'},
                            {name: 'OffAbility', displayName: 'Off Ability', width: '7%', cellTooltip: 'Skill level at coaching offense.'},
                            {name: 'OffPhil', displayName: 'Off Phil.', width: '7%', cellTooltip: 'Base offensive philosophy for the coach.'},
                            {name: 'DefAbility', displayName: 'Def Ability', width: '7%', cellTooltip: 'Skill level at coaching defense.'},
                            {name: 'DefPhil', displayName: 'Def Phil.', width: '7%', cellTooltip: 'Base defensive philosophy for the coach.'},
                            {name: 'CoachAbility', displayName: 'Coach Ability', width: '8%', 
                                cellTemplate: `<div class="ui-grid-cell-constants" title="Gives an overall coaching skill level for all positions">
                                {{grid.appScope.Coach(grid, row)}}</div>`},
                            {name: 'JudgingPlayers',  displayName: 'Judg. Players', width: '9%', 
                                cellTemplate: `<div class="ui-grid-cell-contents" title="Gives an overall grade for their skill in judging players. Skills may widely vary by position.">
                                {{grid.appScope.Calc(grid, row)}}</div>`},
                            {name: 'DevPlayers', displayName: 'Dev. Players', width: '7%',
                                cellTemplate: `<div class="ui-grid-cell-contents" title="How well the coach develops players. Overall rating for all positions.">
                                {{grid.appScope.Dev(grid, row)}}</div>`},
                            {name: 'TrainPlayers', displayName: 'Training', width: '7%',
                            cellTemplate: `<div class="ui-grid-cell-contents" title="How effective the coach is at training players."> {{grid.appScope.Train(grid, row)}}</div>`},                       
                    ]
                };
                $scope.gridOptions2 = {
                    onRegisterApi: function(gridApi) {
                            $scope.gridApi = gridApi;
                            $interval(function () {
                                $scope.gridApi.core.handleWindowResize();
                            }, 1000, 45);
                        },
                        enableSorting: true,
                        columnDefs: [
                            {name: 'FName', displayName: 'First Name', width: '7%'},
                            {name: 'LName', displayName: 'Last Name', width: '7%'},
                            {name: 'Age', width: '4%'},
                            {name: 'Experience', displayName: 'Exp', width: '4%', cellTooltip: 'Years of NFL personnel experience'},
                            {name: 'PersonnelType', visible: false, sort: {direction: uiGridConstants.ASC, priority: 0}},
                            {name: 'PersonnelTypeStr', displayName: 'Position', width: '12%'},
                            {name: 'ScoutRegion', displayName: 'Scout Region', width: '7%', cellTooltip: 'Scouting region or assignment they are responsible for.'},
                            {name: 'DraftStrategy', displayName: 'Draft Strat', width: '8%', cellTooltip: 'Preferred strategy when it comes to the draft and drafting players.'},
                            {name: 'TeamBuilding', displayName: 'Team Bldg', width: '8%', cellToolTip: 'Preferred strategy when it comes to building their team.'},
                            {name: 'ValuesDraftPicks', displayName: 'Val Draft Picks', width: '7%', cellTooltip: 'How much they value draft picks when grading players.'},                
                            {name: 'ValuesProduction', displayName: 'Val Production', width: '7%', cellTooltip: 'How much they value production when grading players.'},
                            {name: 'ValuesIntangibles', displayName: 'Val Intangibles', width: '7%', cellTooltip: 'How much they value intangibles when grading players.'},
                            {name: 'ValuesCombine', displayName: 'Val Combine', width: '7%', cellTooltip: 'How much they value the combine when grading players.'},
                            {name: 'ValuesCharacter', displayName: 'Val Character', width: '7%', cellTooltip: 'How much they value a player\'s character when grading players.'},
                            {name: 'JudgingDraft', displayName: 'Judg. Draft', width: '7%', cellTooltip: 'How good they are at judging players in the draft.'},
                            {name: 'JudgingFA', displayName: 'Judg. FA', width: '7%', cellTooltip: 'How good they are at judging free agents'},
                            {name: 'JudgingOwn', displayName: 'Judg. Own', width: '7%', cellTooltip: 'How good they are at accurately juding players on their own team and not "falling in love" with certain players'},
                            {name: 'JudgingPot', displayName: 'Judg. Pot', width: '7%', cellTooltip: 'How good they are at judging a player\'s potential'},
                            {name: 'OffPhil', displayName: 'Off Phil.', width: '7%', cellTooltip: 'Base offensive philosophy for the coach.'},
                            {name: 'DefPhil', displayName: 'Def Phil.', width: '7%', cellTooltip: 'Base defensive philosophy for the coach.'}    
                    ]
                };
                $scope.ok = function () {
                    $uibModalInstance.close();
                };          
                $scope.gridOptions1.data = coaches[0];
                $scope.gridOptions2.data = personnel[0];
            },

                

        });
    };

vm.sides = [];
  
  //load the teams from the DB.Teams object
    angular.forEach(DB.load.data.Teams, function(value) {
        vm.sides.push({image: value.TeamLogoPath, teamID: value.TeamID, title: value.TeamName + ' ' + value.TeamNickname, 
        listItems: ['Offense Rating: 85', 'Defense Rating: 88', 'Special Teams Rating: 79']});
    });

  /*vm.sides = [      
        {image: 'New_Game_Wizard/assets/Arizona_Cardinals3.jpg', teamID: 29, title: 'Arizona Cardinals', listItems: ['Offense Rating: 85', 'Defense Rating: 88', 'Special Teams Rating: 79']},
        {image: 'New_Game_Wizard/assets/Atlanta_Falcons2.jpg', teamID: 26, title: 'Atlanta Falcons', listItems: ['Offense Rating: 98', 'Defense Rating: 81', 'Special Teams Rating: 77']},
        {image: 'New_Game_Wizard/assets/Baltimore-Ravens-3.jpg', teamID: 7, title: 'Baltimore Ravens', listItems: ['Offense Rating: 78', 'Defense Rating: 95', 'Special Teams Rating: 83']},
        {image: 'New_Game_Wizard/assets/Buffalo_Bills02.jpg', teamID: 1, title: 'Buffalo Bills', listItems: ['Offense Rating: 88', 'Defense Rating: 82', 'Special Teams Rating: 83']},
        {image: 'New_Game_Wizard/assets/Carolina_Panthers2.jpg', teamID: 25, title: 'Carolina Panthers', listItems: ['Offense Rating: 83', 'Defense Rating: 86', 'Special Teams Rating: 82']},
        {image: 'New_Game_Wizard/assets/Chicago-Bears-3.jpg', teamID: 24, title: 'Chicago Bears', listItems: ['Offense Rating: 72', 'Defense Rating: 75', 'Special Teams Rating: 76']},
        {image: 'New_Game_Wizard/assets/Cincinnati_Bengals3.jpg', teamID: 5, title: 'Cincinnati Bengals', listItems: ['Offense Rating: 80', 'Defense Rating: 85', 'Special Teams Rating: 80']},
        {image: 'New_Game_Wizard/assets/Cleveland_Browns2.jpg', teamID: 8, title: 'Cleveland Browns', listItems: ['Offense Rating: 72', 'Defense Rating: 70', 'Special Teams Rating: 71']},
        {image: 'New_Game_Wizard/assets/Dallas_Cowboys3.jpg', teamID: 20, title: 'Dallas Cowboys', listItems: ['Offense Rating: 95', 'Defense Rating: 88', 'Special Teams Rating: 86']},
        {image: 'New_Game_Wizard/assets/Denver_Broncos2.jpg', teamID: 13, title: 'Denver Broncos', listItems: ['Offense Rating: 78', 'Defense Rating: 97', 'Special Teams Rating: 85']},
        {image: 'New_Game_Wizard/assets/detroit-lions-3.jpg', teamID: 23, title: 'Detroit Lions', listItems: ['Offense Rating: 86', 'Defense Rating: 84', 'Special Teams Rating: 82']},
        {image: 'New_Game_Wizard/assets/Green_Bay_Packers5.jpg', teamID: 22, title: 'Green Bay Packers', listItems: ['Offense Rating: 91', 'Defense Rating: 84', 'Special Teams Rating: 82']},
        {image: 'New_Game_Wizard/assets/Houston_Texans2.jpg', teamID: 9, title: 'Houston Texans', listItems: ['Offense Rating: 79', 'Defense Rating: 98', 'Special Teams Rating: 92']},
        {image: 'New_Game_Wizard/assets/Indianapolis_Colts2.jpg', teamID: 10, title: 'Indianapolis Colts', listItems: ['Offense Rating: 86', 'Defense Rating: 74', 'Special Teams Rating: 78']},
        {image: 'New_Game_Wizard/assets/Jacksonville_Jaguars2.jpg', teamID: 11, title: 'Jacksonville Jaguars', listItems: ['Offense Rating: 79', 'Defense Rating: 89', 'Special Teams Rating: 80']},
        {image: 'New_Game_Wizard/assets/Kansas_City_Chiefs3.jpg', teamID: 14, title: 'Kansas City Chiefs', listItems: ['Offense Rating: 84', 'Defense Rating: 92', 'Special Teams Rating: 90']},
        {image: 'New_Game_Wizard/assets/LARams2.png', teamID: 31, title: 'Los Angeles Rams', listItems: ['Offense Rating: 70', 'Defense Rating: 83', 'Special Teams Rating: 75']},
        {image: 'New_Game_Wizard/assets/Miami_Dolphins2.jpg', teamID: 4, title: 'Miami Dolphins', listItems: ['Offense Rating: 86', 'Defense Rating: 85', 'Special Teams Rating: 85']},
        {image: 'New_Game_Wizard/assets/Minnesota_Vikings_2013_06.jpg', teamID: 21, title: 'Minnesota Vikings', listItems: ['Offense Rating: 79', 'Defense Rating: 88', 'Special Teams Rating: 82']},
        {image: 'New_Game_Wizard/assets/New_England_Patriots2.jpg', teamID: 2, title: 'New England Patriots', listItems: ['Offense Rating: 98', 'Defense Rating: 89', 'Special Teams Rating: 92']},
        {image: 'New_Game_Wizard/assets/New_Orleans_Saints2.jpg', teamID: 27, title: 'New Orleans Saints', listItems: ['Offense Rating: 98', 'Defense Rating: 70', 'Special Teams Rating: 72']},
        {image: 'New_Game_Wizard/assets/New_York_Giants5.jpg', teamID: 19, title: 'New York Giants', listItems: ['Offense Rating: 86', 'Defense Rating: 85', 'Special Teams Rating: 83']},
        {image: 'New_Game_Wizard/assets/New_York_Jets2.jpg', teamID: 3, title: 'New York Jets', listItems: ['Offense Rating: 73', 'Defense Rating: 75', 'Special Teams Rating: 75']},
        {image: 'New_Game_Wizard/assets/Oakland_Raiders2.jpg', teamID: 15, title: 'Oakland Raiders', listItems: ['Offense Rating: 94', 'Defense Rating: 74', 'Special Teams Rating: 86']},
        {image: 'New_Game_Wizard/assets/Philadelphia_Eagles2.jpg', teamID: 18, title: 'Philadelphia Eagles', listItems: ['Offense Rating: 80', 'Defense Rating: 89', 'Special Teams Rating: 84']},
        {image: 'New_Game_Wizard/assets/Pittsburgh_Steelers2.jpg', teamID: 6, title: 'Pittsburgh Steelers', listItems: ['Offense Rating: 88', 'Defense Rating: 84', 'Special Teams Rating: 84']},
        {image: 'New_Game_Wizard/assets/San_Diego_Chargers5.jpg', teamID: 16, title: 'San Diego Chargers', listItems: ['Offense Rating: 89', 'Defense Rating: 82', 'Special Teams Rating: 82']},
        {image: 'New_Game_Wizard/assets/San_Francisco_49ers04.jpg', teamID: 32, title: 'San Francisco 49ers', listItems: ['Offense Rating: 73', 'Defense Rating: 70', 'Special Teams Rating: 71']},
        {image: 'New_Game_Wizard/assets/Seattle_Seahawks2_2012.jpg', teamID: 30, title: 'Seattle Seahawks', listItems: ['Offense Rating: 84', 'Defense Rating: 93', 'Special Teams Rating: 86']},
        {image: 'New_Game_Wizard/assets/Tampa_Bay_Buccaneers2.jpg', teamID: 28, title: 'Tampa Bay Buccaneers', listItems: ['Offense Rating: 84', 'Defense Rating: 88', 'Special Teams Rating: 83']},
        {image: 'New_Game_Wizard/assets/Tennessee_Titans2.jpg', teamID: 12, title: 'Tennessee Titans', listItems: ['Offense Rating: 89', 'Defense Rating: 84', 'Special Teams Rating: 81']},
        {image: 'New_Game_Wizard/assets/Washington_Redskins2.jpg', teamID: 17, title: 'Washington Redskins', listItems: ['Offense Rating: 91', 'Defense Rating: 83', 'Special Teams Rating: 85']},
       
    ] */      
  

}]);
})();