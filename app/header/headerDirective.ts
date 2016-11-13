﻿(function (angular) {
    'use strict';

    //angular.module('clinic')
    //    .factory('appNav', [appNav]);

    angular.module('clinic').directive('appNav', appNav);

    function appNav() {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: '/app/header/header.html',
            controller: 'HeaderCtrl'
        };
    };
}((<any>window).angular));