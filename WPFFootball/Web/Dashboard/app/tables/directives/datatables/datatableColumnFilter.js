'use strict';

angular.module('app.tables').directive('datatableColumnFilter', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
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

            var otable = element.DataTable({
                //"bFilter": false,
                //"bInfo": false,
                //"bLengthChange": false
                //"bAutoWidth": false,
                //"bPaginate": false,
                //"bStateSave": true // saves sort state using localStorage
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6 hidden-xs'f><'col-sm-6 col-xs-12 hidden-xs'<'toolbar'>>r>"+
                    "t"+
                    "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
                oLanguage:{
                    "sSearch": "<span class='input-group-addon input-sm'><i class='glyphicon glyphicon-search'></i></span> "
                },
                "autoWidth" : false,
                "preDrawCallback" : function() {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper) {
                        responsiveHelper = new ResponsiveDatatablesHelper(element, breakpointDefinition);
                    }
                },
                "rowCallback" : function(nRow) {
                    responsiveHelper.createExpandIcon(nRow);
                },
                "drawCallback" : function(oSettings) {
                    responsiveHelper.respond();
                }

            });

            // custom toolbar
            element.parent().find("div.toolbar").html('<div class="text-right"><img src="styles/img/logo.png" alt="SmartAdmin" style="width: 111px; margin-top: 3px; margin-right: 10px;"></div>');

            // Apply the filter
            element.on( 'keyup change', 'thead th input[type=text]', function () {

                otable
                    .column( $(this).parent().index()+':visible' )
                    .search( this.value )
                    .draw();

            } );
        }
    }
});