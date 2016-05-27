var app = angular.module("mainApp", ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/login', {
            templateUrl: 'views/login.html',
            controller: 'LoginCtrl'
        })
        .when('/userpage/:id', {
            templateUrl: 'views/userpage.html',
            controller: 'UserCtrl'
        })
        .when('/userpage/:id/neworder', {
            templateUrl: 'views/neworder.html',
            controller: 'NewOrderCtrl'
        })
        .otherwise({
            redirectTo: '/login'
        });
}]);

app.controller('LoginCtrl', function ($rootScope, $scope) {

});

app.controller('UserCtrl', function ($scope, $routeParams, $http) {

    $scope.user_id = $routeParams['id'];
	$scope.loadCounter = 3;
	
    $http({
        method: 'GET',
        url: 'http://' + window.location.hostname + ':8085/companies'
    }).success(function (res) {
        $scope.companies = {}
        for (var i = 0; i < res.length; i++) {
            $scope.companies[res[i].id] = res[i].name;
        }
		$scope.loadCounter--;
    });

    $http({
        method: 'GET',
        url: 'http://' + window.location.hostname + ':8085/clients/' + $scope.user_id
    }).success(function (res) {
        $scope.user = res;		
		$scope.loadCounter--;
    });

    $http({
        method: 'GET',
        url: 'http://' + window.location.hostname + ':8085/clients/' + $scope.user_id + '/orders'
    }).success(function (res) {
        $scope.orders = res;
		$scope.loadCounter--;
    });
});

app.controller('NewOrderCtrl', function ($rootScope, $scope, $routeParams, $http) {

    $scope.user_id = $routeParams['id'];
    $scope.order = {}
    $scope.order.client = $scope.user_id;
    $scope.types = [
        {id: 0, label: "Buy"},
        {id: 1, label: "Sell"}
    ];
    $scope.submitOrder = function () {
        $http({
            method: 'POST',
            data: $scope.order,
            url: 'http://' + window.location.hostname + ':8085/orders'
        }).then(function (res) {
            console.log(res);

            if(res.status == 200)
                $('.alert-success').show();
                $('input').val("");

        });
    }
	$scope.loadCounter = 1;

    $http({
        method: 'GET',
        url: 'http://' + window.location.hostname + ':8085/companies'
    }).success(function (res) {
        $scope.companies = res;
		$scope.loadCounter--;
    });
});
