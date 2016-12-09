var _;
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
        };
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
        $scope.isModified = function () {
            return _.IsEqual($scope.selectedClinic, $scope.selectedClinicOriginal);
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
            $scope.curTab === $scope.tabs[1]
                ? openProvider(obj)
                : $scope.curTab === $scope.tabs[2]
                    ? openService(obj)
                    : openRoom(obj);
        };
        function openProvider(obj) {
            $uibModal.open({
                templateUrl: 'app/home/clinics/provider/provider.html',
                controller: 'ProviderCtrl',
                resolve: {
                    params: function () {
                        return {
                            provider: obj,
                            clinic: $scope.selectedClinic,
                            submit: function (user) {
                                var provider = obj ? obj : user;
                                if (obj) {
                                    obj.Id = user.Id;
                                    obj.EntityId = user.EntityId;
                                    obj.FirstName = user.FirstName;
                                    obj.MiddleName = user.MiddleName;
                                    obj.Username = user.Username;
                                }
                                else {
                                    $scope.dataToDisplay.push({
                                        Id: null,
                                        ClinicId: $scope.selectedClinic.Id,
                                        EntityId: provider.EntityId,
                                        FirstName: provider.FirstName,
                                        MiddleName: provider.MiddleName,
                                        LastName: provider.LastName,
                                        Username: provider.Username
                                    });
                                }
                            }
                        };
                    }
                }
            });
        }
        function openService(obj) {
            $uibModal.open({
                templateUrl: 'app/home/clinics/service/service.html',
                controller: 'ServiceCtrl',
                resolve: {
                    params: function () {
                        return {
                            service: obj,
                            submit: function (data) {
                                var dfd = $q.defer();
                                alert('todo');
                                //userService.register(data).then(function (response) {
                                //    dfd.resolve();
                                //    $location.path('/home');
                                //});
                                return dfd.promise;
                            }
                        };
                    }
                }
            });
        }
        function openRoom(obj) {
            $uibModal.open({
                templateUrl: 'app/home/clinics/room/room.html',
                controller: 'roomCtrl',
                resolve: {
                    params: function () {
                        return {
                            room: obj,
                            submit: function (data) {
                                var dfd = $q.defer();
                                alert('todo');
                                //userService.register(data).then(function (response) {
                                //    dfd.resolve();
                                //    $location.path('/home');
                                //});
                                return dfd.promise;
                            }
                        };
                    }
                }
            });
        }
        init();
    }
}(window.angular));
