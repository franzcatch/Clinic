(function (angular) {
    'use strict';
    angular.module('clinic').directive('maxLength', maxLength);
    function maxLength() {
        return {
            scope: {
                validLength: '='
            },
            link: function (scope, elm, attrs) {
                elm.bind('keypress', function (e) {
                    // stop typing if length is equal or greater then limit
                    if (elm[0].value.length >= scope.validLength) {
                        e.preventDefault();
                        return false;
                    }
                });
            }
        };
    }
    ;
}(window.angular));
