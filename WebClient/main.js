var app = angular.module("mainApp",['ngRoute']);

app.config(['$routeProvider', function($routeProvider) {
	$routeProvider
	.when('/login', {
		templateUrl: 'views/login.html',
		controller: 'LoginCtrl'
	})
	.when('/userpage/:id', {
		templateUrl: 'views/userpage.html',
		controller: 'UserCtrl'
	})
	.otherwise({
		redirectTo: '/login'
	});
}]);

app.controller('LoginCtrl', function($rootScope, $scope) {

});

app.controller('UserCtrl', function($scope, $routeParams, $http) {	

	$scope.user_id = $routeParams['id'];
	
	$http({
		method: 'GET',
		url: 'http://'+window.location.hostname+':8085/clients/'+$scope.user_id
	}).success(function(res) {
		$scope.user = res;
	});
	
	$http({
		method: 'GET',
		url: 'http://'+window.location.hostname+':8085/clients/'+$scope.user_id+'/orders'
	}).success(function(res) {
		$scope.orders = res;
	});
});