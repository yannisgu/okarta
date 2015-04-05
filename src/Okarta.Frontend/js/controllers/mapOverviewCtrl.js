angular.module('app.controllers').controller('MapOverviewCtrl', ['$scope', '$http',
  function ($scope, $http) {
    angular.extend($scope, {
      myCenter: {
        lat: 55.6760968,
        lon: 12.568337100000008,
        zoom: 4
      }
    });

    $http.get("/api/maps").success(function(data, status, headers, config) {
      $scope.maps = data;
      for(var i in $scope.maps) {
        var map = $scope.maps[i];
        map.label = {
          show: false,
          showOnMouseClick: true
        }
      }
    });
  }]
);
