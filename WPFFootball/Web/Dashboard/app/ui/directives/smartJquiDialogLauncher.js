'use strict';

//    $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
//        _title: function (title) {
//            if (!this.options.title) {
//                title.html("&#160;");
//            } else {
//                title.html(this.options.title);
//            }
//        }
//    }));


angular.module('app.ui').directive('smartJquiDialogLauncher', function () {
    return {
        restrict: 'A',
        compile: function (element, attributes) {
            element.removeAttr('smart-jqui-dialog-launcher data-smart-jqui-dialog-launcher');
            element.on('click', function (e) {
                $(attributes.smartJquiDialogLauncher).dialog('open');
                e.preventDefault();
            })
        }
    }
});