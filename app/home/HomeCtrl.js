(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('HomeCtrl', HomeCtrl);
    function HomeCtrl($scope, settings, homeTiles, $location) {
        $scope.tiles = homeTiles.getTiles();
        ;
        $scope.goTo = function (hash) {
            $location.path(hash);
        };
    }
}(window.angular));
