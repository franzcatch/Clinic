(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('ProfileCtrl', ProfileCtrl);
    function ProfileCtrl($scope, settings, userService, householdService) {
        $scope.isHousehold = settings.User.Role.Name === 'User';
        $scope.isLoading = true;
        $scope.user = settings.User;
        $scope.household = {};
        $scope.tabs = $scope.isHousehold
            ? [
                { title: 'Name', id: 'name' },
                { title: 'User/Pass', id: 'user' },
                { title: 'Address', id: 'address' },
                { title: 'Phone', id: 'phone' },
                { title: 'Insurance', id: 'insurance' },
                { title: 'Dependents', id: 'dependents' }
            ]
            : [
                { title: 'Name', id: 'name' },
                { title: 'User/Pass', id: 'user' },
                { title: 'Address', id: 'address' },
                { title: 'Phone', id: 'phone' }
            ];
        $scope.curTab = $scope.tabs[0];
        function init() {
            $scope.model = _.cloneDeep($scope.user);
            if ($scope.isHousehold) {
                householdService.getByUserId($scope.User.Id).then(function (household) {
                    $scope.household = household;
                    $scope.isLoading = false;
                });
            }
            else {
                $scope.isLoading = false;
            }
        }
        $scope.setTab = function (tab) {
            $scope.curTab = tab;
        };
        $scope.isFormError = function (tab) {
            return !$scope[tab.id + 'Form'].$valid;
        };
        $scope.isDirty = function () {
            var val = ($scope.userForm.$dirty ||
                $scope.nameForm.$dirty ||
                $scope.addressForm.$dirty ||
                $scope.phoneForm.$dirty ||
                $scope.insuranceForm.$dirty ||
                $scope.dependentsForm.$dirty);
            return val;
        };
        $scope.isValid = function () {
            var val = ($scope.userForm.$valid &&
                $scope.nameForm.$valid &&
                $scope.addressForm.$valid &&
                $scope.phoneForm.$valid &&
                (!$scope.isHousehold ||
                    ($scope.insuranceForm.$valid &&
                        $scope.dependentsForm.$valid)));
            return val;
        };
        $scope.update = function () {
            $scope.isLoading = true;
            if ($scope.isHousehold) {
                householdService.update($scope.model).then(function (household) {
                    $scope.household = household;
                    $scope.isLoading = false;
                });
            }
            else {
                userService.update($scope.model).then(function () {
                    init();
                    $scope.isLoading = false;
                });
            }
        };
        init();
    }
}(window.angular));
