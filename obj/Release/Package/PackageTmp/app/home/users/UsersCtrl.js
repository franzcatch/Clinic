var toastr;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('UsersCtrl', UsersCtrl);
    function UsersCtrl($q, $scope, settings, userService, $uibModal) {
        $scope.isLoading = true;
        $scope.users = [];
        $scope.tabs = [
            { title: 'Manage Staff', id: 'staff' },
            { title: 'Manage Clients', id: 'clients' }
        ];
        $scope.curTab = {};
        function init() {
            $scope.setTab($scope.tabs[0]);
        }
        $scope.setTab = function (tab) {
            $scope.curTab = tab;
            if ($scope.curTab.id === 'staff') {
                $scope.getStaff();
            }
            else {
                $scope.getClients();
            }
        };
        $scope.register = function (user) {
            $uibModal.open({
                templateUrl: 'app/header/login/register.html',
                controller: 'RegisterCtrl',
                resolve: {
                    params: function () {
                        return {
                            user: user,
                            gatherHouseholdId: $scope.curTab === $scope.tabs[1],
                            submit: function (data) {
                                var dfd = $q.defer();
                                if (!data.isEditMode) {
                                    userService.register(data).then(function (response) {
                                        $scope.setTab($scope.curTab);
                                        dfd.resolve();
                                        toastr.info('Created. Instruct user to complete profile, or go to "People".');
                                    });
                                }
                                else {
                                    userService.update(data).then(function (response) {
                                        $scope.setTab($scope.curTab);
                                        dfd.resolve();
                                    });
                                }
                                return dfd.promise;
                            }
                        };
                    }
                }
            });
        };
        $scope.confirmDelete = function (user) {
            $uibModal.open({
                templateUrl: 'app/controls/confirm/confirm.html',
                controller: 'ConfirmCtrl',
                resolve: {
                    params: function () {
                        return {
                            confirm: function () {
                                return userService.remove(user);
                            }
                        };
                    }
                }
            });
        };
        $scope.getStaff = function () {
            $scope.isLoading = true;
            userService.getStaff().then(function (data) {
                $scope.users = data;
                $scope.isLoading = false;
            });
        };
        $scope.getClients = function () {
            $scope.isLoading = true;
            userService.getClients().then(function (data) {
                $scope.users = data;
                $scope.isLoading = false;
            });
        };
        init();
    }
}(window.angular));
