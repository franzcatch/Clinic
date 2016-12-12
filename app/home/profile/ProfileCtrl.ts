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
        $scope.isHousehold = (($scope.settings && $scope.settings.isHousehold) || (!$scope.settings && $scope.user.Role.Name === 'User')) ? true : false;
        $scope.isLoading = true;
        $scope.roles = [];
        $scope.relationships = [];
        $scope.originalDependents = [];
        $scope.curDependent = null;
        
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
                $scope.household = {};
            }

            if (!($scope.household.People && $scope.household.People.length)) {
                $scope.household.People = [
                    {
                        IsPayer: true,
                        Relationship: _.find($scope.relationships, function (relationship) {
                            return relationship.Name === "Primary";
                        })
                    }
                ]
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

            $scope.model.FirstName = $scope.householdPerson.FirstName;
            $scope.model.MiddleName = $scope.householdPerson.MiddleName;
            $scope.model.LastName = $scope.householdPerson.LastName;
            $scope.model.Address1 = $scope.householdPerson.Address1;
            $scope.model.Address2 = $scope.householdPerson.Address2;
            $scope.model.City = $scope.householdPerson.City;
            $scope.model.State = $scope.householdPerson.State;
            $scope.model.Zip = $scope.householdPerson.Zip;
            $scope.model.Phone1 = $scope.householdPerson.Phone1;
            $scope.model.Phone2 = $scope.householdPerson.Phone2;
            $scope.model.Phone3 = $scope.householdPerson.Phone3;

            $scope.originalDependents = _.cloneDeep($scope.household.People);
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

        $scope.getRelationshipMessage = function (householdPerson) {
            return householdPerson && householdPerson.Relationship ? householdPerson.Relationship.Name : 'Select a relationship...';
        };

        $scope.shouldGatherRole = function () {
            return !$scope.isHousehold && $scope.settings && $scope.user.Id !== settings.User.Id;
        };

        $scope.cancelEditDependent = function () {
            $scope.curDependent = null;
            $scope.isAddingDependent = false;
        };

        $scope.editDependent = function (dependent) {
            $scope.isAddingDependent = true;

            $scope.curDependent = dependent;

            if ($scope.householdPerson && !$scope.curDependent.FirstName) {
                _.merge($scope.curDependent, $scope.model);
                $scope.curDependent.FirstName = '';
                $scope.curDependent.MiddleName = '';
                $scope.curDependent.LastName = '';
                $scope.curDependent.DateOfBirth = null;
                $scope.curDependent.Relationship = null;
                $scope.curDependent.Id = null;
                $scope.curDependent.EntityId = null;
            }
        }

        $scope.saveDependent = function () {
            $scope.household.People.push($scope.curDependent);
            $scope.cancelEditDependent();
        };

        $scope.isDirty = function () {
            var val = ($scope.userForm.$dirty ||
                       $scope.nameForm.$dirty ||
                       $scope.addressForm.$dirty ||
                       $scope.phoneForm.$dirty ||
                       $scope.insuranceForm.$dirty ||
                       ($scope.isHousehold && !_.isEqual($scope.originalDependents, $scope.household.People)));
            return val;
        };

        $scope.isValid = function () {
            var val = ($scope.userForm.$valid &&
                       $scope.nameForm.$valid &&
                       $scope.addressForm.$valid &&
                       $scope.phoneForm.$valid &&
                       (
                           !$scope.isHousehold ||
                           ($scope.isHousehold && $scope.insuranceForm.$valid && $scope.household.People.length > 0) // one is the primary
                       ));
            return val;
        };

        $scope.update = function () {
            $scope.isLoading = true;
            _.merge($scope.user, $scope.model);

            userService.update($scope.user).then(function (user) {
                $scope.user = user;

                if ($scope.isHousehold) {
                    var personId = $scope.householdPerson.Id;
                    _.merge($scope.householdPerson, $scope.model);
                    $scope.householdPerson.Id = personId;
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