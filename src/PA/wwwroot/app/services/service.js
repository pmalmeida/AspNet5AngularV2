appServices.service('serviceAuthentication', function () {
    var dataService = {};
    var urlBase = '/api/login';

    var _authentication = {
        IsAuthenticated: false,
        username: ""
    };

    dataService.authenticationData = _authentication;

    dataService.login = function (username, password) {
        //var retValue = $http.post(urlBase, {Username : username, Password : password});
        if (username == 'admin' && password == '1234') {
            dataService.authenticationData.username = username;
            dataService.authenticationData.IsAuthenticated = true;
            return true;
        } else {
            dataService.authenticationData.username = "";
            dataService.authenticationData.IsAuthenticated = false;

            return false;
        }
    }

    dataService.logout = function () {
        dataService.authenticationData.username = "";
        dataService.authenticationData.IsAuthenticated = false;
    }

    return dataService;
});
