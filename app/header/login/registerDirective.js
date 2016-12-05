(function (angular) {
    'use strict';
    angular.module('clinic').directive('register', registerDirective);
    function registerDirective(settings) {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: 'app/header/login/register.html',
            controller: 'RegisterCtrl'
        };
    }
    ;
}(window.angular));
