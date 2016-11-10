var clinic = angular.module('clinic', ['ngRoute']);

clinic.config(routeConfig);

routeConfig.$inject = ['$routeProvider'];

function routeConfig($routeProvider: ng.route.IRouteProvider): void {
    $routeProvider
        .when('/home', {
            templateUrl: '/app/landing/landing.html',
            controller: 'LandingCtrl'
        })
        .otherwise({ redirectTo: '/home' });
}