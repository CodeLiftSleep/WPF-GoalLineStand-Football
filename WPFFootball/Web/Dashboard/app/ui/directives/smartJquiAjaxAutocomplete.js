'use strict';

angular.module('app.ui').directive('smartJquiAjaxAutocomplete', function () {
    return {
        restrict: 'A',
        scope: {
            ngModel: '='
        },
        link: function (scope, element, attributes) {
            function split(val) {
                return val.split(/,\s*/);
            }

            function extractLast(term) {
                return split(term).pop();
            }

            function extractFirst(term) {
                return split(term)[0];
            }


            element.autocomplete({
                source: function (request, response) {
                    jQuery.getJSON(
                            "http://gd.geobytes.com/AutoCompleteCity?callback=?&q=" + extractLast(request.term),
                        function (data) {
                            response(data);
                        }
                    );
                },
                minLength: 3,
                select: function (event, ui) {
                    var selectedObj = ui.item,
                    placeName = selectedObj.value;
                    if (typeof placeName == "undefined") placeName = element.val();

                    if (placeName) {
                        var terms = split(element.val());
                        // remove the current input
                        terms.pop();
                        // add the selected item (city only)
                        terms.push(extractFirst(placeName));
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");

                        scope.$apply(function(){
                            scope.ngModel = terms.join(", ")
                        });
                    }

                    return false;
                },
                focus: function() {
                    // prevent value inserted on focus
                    return false;
                },
                delay: 100
            });
        }
    }
});