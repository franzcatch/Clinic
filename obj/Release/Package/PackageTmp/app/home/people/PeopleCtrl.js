(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('PeopleCtrl', PeopleCtrl);
    function PeopleCtrl($scope, userService) {
        $scope.people = [];
        $scope.isLoading = false;
        $scope.tabs = [
            { title: 'Manage Staff', id: 'staff' },
            { title: 'Manage Clients', id: 'clients' }
        ];
        $scope.curTab = {};
        function init() {
            $scope.setTab($scope.tabs[0]);
        }
        $scope.setTab = function (tab) {
            $scope.curTab = tab;
            if ($scope.curTab.id === 'staff') {
                $scope.getStaff();
            }
            else {
                $scope.getClients();
            }
        };
        $scope.getStaff = function () {
            $scope.isLoading = true;
            userService.getStaff().then(function (data) {
                $scope.users = data;
                $scope.isLoading = false;
            });
        };
        $scope.getClients = function () {
            $scope.isLoading = true;
            userService.getClients().then(function (data) {
                $scope.users = data;
                $scope.isLoading = false;
            });
        };
        init();
    }
}(window.angular));
