'use strict';

appFactories.factory('factoryArea', ['$http', function ($http) {
    var urlBase = '/api/area';
    var dataFactory = {};

    dataFactory.get = function () {
        return $http.get(urlBase);
    };

    dataFactory.insert = function (record) {
        return $http.post(urlBase, record);
    };

    dataFactory.update = function (record) {
        //return $http.put(urlBase + '/' + record.Id, record)
        return $http.put(urlBase, record)
    };

    dataFactory.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    };

    return dataFactory;
}]);

appFactories.factory('factorySubject', ['$http', function ($http) {
    var urlBase = '/api/subject';
    var dataFactory = {};

    dataFactory.get = function () {
        return $http.get(urlBase);
    };

    dataFactory.insert = function (record) {
        return $http.post(urlBase, record);
    };

    dataFactory.update = function (record) {
        return $http.put(urlBase, record)
    };

    dataFactory.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    };

    return dataFactory;
}]);

appFactories.factory('factoryContact', ['$http', function ($http) {
    var urlBase = '/api/contact';
    var dataFactory = {};

    dataFactory.get = function (id) {
        if(typeof id != 'undefined')
            return $http.get(urlBase + '/' + id);
        else
            return $http.get(urlBase);
    };

    dataFactory.insert = function (record) {
        return $http.post(urlBase, record);
    };
    
    return dataFactory;
}]);

////TODO AUTH
//appFactories.factory('factoryAuthData', [function () {
//    var authDataFactory = {};

//    var _authentication = {
//        IsAuthenticated: false,
//        username: ""
//    };
//    authDataFactory.authenticationData = _authentication;

//    return authDataFactory;
//}]);


//appFactories.factory('factoryAuthentication', ['$http', 'factoryAuthData', function ($http, factoryAuthData) {
//    var urlBase = '/api/login';
//    var dataFactory = {};

//    dataFactory.login = function (username, password) {
//        //var retValue = $http.post(urlBase, {Username : username, Password : password});
//        if (username == 'admin' && password == '1234') {
//            factoryAuthData.username = username;
//            factoryAuthData.IsAuthenticated = true;
//            return true;
//        } else {
//            factoryAuthData.username = "";
//            factoryAuthData.IsAuthenticated = false;

//            return false;
//        }
//    }

//    dataFactory.logout = function () {
//        factoryAuthData.username = "";
//        factoryAuthData.IsAuthenticated = false;
//    }

//    return dataFactory;
//}]);