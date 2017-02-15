"use strict";


angular.module('app.maps').factory('SmartMapStyle', function ($q, $http, APP_CONFIG) {

    var styles = {
        'colorful': { name: 'Colorful', url: APP_CONFIG.apiRootUrl + '/maps/colorful.json'},
        'greyscale': { name: 'greyscale', url: APP_CONFIG.apiRootUrl + '/maps/greyscale.json'},
        'metro': { name: 'metro', url: APP_CONFIG.apiRootUrl + '/maps/metro.json'},
        'mono-color': { name: 'mono-color', url: APP_CONFIG.apiRootUrl + '/maps/mono-color.json'},
        'monochrome': { name: 'monochrome', url: APP_CONFIG.apiRootUrl + '/maps/monochrome.json'},
        'nightvision': { name: 'Nightvision', url: APP_CONFIG.apiRootUrl + '/maps/nightvision.json'},
        'nightvision-highlight': { name: 'nightvision-highlight', url: APP_CONFIG.apiRootUrl + '/maps/nightvision-highlight.json'},
        'old-paper': { name: 'Old Paper', url: APP_CONFIG.apiRootUrl + '/maps/old-paper.json'}
    };


    function getMapType(key){
        var keyData = styles[key];

        if(!keyData.cache){
            keyData.cache = createMapType(keyData)
        }

        return keyData.cache;
    }

    function createMapType(keyData){
        var dfd = $q.defer();
        $http.get(keyData.url).then(function(resp){
            var styleData = resp.data;
            var type = new google.maps.StyledMapType(styleData, {name: keyData.name})
            dfd.resolve(type);
        }, function(reason){
            console.error(reason);
            dfd.reject(reason);
        });

        return dfd.promise;
    }


    return {
        getMapType: getMapType,
        styles: styles
    }



});