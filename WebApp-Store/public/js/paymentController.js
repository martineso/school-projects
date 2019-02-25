// paymentProductsController.js
(function () {

  "use strict";

  angular.module("store-app")
    .controller("paymentController", paymentController);

  function paymentController($http) {

    var p = this;

    p.processPayment = function () {
        var paymentUrl = document.getElementById("payment-url").value;
        window.open(paymentUrl, "_blank");
    }
  }
})();