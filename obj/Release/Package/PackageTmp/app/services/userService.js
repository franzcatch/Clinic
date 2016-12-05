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
        function update(data) {
            var dfd = $q.defer();
            ajaxService.post("User", "Update", data).then(function (user) {
                settings.getSettings(true).then(function () {
                    dfd.resolve(user);
                });
            });
            return dfd.promise;
        }
        function remove(data) {
            var dfd = $q.defer();
            ajaxService.post("User", "Remove", data).then(function (user) {
                settings.getSettings(true).then(function () {
                    dfd.resolve(user);
                });
            });
            return dfd.promise;
        }
        function getStaff() {
            var dfd = $q.defer();
            ajaxService.post("User", "GetStaff").then(function (data) {
                dfd.resolve(data);
            });
            return dfd.promise;
        }
        function getClients() {
            var dfd = $q.defer();
            ajaxService.post("User", "GetClients").then(function (data) {
                dfd.resolve(data);
            });
            return dfd.promise;
        }
        function getRoles() {
            var dfd = $q.defer();
            ajaxService.post("User", "GetRoles").then(function (data) {
                dfd.resolve(data);
            });
            return dfd.promise;
        }
        return {
            login: login,
            logout: logout,
            register: register,
            update: update,
            remove: remove,
            getStaff: getStaff,
            getClients: getClients,
            getRoles: getRoles
        };
    }
}(window.angular));
