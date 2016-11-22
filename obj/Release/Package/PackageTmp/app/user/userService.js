(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('userService', userService);
    function userService($q, ajaxService, settings) {
        function login(data) {
            var dfd = $q.defer();
            ajaxService.post("User", "Login", data).then(function (user) {
                settings.getSettings(true).then(function () {
                    dfd.resolve();
                });
            });
            return dfd.promise;
        }
        function logout() {
            var dfd = $q.defer();
            ajaxService.post("User", "Logout").then(function () {
                settings.getSettings(true).then(function () {
                    dfd.resolve();
                });
            });
            return dfd.promise;
        }
        function register(data) {
            var dfd = $q.defer();
            ajaxService.post("User", "Register", data).then(function (user) {
                settings.getSettings(true).then(function () {
                    dfd.resolve();
                });
            });
            return dfd.promise;
        }
        return {
            login: login,
            logout: logout,
            register: register
        };
    }
}(window.angular));
