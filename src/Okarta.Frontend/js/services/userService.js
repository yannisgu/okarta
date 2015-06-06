angular.module('app.services')
.factory('userService', ["$http", "$q", "$cookies", function($http, $q,$cookies) {
    return {
        currentUser : function() {
            return $q(function(resolve, reject){
                if($cookies.token) {
                    $http.defaults.headers.common["Authorization"] = "Token " + $cookies.token;
                }
                else {
                    reject();
                    return;
                }

                $http.get("/api/me").success(function(data, status, headers, config) {
                    resolve(data);
                })
                .error(function() {
                    reject();
                });

            });
        },
        login: function(username, password) {
            var self = this;
            return $q(function(resolve, reject){
                $http.post("/api/login", {username: username, password: password})
                .success(function(data) {
                    $cookies.token = data.token;
                    $http.defaults.headers.common["Authorization"] = "Token " + data.token;
                    self.currentUser().then(resolve);
                })
                .error(function() {
                    reject();
                });
            });
        },
        logout: function() {
            $http.defaults.headers.common["Authorization"] = null;
            $cookies.token = "";
        }
    };
}]);
