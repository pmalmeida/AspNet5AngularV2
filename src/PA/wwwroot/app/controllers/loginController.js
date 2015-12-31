'use strict';

appControllers.controller('loginController', ['$scope', '$location', 'serviceAuthentication', function ($scope, $location, serviceAuthentication) {
    //TODO AUTH
    $scope.loginData = {
        username: "",
        password: ""
    };

    //TODO AUTH
    $scope.login = function () {
        if (serviceAuthentication.login($scope.loginData.username, $scope.loginData.password))
            $location.path('/');
        else
            $scope.message = "Login inválido.";
    }
}]);