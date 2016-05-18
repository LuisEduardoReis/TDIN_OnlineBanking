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
	
});