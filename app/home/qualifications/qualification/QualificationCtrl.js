var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('QualificationCtrl', QualificationCtrl);
    function QualificationCtrl($scope, $uibModalInstance, params, userService, settings) {
        $scope.selectedService = null;
        $scope.services = [];
        $scope.isLoading = true;
        function init() {
            userService.getEligibleQualifications(settings.User.Id).then(function (services) {
                $scope.services = services;
                $scope.isLoading = false;
            });
        }
        $scope.getServiceMessage = function () {
            return $scope.selectedService ? ($scope.selectedService.Name) : 'Select a service...';
        };
        $scope.isValid = function () {
            return $scope.selectedService;
        };
        $scope.setService = function (service) {
            $scope.selectedService = service;
        };
        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.save = function () {
            params.submit($scope.selectedService);
            $scope.close();
        };
        init();
    }
}(window.angular));
