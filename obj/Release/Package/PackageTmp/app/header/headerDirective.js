var _baseUrl;
(function (angular) {
    'use strict';
    angular.module('clinic').directive('appNav', appNav);
    function appNav() {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: _baseUrl + 'app/header/header.html',
            controller: 'HeaderCtrl'
        };
    }
    ;
}(window.angular));
