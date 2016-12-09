var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('ServiceCtrl', ServiceCtrl);
    function ServiceCtrl($scope, $uibModalInstance, params, clinicService) {
        $scope.isEdit = params.service && params.service.Id ? true : false;
        $scope.isCreating = false;
        $scope.selectedService = null;
        $scope.isLoading = true;
        $scope.services = [];
        function init() {
            clinicService.getAllServices().then(function (services) {
                $scope.services = services;
                if (!params.service) {
                    _.each(params.clinic.Services, function (clinicService) {
                        _.remove($scope.services, function (service) {
                            return clinicService.Id === service.Id;
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
            return $scope.isCreating || !$scope.services.length;
        };
        $scope.startCreateNew = function () {
            $scope.isCreating = true;
        };
        $scope.getServiceMessage = function () {
            return $scope.selectedService ? ($scope.selectedService.Name) : 'Select a service...';
        };
        $scope.isValid = function () {
            return (($scope.isEdit || $scope.isCreating) && $scope.serviceForm.$valid) ||
                (!$scope.isEdit && $scope.selectedService);
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
