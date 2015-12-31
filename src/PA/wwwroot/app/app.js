/// <reference path="../views/authentication/login.html" />
var appPA = angular.module('appPA', [
    'ngRoute',
    'ngCookies',
    'appControllers',
    'appFactories',
    'appFilters',
    'appDirectives',
    'appServices',
    'blockUI'
]);

var appControllers = angular.module('appControllers', []);
var appFactories = angular.module('appFactories', []);
var appFilters = angular.module('appFilters', []);
var appDirectives = angular.module('appDirectives', []);
var appServices = angular.module('appServices', []);


////---------------- Authentication Interceptor ---------
//appPA.config(function ($httpProvider) {
//    $httpProvider.interceptors.push('authInterceptorService');
//});

//---------------- Routing Pages ----------------------
appPA.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
            when('/', {
                templateUrl: 'views/home/index.html',
                controller: 'homeController'
            }).
            //when('/admin', {
            //    templateUrl: 'views/backoffice/index.html'
            //}).
            when('/admin/area', {
                templateUrl: 'views/backoffice/area.html'
            }).
            when('/admin/subject', {
                templateUrl: 'views/backoffice/subject.html'
            }).
            when('/admin/contact', {
                templateUrl: 'views/backoffice/contact.html'
            }).
            when('/contact', {
                templateUrl: 'views/contact/index.html',
                controller: 'contactController'
            }).
            when('/login', {
                templateUrl: 'views/authentication/login.html'
            }).
            when('/logout', {
                templateUrl: 'views/authentication/logout.html'
            }).
            otherwise({
                redirectTo: '/'
            });
  }]);
