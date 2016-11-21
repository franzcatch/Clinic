(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('LoginCtrl', LoginCtrl);
    function LoginCtrl($scope, $uibModalInstance, userService) {
        $scope.username = '';
        $scope.password = '';
        $scope.loading = false;
        $scope.isPageValid = function () {
            return $scope.username.length > 0 &&
                $scope.password.length > 0 &&
                !$scope.loading;
        };
        $scope.close = function () {
            $uibModalInstance.dismiss();
        };
        $scope.login = function () {
            $scope.loading = true;
            userService.login({
                username: $scope.username,
                password: $scope.password
            }).then(function (response) {
                $scope.loading = false;
                $uibModalInstance.close();
            });
        };
    }
}(window.angular));
