"use strict";


angular.module('app.appViews', ['ui.router'])
.config(function ($stateProvider) {

    $stateProvider
        .state('app.appViews', {
            abstract: true,
            data: {
                title: 'App views'
            }
        })

        .state('app.appViews.projects', {
            url: '/projects',
            data: {
                title: 'Projects'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/project-list.html',
                    controller: 'ProjectsDemoCtrl',
                    resolve: {
                        projects: function($http, APP_CONFIG){
                            return $http.get(APP_CONFIG.apiRootUrl + '/project-list.json')
                        }
                    }
                }
            }, 
            resolve: {
                scripts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.datatables.js'
                    ]);
                }
            }
        })

        .state('app.appViews.blogDemo', {
            url: '/blog',
            data: {
                title: 'Blog'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/blog-demo.html'
                }
            }
        })

        .state('app.appViews.galleryDemo', {
            url: '/gallery',
            data: {
                title: 'Gallery'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/gallery-demo.html'
                }
            }, 
            resolve: {
                scripts: function(lazyScript){
                    return lazyScript.register([
                        'smartadmin-plugin/legacy/superbox/superbox.min.js'
                    ]);
                }
            }
        })

        .state('app.appViews.forumDemo', {
            url: '/forum',
            data: {
                title: 'Forum'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/forum-demo.html'
                }
            }
        })

        .state('app.appViews.forumTopicDemo', {
            url: '/forum-topic',
            data: {
                title: 'Forum Topic'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/forum-topic-demo.html'
                }
            }
        })

        .state('app.appViews.forumPostDemo', {
            url: '/forum-post',
            data: {
                title: 'Forum Post'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/forum-post-demo.html'
                }
            }, 
            resolve: {
                scripts: function(lazyScript){
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ]);
                }
            }
        })


        .state('app.appViews.profileDemo', {
            url: '/profile',
            data: {
                title: 'Profile'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/profile-demo.html'
                }
            }
        })


        .state('app.appViews.timelineDemo', {
            url: '/timeline',
            data: {
                title: 'Timeline'
            },
            views: {
                "content@app": {
                    templateUrl: 'app/app-views/views/timeline-demo.html'
                }
            }
        })
});
