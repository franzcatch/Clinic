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
        $routeProvider
            .when('/', {
                name: 'landing',
                templateUrl: 'app/landing/landing.html',
                controller: 'LandingCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home', {
                name: 'home',
                templateUrl: 'app/home/home.html',
                controller: 'HomeCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/appointment', {
                name: 'appointment',
                templateUrl: 'app/home/appointment/appointment.html',
                controller: 'AppointmentCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/billing', {
                name: 'billing',
                templateUrl: 'app/home/billing/billing.html',
                controller: 'BillingCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/people', {
                name: 'people',
                templateUrl: 'app/home/people/people.html',
                controller: 'PeopleCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/clinics', {
                name: 'clinics',
                templateUrl: 'app/home/clinics/clinics.html',
                controller: 'ClinicsCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/profile', {
                name: 'profile',
                templateUrl: 'app/home/profile/profile.html',
                controller: 'ProfileCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/qualifications', {
                name: 'qualifications',
                templateUrl: 'app/home/qualifications/qualifications.html',
                controller: 'QualificationsCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/services', {
                name: 'services',
                templateUrl: 'app/home/services/services.html',
                controller: 'ServicesCtrl',
                resolve: {
                    factory: checkRouting
                }
            })
            .when('/home/reports', {
                name: 'reports',
                templateUrl: 'app/home/reports/reports.html',
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