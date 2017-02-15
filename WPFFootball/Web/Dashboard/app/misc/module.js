"use strict";

angular.module('app.misc', ['ui.router']);


angular.module('app.misc').config(function ($stateProvider) {

    $stateProvider
        .state('app.misc', {
            abstract: true,
            data: {
                title: 'Miscellaneous'
            }
        })

        .state('app.misc.pricingTable', {
            url: '/pricing-table',
            data: {
                title: 'Pricing Table'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/pricing-table.html'
                }
            }
        })

        .state('app.misc.invoice', {
            url: '/invoice',
            data: {
                title: 'Invoice'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/invoice.html'
                }
            }
        })

        .state('app.misc.error404', {
            url: '/404',
            data: {
                title: 'Error 404'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/error404.html'
                }
            }
        })

        .state('app.misc.error500', {
            url: '/500',
            data: {
                title: 'Error 500'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/error500.html'
                }
            }
        })

        .state('app.misc.blank', {
            url: '/blank',
            data: {
                title: 'Blank'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/blank.html'
                }
            }
        })

        .state('app.misc.test', {
            url: '/test',
            data: {
                title: 'Test'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/test.html'
                }
            }
        })

        .state('app.misc.emailTemplate', {
            url: '/email-template',
            data: {
                title: 'Email Template'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/email-template.html'
                }
            }
        })

        .state('app.misc.search', {
            url: '/search',
            data: {
                title: 'Search'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/search.html'
                }
            }
        })

        .state('app.misc.ckeditor', {
            url: '/ckeditor',
            data: {
                title: 'CK Editor'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/misc/views/ckeditor.html'
                }
            },
            resolve:{
                scripts: function(lazyScript){
                    return lazyScript.register('smartadmin-plugin/legacy/ckeditor/ckeditor.js');
                }
            }
        })
});