var _: any;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('settings', settings);

    function settings($q, ajaxService) {
        var settings: any = {};

        settings.getSettings = function(force = false) {
            var dfd = $q.defer();

            if (force || Object.keys(settings).length === 1) {
                return ajaxService.post("GlobalSettings", "GetSettings").then(function (response) {
                    _.merge(settings, JSON.parse(response));
                    dfd.resolve(settings);
                });
            } else {
                dfd.resolve(settings);
            }

            return dfd.promise;
        }

        settings.getSettings();

        return settings;
    }
} ((<any>window).angular));