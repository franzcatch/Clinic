(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('LoginCtrl', LoginCtrl);

    function LoginCtrl($scope, $uibModalInstance, params) {
        $scope.username = '';
        $scope.password = '';
        $scope.isLoading = false;
        
        $scope.isPageValid = function () {
            return $scope.username.length > 0 &&
                   $scope.password.length > 0 &&
                   !$scope.isLoading;
        };

        $scope.close = function () {
            $uibModalInstance.dismiss();
        };

        $scope.login = function () {
            $scope.isLoading = true;

            var data = {
                username: $scope.username,
                password: $scope.password
            };

            params.submit(data).then(function () {
                $scope.isLoading = false;
                $scope.close();
            });
        };
    }
} ((<any>window).angular));