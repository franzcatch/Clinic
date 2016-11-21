var _: any;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('homeTiles', homeTiles);

    var admin = 'Administrator',
        office = 'Office',
        user = 'User';

    function homeTiles($q, ajaxService, userService) {
        var tiles = [
            {
                name: 'Appointments',
                icon: 'fa-calendar',
                color: '#6fb7ff',
                permissions: [user, office, admin]
            },
            {
                name: 'Billing',
                icon: 'fa-dollar',
                color: '#6fb7ff',
                permissions: [user, office, admin]
            },
            {
                name: 'Clients',
                icon: 'fa-users',
                color: '#6fb7ff',
                permissions: [office, admin]
            },
            {
                name: 'Qualifications',
                icon: 'fa-check',
                color: '#6fb7ff',
                permissions: [office, admin]
            },
            {
                name: 'Reports',
                icon: 'fa-bar-chart',
                color: '#6fb7ff',
                permissions: [office, admin]
            },
            {
                name: 'Staff',
                icon: 'fa-cogs',
                color: '#6fb7ff',
                permissions: [admin]
            },
            {
                name: 'Clinics',
                icon: 'fa-cubes',
                color: '#6fb7ff',
                permissions: [admin]
            },
            {
                name: 'Profile',
                icon: 'fa-user',
                color: '#6fb7ff',
                permissions: [user, office, admin]
            }
        ];

        function getTiles() {
            if (userService.isLoggedIn()) {
                return _.filter(tiles, function (tile) {
                    return _.find(tile.permissions, function (perm) {
                        return perm === userService.curUser.Role.Name;
                    });
                });
            } 

            return [];
        }
        
        return {
            getTiles: getTiles
        };
    }
} ((<any>window).angular));