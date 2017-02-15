"use strict";

 angular.module('app').directive('todoList', function ($timeout, Todo) {

    return {
        restrict: 'E',
        replace: true,
        templateUrl: 'app/dashboard/todo/directives/todo-list.tpl.html',
        scope: {
            todos: '='
        },
        link: function (scope, element, attributes) {
            scope.title = attributes.title
            scope.icon = attributes.icon
            scope.state = attributes.state
            scope.filter = {
                state: scope.state
            }

            element.find('.todo').sortable({
                handle: '.handle',
                connectWith: ".todo",
                receive: function (event, ui) {

                    console.log(ui.item.scope().todo,scope.state)
                    var todo = ui.item.scope().todo;
                    var state = scope.state
                    // // console.log(ui.item, todo, state)
                    // // console.log(state, todo)
                    if (todo && state) {
                        todo.setState(state);
                         // ui.sender.sortable("cancel");
                        // scope.$apply();
                    } else {
                        console.log('Wat', todo, state);
                    }
                    
                }
            }).disableSelection();

        }
    }
});