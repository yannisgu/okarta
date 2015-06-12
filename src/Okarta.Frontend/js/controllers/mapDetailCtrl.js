angular.module('app.controllers').controller('MapDetailCtrl', ['$scope', '$http',  '$routeParams', 'openLayerService',
  function ($scope, $http,  $routeParams, openLayerService) {
      $scope.openLayerConfig = openLayerService.config;

      $scope.buyValues = {};

      $scope.buy = function() {
          console.log($scope.buyValues)
      }

    $http.get("/api/maps").success(function(data, status, headers, config) {
      for(var i in data) {
        var map = data[i];
        if(map.id ==  $routeParams.mapId) {
          $scope.map = map;
          $scope.mapCenter = {'lat': map.lat, 'lon': map.lon, 'zoom': 8};
          $scope.markers = [
            {lat: map.lat, lon: map.lon}
          ]
        }
      }
    });
  }]
);
