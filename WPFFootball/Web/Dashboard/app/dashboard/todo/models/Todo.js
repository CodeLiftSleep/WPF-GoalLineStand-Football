"use strict";

angular.module('app').factory('Todo', function (Restangular, APP_CONFIG, $httpBackend) {

    var normalize = function(todo) {
        if(!todo._id){
            todo._id = _.unique('todo')
        }
        todo.toggle = function(){
            if (!todo.completedAt) {
                todo.state = 'Completed';
                todo.completedAt = JSON.stringify(new Date());
            } else {
                todo.state = 'Critical';
                todo.completedAt = null;
            }
        };

        todo.setState = function(state){
            todo.state = state;
            if (state == 'Completed') {
                todo.completedAt = JSON.stringify(new Date());
            } else {
                todo.completedAt = null;
            }
        };

        return todo;
    };

    var Todo = Restangular.all(APP_CONFIG.apiRootUrl + '/todos.json');

    Restangular.extendModel(APP_CONFIG.apiRootUrl + '/todos.json', normalize);
    Todo.normalize = normalize;

    return Todo
});