var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('RegisterCtrl', RegisterCtrl);
    function RegisterCtrl($scope, userService, householdService, settings) {
        $scope.settings = settings.temp.register;
        $scope.model = {
            householdId: null,
            username: '',
            password: '',
            passConf: '',
            selectedRole: null
        };
        $scope.isLoading = false;
        $scope.isAdmin = (settings.User && settings.User.Role && settings.User.Role.Name === "Administrator") ? true : false;
        $scope.roles = [];
        $scope.isEditMode = ($scope.settings && $scope.settings.user);
        function init() {
            $scope.isLoading = true;
            userService.getRoles().then(function (roles) {
                $scope.roles = roles;
                if ($scope.isEditMode) {
                    if ($scope.shouldGatherHouseholdId()) {
                        householdService.GetByUserId($scope.settings.user.Id).then(function (household) {
                            $scope.model.householdId = household.Id;
                            $scope.isLoading = false;
                        });
                    }
                    else {
                        $scope.model.selectedRole = _.find($scope.roles, function (role) {
                            return role.Id === $scope.settings.user.Role.Id;
                        });
                        $scope.isLoading = false;
                    }
                    if ($scope.settings) {
                        $scope.model.householdId = $scope.settings.householdId;
                        $scope.model.username = $scope.settings.user.Username;
                        $scope.model.password = $scope.settings.user.Password;
                        $scope.model.passConf = $scope.settings.user.Password;
                        if ($scope.shouldGatherHouseholdId()) {
                            $scope.model.selectedRole = _.find($scope.roles, function (role) {
                                return role.Name === 'User';
                            });
                        }
                        else {
                            $scope.model.selectedRole = $scope.settings.user.Role;
                        }
                    }
                }
                else {
                    $scope.isLoading = false;
                }
            });
        }
        $scope.passwordsMatch = function () {
            return $scope.model.password.trim() === $scope.model.passConf.trim();
        };
        $scope.shouldGatherRole = function () {
            return $scope.isAdmin && !$scope.shouldGatherHouseholdId();
        };
        $scope.shouldGatherHouseholdId = function () {
            return settings.AdminExists && $scope.settings.gatherHouseholdId;
        };
        $scope.setRole = function (role) {
            $scope.model.selectedRole = role;
        };
        $scope.getRoleMessage = function () {
            return $scope.model.selectedRole ? $scope.model.selectedRole.Name : 'Select a role...';
        };
        $scope.isPageValid = function () {
            return $scope.passwordsMatch() &&
                $scope.model.password.length > 0 &&
                $scope.model.username.length > 0 &&
                (!$scope.shouldGatherHouseholdId() || ($scope.shouldGatherHouseholdId() && $scope.model.householdId > 0)) &&
                (!$scope.shouldGatherRole() || ($scope.shouldGatherRole() && $scope.model.selectedRole)) &&
                !$scope.isLoading;
        };
        $scope.close = function () {
            $scope.settings.close();
        };
        $scope.register = function () {
            $scope.isLoading = true;
            var data = {
                isEditMode: $scope.isEditMode,
                householdId: $scope.model.householdId,
                username: $scope.model.username,
                password: $scope.model.password,
                role: $scope.model.selectedRole
            };
            $scope.settings.submit(data);
        };
        init();
    }
}(window.angular));
