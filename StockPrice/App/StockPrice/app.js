(function () {
    'use strict';

    var app = angular.module('app', ['ngResource', 'ngRoute', 'highcharts-ng']);

    app.config(['$routeProvider', function ($routeProvider) {

        $routeProvider.when('/stockPrice', {
            templateUrl: '/app/stockprice/templates/stockPrice.html',
            controller: 'stockPriceController',
            controllerAs: 'vm',
            caseInsensitiveMatch: true
        });
        $routeProvider.when('/home', {
            templateUrl: '/app/stockprice/templates/home.html',
            caseInsensitiveMatch: true
        });
        $routeProvider.otherwise({
            redirectTo: '/home'
        });
    }]);
    
   

})();

