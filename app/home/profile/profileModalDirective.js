(function (angular) {
    'use strict';
    angular.module('clinic').directive('appNav', appNav);
    function appNav($uibModalInstance) {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: 'app/home/profile/profileModal.html',
            link: function (scope, elem, attr) {
                scope.close = function () {
                    $uibModalInstance.close();
                };
            }
        };
    }
    ;
}(window.angular));
