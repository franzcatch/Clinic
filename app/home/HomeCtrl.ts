(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('HomeCtrl', HomeCtrl);

    function HomeCtrl($scope, userService, homeTiles) {
        $scope.tiles = [];
        
        $scope.$watch(userService.isLoggedIn, checkTiles);

        function checkTiles() {
            $scope.tiles = homeTiles.getTiles();
        }

        checkTiles();
    }
} ((<any>window).angular));