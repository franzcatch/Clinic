(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('householdService', householdService);
    function householdService($q, ajaxService, settings) {
        function getByUserId(userId) {
            var dfd = $q.defer();
            ajaxService.post("Household", "GetByUserId", { UserId: userId }).then(function (user) {
                settings.getSettings(true).then(function () {
                    dfd.resolve();
                });
            });
            return dfd.promise;
        }
        function update(household) {
            var dfd = $q.defer();
            ajaxService.post("Household", "Update", household).then(function (household) {
                settings.getSettings(true).then(function () {
                    dfd.resolve(household);
                });
            });
            return dfd.promise;
        }
        return {
            getByUserId: getByUserId,
            update: update
        };
    }
}(window.angular));
