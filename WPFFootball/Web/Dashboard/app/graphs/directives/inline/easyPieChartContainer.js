'use strict';

angular.module('app.graphs').directive('easyPieChartContainer', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {
            /*
             * EASY PIE CHARTS
             * DEPENDENCY: js/plugins/easy-pie-chart/jquery.easy-pie-chart.min.js
             * Usage: <div class="easy-pie-chart txt-color-orangeDark" data-pie-percent="33" data-pie-size="72" data-size="72">
             *			<span class="percent percent-sign">35</span>
             * 	  	  </div>
             */

            if ($.fn.easyPieChart) {

                $('.easy-pie-chart').each(function() {
                    var $this = $(this),
                        barColor = $this.css('color') || $this.data('pie-color'),
                        trackColor = $this.data('pie-track-color') || 'rgba(0,0,0,0.04)',
                        size = parseInt($this.data('pie-size')) || 25;

                    $this.easyPieChart({

                        barColor : barColor,
                        trackColor : trackColor,
                        scaleColor : false,
                        lineCap : 'butt',
                        lineWidth : parseInt(size / 8.5),
                        animate : 1500,
                        rotate : -90,
                        size : size,
                        onStep: function(from, to, percent) {
                            $(this.el).find('.percent').text(Math.round(percent));
                        }

                    });

                    $this = null;
                });

            } // end if
        }
    }
});