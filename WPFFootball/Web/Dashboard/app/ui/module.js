'use strict'

angular.module('app.ui', ['ui.router']);

angular.module('app.ui').config(function($stateProvider){

    $stateProvider
        .state('app.ui', {
            abstract: true,
            data: {
                title: 'UI Elements'
            }
        })
        .state('app.ui.general', {
            url: '/ui/general',
            data: {
                title: 'General Elements'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/general-elements.html',
                    controller: 'GeneralElementsCtrl'
                }
            }
        })
        .state('app.ui.buttons', {
            url: '/ui/buttons',
            data: {
                title: 'Buttons'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/buttons.html',
                    controller: 'GeneralElementsCtrl'
                }
            }
        })
        .state('app.ui.iconsFa', {
            url: '/ui/icons-font-awesome',
            data: {
                title: 'Font Awesome'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/icons-fa.html'
                }
            }
        })
        .state('app.ui.iconsGlyph', {
            url: '/ui/icons-glyph',
            data: {
                title: 'Glyph Icons'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/icons-glyph.html'
                }
            }
        })
        .state('app.ui.iconsFlags', {
            url: '/ui/icons-flags',
            data: {
                title: 'Flags'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/icons-flags.html'
                }
            }
        })
        .state('app.ui.grid', {
            url: '/ui/grid',
            data: {
                title: 'Grid'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/grid.html'
                }
            }
        })
        .state('app.ui.treeView', {
            url: '/ui/tree-view',
            data: {
                title: 'Tree View'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/tree-view.html',
                    controller: 'TreeviewCtrl'
                }
            }
        })
        .state('app.ui.nestableLists', {
            url: '/ui/nestable-lists',
            data: {
                title: 'Nestable Lists'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/nestable-lists.html'
                }
            },
            resolve: {
                srcipts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])

                }
            }
        })
        .state('app.ui.jqueryUi', {
            url: '/ui/jquery-ui',
            data: {
                title: 'JQuery UI'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/jquery-ui.html',
                    controller: 'JquiCtrl'
                }
            },
            resolve: {
                srcipts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])

                }
            }
        })
        .state('app.ui.typography', {
            url: '/ui/typography',
            data: {
                title: 'JQuery UI'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/ui/views/typography.html'
                }
            }
        })
});