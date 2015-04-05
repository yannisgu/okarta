angular.module('app.controllers').controller('MapDetailCtrl', ['$scope', '$http',  '$routeParams',
  function ($scope, $http,  $routeParams) {
    $http.get("/assets/js/data/maps.json").success(function(data, status, headers, config) {
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
