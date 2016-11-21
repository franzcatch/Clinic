(function (angular) {
    'use strict';

    var clinic = angular.module('clinic', [
        'ngRoute',
        'ngAnimate',
        'ui.bootstrap',
        'ui.router'
    ]);

    clinic.config(routeConfig);

    function routeConfig($routeProvider: ng.route.IRouteProvider): void {
        $routeProvider
            .when('/', {
                templateUrl: 'app/landing/landing.html',
                controller: 'LandingCtrl'
            })
            .when('/home', {
                templateUrl: 'app/home/home.html',
                controller: 'HomeCtrl'
            })
            .otherwise('/');
    }
} ((<any>window).angular));