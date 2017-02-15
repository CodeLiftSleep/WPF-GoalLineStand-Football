"use strict";

angular.module('app.graphs', [
    'ui.router'
]).config(function ($stateProvider) {
    $stateProvider
        .state('app.graphs', {
            abstract: true,
            data: {
                title: 'Graphs'
            }
        })

        .state('app.graphs.flot', {
            url: '/graphs/flot',
            data: {
                title: 'Flot Charts'
            },
            views: {
                "content@app": {
                    controller: 'FlotCtrl',
                    templateUrl: "app/graphs/views/flot-charts.html"
                }
            }
        })

        .state('app.graphs.morris', {
            url: '/graphs/morris',
            data: {
                title: 'Morris Charts'
            },
            views: {
                "content@app": {
                    templateUrl: "app/graphs/views/morris-charts.html"
                }
            },
            resolve: {
                scripts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.graphs.js'
                    ]);
                }
            }
        })

        .state('app.graphs.sparkline', {
            url: '/graphs/sparkline',
            data: {
                title: 'Sparklines'
            },
            views: {
                "content@app": {
                    templateUrl: "app/graphs/views/sparkline.html"
                }
            }
        })
        .state('app.graphs.easyPieCharts', {
            url: '/graphs/easy-pie',
            data: {
                title: 'Easy Pie Charts'
            },
            views: {
                "content@app": {
                    templateUrl: "app/graphs/views/easy-pie-charts.html"
                }
            }
        })

        .state('app.graphs.dygraphs', {
            url: '/graphs/dygraphs',
            data: {
                title: 'Dygraphs Charts'
            },
            views: {
                "content@app": {
                    templateUrl: "app/graphs/views/dygraphs-charts.html"
                }
            },
            resolve: {
                scripts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.graphs.js'
                    ]);
                }
            }
        })

        .state('app.graphs.chartjs', {
            url: '/graphs/chartjs',
            data: {
                title: 'Chart.js'
            },
            views: {
                "content@app": {
                    templateUrl: "app/graphs/views/chartjs-charts.html"
                }
            },
            resolve: {
                scripts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.graphs.js'
                    ]);
                }
            }
        })


        .state('app.graphs.highchartTables', {
            url: '/graphs/highchart-tables',
            data: {
                title: 'Highchart Tables'
            },
            views: {
                "content@app": {
                    templateUrl: "app/graphs/views/highchart-tables.html"
                }
            }
        })
});