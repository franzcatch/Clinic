var _;
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
            var appt = _.clone(appointment);
            appt.Person.DateOfBirth = new Date(appt.Person.DateOfBirthString);
            _.each(appt.AppointmentServices, function (svc) {
                delete svc.Id;
            });
            var dfd = $q.defer();
            ajaxService.post("Appointment", "Create", appt).then(function (result) {
                dfd.resolve(result);
            });
            return dfd.promise;
        }
        function deleteAppointment(appointment) {
            var appt = _.clone(appointment);
            appt.Person.DateOfBirth = new Date(appt.Person.DateOfBirthString);
            _.each(appt.AppointmentServices, function (svc) {
                svc.StartTime = new Date(svc.StartTimeString);
            });
            var dfd = $q.defer();
            ajaxService.post("Appointment", "Delete", appt).then(function (results) {
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
