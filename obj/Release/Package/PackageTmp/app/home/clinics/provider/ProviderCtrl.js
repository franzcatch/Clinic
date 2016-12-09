var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('ProviderCtrl', ProviderCtrl);
    function ProviderCtrl($scope, $uibModalInstance, params, clinicService) {
        $scope.isEdit = params.provider && params.provider.Id ? true : false;
        $scope.isCreating = false;
        $scope.selectedUser = null;
        $scope.isLoading = true;
        $scope.users = [];
        function init() {
            clinicService.getEligibleProviders(params.clinic.Id).then(function (users) {
                $scope.users = users;
                if (params.provider) {
                    _.each(users, function (user) {
                        if (user.EntityId === params.provider.EntityId) {
                            $scope.selectedUser = user;
                        }
                    });
                }
                _.each(params.clinic.Providers, function (provider) {
                    _.remove($scope.users, function (user) {
                        return provider.EntityId === user.EntityId;
                    });
                });
                $scope.isLoading = false;
            });
        }
        $scope.getProviderMessage = function () {
            return $scope.selectedUser ? ($scope.selectedUser.FirstName + ' ' + $scope.selectedUser.LastName) : 'Select a staff member...';
        };
        $scope.isValid = function () {
            return (($scope.isEdit || $scope.isCreating) &&
                $scope.userForm.$valid);
        };
        $scope.setUser = function (user) {
            $scope.selectedUser = user;
        };
        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.save = function () {
            params.submit($scope.selectedUser);
            $scope.close();
        };
        init();
    }
}(window.angular));
