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
        function doSearch() {
            $scope.isLoading = true;
            if ($scope.isUser) {
                appointmentService.getAppointmentsForUser($scope.settings.User.Id, $scope.serviceDate).then(function (appointments) {
                    $scope.appointments = appointments;
                    $scope.isLoading = false;
                });
            }
            else {
                appointmentService.getAppointmentsForClinic($scope.selectedClinic.Id, $scope.serviceDate).then(function (appointments) {
                    $scope.appointments = appointments;
                    $scope.isLoading = false;
                });
            }
        }
        $scope.openAppointment = function (origAppointment) {
            $uibModal.open({
                templateUrl: 'app/home/appointment/manage/newAppointment.html',
                controller: 'NewAppointmentCtrl',
                size: 'appointment-modal-size',
                resolve: {
                    params: function () {
                        return {
                            appointment: origAppointment,
                            clinic: $scope.selectedClinic,
                            submit: function (appointment) {
                                return appointmentService.createAppointment(appointment).then(function () {
                                    return;
                                });
                            }
                        };
                    }
                }
            });
        };
        $scope.delete = function (appointment) {
            $scope.isLoading = true;
            appointmentService.deleteAppointment(appointment).then(function () {
                $scope.isLoading = false;
            });
        };
        init();
    }
}(window.angular));
