'use strict';

angular.module('app.tables').directive('datatableColumnReorder', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {
            /* // DOM Position key index //

             l - Length changing (dropdown)
             f - Filtering input (search)
             t - The Table! (datatable)
             i - Information (records)
             p - Pagination (paging)
             r - pRocessing
             < and > - div elements
             <"#id" and > - div with an id
             <"class" and > - div with a class
             <"#id.class" and > - div with an id and class

             Also see: http://legacy.datatables.net/usage/features
             */

            var responsiveHelper = undefined;

            var breakpointDefinition = {
                tablet: 1024,
                phone: 480
            };

            element.dataTable({
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 hidden-xs'C>r>" +
                    "t" +
                    "<'dt-toolbar-footer'<'col-sm-6 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
                oLanguage: {
                    "sSearch": "<span class='input-group-addon input-sm'><i class='glyphicon glyphicon-search'></i></span> "
                },
                "autoWidth": false,
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper) {
                        responsiveHelper = new ResponsiveDatatablesHelper(element, breakpointDefinition);
                    }
                },
                "rowCallback": function (nRow) {
                    responsiveHelper.createExpandIcon(nRow);
                },
                "drawCallback": function (oSettings) {
                    responsiveHelper.respond();
                }
            });
        }
    }
});