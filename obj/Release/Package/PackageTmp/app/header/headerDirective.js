(function (angular) {
    'use strict';
    //angular.module('clinic')
    //    .factory('appNav', [appNav]);
    angular.module('clinic').directive('appNav', appNav);
    function appNav() {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: '/adamfranzen71/App/header/header.html',
            controller: 'HeaderCtrl'
        };
    }
    ;
}(window.angular));
