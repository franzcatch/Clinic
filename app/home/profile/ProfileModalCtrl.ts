(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('ProfileModalCtrl', ProfileModalCtrl);

    function ProfileModalCtrl($scope, settings, $uibModalInstance) {
        $scope.close = $uibModalInstance.close;
    }
} ((<any>window).angular));