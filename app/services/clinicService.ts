(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('clinicService', clinicService);

    function clinicService($q, ajaxService, settings) {
        function getAll() {
            var dfd = $q.defer();
            ajaxService.post("Clinic", "GetAll").then(function (clinics) {
                dfd.resolve(clinics);
            });

            return dfd.promise;
        }

        function update(data) {
            var dfd = $q.defer();
            ajaxService.post("Clinic", "Update", data).then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function deleteClinic(id) {
            var dfd = $q.defer();
            ajaxService.post("Clinic", "Delete", { Id: id }).then(function () {
                dfd.resolve();
            });

            return dfd.promise;
        }

        function getEligibleProviders(clinicId) {
            var dfd = $q.defer();
            ajaxService.post("Clinic", "GetEligibleProviders", { Id: clinicId }).then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getRooms(clinicId) {
            var dfd = $q.defer();
            ajaxService.post("Clinic", "GetRooms", { Id: clinicId }).then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        return {
            getAll: getAll,
            update: update,
            deleteClinic: deleteClinic,
            getEligibleProviders: getEligibleProviders,
            getRooms: getRooms
        };
    }
} ((<any>window).angular));