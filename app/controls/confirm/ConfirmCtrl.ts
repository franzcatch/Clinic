(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('ConfirmCtrl', ConfirmCtrl);

    function ConfirmCtrl($scope, $uibModalInstance, params) {
        $scope.confirm = function () {
            $scope.isLoading = true;
            params.confirm().then(function () {
                $scope.isLoading = false;
                $scope.close();
            });
        };

        $scope.close = function () {
            $uibModalInstance.dismiss();
        };
    }
} ((<any>window).angular));