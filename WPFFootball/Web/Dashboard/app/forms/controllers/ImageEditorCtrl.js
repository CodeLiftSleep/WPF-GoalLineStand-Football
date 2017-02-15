"use strict";


angular.module('app.forms').controller('ImageEditorCtrl', function ($scope) {

    // api tab
    $scope.apiDemoSelection = [100, 100, 400, 300];

    $scope.apiDemoOptions = {
        allowSelect: true,
        allowResize: true,
        allowMove: true,
        animate: false
    };

    $scope.apiRandomSelection = function () {
        $scope.apiDemoOptions.animate = false;
        $scope.apiDemoSelection = [
            Math.round(Math.random() * 600),
            Math.round(Math.random() * 400),
            Math.round(Math.random() * 600),
            Math.round(Math.random() * 400)
        ]
    };

    $scope.apiRandomAnimation = function () {
        $scope.apiDemoOptions.animate = true;
        $scope.apiDemoSelection = [
            Math.round(Math.random() * 600),
            Math.round(Math.random() * 400),
            Math.round(Math.random() * 600),
            Math.round(Math.random() * 400)
        ]
    };

    $scope.apiReleaseSelection = function () {
        $scope.apiDemoOptions.animate = true;
        $scope.apiDemoSelection = 'release';
    };


    $scope.apiToggleDisable = function () {
        $scope.apiDemoOptions.disabled = !$scope.apiDemoOptions.disabled;
    };

    $scope.apiToggleDestroy = function () {
        $scope.apiDemoOptions.destroyed = !$scope.apiDemoOptions.destroyed;
    };

    $scope.apiDemoShowAspect = false;
    $scope.apiDemoToggleAspect = function () {
        $scope.apiDemoShowAspect = !$scope.apiDemoShowAspect;
        if ($scope.apiDemoShowAspect)
            $scope.apiDemoOptions.aspectRatio = 4 / 3;
        else
            $scope.apiDemoOptions.aspectRatio = 0;
    };

    $scope.apiDemoShowSizeRestrict = false;
    $scope.apiDemoToggleSizeRestrict = function () {
        $scope.apiDemoShowSizeRestrict = !$scope.apiDemoShowSizeRestrict;
        if ($scope.apiDemoShowSizeRestrict) {
            $scope.apiDemoOptions.minSizeWidth = 80;
            $scope.apiDemoOptions.minSizeHeight = 80;
            $scope.apiDemoOptions.maxSizeWidth = 350;
            $scope.apiDemoOptions.maxSizeHeight = 350;
        } else {
            $scope.apiDemoOptions.minSizeWidth = 0;
            $scope.apiDemoOptions.minSizeHeight = 0;
            $scope.apiDemoOptions.maxSizeWidth = 0;
            $scope.apiDemoOptions.maxSizeHeight = 0;
        }

    };


    $scope.setApiDemoImage = function (image) {
        $scope.apiDemoImage = image;
        $scope.apiDemoOptions.src = image.src;
        $scope.apiDemoOptions.bgOpacity = image.bgOpacity;
        $scope.apiDemoOptions.outerImage = image.outerImage;
        $scope.apiRandomAnimation();
    };

    $scope.apiDemoImages = [
        {
            name: 'Lego',
            src: 'styles/img/superbox/superbox-full-24.jpg',
            bgOpacity: .6
        },
        {
            name: 'Breakdance',
            src: 'styles/img/superbox/superbox-full-7.jpg',
            bgOpacity: .6
        },
        {
            name: 'Dragon Fly',
            src: 'styles/img/superbox/superbox-full-20.jpg',
            bgOpacity: 1,
            outerImage: 'styles/img/superbox/superbox-full-20-bw.jpg'
        }
    ];

    $scope.apiDemoImage = $scope.apiDemoImages[1];

    // animations tab
    $scope.animationsDemoOptions = {
        bgOpacity: undefined,
        bgColor: undefined,
        bgFade: true,
        shade: false,
        animate: true
    };
    $scope.animationsDemoSelection = undefined;
    $scope.selections = {
        1: [217, 122, 382, 284],
        2: [20, 20, 580, 380],
        3: [24, 24, 176, 376],
        4: [347, 165, 550, 355],
        5: [136, 55, 472, 183],
        Release: 'release'
    };

    $scope.opacities = {
        Low: .2,
        Mid: .5,
        High: .8,
        Full: 1
    };

    $scope.colors = {
        R: '#900',
        B: '#4BB6F0',
        Y: '#F0B207',
        G: '#46B81C',
        W: 'white',
        K: 'black'
    };


    // styling tab

    $scope.styles = [
        {
            name: 'jcrop-light',
            bgFade: true,
            animate: true,
            selection: [130, 65, 130 + 350, 65 + 285],
            bgColor: 'white',
            bgOpacity: 0.5
        },
        {
            name: 'jcrop-dark',
            bgFade: true,
            animate: true,
            selection: [130, 65, 130 + 350, 65 + 285],
            bgColor: 'black',
            bgOpacity: 0.4
        },
        {
            name: 'jcrop-normal',
            bgFade: true,
            animate: true,
            selection: [130, 65, 130 + 350, 65 + 285],
            bgColor: 'black',
            bgOpacity: 0.6
        }
    ];

    $scope.demoStyle = $scope.styles[0]
});