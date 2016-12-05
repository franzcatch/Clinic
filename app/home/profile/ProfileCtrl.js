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
            }
            else {
                houseDfd.resolve();
                $scope.model = $scope.user;
            }
            $q.all([rolesDfd, relationDfd, houseDfd.promise]).then(function (data) {
                $scope.roles = data[0];
                $scope.relationships = data[1];
                $scope.household = data[2];
                initHouseholdInfo();
                initUserInfo();
                $scope.isLoading = false;
            });
        }
        function initUserInfo() {
            if (!$scope.user) {
                $scope.user = {
                    Role: _.find($scope.roles, function (role) { return role.Name === "User"; })
                };
            }
        }
        function initHouseholdInfo() {
            if (!$scope.isHousehold) {
                return;
            }
            if (!$scope.household) {
                $scope.household = {};
            }
            $scope.dependents = _.filter($scope.household.People, function (person) {
                return person.IsPayer === false;
            });
            if (!$scope.household.People) {
                $scope.household.People = [];
            }
            $scope.householdPerson = _.find($scope.household.People, function (person) {
                return $scope.user && $scope.user.EntityId === person.EntityId;
            });
            _.each($scope.household.People, function (person) {
                person.DateOfBirth = new Date($scope.household.People[0].DateOfBirthString);
            });
            if (!$scope.householdPerson) {
                $scope.householdPerson = {
                    IsPayer: true,
                };
            }
            _.merge($scope.model, $scope.householdPerson);
            if ($scope.householdPerson && !$scope.householdPerson.Relationship) {
                $scope.householdPerson.Relationship = _.find($scope.relationships, function (relationship) {
                    return relationship.Name === "Primary";
                });
            }
        }
        $scope.getToday = function () {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
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
            return !$scope.isHousehold && $scope.user && $scope.user.Id !== settings.User.Id;
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
                (!$scope.isHousehold ||
                    ($scope.insuranceForm.$valid &&
                        $scope.dependentsForm.$valid)));
            return val;
        };
        $scope.update = function () {
            $scope.isLoading = true;
            if ($scope.isHousehold) {
                var id = $scope.householdPerson.Id;
                _.merge($scope.householdPerson, $scope.model);
                $scope.householdPerson.Id = id;
                if ($scope.householdPerson.Id) {
                    _.each($scope.household.People, function (person) {
                        if (person.Id === $scope.householdPerson.Id) {
                            _.merge(person, $scope.householdPerson);
                        }
                    });
                }
                else {
                    $scope.household.People.push($scope.householdPerson);
                }
                householdService.update($scope.household).then(function (household) {
                    $scope.household = household;
                    $scope.isLoading = false;
                    if ($scope.settings) {
                        $scope.settings.close();
                    }
                });
            }
            userService.update($scope.user).then(function () {
                init();
                $scope.isLoading = false;
                if ($scope.settings) {
                    $scope.settings.close();
                }
            });
        };
        init();
    }
}(window.angular));
