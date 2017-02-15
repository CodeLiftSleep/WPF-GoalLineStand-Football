"use strict";

angular.module('app.ui').controller('GeneralElementsCtrl', function ($scope, $sce) {
    /*
     * Smart Notifications
     */
    $scope.eg1 = function () {

        $.bigBox({
            title: "Big Information box",
            content: "This message will dissapear in 6 seconds!",
            color: "#C46A69",
            //timeout: 6000,
            icon: "fa fa-warning shake animated",
            number: "1",
            timeout: 6000
        });
    };

    $scope.eg2 = function () {

        $.bigBox({
            title: "Big Information box",
            content: "Lorem ipsum dolor sit amet, test consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
            color: "#3276B1",
            //timeout: 8000,
            icon: "fa fa-bell swing animated",
            number: "2"
        });

    };

    $scope.eg3 = function () {

        $.bigBox({
            title: "Shield is up and running!",
            content: "Lorem ipsum dolor sit amet, test consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
            color: "#C79121",
            //timeout: 8000,
            icon: "fa fa-shield fadeInLeft animated",
            number: "3"
        });

    };

    $scope.eg4 = function () {

        $.bigBox({
            title: "Success Message Example",
            content: "Lorem ipsum dolor sit amet, test consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
            color: "#739E73",
            //timeout: 8000,
            icon: "fa fa-check",
            number: "4"
        }, function () {
            $scope.closedthis();
        });

    };


    $scope.eg5 = function() {

        $.smallBox({
            title: "Ding Dong!",
            content: "Someone's at the door...shall one get it sir? <p class='text-align-right'><a href-void class='btn btn-primary btn-sm'>Yes</a> <a href-void class='btn btn-danger btn-sm'>No</a></p>",
            color: "#296191",
            //timeout: 8000,
            icon: "fa fa-bell swing animated"
        });
    };


    $scope.eg6 = function() {

        $.smallBox({
            title: "Big Information box",
            content: "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
            color: "#5384AF",
            //timeout: 8000,
            icon: "fa fa-bell"
        });

    };

    $scope.eg7 = function() {

        $.smallBox({
            title: "James Simmons liked your comment",
            content: "<i class='fa fa-clock-o'></i> <i>2 seconds ago...</i>",
            color: "#296191",
            iconSmall: "fa fa-thumbs-up bounce animated",
            timeout: 4000
        });

    };

    $scope.closedthis = function() {
        $.smallBox({
            title: "Great! You just closed that last alert!",
            content: "This message will be gone in 5 seconds!",
            color: "#739E73",
            iconSmall: "fa fa-cloud",
            timeout: 5000
        });
    };

    /*
     * SmartAlerts
     */
    // With Callback
    $scope.smartModEg1 =  function () {
        $.SmartMessageBox({
            title: "Smart Alert!",
            content: "This is a confirmation box. Can be programmed for button callback",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $.smallBox({
                    title: "Callback function",
                    content: "<i class='fa fa-clock-o'></i> <i>You pressed Yes...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
            if (ButtonPressed === "No") {
                $.smallBox({
                    title: "Callback function",
                    content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }

        });
    };

    // With Input
    $scope.smartModEg2 =  function () {
        $.SmartMessageBox({
            title: "Smart Alert: Input",
            content: "Please enter your user name",
            buttons: "[Accept]",
            input: "text",
            placeholder: "Enter your user name"
        }, function (ButtonPress, Value) {
            alert(ButtonPress + " " + Value);
        });
    };

    // With Buttons
    $scope.smartModEg3 =  function () {
        $.SmartMessageBox({
            title: "Smart Notification: Buttons",
            content: "Lots of buttons to go...",
            buttons: '[Need?][You][Do][Buttons][Many][How]'
        });

    }
    // With Select
    $scope.smartModEg4 =  function () {
        $.SmartMessageBox({
            title: "Smart Alert: Select",
            content: "You can even create a group of options.",
            buttons: "[Done]",
            input: "select",
            options: "[Costa Rica][United States][Autralia][Spain]"
        }, function (ButtonPress, Value) {
            alert(ButtonPress + " " + Value);
        });

    };

    // With Login
    $scope.smartModEg5 =  function () {

        $.SmartMessageBox({
            title: "Login form",
            content: "Please enter your user name",
            buttons: "[Cancel][Accept]",
            input: "text",
            placeholder: "Enter your user name"
        }, function (ButtonPress, Value) {
            if (ButtonPress == "Cancel") {
                alert("Why did you cancel that? :(");
                return 0;
            }

            var Value1 = Value.toUpperCase();
            var ValueOriginal = Value;
            $.SmartMessageBox({
                title: "Hey! <strong>" + Value1 + ",</strong>",
                content: "And now please provide your password:",
                buttons: "[Login]",
                input: "password",
                placeholder: "Password"
            }, function (ButtonPress, Value) {
                alert("Username: " + ValueOriginal + " and your password is: " + Value);
            });
        });

    };

    $scope.tabsPopoverContent = $sce.trustAsHtml("<ul id='popup-tab' class='nav nav-tabs bordered'><li class='active'><a href='#pop-1' data-toggle='tab'>Active Tab </a></li><li><a href='#pop-2' data-toggle='tab'>Tab 2</a></li></ul><div id='popup-tab-content' class='tab-content padding-10'><div class='tab-pane fade in active' id='pop-1'><p>I have six locks on my door all in a row. When I go out, I lock every other one. I figure no matter how long somebody stands there picking the locks, they are always locking three.</p></div><div class='tab-pane fade' id='pop-2'><p>Food truck fixie locavore, accusamus mcsweeneys marfa nulla single-origin coffee squid. wes anderson artisan four loko farm-to-table craft beer twee.</p></div></div>")

    $scope.formPopoverContent = $sce.trustAsHtml("<form action='/api/plug' style='min-width:170px'><div class='checkbox'><label><input type='checkbox' class='checkbox style-0' checked='checked'><span>Read</span></label></div><div class='checkbox'><label><input type='checkbox' class='checkbox style-0'><span>Write</span></label></div><div class='checkbox'><label><input type='checkbox' class='checkbox style-0'><span>Execute</span></label></div><div class='form-actions'><div class='row'><div class='col-md-12'><button class='btn btn-primary btn-sm' type='submit'>SAVE</button></div></div></div></form>")

});