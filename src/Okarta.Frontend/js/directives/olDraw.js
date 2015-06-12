angular.module('app.controllers').directive('olDraw', function($log, $location, olMapDefaults, olHelpers) {

    return {
        restrict: 'E',
       replace: false,
       require: '^openlayers',
       transclude: true,
       scope: {
            markers: '=markers',
        },
       link: function(scope, element, attrs, controller) {
            var olScope = controller.getOpenlayersScope();
            olScope.getMap().then(function(map) {
                map.on("click", function(e){
                    var cord = ol.proj.transform(e.coordinate,'EPSG:3857', 'EPSG:4326');
                    scope.markers[0] = {lon: cord[0], lat: cord[1]};
                    scope.$apply();
                });
            });
        }
    }
});
