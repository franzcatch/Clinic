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
        function init() {
            clinicService.getAll().then(function (clinics) {
                $scope.clinics = clinics;
                $scope.setClinic(clinics[0]);
            });
        }
        $scope.setClinic = function (clinic) {
            $scope.isLoading = false;
            $scope.selectedClinic = clinic;
            var method = $scope.isUser ? 'getAppointmentsForUser' : 'getAppointmentsForClinic';
            var id = $scope.isUser ? settings.User.Id : clinic.Id;
            //appointmentService[method](id).then(function (appointments) {
            //    $scope.appointments = appointments;
            //    $scope.isLoading = false;
            //});
        };
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
                            }
                        };
                    }
                }
            });
        };
        init();
    }
}(window.angular));
