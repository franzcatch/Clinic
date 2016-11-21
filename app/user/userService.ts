(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('userService', userService);

    function userService($q, ajaxService) {
        var curUser: any = {};

        function isLoggedIn() {
            return !!curUser.Role;
        }

        function login(data) {
            var dfd = $q.defer();

            ajaxService.post("User", "Login", data).then(function (user) {
                curUser = user;
                dfd.resolve();
            });

            return dfd.promise;
        }

        function logout() {
            curUser = {};
            return ajaxService.post("User", "Logout");
        }

        function register(data) {
            var dfd = $q.defer();

            ajaxService.post("User", "Register", data).then(function (user) {
                curUser = user;
                dfd.resolve();
            });

            return dfd.promise;
        }

        return {
            curUser: curUser,
            isLoggedIn: isLoggedIn,
            login: login,
            logout: logout,
            register: register
        };
    }
} ((<any>window).angular));