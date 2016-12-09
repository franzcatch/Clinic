var _;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('RoomCtrl', RoomCtrl);

    function RoomCtrl($scope, $uibModalInstance, params) {
        $scope.Name = '';

        function init() {
            if (params.room) {
                $scope.Name = params.room.Name
            }
        }

        $scope.isValid = function () {
            return $scope.Name.length;
        };
        
        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.save = function () {
            params.submit({ Name: $scope.Name });
            $scope.close();
        };

        init();
    }
} ((<any>window).angular));