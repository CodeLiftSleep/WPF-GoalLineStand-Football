(function () {

    'use strict';

    angular
        .module('angular-3d-carousel', [])
        .directive('carousel', carousel);

    function carousel() {
        return {
            restrict: 'E',
            template: '' +
            '<div id="carousel" style="{{ styles.carousel }}">' +
            '<figure ng-repeat="side in sides track by $index" style="{{ styles.figures[$index] }}">' +
            '<img ng-src="{{ side.image }}">' +
            '<div>' +
            '<h2>{{ side.title }}</h2>' +
            '<hr>' +
            '<ul>' +
            '<li ng-repeat="li in side.listItems track by $index">{{ li }}</li>' +
            '</ul>' +
            '</div>' +
            '</figure>' +
            '</div>',
            replace: true,
            scope: {
                sides: '=',
                current: '='
            },
            link: function (scope, element, attributes) {
                var position;
                var time;
                var rotation = {
                    c: 0
                };
                var properties;
                var calculate;
                var isMouseDown;
                var requestID;

                init();

                scope.$watch('sides', function () {
                    init();
                    rotation.c = 0;
                }, true);

                var carousel = element[0];

                window.addEventListener('mousedown', onStart);
                window.addEventListener('mousemove', onMove);
                window.addEventListener('mouseup', onEnd);

                window.addEventListener('touchstart', onStart);
                window.addEventListener('touchmove', onMove);
                window.addEventListener('touchend', onEnd);

                function onStart(e) {
                    init();

                    isMouseDown = true;

                    position.c = typeof e.clientX === 'undefined' ? e.touches[0].clientX : e.clientX;
                    position.p = position.c;
                    time.i = performance.now();
                }

                function onMove(e) {
                    e.preventDefault();

                    if (isMouseDown) {

                        if (time.c === null) {
                            time.c = performance.now();
                            position.c = (typeof e.clientX === 'undefined' ? e.touches[0].clientX : e.clientX) / 2;
                            return;
                        }

                        var now = performance.now();
                        var dt = now - time.c;
                        var dx = (typeof e.clientX === 'undefined' ? e.touches[0].clientX - position.c : e.clientX - position.c) / 2;
                        properties.velocities.push(dx / dt * 100);

                        time.c = now;
                        position.c = (typeof e.clientX === 'undefined' ? e.touches[0].clientX : e.clientX) / 2;

                        carousel.removeAttribute('style');

                        position.c = typeof e.clientX === 'undefined' ? e.touches[0].clientX : e.clientX;
                        rotation.c -= ((position.p - position.c) / (scope.sides.length * 2));
                        position.p = position.c;

                        var css = 'transform: translate3d(0, 0, -' + properties.radius + 'vw) rotateY(' + rotation.c + 'deg);';

                        carousel.setAttribute(
                            'style', css
                        );
                    }
                }

                function onEnd(e) {
                    isMouseDown = false;

                    position.f = (typeof e.clientX === 'undefined' ? e.changedTouches[0].clientX : e.clientX) / 2;

                    time.f = performance.now();

                    calculate.angularVelocity();
                }

                function angularVelocity() {
                    if (properties.velocities.length < 3) {
                        return;
                    }

                    removeValueFromArray(properties.velocities, 0);

                    var tail = properties.velocities.slice(properties.velocities.length - 3);
                    properties.angularVelocity = tail.reduce(add, 0) / tail.length;

                    if (properties.angularVelocity > 500) {
                        properties.angularVelocity = 500;
                    }

                    if (properties.angularVelocity < -500) {
                        properties.angularVelocity = -500;
                    }

                    predict();
                }

                function predict() {
                    if (properties.angularVelocity < 0) {
                        predict.iterations = Math.floor(properties.angularVelocity);
                    } else {
                        predict.iterations = Math.ceil(properties.angularVelocity);
                    }

                    predict.angularVelocity = properties.angularVelocity;
                    predict.rotation = rotation.c;

                    for (var i = Math.abs(predict.iterations); i > 0; i--) {
                        if (properties.angularVelocity < 0) {
                            predict.rotation += ((predict.angularVelocity + i) / 60);
                        } else {
                            predict.rotation += ((predict.angularVelocity - i) / 60);
                        }
                    }

                    predict.rounded = Math.round(predict.rotation / properties.degreesPerSide) * properties.degreesPerSide;
                    predict.offset = (predict.rotation - predict.rounded) / predict.iterations;

                    requestAnimationFrame(assignVelocity);
                }

                function friction() {
                    if (Math.abs(properties.angularVelocity) > 1) {
                        if (properties.angularVelocity < 0) {
                            properties.angularVelocity++;
                        } else {
                            properties.angularVelocity--;
                        }
                    } else {
                        properties.angularVelocity = 0;
                        cancelAnimationFrame(requestID);

                        var currentSide;

                        if (rotation.c < 0) {
                            currentSide = (Math.round(Math.abs(rotation.c)) / properties.degreesPerSide) % scope.sides.length;
                        } else {
                            currentSide = (scope.sides.length - ((Math.round(rotation.c) / properties.degreesPerSide)) % scope.sides.length) % scope.sides.length;
                        }

                        currentSide = Math.round(currentSide);

                        //alert('You got ' + scope.sides[currentSide].title);

                        scope.$apply(function () {
                            scope.current = currentSide;
                        });
                    }

                    return properties.angularVelocity / 60;
                }

                function assignVelocity() {
                    requestID = requestAnimationFrame(assignVelocity);

                    carousel.removeAttribute('style');

                    if (properties.angularVelocity < 0) {
                        rotation.c += predict.offset;
                    } else {
                        rotation.c -= predict.offset;
                    }

                    rotation.c += friction();

                    var css = 'transform: translate3d(0, 0, -' + properties.radius + 'vw) rotateY(' + rotation.c + 'deg);';

                    carousel.setAttribute(
                        'style', css
                    );
                }

                function init() {
                    /**
                     * i: initial
                     * f: final
                     * c: current
                     * p: previous
                     */

                    /**
                     * Position of user's action
                     */
                    position = {
                        i: null,
                        f: null,
                        c: null,
                        p: null
                    };

                    /**
                     * Time of user's action
                     */
                    time = {
                        i: null,
                        f: null,
                        c: null,
                        p: null
                    };

                    properties = {
                        positions: [],
                        velocities: [],
                        angularVelocity: null,
                        radius: (25 / (Math.tan((180 / scope.sides.length) * ((Math.PI / 180))))),
                        degreesPerSide: 360 / scope.sides.length
                    };

                    calculate = {
                        angularVelocity: angularVelocity
                    };

                    isMouseDown = false;

                    requestID;

                    scope.styles = {
                        carousel: 'transform: translate3d(0, 0, -' + properties.radius + 'vw)',
                        figures: []
                    };

                    document.querySelector('html').style.perspective = (500 / scope.sides.length) + 'vw';
                    document.querySelector('body').style.perspective = (500 / scope.sides.length) + 'vw';

                    angular.forEach(scope.sides, function (side, index) {
                        scope.styles.figures[index] = 'transform: rotate3d(0, 1, 0, ' + (360 * index / scope.sides.length) + 'deg ) translate3d(0, 0, ' + properties.radius + 'vw)'
                    });

                    cancelAnimationFrame(requestID);
                }

                function removeValueFromArray(array, value) {
                    array.forEach(function (v, i) {
                        if (v === value) {
                            array.splice(i, 1);
                            removeValueFromArray(array, value);
                        }
                    });
                }

                function add(a, b) {
                    return a + b;
                }
            }
        };
    }
})();