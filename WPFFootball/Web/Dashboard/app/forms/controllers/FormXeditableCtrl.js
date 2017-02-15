"use strict";

angular.module('app.forms').controller('FormXeditableCtrl', function($scope, $log){

    $scope.username = 'superuser';
    $scope.firstname = null;
    $scope.sex = 'not selected';
    $scope.group = "Admin";
    $scope.vacation = "25.02.2013";
    $scope.combodate = "15/05/1984";
    $scope.event = null;
    $scope.comments = 'awesome user!';
    $scope.state2 = 'California';
    $scope.fruits = 'peach<br/>apple';
    

    $scope.fruits_data = [
        {value: 'banana', text: 'banana'},
        {value: 'peach', text: 'peach'},
        {value: 'apple', text: 'apple'},
        {value: 'watermelon', text: 'watermelon'},
        {value: 'orange', text: 'orange'}]
    ;


    $scope.genders =  [
        {value: 'not selected', text: 'not selected'},
        {value: 'Male', text: 'Male'},
        {value: 'Female', text: 'Female'}
    ];

    $scope.groups =  [
        {value: 'Guest', text: 'Guest'},
        {value: 'Service', text: 'Service'},
        {value: 'Customer', text: 'Customer'},
        {value: 'Operator', text: 'Operator'},
        {value: 'Support', text: 'Support'},
        {value: 'Admin', text: 'Admin'}
    ]; 

});