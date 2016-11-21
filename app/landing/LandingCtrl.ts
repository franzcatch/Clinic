(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('LandingCtrl', LandingCtrl);

    function LandingCtrl($scope, $location, userService) {
        $scope.$watch(userService.isLoggedIn, checkForRedir);

        function checkForRedir() {
            if (userService.isLoggedIn() && $location.$$url === '/') {
                $location.path('/home');
            }
        }

        checkForRedir();
    }
} ((<any>window).angular));