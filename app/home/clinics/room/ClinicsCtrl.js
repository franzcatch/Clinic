(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('ClinicsCtrl', ClinicsCtrl);
    function ClinicsCtrl($scope, $uibModal, clinicService) {
        $scope.isLoading = true;
        $scope.dataToDisplay = [];
        $scope.clinics = [];
        $scope.selectedClinic = null;
        $scope.tabs = [
            {
                title: 'Name', id: 'name'
            },
            {
                title: 'Providers', id: 'providers', button: 'Add Provider',
                columns: [
                    { colName: 'First Name', valueName: 'FirstName' },
                    { colName: 'Middle Name', valueName: 'MiddleName' },
                    { colName: 'Last Name', valueName: 'LastName' },
                    { colName: 'Username', valueName: 'Username' },
                    { colName: 'Id', valueName: 'Id' }
                ]
            },
            {
                title: 'Services', id: 'services', button: 'Add Service',
                columns: [
                    { colName: 'Service', valueName: 'Name' },
                    { colName: 'Base Price', valueName: 'Cost' }
                ]
            },
            {
                title: 'Rooms', id: 'rooms', button: 'Add Room',
                columns: [
                    { colName: 'Name', valueName: 'Name' }
                ]
            }
        ];
        function init() {
            // load clinics
            clinicService.getAll().then(function (clinics) {
                if (!clinics || !clinics.length) {
                    $scope.isLoading = false;
                    return;
                }
                $scope.clinics = clinics;
                $scope.selectedClinic = clinics[0];
                $scope.setTab($scope.tabs[0]);
                $scope.isLoading = false;
            });
        }
        $scope.setTab = function (tab) {
            $scope.curTab = tab;
            $scope.dataToDisplay = tab === $scope.tabs[0]
                ? $scope.selectedClinic.Providers
                : tab === $scope.tabs[1]
                    ? $scope.selectedClinic.Services
                    : $scope.selectedClinic.Rooms;
        };
        $scope.setClinic = function (clinic) {
            $scope.selectedClinic = clinic;
        };
        $scope.addNewClinic = function (obj) {
            $scope.selectedClinic = {};
            $scope.setTab($scope.tabs[0]);
        };
        $scope.isValid = function () {
            return $scope.selectedClinic &&
                $scope.selectedClinic.Name &&
                $scope.selectedClinic.Name.length > 0 &&
                $scope.selectedClinic.Providers &&
                $scope.selectedClinic.Providers.length &&
                $scope.selectedClinic.Services &&
                $scope.selectedClinic.Services.length &&
                $scope.selectedClinic.Rooms &&
                $scope.selectedClinic.Rooms.length;
        };
        $scope.isFormError = function (tab) {
            return (tab === $scope.tabs[0] && !$scope.selectedClinic.Name) ||
                (tab === $scope.tabs[1] && (!$scope.selectedClinic.Providers || !$scope.selectedClinic.Providers.length)) ||
                (tab === $scope.tabs[2] && (!$scope.selectedClinic.Services || !$scope.selectedClinic.Services.length)) ||
                (tab === $scope.tabs[3] && (!$scope.selectedClinic.Rooms || !$scope.selectedClinic.Rooms.length));
        };
        $scope.saveClinic = function () {
            $scope.isLoading = true;
            clinicService.update($scope.selectedClinic).then(function (result) {
                init();
            });
        };
        $scope.deleteClinic = function () {
            // confirm
            $scope.isLoading = true;
            clinicService.deleteClinic($scope.selectedClinic.Id).then(function () {
                init();
            });
        };
        $scope.delete = function (obj) {
            // confirm
            $scope.isLoading = true;
            var property = $scope.curTab === $scope.tabs[0]
                ? 'Provider'
                : $scope.curTab === $scope.tabs[1]
                    ? 'Service'
                    : 'Room';
            var clinic = $scope.selectedClinic;
            _.remove(clinic[property + 's'], function (curObj) {
                return obj === curObj;
            });
            clinicService.update(clinic).then(function () {
                $scope.isLoading = false;
            });
        };
        $scope.edit = function (obj) {
            $scope.curTab === $scope.tabs[0]
                ? openProvider(obj)
                : $scope.curTab === $scope.tabs[1]
                    ? openService(obj)
                    : openRoom(obj);
        };
        function openProvider(obj) {
        }
        function openService(obj) {
        }
        function openRoom(obj) {
        }
        init();
    }
}(window.angular));
