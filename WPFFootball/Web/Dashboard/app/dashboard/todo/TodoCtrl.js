"use strict";

angular.module('app').controller('TodoCtrl', function ($scope, $timeout, Todo) {
    $scope.newTodo = undefined;

    $scope.states = ['Critical', 'Important', 'Completed'];

    $scope.todos = Todo.getList().$object;

    // $scope.$watch('todos', function(){ }, true)

    $scope.toggleAdd = function () {
        if (!$scope.newTodo) {
            $scope.newTodo = {
                state: 'Important'
            };
        } else {
            $scope.newTodo = undefined;
        }
    };

    $scope.createTodo = function () {
        $scope.todos.push(
           Todo.normalize($scope.newTodo)
        );
        $scope.newTodo = undefined;

    };

    $scope.deleteTodo = function (todo) {
        todo.remove().then(function () {
            $scope.todos.splice($scope.todos.indexOf(todo), 1);
        });

    };

});