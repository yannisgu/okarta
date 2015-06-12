angular.module('app.services')
.factory('mapsService', ["$http", "$q", function($http, $q) {
    return {
        all: function() {
            return $http.get("/api/maps").success(function() {

            });
        },
        get: function(id) {
            return $q(function(resolve, reject) {
                return $http.get("/api/maps/" + id).success(function(data) {
                    resolve(data);
                })

                .error(function(error){
                    reject(error);
                });
            })
        },
        save: function(map) {
            return $q(function(resolve, reject) {
                return $http.post("/api/maps/" + map.id, map).success(function(data) {
                    resolve(data);
                })

                .error(function(error){
                    reject(error);
                });
            });
        }

    }
}]);
