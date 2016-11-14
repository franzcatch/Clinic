(function (angular) {
    'use strict';
    angular.module('clinic')
        .factory('userService', userService);
    function userService($q) {
        var _curUser;
        function login(user, password) {
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
                    alert(result);
                },
                error: function (e) {
                }
            });
        }
        function logout() {
        }
        function getCurrentUser() {
            return _curUser;
        }
        return {
            login: login,
            logout: logout,
            getCurrentUser: getCurrentUser
        };
    }
}(window.angular));
