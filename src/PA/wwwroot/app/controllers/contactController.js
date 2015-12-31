(function () {    
    appControllers.controller('contactController', ['$scope', 'factoryArea', 'factorySubject', 'factoryContact', 'blockUI', function ($scope, factoryArea, factorySubject, factoryContact, blockUI) {
        $scope.subjectList = [];
        $scope.areaList = [];

        $scope.record = {
            Email: null,
            Name: null,
            Telefone: null,
            IdSubject: null,
            Message: null,
            ContactAreas: []
        }

        $scope.IsSent = false;

        //var block = blockUI.instances.get('contactBlock');

        var selectedAreas = [];
        


        factoryArea.get().success(function (e) { $scope.areaList = e; });
        factorySubject.get().success(function (e) { $scope.subjectList = e; });
           
        $scope.updateAreaListSelected = function (id) {
            var idx = selectedAreas.indexOf(id);

            // is currently selected
            if (idx > -1) {
                selectedAreas.splice(idx, 1);
            }

                // is newly selected
            else {
                selectedAreas.push(id);
            }
        }

        $scope.save = function () {

            $scope.record.ContactAreas = [];
            selectedAreas.forEach(function (idArea) {
                $scope.record.ContactAreas.push({ Email: $scope.record.Email, IdArea: idArea });
            });

            factoryContact.insert($scope.record).success(function (e) {
                $scope.IsSent = true;
            });
        }
    }]);
})();
