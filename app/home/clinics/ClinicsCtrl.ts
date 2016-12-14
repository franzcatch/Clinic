var _;
var _baseUrl;
(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('ClinicsCtrl', ClinicsCtrl);

    function ClinicsCtrl($scope, $uibModal, $q, clinicService) {
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
                    { colName: 'Base Price', valueName: 'Cost' },
                    { colName: 'Duration', valueName: 'Minutes' }
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

        $scope.selectedClinicMessage = function () {
            return $scope.selectedClinic ? $scope.selectedClinic.Name : 'Select a clinic...';
        };

        $scope.getValue = function (row, column) {
            return row[column.valueName];
        };

        $scope.setTab = function (tab) {
            $scope.curTab = tab;

            $scope.dataToDisplay = $scope.curTab === $scope.tabs[1]
                ? $scope.selectedClinic.Providers
                : $scope.curTab === $scope.tabs[2]
                    ? $scope.selectedClinic.Services
                    : $scope.curTab === $scope.tabs[3]
                        ? $scope.selectedClinic.Rooms
                        : [];
        };

        $scope.setClinic = function (clinic) {
            $scope.selectedClinic = clinic;
            $scope.selectedClinicOriginal = _.cloneDeep(clinic);
            $scope.setTab($scope.tabs[0]);
        }

        $scope.addNewClinic = function (obj) {
            var newClinic = {
                Providers: [],
                Services: [],
                Rooms: []
            };
            $scope.setClinic(newClinic);
            $scope.setTab($scope.tabs[0]);
        };

        $scope.isValid = function () {
            return $scope.clinicForm.$valid &&
                   $scope.selectedClinic.Providers &&
                   $scope.selectedClinic.Providers.length &&
                   $scope.selectedClinic.Services &&
                   $scope.selectedClinic.Services.length &&
                   $scope.selectedClinic.Rooms &&
                   $scope.selectedClinic.Rooms.length;
        };

        $scope.isModified = function () {
            return !_.isEqual($scope.selectedClinic, $scope.selectedClinicOriginal);
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
                $scope.isLoading = false;
            });
        };

        $scope.delete = function (obj) {
            var property = $scope.curTab === $scope.tabs[1]
                ? 'Provider'
                : $scope.curTab === $scope.tabs[2]
                    ? 'Service'
                    : 'Room';

            var clinic = $scope.selectedClinic;

            _.remove(clinic[property + 's'], function (curObj) {
                return obj === curObj;
            });
        };

        $scope.edit = function (obj) {
            $scope.curTab === $scope.tabs[1]
                ? openProvider(obj)
                : $scope.curTab === $scope.tabs[2]
                    ? openService(obj)
                    : openRoom(obj);
        }

        function openProvider(existingProvider) {
            $uibModal.open({
                templateUrl: _baseUrl + 'app/home/clinics/provider/provider.html',
                controller: 'ProviderCtrl',
                resolve: {
                    params: function () {
                        return {
                            provider: existingProvider,
                            clinic: $scope.selectedClinic,
                            submit: function (provider) {
                                if (existingProvider) {
                                    existingProvider.Id = provider.Id;
                                    existingProvider.EntityId = provider.EntityId;
                                    existingProvider.FirstName = provider.FirstName;
                                    existingProvider.MiddleName = provider.MiddleName;
                                    existingProvider.LastName = provider.LastName;
                                    existingProvider.Username = provider.Username;
                                } else {
                                    $scope.dataToDisplay.push({
                                        Id: null,
                                        EntityId: provider.EntityId,
                                        FirstName: provider.FirstName,
                                        MiddleName: provider.MiddleName,
                                        LastName: provider.LastName,
                                        Username: provider.Username
                                    });
                                }
                            }
                        }
                    }
                }
            });
        }

        function openService(existingService) {
            $uibModal.open({
                templateUrl: _baseUrl + 'app/home/clinics/service/service.html',
                controller: 'ServiceCtrl',
                resolve: {
                    params: function () {
                        return {
                            service: existingService,
                            clinic: $scope.selectedClinic,
                            submit: function (service) {
                                if (existingService) {
                                    existingService.Id = service.Id;
                                    existingService.Name = service.Name;
                                    existingService.Cost = service.Cost;
                                    existingService.Minutes = service.Minutes;
                                } else {
                                    $scope.dataToDisplay.push({
                                        Id: null,
                                        Name: service.Name,
                                        Cost: service.Cost,
                                        Minutes: service.Minutes
                                    });
                                }
                            }
                        }
                    }
                }
            });
        }

        function openRoom(existingRoom) {
            $uibModal.open({
                templateUrl: _baseUrl + 'app/home/clinics/room/room.html',
                controller: 'RoomCtrl',
                resolve: {
                    params: function () {
                        return {
                            room: existingRoom,
                            clinic: $scope.selectedClinic,
                            submit: function (room) {
                                if (existingRoom) {
                                    existingRoom.Name = room.Name;
                                } else {
                                    $scope.dataToDisplay.push({
                                        Id: null,
                                        Name: room.Name
                                    });
                                }
                            }
                        }
                    }
                }
            });
        }

        init();
    }
} ((<any>window).angular));