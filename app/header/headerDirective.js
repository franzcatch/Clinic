angular.module('clinic', []).directive('appNav', function () {
    return {
        restrict: 'E',
        scope: {},
        templateUrl: '/app/header/header.html',
        controller: 'HeaderCtrl'
    };
});
