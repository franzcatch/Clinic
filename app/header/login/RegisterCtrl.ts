(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('RegisterCtrl', RegisterCtrl);

    function RegisterCtrl($scope, $uibModalInstance, userService, settings, $location) {
        $scope.settings = settings;
        $scope.householdId;
        $scope.username = '';
        $scope.password = '';
        $scope.passConf = '';
        $scope.loading = false;

        $scope.passwordsMatch = function () {
            return $scope.password.trim() === $scope.passConf.trim();
        };

        $scope.isPageValid = function () {
            return $scope.passwordsMatch() &&
                   $scope.password.length > 0 && 
                   $scope.username.length > 0 &&
                   (!$scope.hasAdmin || ($scope.hasAdmin && $scope.householdId > 0)) &&
                   !$scope.loading;
        };

        $scope.close = function () {
            $uibModalInstance.dismiss();
        };

        $scope.register = function () {
            $scope.loading = true;

            userService.register({
                householdId: $scope.householdId,
                username: $scope.username,
                password: $scope.password
            }).then(function (response) {
                $scope.loading = false;
                $uibModalInstance.close();
                $location.path('/profile');
            });
        };
    }
} ((<any>window).angular));