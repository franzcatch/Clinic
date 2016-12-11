(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('appointmentService', appointmentService);
    function appointmentService($q, ajaxService, settings) {
        function getAppointmentsForClinic(clinicId, date) {
            var params = {
                Id: clinicId,
                ServiceDate: date
            };
            var dfd = $q.defer();
            ajaxService.post("Appointment", "GetAppointmentsForClinic", params).then(function (results) {
                dfd.resolve(results);
            });
            return dfd.promise;
        }
        function getAppointmentsForUser(userId, date) {
            var params = {
                Id: userId,
                ServiceDate: date
            };
            var dfd = $q.defer();
            ajaxService.post("Appointment", "GetAppointmentsForUser", params).then(function (results) {
                dfd.resolve(results);
            });
            return dfd.promise;
        }
        function getAvailableAppointments(clinicId, serviceId, date) {
            var params = {
                ClinicId: clinicId,
                ServiceId: serviceId,
                ServiceDate: date
            };
            var dfd = $q.defer();
            ajaxService.post("Appointment", "GetAvailableAppointments", params).then(function (results) {
                dfd.resolve(results);
            });
            return dfd.promise;
        }
        function createAppointment(appointment) {
            var dfd = $q.defer();
            ajaxService.post("Appointment", "Create", appointment).then(function (results) {
                dfd.resolve(results);
            });
            return dfd.promise;
        }
        function deleteAppointment(appointment) {
            var dfd = $q.defer();
            ajaxService.post("Appointment", "Delete", appointment).then(function (results) {
                dfd.resolve(results);
            });
            return dfd.promise;
        }
        return {
            getAppointmentsForClinic: getAppointmentsForClinic,
            getAppointmentsForUser: getAppointmentsForUser,
            getAvailableAppointments: getAvailableAppointments,
            createAppointment: createAppointment,
            deleteAppointment: deleteAppointment
        };
    }
}(window.angular));
