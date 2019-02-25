(function () {

  "use strict";

  // Creating the Module
  var app = angular.module("store-app", ["xeditable"]);

  app.run(function(editableOptions) {
      editableOptions.theme = 'bs3';
  });
  
})();