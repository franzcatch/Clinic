var toastr;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('HeaderCtrl', HeaderCtrl);
    function HeaderCtrl($q, $rootScope, $scope, $uibModal, userService, settings, $location) {
        $scope.settings = settings;
        $scope.getName = function () {
            if ($scope.settings.User) {
                var name = ($scope.settings.User.FirstName ? $scope.settings.User.FirstName : '') +
                    ($scope.settings.User.LastName ? ' ' + $scope.settings.User.LastName + ' ' : '') +
                    ($scope.settings.User.Role ? ('(' + $scope.settings.User.Role.Name + ')') : '');
                return name.trim();
            }
            return '';
        };
        $scope.goHome = function () {
            if ($scope.settings.User) {
            }
            else {
            }
        };
        $scope.login = function () {
            $uibModal.open({
                templateUrl: 'app/header/login/login.html',
                controller: 'LoginCtrl',
                resolve: {
                    params: function () {
                        return {
                            submit: function (data) {
                                var dfd = $q.defer();
                                userService.login(data).then(function (response) {
                                    dfd.resolve();
                                    $location.path('/home');
                                });
                                return dfd.promise;
                            }
                        };
                    }
                }
            });
        };
        $scope.register = function () {
            $uibModal.open({
                templateUrl: 'app/header/login/register.html',
                controller: 'RegisterCtrl',
                resolve: {
                    params: function () {
                        return {
                            gatherHouseholdId: true,
                            submit: function (data) {
                                var dfd = $q.defer();
                                userService.register(data).then(function (response) {
                                    dfd.resolve();
                                    $location.path('/home');
                                });
                                return dfd.promise;
                            }
                        };
                    }
                }
            });
        };
        $scope.logout = function () {
            userService.logout().then(function () {
                $location.path('/');
            });
        };
    }
}(window.angular));
