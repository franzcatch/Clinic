var _;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('NewAppointmentCtrl', NewAppointmentCtrl);

    function NewAppointmentCtrl($scope, appointmentService, householdService, clinicService, settings, params) {
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
        $scope.serviceDate = null;
        $scope.availableTimesForService = [];

        function init() {

        }

        $scope.householdSearch = function () {
            $scope.isLoading = true;
            var names = _.split($scope.householdSearchName, ' ', 3);
            var params: any = {};

            if (names.length === 0 || names.length > 3) {
                $scope.isLoading = false;
                return;
            }

            if (names.length === 1) {
                params.LastName = names[0];
            } else if (names.length === 2) {
                params.FirstName = names[0];
                params.LastName = names[1];
            } else if (names.length === 3) {
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

        $scope.serviceDateChanged = function () {
            _.remove($scope.model.AppointmentServices, function () { return true; });
        };

        $scope.isAdded = function (service) {
            return $scope.model.AppointmentServices && 
                   _.Any($scope.model.AppointmentServices, function (aptSvc) { return aptSvc.Service.Name === service.Name; });
        };

        $scope.addAppointmentService = function (availableService) {
            var appointmentService = {
                Provider: availableService.Provider,
                Service: availableService.Service,
                Room: availableService.Room,
                Cost: availableService.Service.Cost,
                StartTime: availableService.StartTime
            };

            $scope.model.AppointmentServices.push(appointmentService);

            _.remove($scope.availableTimesForService, availableService);
        };

        $scope.removeServiceFromAppointment = function (addedService) {
            $scope.isLoading = true;

            _.remove($scope.model.AppointmentServices, addedService);

            $scope.searchServiceAvailability($scope.selectedService);
        };

        $scope.getToday = function () {
            var today = new Date();
            var dd: any = today.getDate();
            var mm: any = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            return yyyy + '-' + mm + '-' + dd;
        };

        $scope.searchServiceAvailability = function (service) {
            $scope.selectedService = service;
            $scope.isLoading = true;

            appointmentService.getAvailableTimesAndProvidersForService($scope.model.Clinic.Id, $scope.selectedService.Id, $scope.serviceDate)
                .then(function (results) {
                    // TODO remove timeslots already chosen or that fall within existing ranges

                    $scope.availableTimesForService = results;

                    $scope.isLoading = false;
                });
        };

        $scope.isPageValid = function () {
            return false;
        };

        $scope.close = function () {
            params.close();
        };

        $scope.NewAppointment = function () {
            $scope.isLoading = true;
            
            params.submit($scope.model);
        };

        init();
    }
} ((<any>window).angular));