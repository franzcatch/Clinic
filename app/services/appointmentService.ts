(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('clinicService', clinicService);

    function clinicService($q, ajaxService, settings) {
        function getAppointmentsForClinic(clinicId) {

        }

        function getAppointmentsForUser(userId) {

        }

        function getAvailableTimesForDate(date) {

        }

        function searchHouseholdsByPayerName(name) {

        }

        function updateAppointment(appointment) {

        }

        function deleteAppointment(appointment) {

        }



        return {
            getAll: getAll,
            update: update,
            deleteClinic: deleteClinic,
            getEligibleProviders: getEligibleProviders,
            getAllServices: getAllServices,
            getRooms: getRooms
        };
    }
} ((<any>window).angular));