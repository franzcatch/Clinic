(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('HomeCtrl', HomeCtrl);
    function HomeCtrl($scope, settings, homeTiles, $location, $route) {
        $scope.tiles = homeTiles.getTiles();
        ;
        $scope.goTo = function (hash) {
            $location.path(hash);
        };
        $scope.getClass = function (tile) {
            return $route.current.$$route.originalPath === tile.route ? 'active' : '';
        };
    }
}(window.angular));
