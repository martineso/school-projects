// userProductsController.js
(function () {

  "use strict";

  angular.module("store-app")
    .controller("userProductsController", userProductsController);

  function userProductsController($http) {

    var pc = this;

    pc.products = [];
    
    pc.errorMessage = "";
    pc.notification = "";
    pc.isBusy = true;

    pc.getProducts = function () {

      pc.isBusy = true;
      $http.get('/products/all', {
        'Content-type': 'application/json'
      })
      .then(function (response) {
        console.log(response.data);
        angular.copy(response.data, pc.products);
      }, function (error) {
        pc.errorMessage = "Failed to load data: " + error;
      })
      .finally(function () {
          pc.isBusy = false;
      });
    }

    pc.addToCart = function (id) {
      pc.isBusy = true;
      
      $http.post(`/cart/${id}`, {})
        .then(function(response){
          
          pc.displayNotification(response.data);
          pc.updateCartItemsCount();
        }, function(error){
         
          pc.errorMessage = error.data;
        }).finally(function(){
          pc.isBusy = false;

        });
    }

    pc.updateCartItemsCount = function () {
      var cartBadge = document.getElementById("cart-items-count");
      cartBadge.innerHTML = parseInt(cartBadge.innerHTML) + 1;
    }

    pc.displayNotification = function (message) {
      var notification = document.getElementById("notification-modal");
      notification.innerHTML = message;
      notification.classList.toggle("modal-notification-show");
      // Hide notification after 5 seconds

      setTimeout(function(){
        notification.classList.toggle("modal-notification-hide");
      }, 5000);
    }

    // On load get the current list of products
    this.getProducts();
  }
})();