var KobenApp = angular.module('KobenApp', []);
KobenApp.controller('StaffController', function ($scope, KobenService) {
    $scope.ItemToBeDeleted = 0;
    $scope.myFunc = function (number) {
        
        var xxx = KobenService.getItemToBeDeleted(number);

        xxx.then(function (data) {

            console.log(data.data);
            $scope.Name = data.data.Name;
            $scope.ID = data.data.ID;
            $scope.Phone = data.data.Phone;
            $scope.Title = data.data.Title;
            $scope.Image = data.data.Image;
            $scope.ItemToBeDeleted = $scope.ID;
            console.log(data.data.ID);
        });

        $scope.ItemToBeDeleted = xxx;
        
    };

    $scope.delete = function () {

        var xxx = KobenService.postItemToBeDeleted($scope.ItemToBeDeleted);
        window.location.reload();
    };

});

KobenApp.factory('KobenService', ['$http', function ($http) {

    var KobenService = {};
    KobenService.getItemToBeDeleted = function (number) {
        return $http.get('/Staffs/Delete/' + number);
    };

    KobenService.postItemToBeDeleted = function (number) {
        return $http.post('/Staffs/Delete/' + number);
    };

    return KobenService;

}]);
