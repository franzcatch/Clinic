var _, toastr;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('ajaxService', ajaxService);

    function ajaxService($q) {
        var curUser;

        function post(controller, method, data, blockToastErrors) {
            var dfd = $q.defer();
            
            $.ajax({
                url: "Controllers/" + controller + "Controller.asmx/" + method,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: data ? angular.toJson(data) : '{}',
                success: function (result) {
                    var response = result.d ? JSON.parse(result.d) : null;

                    if (response && response.errorMessages) {
                        if (!blockToastErrors) {
                            _.each(response.errorMessages, function (message) {
                                toastr.error(message.Message, message.Field);
                            });
                        }
                    }

                    dfd.resolve(response);
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