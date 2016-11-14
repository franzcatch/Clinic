(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('HeaderCtrl', HeaderCtrl);

    function HeaderCtrl($scope, userService) {
        $scope.currentUser = userService.CurUser;
        //$scope.getCurrentUser = userService.getCurrentUser;

        //userService.login('foo', 'bar');
    }
} ((<any>window).angular));