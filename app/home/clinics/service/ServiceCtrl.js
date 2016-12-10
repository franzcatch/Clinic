var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('ServiceCtrl', ServiceCtrl);
    function ServiceCtrl($scope, $uibModalInstance, params, clinicService) {
        $scope.isEdit = params.service ? true : false;
        $scope.isCreating = false;
        $scope.selectedService = {};
        $scope.isLoading = true;
        $scope.services = [];
        $scope.durations = [30, 60, 90, 120, 150, 180];
        $scope.selectedService.Minutes = 30;
        function init() {
            clinicService.getAllServices().then(function (services) {
                $scope.services = services;
                if (!params.service) {
                    _.each(params.clinic.Services, function (clinicService) {
                        _.remove($scope.services, function (service) {
                            return clinicService.Name === service.Name;
                        });
                    });
                }
                else {
                    $scope.selectedService = _.cloneDeep(params.service);
                }
                $scope.isLoading = false;
            });
        }
        $scope.shouldCreate = function () {
            return $scope.isEdit || $scope.isCreating || !$scope.services.length;
        };
        $scope.startCreateNew = function () {
            $scope.isCreating = true;
        };
        $scope.getServiceMessage = function () {
            return $scope.selectedService ? ($scope.selectedService.Name) : 'Select a service...';
        };
        $scope.getDurationMessage = function () {
            return $scope.selectedService ? ($scope.selectedService.Minutes) : 'Select duration...';
        };
        $scope.isValid = function () {
            return (($scope.isEdit || $scope.isCreating) && $scope.selectedService.Name && $scope.selectedService.Cost && $scope.selectedService.Minutes) ||
                (!$scope.isEdit && $scope.selectedService);
        };
        $scope.setService = function (service) {
            $scope.selectedService = service;
        };
        $scope.setDuration = function (duration) {
            $scope.selectedService.Minutes = duration;
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
