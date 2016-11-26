(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('ajaxService', ajaxService);

    function ajaxService($q) {
        var curUser;

        function post(controller, method, data) {
            var dfd = $q.defer();

            $.ajax({
                url: "Controllers/" + controller + "Controller.asmx/" + method,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: (data ? JSON.stringify(data) : '{}'),
                success: function (result) {
                    dfd.resolve(result.d);
                },
                error: function (e) {
                    dfd.reject(e);
                }
            });

            return dfd.promise;
        }

        return {
            post: post
        };
    }
} ((<any>window).angular));