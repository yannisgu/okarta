angular.module('app.controllers').controller('HeaderCtrl', ['$scope', '$http', 'userService', "cartService",
  function ($scope, $http, userService, cartService) {
      function loadCurrentUser() {
          userService.currentUser().then(function(user){
              $scope.isLoggedIn = true;
              $scope.user = user;
          },
          function() {
              $scope.isLoggedIn = false;
          });
      }
      loadCurrentUser();

      $scope.login = function(username, password) {
          userService.login(username, password).then(
              function() {
                  loadCurrentUser();
              },
              function() {
                  $scope.error = "Username or password is wrong."
              }
          )
      }
      $scope.logout = function() {
          userService.logout();
          loadCurrentUser();
      }

      cartService.get().then(function(data){
          console.log(data);
          $scope.cartItems = data;
      });
  }]
);
