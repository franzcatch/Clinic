(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('reportService', reportService);

    function reportService($q, ajaxService, settings) {
        function generateAllHouseholdReport() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GenerateAllHouseholdReport").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getHouseholdAndInsurance() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetHouseholdAndInsurance").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getAllPatientsAndInsurance() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetAllPatientsAndInsurance").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getAllBilling() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetAllBilling").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getHouseholdTotalCosts() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetHouseholdTotalCosts").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getProvidersAndServices() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetProvidersAndServices").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getServicesAndProviders() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetServicesAndProviders").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getFutureAppointmentsByPatient() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetFutureAppointmentsByPatient").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getAllServicesProvided() {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetAllServicesProvided").then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        function getTotalServicesForProviders(serviceDate) {
            var dfd = $q.defer();
            ajaxService.post("Report", "GetTotalServicesForProviders", { Date: serviceDate }).then(function (response) {
                dfd.resolve(response);
            });

            return dfd.promise;
        }

        return {
            generateAllHouseholdReport: generateAllHouseholdReport,
            getHouseholdAndInsurance: getHouseholdAndInsurance,
            getAllPatientsAndInsurance: getAllPatientsAndInsurance,
            getAllBilling: getAllBilling,
            getHouseholdTotalCosts: getHouseholdTotalCosts,
            getProvidersAndServices: getProvidersAndServices,
            getServicesAndProviders: getServicesAndProviders,
            getFutureAppointmentsByPatient: getFutureAppointmentsByPatient,
            getAllServicesProvided: getAllServicesProvided,
            getTotalServicesForProviders: getTotalServicesForProviders
        };
    }
} ((<any>window).angular));