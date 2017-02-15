'use strict';

angular.module('app.maps').controller('MapsDemoCtrl',
    function ($scope, $http, $q, SmartMapStyle, uiGmapGoogleMapApi) {


        $scope.styles = SmartMapStyle.styles;

        $scope.setType = function (key) {
            SmartMapStyle.getMapType(key).then(function (type) {
                $scope.map.control.getGMap().mapTypes.set(key, type);
                $scope.map.control.getGMap().setMapTypeId(key);
            });
            $scope.currentType = key;
        };


        $scope.map = {
            center: {latitude: 45, longitude: -73},
            zoom: 8,
            control: {}
        };


        uiGmapGoogleMapApi.then(function (maps) {

            })
            .then(function () {
                return SmartMapStyle.getMapType('colorful')
            }).then(function () {
            $scope.setType('colorful')
        });



    });