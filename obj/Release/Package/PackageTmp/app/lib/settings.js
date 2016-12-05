var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('settings', settings);
    function settings($q, ajaxService) {
        var settings = {};
        settings.getSettings = function (force) {
            if (force === void 0) { force = false; }
            var dfd = $q.defer();
            if (force || Object.keys(settings).length === 1) {
                return ajaxService.post("GlobalSettings", "GetSettings").then(function (response) {
                    _.merge(settings, response);
                    dfd.resolve(settings);
                });
            }
            else {
                dfd.resolve(settings);
            }
            return dfd.promise;
        };
        settings.getSettings();
        return settings;
    }
}(window.angular));
