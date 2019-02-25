// cartProductsController.js
(function () {

  "use strict";

  angular.module("store-app")
    .controller("cartController", cartController);

  function cartController($http) {

    var cart = this;

    cart.products = [];
    
    cart.errorMessage = "";
    cart.isBusy = true;
    cart.subtotal = undefined;

    cart.getProducts = function () {

      cart.isBusy = true;
      $http.get('/cart/all', {
        'Content-type': 'application/json'
      })
      .then(function (response) {
        console.log(response.data);
        angular.copy(response.data, cart.products);
      }, function (error) {
        cart.errorMessage = "Failed to load data: " + error;
      })
      .finally(function () {
          cart.isBusy = false;
          cart.subtotal = cart.calculateSubtotal();
      });
    }

    cart.clearCart = function () {
      
      $http.get('/cart/clear')
        .then(function(response) {
          cart.displayNotification(response.data);
        }, function(error) {
          cart.errorMessage = error.data;
        }).finally(function () {
          // Update the icon in the navbar 
          // and update the items in the cart
          document.getElementById("cart-items-count").innerHTML = 0;
          cart.products = [];
          cart.subtotal = undefined;
        })
    }

    cart.calculateSubtotal = function () {
      var subtotal = 0;
      cart.products.forEach(function(product) {
        subtotal = subtotal + (product.price * product.quantity)
      });
      return subtotal;
    }

    cart.displayNotification = function (message) {
      var notification = document.getElementById("notification-modal");
      notification.innerHTML = message;
      notification.classList.toggle("modal-notification-show");
      // Hide notification after 5 seconds

      setTimeout(function(){
        notification.classList.toggle("modal-notification-hide");
      }, 5000);
    }

    // On load get the current list of products
    cart.getProducts();
  }
})();