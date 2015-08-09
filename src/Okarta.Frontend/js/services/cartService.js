angular.module('app.services')
.factory('cartService', ["$http", "$q", function($http, $q) {
        return {
            get: function() {
                return $q(function(resolve, reject) {
                    return $http.get("/api/cart").success(function(data) {
                        resolve(data);
                    })

                    .error(function(error){
                        reject(error);
                    });
                })
            },
            add: function(cartItem) {
                return $q(function(resolve, reject) {
                    return $http.post("/api/cart", cartItem).success(function(data) {
                        resolve(data);
                    })

                    .error(function(error){
                        reject(error);
                    });
                });
            }
        };
}]);
