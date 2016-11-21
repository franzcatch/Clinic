(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('userService', userService);
    function userService($q) {
        var CurUser;
        function login(user, password) {
            var dfd = $q.defer();
            $.ajax({
                url: "Controllers/UserController.asmx/Login",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    Username: user,
                    Password: password
                }),
                success: function (result) {
                    CurUser = result;
                    dfd.resolve();
                },
                error: function (e) {
                    dfd.reject(e);
                }
            });
            return dfd.promise;
        }
        function logout() {
            var dfd = $q.defer();
            $.ajax({
                url: "Controllers/UserController.asmx/Logout",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                success: function (result) {
                    dfd.resolve();
                },
                error: function (e) {
                    dfd.reject(e);
                }
            });
            return dfd.promise;
        }
        return {
            CurUser: CurUser,
            login: login,
            logout: logout
        };
    }
}(window.angular));
