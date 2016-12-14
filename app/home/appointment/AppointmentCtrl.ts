var _;
var _baseUrl;
(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('AppointmentCtrl', AppointmentCtrl);

    function AppointmentCtrl($scope, $uibModal, clinicService, appointmentService, settings) {
        $scope.isUser = settings.User.Role.Name === "User";
        $scope.isLoading = true;
        $scope.clinics = [];
        $scope.selectedClinic = {};
        $scope.appointments = [];
        $scope.serviceDate = $scope.isUser ? null : new Date();

        function init() {
            clinicService.getAll().then(function (clinics) {
                $scope.clinics = clinics;
                $scope.setClinic(clinics[0]);
            });
        }

        $scope.setClinic = function (clinic) {
            $scope.isLoading = true;
            $scope.selectedClinic = clinic;

            doSearch();
        };

        $scope.clearDate = function () {
            $scope.serviceDate = null;
            doSearch();
        };

        $scope.getStartTime = function (appointment) {
            return appointment.AppointmentServices[0].StartTimeString;
        };

        $scope.getServicesString = function (appointment) {
            var services = _.map(appointment.AppointmentServices, function (aptSvc) {
                return aptSvc.Service.Name;
            });
            return _.join(services, ', ');
        }; 

        $scope.getProvidersString = function (appointment) {
            var rooms = _.map(appointment.AppointmentServices, function (aptSvc) {
                return aptSvc.Provider.FirstName + ' ' + aptSvc.Provider.LastName;
            });
            return _.join(rooms, ', ');
        };

        $scope.getRoomsString = function (appointment) {
            var rooms = _.map(appointment.AppointmentServices, function (aptSvc) {
                return aptSvc.Room.Name;
            });
            return _.join(rooms, ', ');
        };

        $scope.getTotalTime = function (appointment) {
            var total = 0;
            _.each(appointment.AppointmentServices, function (apsSvc) {
                total += apsSvc.Service.Minutes;
            });
            return total
        }; 

        $scope.getTotalPrice = function (appointment) {
            var total = 0;
            _.each(appointment.AppointmentServices, function (apsSvc) {
                total += apsSvc.Service.Cost;
            });
            return total
        }; 

        function doSearch() {
            $scope.isLoading = true;
            if ($scope.isUser) {
                appointmentService.getAppointmentsForUser(settings.User.Id, $scope.serviceDate).then(function (appointments) {
                    $scope.appointments = _.uniqBy(appointments, function (apt) {
                        return apt.Id;
                    });;
                    $scope.isLoading = false;
                });
            } else {
                appointmentService.getAppointmentsForClinic($scope.selectedClinic.Id, $scope.serviceDate).then(function (appointments) {
                    $scope.appointments = _.uniqBy(appointments, function (apt) {
                        return apt.Id;
                    });
                    $scope.isLoading = false;
                });
            }
        }

        $scope.openAppointment = function (origAppointment) {
            var modal = $uibModal.open({
                templateUrl: _baseUrl + 'app/home/appointment/manage/newAppointment.html',
                controller: 'NewAppointmentCtrl',
                size: 'appointment-modal-size',
                resolve: {
                    params: function () {
                        return {
                            appointment: origAppointment,
                            clinic: $scope.selectedClinic,
                            submit: function (appointment) {
                                return appointmentService.createAppointment(appointment).then(function (appointment) {
                                    if (appointment.Id) {
                                        $scope.appointments.push(appointment);
                                        modal.close();
                                    }
                                });
                            },
                            close: function () {
                                modal.close();
                            }
                        };
                    }
                }
            });
        };

        $scope.delete = function (appointment) {
            $scope.isLoading = true;
            appointmentService.deleteAppointment(appointment).then(function () {
                _.remove($scope.appointments, appointment);
                $scope.isLoading = false;
            });
        };

        init();
    }
} ((<any>window).angular));