var _;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .controller('ProfileCtrl', ProfileCtrl);

    function ProfileCtrl($q, $scope, settings, userService, householdService) {
        $scope.settings = settings.temp.profile;

        $scope.household = null;
        $scope.householdPerson = {};
        $scope.user = _.cloneDeep($scope.settings ? $scope.settings.user : settings.User);
        $scope.model = $scope.user;

        $scope.isModalEdit = $scope.settings ? true : false;
        $scope.isHousehold = ($scope.user && $scope.user.Role.Name === 'User') || ($scope.settings && $scope.settings.isHousehold) ? true : false;
        $scope.isLoading = true;
        $scope.roles = [];
        $scope.relationships = [];
        $scope.dependents = [];
        
        $scope.tabs = $scope.isHousehold
            ? [
                { title: 'Name', id: 'name' },
                { title: 'User/Pass', id: 'user' }, 
                { title: 'Address', id: 'address' },
                { title: 'Phone', id: 'phone' },
                { title: 'Insurance', id: 'insurance' },
                { title: 'Dependents', id: 'dependents' }
            ]
            : [
                { title: 'Name', id: 'name' },
                { title: 'User/Pass', id: 'user' },
                { title: 'Address', id: 'address' },
                { title: 'Phone', id: 'phone' },
            ];

        $scope.curTab = $scope.tabs[0];

        function init() {
            var rolesDfd = userService.getRoles();
            var relationDfd = householdService.getRelationships();
            var houseDfd = $q.defer();

            if ($scope.isHousehold && $scope.user) {
                householdService.getByUserId($scope.user.Id).then(function (household) {
                    houseDfd.resolve(household);
                });
            } else {
                houseDfd.resolve();
                $scope.model = $scope.user;
            }

            $q.all([rolesDfd, relationDfd, houseDfd.promise]).then(function (data) {
                $scope.roles = data[0];
                $scope.relationships = data[1];
                $scope.household = data[2];

                initUserInfo();
                initHouseholdInfo();

                $scope.isLoading = false;
            });
        }

        function initUserInfo() {
            if ($scope.isHousehold) {
                $scope.user.Role = _.find($scope.roles, function (role) { return role.Name === "User"; })
            } else {
                _.remove($scope.roles, function (role) {
                    return role.Name === "User";
                });
                if ($scope.user && $scope.user.Role) {
                    $scope.user.Role = _.find($scope.roles, function (role) { return role.Name === $scope.user.Role.Name; });
                }
                _.merge($scope.model, $scope.user);
            }
        }

        function initHouseholdInfo() {
            if (!$scope.isHousehold) {
                return;
            }

            if (!$scope.household) {
                $scope.household = {
                    People: [
                        {
                            IsPayer: true,
                            Relationship: _.find($scope.relationships, function (relationship) {
                                return relationship.Name === "Primary";
                            })
                        }
                    ]
                };
            }

            $scope.dependents = _.filter($scope.household.People, function (person) {
                return person.IsPayer === false;
            });

            $scope.householdPerson = _.find($scope.household.People, function (person) {
                return person.IsPayer;
            });

            _.each($scope.household.People, function (person) {
                person.DateOfBirth = new Date($scope.household.People[0].DateOfBirthString);
            });

            _.merge($scope.model, $scope.householdPerson);
        }

        $scope.getToday = function () {
            var today = new Date();
            var dd: any = today.getDate();
            var mm: any = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            } 

            return yyyy + '-' + mm + '-' + dd;
        };

        $scope.setTab = function (tab) {
            $scope.curTab = tab;
        };

        $scope.isFormError = function (tab) {
            return !$scope[tab.id + 'Form'].$valid;
        };

        $scope.setRole = function (role) {
            $scope.user.Role = role;
        };

        $scope.getRoleMessage = function () {
            return $scope.user && $scope.user.Role ? $scope.user.Role.Name : 'Select a role...';
        };

        $scope.setRelationship = function (relationship) {
            $scope.householdPerson.Relationship = relationship;
        };

        $scope.shouldGatherRelationship = function () {
            return $scope.settings.user ? true : false;
        };

        $scope.getRelationshipMessage = function () {
            return $scope.householdPerson.Relationship ? $scope.householdPerson.Relationship.Name : 'Select a relationship...';
        };

        $scope.shouldGatherRole = function () {
            return !$scope.isHousehold && $scope.settings && $scope.user.Id !== settings.User.Id;
        };

        $scope.isDirty = function () {
            var val = ($scope.userForm.$dirty ||
                       $scope.nameForm.$dirty ||
                       $scope.addressForm.$dirty ||
                       $scope.phoneForm.$dirty ||
                       $scope.insuranceForm.$dirty ||
                       $scope.dependentsForm.$dirty);
            return val;
        };

        $scope.isValid = function () {
            var val = ($scope.userForm.$valid &&
                       $scope.nameForm.$valid &&
                       $scope.addressForm.$valid &&
                       $scope.phoneForm.$valid &&
                       (
                           !$scope.isHousehold ||
                           (
                               $scope.insuranceForm.$valid &&
                               $scope.dependentsForm.$valid
                           )
                       ));
            return val;
        };

        $scope.update = function () {
            $scope.isLoading = true;
            _.merge($scope.user, $scope.model);

            userService.update($scope.user).then(function (user) {
                $scope.user = user;

                if ($scope.isHousehold) {
                    _.merge($scope.householdPerson, $scope.model);
                    $scope.householdPerson.EntityId = $scope.user.EntityId;

                    householdService.update($scope.household).then(function (household) {
                        $scope.household = household;
                        $scope.householdPerson = _.find($scope.household.People, function (person) {
                            return person.IsPayer === true;
                        });

                        if ($scope.settings) {
                            $scope.settings.close();
                        } else {
                            init();
                            $scope.isLoading = false;
                        }
                    });
                } else {
                    if ($scope.settings) {
                        $scope.settings.close();
                    } else {
                        init();
                        $scope.isLoading = false;
                    }
                }       
            });
        };

        init();
    }
} ((<any>window).angular));