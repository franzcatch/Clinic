var _;
(function (angular) {
    'use strict';
    angular.module('clinic')
        .controller('ReportsCtrl', ReportsCtrl);
    function ReportsCtrl($scope, reportService) {
        $scope.isLoading = true;
        $scope.data = null;
        function init() {
            $scope.setTab($scope.tabs[0]);
        }
        $scope.tabs = [
            {
                title: 'Report 1', id: '1', getter: reportService.generateAllHouseholdReport,
                message: 'List of all households with the household ID, name, address, and home phone along with the patient ID, name and relationship for each patient.'
            },
            {
                title: 'Report 2', id: '2', getter: reportService.getHouseholdAndInsurance,
                message: 'List of the insurance coverage for all households by household ID, household name, insurance company ID and company name.'
            },
            {
                title: 'Report 3', id: '3', getter: reportService.getAllPatientsAndInsurance,
                message: 'List all patients in alphabetical order by patient ID, name, and date of birth along with the name of the insurance company and policy number.'
            },
            {
                title: 'Report 4', id: '4', getter: reportService.getAllBilling,
                message: 'Itemized billings for all households with the household ID, household name, patient ID, patient name, service received, and the cost of the service.Show the output in alphabetical order by household name, patient name and billing date.'
            },
            {
                title: 'Report 5', id: '5', getter: reportService.getHouseholdTotalCosts,
                message: 'List the total cost of all services received for each household.'
            },
            {
                title: 'Report 6', id: '6', getter: reportService.getProvidersAndServices,
                message: 'List each provider with all services he or she is qualified to render.'
            },
            {
                title: 'Report 7', id: '7', getter: reportService.getServicesAndProviders,
                message: 'List each service available with all providers who are qualified to offer this service.'
            },
            {
                title: 'Report 8', id: '8', getter: reportService.getFutureAppointmentsByPatient,
                message: 'List all future appointments by name of patient, appointment date and time, estimated length of service, and contact home phone number.Dates and times should be in calendar order'
            },
            {
                title: 'Report 9', id: '9', getter: reportService.getAllServicesProvided,
                message: 'For a given date, list all services provided by each provider in alphabetical order by name of the provider.Show the service ID, service description and cost of service.'
            },
            {
                title: 'Report 10', id: '10', getter: reportService.getTotalServicesForProviders,
                requiredInputs: [{ title: 'ServiceDate', type: 'date', value: null }],
                message: 'For a given date, list the total amount of services each provider rendered.Show in alphabetical order by the provider’s name.'
            }
        ];
        $scope.setTab = function (tab) {
            $scope.isLoading = true;
            $scope.curTab = tab;
            if (tab.requiredInputs) {
                $scope.isLoading = false;
                return;
            }
            else {
                $scope.doSearch();
            }
        };
        $scope.doSearch = function () {
            $scope.isLoading = true;
            var params = null;
            if ($scope.curTab.requiredInputs) {
                params = {};
                _.each($scope.curTab.requiredInputs, function (input) {
                    params[input.title] = input.value;
                });
            }
            $scope.curTab.getter(params).then(function (response) {
                $scope.data = response;
                $scope.isLoading = false;
            });
        };
        $scope.stripUnder = function (str) {
            return _.replace(str, '_', ' ');
        };
        init();
    }
}(window.angular));
