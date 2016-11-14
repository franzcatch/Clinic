(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('HeaderCtrl', HeaderCtrl);
    function HeaderCtrl($scope, userService) {
        $scope.getCurrentUser = userService.getCurrentUser;
        userService.login('foo', 'bar');
    }
}(window.angular));
