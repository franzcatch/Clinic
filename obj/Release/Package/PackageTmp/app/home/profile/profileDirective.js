var _baseUrl;
(function (angular) {
    'use strict';
    angular.module('clinic').directive('profile', profile);
    function profile() {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: _baseUrl + 'app/home/profile/profile.html',
            controller: 'ProfileCtrl'
        };
    }
    ;
}(window.angular));
