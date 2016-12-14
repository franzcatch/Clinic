var _baseUrl;
(function (angular) {
    'use strict';

    var clinic = angular.module('clinic', [
        'ngRoute',
        'ngAnimate',
        'ngSanitize',
        'ui.bootstrap',
        'ui.router',
        'ui.select'
    ]);

    clinic.config(routeConfig);

    function routeConfig($routeProvider: ng.route.IRouteProvider): void {
        var base = 'adamfranzen71';

        $routeProvider
            .when('/', {
                name: 'landing',
                templateUrl: _baseUrl + 'app/landing/landing.html',
                controller: 'LandingCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home', {
                name: 'home',
                templateUrl: _baseUrl + 'app/home/home.html',
                controller: 'HomeCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/appointment', {
                name: 'appointment',
                templateUrl: _baseUrl + 'app/home/appointment/appointment.html',
                controller: 'AppointmentCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/billing', {
                name: 'billing',
                templateUrl: _baseUrl + 'app/home/billing/billing.html',
                controller: 'BillingCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/people', {
                name: 'people',
                templateUrl: _baseUrl + 'app/home/people/people.html',
                controller: 'PeopleCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/clinics', {
                name: 'clinics',
                templateUrl: _baseUrl + 'app/home/clinics/clinics.html',
                controller: 'ClinicsCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/profile', {
                name: 'profile',
                templateUrl: _baseUrl + 'app/home/profile/profile.html',
                controller: 'ProfileCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/qualifications', {
                name: 'qualifications',
                templateUrl: _baseUrl + 'app/home/qualifications/qualifications.html',
                controller: 'QualificationsCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/services', {
                name: 'services',
                templateUrl: _baseUrl + 'app/home/services/services.html',
                controller: 'ServicesCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/reports', {
                name: 'reports',
                templateUrl: _baseUrl + 'app/home/reports/reports.html',
                controller: 'ReportsCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .otherwise('/');
    }

    function checkRouting($q, settings, $route, $location) {
        settings.clearTemp();

        var dfd = $q.defer();

        settings.getSettings(true).then(function () {
            if (!settings.User && $route.current.$$route.name !== 'landing') {
                $location.path('/');
            } else if (settings.User && $route.current.$$route.name === 'landing') {
                $location.path('/home');
            }
            dfd.resolve();
        });

        return dfd.promise;
    }
} ((<any>window).angular));