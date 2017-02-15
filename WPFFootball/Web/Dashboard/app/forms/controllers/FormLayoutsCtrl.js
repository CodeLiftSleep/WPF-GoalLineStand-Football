
"use strict";

angular.module('app.forms').controller('FormLayoutsCtrl', function($scope, $modal, $log){

    $scope.openModal = function () {
        var modalInstance = $modal.open({
            templateUrl: 'app/forms/views/form-layout-modal.html',
            controller: 'ModalDemoCtrl' 
        });

        modalInstance.result.then(function () {
            $log.info('Modal closed at: ' + new Date());

        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });


    };

    $scope.registration = {};

    $scope.$watch('registration.date', function(changed){
        console.log('registration model changed', $scope.registration)
    })


});