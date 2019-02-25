// productsController.js
(function () {

  "use strict";

  angular.module("store-app")
    .controller("productsController", productsController);

  function productsController($http, $q, $scope) {

    var pc = this;

    pc.products = [];
    pc.newProduct = {};

    pc.currentUser = {
      id: document.getElementById('userId').value
    };
    
    pc.errorMessage = "";
    pc.errorMessageEdit = "";
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

    pc.addProduct = function () {

      pc.isBusy = true;
      pc.errorMessage = "";
      console.log(pc.newProduct);
      $http.post('/products', pc.newProduct)
      .then(function (response) {
        pc.newProduct = {};
        // Update the current list of products
        pc.getProducts();
      }, function (error) {
        pc.errorMessage = error.data;
      })
      .finally(function () {
        pc.isBusy = false;
      })
    }

    pc.editProduct = function (data, productId) {
      
      data.id = productId;
      data.lastEditedBy = pc.currentUser.id;
      console.log(data);
      pc.isBusy = true;
      pc.errorMessageEdit = "";
      $http.patch('/products/' + productId, data)
        .then(function (response) {
          pc.getProducts();
        }, function(error){
          pc.errorMessageEdit = error.data;
        }).finally(function () {

          pc.isBusy = false;
        });

    }
    
    // Helper validation functions
    pc.validateQty = function(qty) {
      if(qty < 0) {
        return "You cannot have negative quantity!";
      } 
    }

    pc.validateProductName = function(productName, productId) {
      var duplicateExists = false;

      pc.products.map(function(element) {
        
        if(element.productName == productName) {
          if(element.id !== productId) {
            duplicateExists = true;
          }
        }
      });

      if(duplicateExists) {
        return "The product already exists!";
      }
    }

    pc.validatePrice = function(price) {
      if(price < 0) {
        return "Price cannot be a negative value!";
      } 
    }

    // On load get the current list of products
    this.getProducts();
  }
})();