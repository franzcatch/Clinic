var _, toastr;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('NewAppointmentCtrl', NewAppointmentCtrl);
    function NewAppointmentCtrl($scope, $timeout, appointmentService, householdService, clinicService, settings, params) {
        $scope.model = {
            Clinic: params.clinic,
            Person: null,
            AppointmentServices: []
        };
        $scope.isLoading = false;
        $scope.householdSearchName = null;
        $scope.selectedHousehold = null;
        $scope.householdResults = [];
        $scope.desiredServices = [];
        $scope.selectedService = null;
        $scope.serviceDate = new Date();
        $scope.availableTimesForService = [];
        function init() {
        }
        $scope.householdSearch = function () {
            $scope.isLoading = true;
            var names = _.split($scope.householdSearchName, ' ', 3);
            var params = {};
            if (names.length === 0 || names.length > 3) {
                $scope.isLoading = false;
                return;
            }
            if (names.length === 1) {
                params.LastName = names[0];
            }
            else if (names.length === 2) {
                params.FirstName = names[0];
                params.LastName = names[1];
            }
            else if (names.length === 3) {
                params.FirstName = names[0];
                params.MiddleName = names[1];
                params.LastName = names[2];
            }
            householdService.getByPayerName(params).then(function (result) {
                $scope.householdResults = result;
                $scope.isLoading = false;
            });
        };
        $scope.isHouseholdSearchValid = function () {
            var names = _.split($scope.householdSearchName, ' ');
            return names.length > 0 && names.length <= 3;
        };
        $scope.selectedMemberMessage = function () {
            return $scope.model.Person ? $scope.model.Person.FirstName + ' ' + $scope.model.Person.LastName : 'Select a member...';
        };
        $scope.selectedServiceMessage = function () {
            return $scope.selectedService ? $scope.selectedService.Name : 'Select a service...';
        };
        $scope.setHousehold = function (household) {
            $scope.selectedHousehold = household;
        };
        $scope.getHouseholdSelectText = function (household) {
            if (!household) {
                return 'Select a household...';
            }
            var names = _.map(household.People, function (person) {
                if (person.IsPayer === 'N') {
                    return person.FirstName + ' ' + person.LastName;
                }
            });
            var nameString = _.join(names, ', ');
            return household.Id + ' - ' +
                household.Payer.FirstName + ' ' +
                household.Payer.LastName + ' ' +
                (nameString ? '( ' + nameString + ' )' : '');
        };
        $scope.setMember = function (member) {
            $scope.model.Person = member;
        };
        $scope.serviceDateChanged = function (date) {
            $scope.serviceDate = date;
            _.remove($scope.model.AppointmentServices, function () { return true; });
            if ($scope.selectedService && $scope.serviceDate && $scope.model.Person) {
                $scope.searchServiceAvailability($scope.selectedService);
            }
        };
        $scope.isAdded = function (service) {
            return $scope.model.AppointmentServices.length &&
                _.find($scope.model.AppointmentServices, function (aptSvc) { return aptSvc.Service.Name === service.Name; });
        };
        $scope.removeServiceFromAppointment = function (addedService) {
            $scope.isLoading = true;
            _.remove($scope.model.AppointmentServices, addedService);
            $scope.searchServiceAvailability($scope.selectedService);
        };
        $scope.getToday = function () {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            return yyyy + '-' + mm + '-' + dd;
        };
        $scope.searchServiceAvailability = function (service) {
            $scope.selectedService = service;
            $scope.isLoading = true;
            appointmentService.getAvailableAppointments($scope.model.Clinic.Id, $scope.selectedService.Id, $scope.serviceDate)
                .then(function (results) {
                _.each(results, function (result) {
                    result.StartTime = new Date(result.StartTimeString);
                });
                $scope.availableTimesForService = results;
                $scope.isLoading = false;
            });
        };
        $scope.addServiceToAppointment = function (availableService) {
            if ($scope.isAdded(availableService)) {
                toastr.info('You can only add a service type once');
                return;
            }
            $scope.model.AppointmentServices.push(availableService);
        };
        $scope.removeAddedService = function (addedService) {
            _.remove($scope.model.AppointmentServices, addedService);
        };
        $scope.isPageValid = function () {
            return $scope.model.Clinic &&
                $scope.model.Person &&
                $scope.model.AppointmentServices &&
                $scope.model.AppointmentServices.length;
        };
        $scope.close = function () {
            params.close();
        };
        $scope.save = function () {
            $scope.isLoading = true;
            params.submit($scope.model).then(function () {
                $scope.isLoading = false;
            });
        };
        $scope.NewAppointment = function () {
            $scope.isLoading = true;
            params.submit($scope.model);
        };
        init();
    }
}(window.angular));
