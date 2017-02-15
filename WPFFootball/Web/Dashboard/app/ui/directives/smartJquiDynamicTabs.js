'use strict';

angular.module('app.ui').directive('smartJquiDynamicTabs', function ($timeout) {

	
	function addDomEvents(element){

		$('#tabs2').tabs();

		var tabTitle = $("#tab_title"), tabContent = $("#tab_content"), tabTemplate = "<li style='position:relative;'> <span class='air air-top-left delete-tab' style='top:7px; left:7px;'><button class='btn btn-xs font-xs btn-default hover-transparent'><i class='fa fa-times'></i></button></span></span><a href='#{href}'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; #{label}</a></li>", tabCounter = 2;

		var tabs = $('#tabs2').tabs();

		// modal dialog init: custom buttons and a "close" callback reseting the form inside
		var dialog = $("#addtab").dialog({
			autoOpen : false,
			width : 600,
			resizable : false,
			modal : true,
			buttons : [{
			html : "<i class='fa fa-times'></i>&nbsp; Cancel",
			"class" : "btn btn-default",
			click : function() {
			$(this).dialog("close");

		}
		}, {

			html : "<i class='fa fa-plus'></i>&nbsp; Add",
			"class" : "btn btn-danger",
			click : function() {
				addTab();
				$(this).dialog("close");
			}
		}]
		});

		// addTab form: calls addTab function on submit and closes the dialog
		var form = dialog.find("form").submit(function(event) {
			addTab();
			dialog.dialog("close");
			event.preventDefault();
		});

		// actual addTab function: adds new tab using the input from the form above
		function addTab() {
			var label = tabTitle.val() || "Tab " + tabCounter, id = "tabs-" + tabCounter, li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label)), tabContentHtml = tabContent.val() || "Tab " + tabCounter + " content.";

			tabs.find(".ui-tabs-nav").append(li);
			tabs.append("<div id='" + id + "'><p>" + tabContentHtml + "</p></div>");
			tabs.tabs("refresh");
			tabCounter++;

			// clear fields
			$("#tab_title").val("");
			$("#tab_content").val("");
		}

		// addTab button: just opens the dialog
		$("#add_tab").button().click(function() {
			dialog.dialog("open");
		});

		// close icon: removing the tab on click
		$("#tabs2").on("click", 'span.delete-tab', function() {

			var panelId = $(this).closest("li").remove().attr("aria-controls");
			$("#" + panelId).remove();
			tabs.tabs("refresh");

		});

	}

	function link(element){

		$timeout(function(){
			addDomEvents(element);
		});

	}


    return {
        restrict: 'A',
        link: link
    }
});
