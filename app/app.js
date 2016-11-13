(function (angular) {
    'use strict';
    var clinic = angular.module('clinic', [
        'ngRoute'
    ]);
    clinic.config(routeConfig);
    //routeConfig.$inject = ['$routeProvider'];
    function routeConfig($routeProvider) {
        $routeProvider
            .when('/home', {
            templateUrl: '/app/landing/landing.html',
            controller: 'LandingCtrl'
        })
            .otherwise('/home');
    }
    //clinic.config(function ($stateProvider, $urlRouterProvider) {
    //    $urlRouterProvider.otherwise('/');
    //    $stateProvider
    //        .state('home', {
    //            url: '/',
    //            templateUrl: '/app/landing/landing.html',
    //            //controller: 'LandingCtrl'
    //        });
    //});
    //clinic.run();
}(window.angular));
