var _: any;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('settingsService', settingsService);

    function settingsService($q, ajaxService, userService) {
        var settings: any = {};

        function getSettings(force = false) {
            if (force || !Object.keys(settings).length) {
                return ajaxService.post("GlobalSettings", "GetSettings").then(function (response) {
                    _.merge(settings, JSON.parse(response));
                    if (!userService.curUser.Role) {
                        _.merge(userService.curUser, settings.LoggedInUser);
                    }
                });
            }
        }

        getSettings();

        return {
            settings: settings
        };
    }
} ((<any>window).angular));