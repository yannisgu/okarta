

angular.module('app', ["ngRoute", "ngCookies", "templates", "app.controllers", "openlayers-directive"])
  .config(['$routeProvider', function($routeProvider) {
       $routeProvider.
           when('/index', {templateUrl: 'mapOverview.html',   controller: "MapOverviewCtrl"}).
           when('/map/:mapId', {templateUrl: 'mapDetail.html', controller: "MapDetailCtrl"}).
           otherwise({redirectTo: '/index'})
   }]);
