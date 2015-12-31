(function () {
    'use strict';

    appControllers.controller('indexController', ['$scope', '$location', 'serviceAuthentication',
        function controller($scope, $location, serviceAuthentication) {

            $scope.getClass = function (path) {
                if ($location.path() === path) {
                    return 'active';
                } else {
                    return '';
                }
            }

            //TODO AUTH
            $scope.logout = function () {
                serviceAuthentication.logout();
                $location.path('/');
            }

            $scope.authentication = serviceAuthentication.authenticationData;
        }]);
})();
