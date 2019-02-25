// commentController.js
(function () {

  "use strict";

  angular.module("app-comment")
    .controller("commentsController", commentsController);

  function commentsController($http) {

    var vm = this;

    vm.comments = [];

    vm.newComment = {};

    vm.errorMessage = "";
    vm.isBusy = true;

    $http.get("/api/feedback")
      .then(function (response) {
        angular.copy(response.data, vm.comments);
      }, function (error) {
        vm.errorMessage = "Failed to load data: " + error;
      })
      .finally(function () {
        vm.isBusy = false;
      });

    vm.addComment = function () {

      vm.isBusy = true;
      vm.errorMessage = "";

      $http.post("/api/feedback", vm.newComment)
        .then(function (response) {
          vm.comments.push(response.data);
          vm.newComment = {};
        }, function () {
          vm.errorMessage = "Failed to post your comment";
        })
        .finally(function () {
          vm.isBusy = false;
        });

    };

  }

})();