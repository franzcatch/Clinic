(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('appointmentService', appointmentService);
    function appointmentService($q, ajaxService, settings) {
        function getAppointmentsForClinic(clinicId) {
        }
        function getAppointmentsForUser(userId) {
        }
        function getAvailableTimesAndProvidersForService(clinicId, serviceId, date) {
            var params = {
                ClinicId: clinicId,
                ServiceId: serviceId,
                ServiceDate: date
            };
            var dfd = $q.defer();
            ajaxService.post("Appointment", "GetAvailableTimesAndProvidersForService", params).then(function (results) {
                dfd.resolve(results);
            });
            return dfd.promise;
        }
        function updateAppointment(appointment) {
        }
        function deleteAppointment(appointment) {
        }
        return {
            getAvailableTimesAndProvidersForService: getAvailableTimesAndProvidersForService
        };
    }
}(window.angular));
