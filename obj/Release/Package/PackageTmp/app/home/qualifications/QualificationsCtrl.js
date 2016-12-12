(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('QualificationsCtrl', QualificationsCtrl);
    function QualificationsCtrl($scope, $uibModal, settings, userService) {
        $scope.services = null;
        $scope.originalServices = null;
        $scope.isLoading = true;
        function init() {
            userService.getQualifications(settings.User.Id).then(function (qualifications) {
                $scope.services = qualifications;
                $scope.originalServices = _.cloneDeep(qualifications);
                $scope.isLoading = false;
            });
        }
        $scope.isModified = function () {
            return !_.isEqual($scope.services, $scope.originalServices);
        };
        $scope.isSaveDisabled = function () {
            return $scope.isLoading || !$scope.isModified();
        };
        $scope.addService = function () {
            $uibModal.open({
                templateUrl: 'app/home/qualifications/qualification/qualification.html',
                controller: 'QualificationCtrl',
                resolve: {
                    params: function () {
                        return {
                            submit: function (serviceToAdd) {
                                if (!_.find($scope.services, function (service) { return service.Name === serviceToAdd.Name; })) {
                                    $scope.services.push(serviceToAdd);
                                }
                            }
                        };
                    }
                }
            });
        };
        $scope.delete = function (delService) {
            _.remove($scope.services, function (service) { return service === delService; });
        };
        $scope.update = function () {
            $scope.isLoading = true;
            userService.updateQualifications(settings.User.Id, $scope.services).then(function () {
                $scope.isLoading = false;
            });
        };
        init();
    }
}(window.angular));
