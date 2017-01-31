(function () {
    'use strict';
angular
.module('formly_templates', ['ui.mask'])

.run(function(formlyConfig) { //UI MASK code
    formlyConfig.setType({
      name: 'maskedInput',
      extends: 'input',
      template: '<input class="form-control" ng-model="model[options.key]" />',
      defaultOptions: {
        ngModelAttrs: {
          mask: {
            attribute: 'ui-mask'
          },
          maskPlaceholder: {
            attribute: 'ui-mask-placeholder'
          }
        },
        templateOptions: {
          maskPlaceholder: ''
        }
      }
    });
  })

.config(function(formlyConfigProvider) {
    formlyConfigProvider.setWrapper([{
        name:'panel', //creates a primary panel header
        template: `<div class="panel panel-primary">               
                        <div class="panel-heading px-nested-panel-heading clearfix">
                            <strong class="control-label" ng-if="options.templateOptions.label">
                                {{options.templateOptions.label}}
                            </strong>
                        </div>
                        
                            <div class="panel-body px-nested-panel-body">
                                <formly-transclude></formly-transclude>
                            </div>
                        
                    </div>`
    },
    {
        name: 'well',
        template: `<div class="well">
                        <formly-transclude></formly-transclude>
                   </div>`
    }]);

    formlyConfigProvider.setType([
        {   
            name: 'toggleSwitch',
            template: `<label class="switch-light" onclick="">
                            <input type="checkbox" style="{{to.margin}}" class="check {{to.pull}}" id="toggle{{to.toggle}}" 
                                name="toggle{{to.toggle}}" ng-model="checked" ng-click="model[options.key || index] = checked" 
                                ng-init="checked = to.checked" ng-class="{'col-md-6': !selected, 'pull-right': selected}" checked/>
                                <strong style="{{to.align}}; margin-bottom: 10px;" >{{to.label}}</strong>
                                <span class="well">
                                <span>No</span>
                                <span>Yes</span>
                                    <a class="btn btn-primary"></a>
                                </span>
                       </label>`,
            controller: function($scope) {
                $scope.model[$scope.options.key] = $scope.to.checked; //sets the initial value of the model
            }
        },
        {
            name:'basicLabel',
            template: '<strong style="font-size:{{to.size}};text-align:{{to.align}}; margin-left:{{to.leftMargin}}">{{to.label}}</strong>'
        },
        {
            name:'nya-bootstrapSelect',
            template: ` <div>{{to.label}}</div>
                        <ol class="nya-bs-select btn-primary" style="{{to.margin}};"
   						ng-model="model[options.key || index]">   
                            <li nya-bs-option="option in to.options" value="option.value"> 
                                <a> <span>{{option.value}}</span> <span class="glyphicon glyphicon-ok check-mark"></span></a>
                            </li> 
                        </ol>`
        },
        {
            name:'vertSlider',
            template: [`<rzslider class="custom-slider" rz-slider-model="to.sliderOptions.value" 
                         rz-slider-options="to.sliderOptions"></rzslider>`].join(''),
            wrapper: ['bootstrapLabel', 'bootstrapHasError']
        },
        {
            name: '3DCarousel',
            template: `<carousel ng-model="vm.carousel" sides="to.options"</carousel>`
        },
        {
            name: 'leagueType',
            template: `<h4><b>Please select the type of league you'd like to create:</b></h4>                  
                    <div class="row">
                        <div class="col-md-5"></div>
                        <a><button type="submit" class="btn btn-lg btn-primary career hvr-shadow-radial pull-left" tooltip-placement="left" 
                            ng-click="model.Mode = 'career'; Update(model.Mode)" ui-sref=".career" 
                            uib-tooltip="Career Mode allows unlimited seasons and is the most in-depth mode. Drafts, retirements, hall of fame inductions, and much more happen.">
                            Career Simulation</button></a>                              
                    </div>
                    <hr/>
                    <div class="row">    
                        <div class="col-md-5"></div>
                        <a><button type="submit" class="btn btn-lg btn-primary single hvr-shadow-radial pull-left" tooltip-placement="left" 
                        ng-click="model.Mode ='single'; Update(model.Mode)" ui-sref-active="active"ui-sref=".single" 
                        uib-tooltip="Single Season Mode allows you take control of your favorite team for a season. How good of a GM are you?  Can you win the Super Bowl?">Single Season</button></a>          
                    </div>
                    <hr/>
                    <div class="row">
                        <div class="col-md-5"></div>
                        <a><button class="btn btn-lg btn-primary quick hvr-shadow-radial pull-left" tooltip-placement="left" ng-click="model.Mode = 'quick'; Update(model.Mode)" 
                        ui-sref-active="active" ui-sref=".quick"
                        uib-tooltip="Quick Game Mode allows you to choose two teams and begin playing a regular season game.">Quick Game</button></a>
                    </div>
                        <hr/>
                    <div class="row">
                        <div class="col-md-5"></div>
                        <a><button class="btn btn-lg btn-primary situation hvr-shadow-radial pull-left" tooltip-placement="left" ng-click="model.Mode='situation'" ui-sref-active="active" ui-sref=".situation"
                        uib-tooltip="Re-live some of the greatest situations in football history. The game hangs in the balance.  Can you change history or will you be doomed to repeat it?">Situational Drills</button></a>                       
                    </div>                    
                    </div>`           
        }

        ]);

    });
})();