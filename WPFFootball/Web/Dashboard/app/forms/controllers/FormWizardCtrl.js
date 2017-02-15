"use strict";


angular.module('app.forms').controller('FormWizardCtrl', function($scope){

    $scope.wizard1CompleteCallback = function(wizardData){
        console.log('wizard1CompleteCallback', wizardData);
        $.smallBox({
            title: "Congratulations! Smart wizard finished",
            content: "<i class='fa fa-clock-o'></i> <i>1 seconds ago...</i>",
            color: "#5F895F",
            iconSmall: "fa fa-check bounce animated",
            timeout: 4000
        });
    };

    $scope.wizard2CompleteCallback = function(wizardData){
        console.log('wizard2CompleteCallback', wizardData);
        $.smallBox({
            title: "Congratulations! Smart fuekux wizard finished",
            content: "<i class='fa fa-clock-o'></i> <i>1 seconds ago...</i>",
            color: "#5F895F",
            iconSmall: "fa fa-check bounce animated",
            timeout: 4000
        });

    };

});