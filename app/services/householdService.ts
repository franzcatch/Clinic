(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('householdService', householdService);

    function householdService($q, ajaxService, settings) {
        function get(id) {
            var dfd = $q.defer();
            ajaxService.post("Household", "Get", { Id: id }).then(function (household) {
                dfd.resolve(household);
            });

            return dfd.promise;
        }

        function getByUserId(userId) {
            var dfd = $q.defer();
            ajaxService.post("Household", "GetByUserId", { UserId: userId }).then(function (household) {
                dfd.resolve(household);
            });

            return dfd.promise;
        }

        function getByPayerName(params) {
            var dfd = $q.defer();
            ajaxService.post("Household", "GetByPayerName", params).then(function (results) {
                dfd.resolve(results);
            });

            return dfd.promise;
        }

        function update(household) {
            var dfd = $q.defer();
            ajaxService.post("Household", "Update", household).then(function (household) {
                dfd.resolve(household);
            });

            return dfd.promise;
        }

        function getRelationships(household) {
            var dfd = $q.defer();
            ajaxService.post("Household", "GetRelationships", household).then(function (household) {
                dfd.resolve(household);
            });

            return dfd.promise;
        }

        return {
            get: get,
            getByUserId: getByUserId,
            getByPayerName: getByPayerName,
            update: update,
            getRelationships: getRelationships
        };
    }
} ((<any>window).angular));