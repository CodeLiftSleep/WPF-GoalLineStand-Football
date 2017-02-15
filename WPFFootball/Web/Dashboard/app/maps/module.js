    "use strict";


    angular.module('app.maps', ['ui.router',
        'uiGmapgoogle-maps'
    ])
    //.config(function(uiGmapGoogleMapApiProvider) {
    //    uiGmapGoogleMapApiProvider.configure({
    //        //    key: 'your api key',
    //        v: '3.20', //defaults to latest 3.X anyhow
    //        libraries: 'weather,geometry,visualization'
    //    });
    //})


    angular.module('app.maps').config(function ($stateProvider) {

        $stateProvider
            .state('app.maps', {
                url: '/maps',
                data: {
                    title: 'Maps'
                },
                views: {
                    "content@app": {
                        controller: 'MapsDemoCtrl',
                        templateUrl: 'app/maps/views/maps-demo.html'
                    }
                }
            })
    });