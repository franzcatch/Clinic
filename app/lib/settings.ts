var _: any;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('settings', settings);

    function settings($q, ajaxService, $location) {
        var settings: any = {};
        settings.temp = {};

        settings.getSettings = function(force = false) {
            var dfd = $q.defer();

            if (force || Object.keys(settings).length === 3) {
                return ajaxService.post("GlobalSettings", "GetSettings").then(function (response) {
                    _.merge(settings, response);

                    settings.isDev = $location.absUrl().indexOf('localhost') ? true : false;

                    dfd.resolve(settings);
                });
            } else {
                dfd.resolve(settings);
            }

            return dfd.promise;
        }

        settings.clearTemp = function () {
            if (settings) {
                settings.temp = {};
            }
        };

        settings.getSettings();

        return settings;
    }
} ((<any>window).angular));