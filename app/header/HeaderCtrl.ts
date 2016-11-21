(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('HeaderCtrl', HeaderCtrl);

    function HeaderCtrl($scope, $uibModal, userService, settingsService) {
        $scope.currentUser = userService.curUser;
        $scope.isLoggedIn = userService.isLoggedIn;
        $scope.settings = settingsService.settings;

        $scope.getName = function () {
            if ($scope.isLoggedIn()) {
                var name = ($scope.currentUser.FirstName ? $scope.currentUser.FirstName : '') +
                           ($scope.currentUser.LastName ? ' ' + $scope.currentUser.LastName : '') +
                           ($scope.currentUser.Role ? ('(' + $scope.currentUser.Role.Name + ')') : '');
                return name.trim();
            }
            return '';
        };

        $scope.goHome = function () {
            if ($scope.currentUser) {
                // go to tile page
            } else {
                // go to home page
            }
        };

        $scope.login = function () {
            $uibModal.open({
                templateUrl: 'app/header/login/login.html',
                controller: 'LoginCtrl',
            });
        };

        $scope.register = function () {
            $uibModal.open({
                templateUrl: 'app/header/login/register.html',
                controller: 'RegisterCtrl',
            });
        };

        $scope.logout = function () {
            userService.logout();
        };
    }
} ((<any>window).angular));