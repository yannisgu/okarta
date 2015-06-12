angular.module('app.controllers').controller('EditMapCtrl',["mapsService", "$scope", '$routeParams', 'openLayerService',
    function(mapsService, $scope, $routeParams, openLayerService) {
        mapsService.get($routeParams.mapId).then(function(map) {
            $scope.map = map;
            $scope.mapCenter = {'lat': map.lat, 'lon': map.lon, 'zoom': 8};
            $scope.markers = [
              {lat: map.lat, lon: map.lon}
            ]
        });


        $scope.openLayerConfig = openLayerService.config;

        $scope.save = function() {
            $scope.map.lat = $scope.markers[0].lat;
            $scope.map.lon = $scope.markers[0].lon;
            mapsService.save($scope.map);
        }

}]);
