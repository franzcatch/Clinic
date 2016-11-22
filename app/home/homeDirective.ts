(function (angular) {
    'use strict';

    angular.module('clinic').directive('homeNav', homeNav);

    function homeNav() {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: 'app/home/home.html',
            controller: 'HomeCtrl',
            link: function (scope, elem, attr) {
                scope.isNavMode = true;
            }
        };
    };
}((<any>window).angular));