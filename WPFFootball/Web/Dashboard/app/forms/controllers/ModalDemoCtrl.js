'use strict'

angular.module('app.forms').controller('ModalDemoCtrl', function($scope, $modalInstance){
    $scope.closeModal = function(){
        $modalInstance.dismiss('cancel');
    }
});