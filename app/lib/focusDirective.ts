(function (angular) {
    'use strict';

    angular.module('clinic').directive('focusMe', focusMe);

    function focusMe($timeout) {
        return {
            link: function (scope, element, attrs) {
                if (attrs.focusMe) {
                    $timeout(function () {
                        $timeout(function () {
                            element[0].focus();
                        });
                    });
                }

                scope.$watch(attrs.focusMe, function (value) {
                    if (value === true) {
                        $timeout(function() {
                            element[0].focus();
                        });
                    }
                });
            }
        };
    };
} ((<any>window).angular));