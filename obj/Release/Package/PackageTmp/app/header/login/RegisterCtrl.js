var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('RegisterCtrl', RegisterCtrl);
    function RegisterCtrl($scope, $uibModalInstance, userService, householdService, settings, params) {
        $scope.settings = settings;
        $scope.householdId;
        $scope.username = '';
        $scope.password = '';
        $scope.passConf = '';
        $scope.isLoading = false;
        $scope.isAdmin = (settings.User && settings.User.Role && settings.User.Role.Name === "Administrator") ? true : false;
        $scope.selectedRole = null;
        $scope.roles = [];
        $scope.isEditMode = params.user;
        function init() {
            $scope.isLoading = true;
            userService.getRoles().then(function (roles) {
                $scope.roles = roles;
                if ($scope.isEditMode) {
                    if ($scope.shouldGatherHouseholdId()) {
                        householdService.GetByUserId(params.user.Id).then(function (household) {
                            $scope.householdId = household.Id;
                            $scope.isLoading = false;
                        });
                    }
                    else {
                        $scope.selectedRole = _.find($scope.roles, function (role) {
                            return role.Id === params.user.Role.Id;
                        });
                        $scope.isLoading = false;
                    }
                    $scope.username = params.user.Username;
                    $scope.password = params.user.Password;
                    $scope.passConf = params.user.Password;
                }
                else {
                    $scope.isLoading = false;
                }
            });
        }
        $scope.passwordsMatch = function () {
            return $scope.password.trim() === $scope.passConf.trim();
        };
        $scope.shouldGatherRole = function () {
            return $scope.isAdmin;
        };
        $scope.shouldGatherHouseholdId = function () {
            return settings.AdminExists && params.gatherHouseholdId;
        };
        $scope.setRole = function (role) {
            $scope.selectedRole.Id = role.Id;
        };
        $scope.getRoleMessage = function () {
            return $scope.selectedRole ? $scope.selectedRole.Name : 'Select a role...';
        };
        $scope.isPageValid = function () {
            return $scope.passwordsMatch() &&
                $scope.password.length > 0 &&
                $scope.username.length > 0 &&
                (!$scope.shouldGatherHouseholdId() || ($scope.shouldGatherHouseholdId() && $scope.householdId > 0)) &&
                (!$scope.shouldGatherRole() || ($scope.shouldGatherRole() && $scope.selectedRole)) &&
                !$scope.isLoading;
        };
        $scope.close = function () {
            $uibModalInstance.dismiss();
        };
        $scope.register = function () {
            $scope.isLoading = true;
            var data = {
                isEditMode: $scope.isEditMode,
                householdId: $scope.householdId,
                username: $scope.username,
                password: $scope.password,
                role: $scope.selectedRole
            };
            params.submit(data).then(function () {
                $scope.isLoading = false;
                $scope.close();
            });
        };
        init();
    }
}(window.angular));
