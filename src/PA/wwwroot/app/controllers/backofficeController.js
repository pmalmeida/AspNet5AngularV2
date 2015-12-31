(function () {
    appControllers.controller('areaController', ['$scope', 'factoryArea', function ($scope, factoryArea) {
        var factory = factoryArea;

        $scope.sortType = 'Name'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order
        $scope.searchText = '';     // set the default search/filter term

        var getEmptyRecord = function () { return { Id: 0, Name: '' }; }

        // Mode :
        // 0 - None
        // 1 - New
        // 2 - Edit
        var editMode = { None: 0, New: 1, Edit: 2 };

        function setEditMode(mode) {
            $scope.editMode = mode;
            $scope.newRecord = getEmptyRecord();
            $scope.editRecord = getEmptyRecord();
        }

        setEditMode(editMode.None);

        $scope.recordList = [];
        
        $scope.load = function () {
            factory.get().success(function (e) {
                $scope.recordList = e;
                $scope.cancel();
            });
        }

        $scope.load();

        //---------------- CRUD Actions ----------------

        $scope.add = function () {
            setEditMode(editMode.New);
        }
        
        $scope.edit = function (record) {
            setEditMode(editMode.Edit);
            //Object.create(record)
            $scope.editRecord = JSON.parse(JSON.stringify(record));
        }

        $scope.remove = function (record) {
            setEditMode(editMode.None);
            factory.delete(record.Id).success(function (ret) {
                $scope.load();
            });            
        }

        $scope.cancel = function (form) {           
            if (form)
                form.$rollbackViewValue();

            setEditMode(editMode.None);
        }       
        
        $scope.save = function () {
            if ($scope.editMode == editMode.Edit) {
                var newRec = factory.update($scope.editRecord).success(function (ret) {
                    $scope.load();
                    setEditMode(editMode.None);
                });
            }

            if ($scope.editMode == editMode.New) {
                var newRec = factory.insert($scope.newRecord).success(function (ret) {
                    $scope.load();
                    setEditMode(editMode.None);
                });
            }
        }
    }]);

    appControllers.controller('subjectController', ['$scope', 'factorySubject', function ($scope, factorySubject) {
        var factory = factorySubject;

        $scope.sortType = 'Name'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order
        $scope.searchText = '';     // set the default search/filter term

        var getEmptyRecord = function () { return { Id: 0, Name: '' }; }

        // Mode :
        // 0 - None
        // 1 - New
        // 2 - Edit
        var editMode = { None: 0, New: 1, Edit: 2 };

        function setEditMode(mode) {
            $scope.editMode = mode;
            $scope.newRecord = getEmptyRecord();
            $scope.editRecord = getEmptyRecord();
        }

        setEditMode(editMode.None);

        $scope.recordList = [];

        $scope.load = function () {
            factory.get().success(function (e) {
                $scope.recordList = e;
                $scope.cancel();
            });
        }

        $scope.load();

        //---------------- CRUD Actions ----------------

        $scope.add = function () {
            setEditMode(editMode.New);
        }

        $scope.edit = function (record) {
            setEditMode(editMode.Edit);
            //Object.create(record)
            $scope.editRecord = JSON.parse(JSON.stringify(record));
        }

        $scope.remove = function (record) {
            setEditMode(editMode.None);
            factory.delete(record.Id).success(function (ret) {
                $scope.load();
            });
        }

        $scope.cancel = function (form) {
            if (form)
                form.$rollbackViewValue();

            setEditMode(editMode.None);
        }

        $scope.save = function () {
            if ($scope.editMode == editMode.Edit) {
                var newRec = factory.update($scope.editRecord).success(function (ret) {
                    $scope.load();
                    setEditMode(editMode.None);
                });
            }

            if ($scope.editMode == editMode.New) {
                var newRec = factory.insert($scope.newRecord).success(function (ret) {
                    $scope.load();
                    setEditMode(editMode.None);
                });
            }
        }
    }]);

    appControllers.controller('adminContactController', ['$scope', 'factoryContact', function ($scope, factoryContact) {
        var factory = factoryContact;

        $scope.sortType = 'Name'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order
        $scope.searchText = '';     // set the default search/filter term
        $scope.recordList = [];
        
        factory.get().success(function (e) {
            $scope.recordList = e;
        });


        $scope.showDetail = function (record) {
            if (record.Detail)
                record.Detail = null;
            else
                factory.get(record.Id).success(function (detail) {
                    record.Detail = detail;
                });
        }
        
    }]);

})();
