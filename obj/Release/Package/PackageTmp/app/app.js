(function (angular) {
    'use strict';
    var clinic = angular.module('clinic', [
        'ngRoute'
    ]);
    clinic.config(routeConfig);
    function routeConfig($routeProvider) {
        $routeProvider
            .when('/home', {
            templateUrl: '/adamfranzen71/App/landing/landing.html',
            controller: 'LandingCtrl'
        })
            .otherwise('/home');
    }
}(window.angular));
