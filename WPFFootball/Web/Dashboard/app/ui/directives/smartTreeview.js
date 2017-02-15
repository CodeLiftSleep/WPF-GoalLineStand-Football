'use strict';

angular.module('app.ui').directive('smartTreeviewContent', function ($compile) {
    return {
        restrict: 'E',
        link: function (scope, element) {
            var $content = $(scope.item.content);

            function handleExpanded(){
                $content.find('>i')
                    .toggleClass('fa-plus-circle', !scope.item.expanded)
                    .toggleClass('fa-minus-circle', !!scope.item.expanded)

            }


            if (scope.item.children && scope.item.children.length) {
                $content.on('click', function(){
                    scope.$apply(function(){
                        scope.item.expanded = !scope.item.expanded;
                        handleExpanded();
                    });


                });
                handleExpanded();
            }

            element.replaceWith($content);


        }
    }
});

angular.module('app.ui').directive('smartTreeview', function ($compile, $sce) {
    return {
        restrict: 'A',
        scope: {
            'items': '='
        },
        template: '<li ng-class="{parent_li: item.children.length}" ng-repeat="item in items" role="treeitem">' +
            '<smart-treeview-content></smart-treeview-content>' +
            '<ul ng-if="item.children.length" smart-treeview ng-show="item.expanded"  items="item.children" role="group" class="smart-treeview-group" ></ul>' +
            '</li>',
        compile: function (element) {
            // Break the recursion loop by removing the contents
            var contents = element.contents().remove();
            var compiledContents;
            return {
                post: function (scope, element) {
                    // Compile the contents
                    if (!compiledContents) {
                        compiledContents = $compile(contents);
                    }
                    // Re-add the compiled contents to the element
                    compiledContents(scope, function (clone) {
                        element.append(clone);
                    });
                }
            };
        }
    };
});