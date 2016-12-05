var _: any;

(function (angular) {
    'use strict';

    angular.module('clinic')
        .factory('homeTiles', homeTiles);

    var admin = 'Administrator',
        office = 'Office',
        user = 'User';

    function homeTiles($q, ajaxService, settings) {
        var tiles = [
            {
                name: 'Appointments',
                icon: 'fa-calendar',
                color: '#6fb7ff',
                route: '/home/appointment',
                permissions: [user, office, admin]
            },
            {
                name: 'Billing',
                icon: 'fa-dollar',
                color: '#6fb7ff',
                route: '/home/billing',
                permissions: [user, office, admin]
            },
            {
                name: 'Qualifications',
                icon: 'fa-graduation-cap',
                color: '#6fb7ff',
                route: '/home/qualifications',
                permissions: [office, admin]
            },
            {
                name: 'Reports',
                icon: 'fa-bar-chart',
                color: '#6fb7ff',
                route: '/home/reports',
                permissions: [office, admin]
            },
            {
                name: 'People',
                icon: 'fa-users',
                color: '#6fb7ff',
                route: '/home/people',
                permissions: [office, admin]
            },
            {
                name: 'Clinics',
                icon: 'fa-cubes',
                color: '#6fb7ff',
                route: '/home/clinics',
                permissions: [admin]
            },
            {
                name: 'Profile',
                icon: 'fa-user',
                color: '#6fb7ff',
                route: '/home/profile',
                permissions: [user, office, admin]
            }
        ];

        function getTiles() {
            if (settings.User) {
                return _.filter(tiles, function (tile) {
                    return _.find(tile.permissions, function (perm) {
                        return perm === settings.User.Role.Name;
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