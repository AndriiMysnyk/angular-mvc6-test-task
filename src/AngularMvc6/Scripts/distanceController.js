(function () {
    'use strict';

    function distanceController($scope, stopsResource, distanceCalculatorService) {
        $scope.title = 'distanceController';

        stopsResource.getAll().$promise.then(function (result) {
            $scope.stops = result;
        });

        function actualizeDistance() {
            if (!$scope.stop1 || !$scope.stop2) {
                $scope.distanceString = "";
                return;
            }

            var distance = distanceCalculatorService.calculate(
                $scope.stop1.originalObject.Latitude,
                $scope.stop1.originalObject.Longitude,
                $scope.stop2.originalObject.Latitude,
                $scope.stop2.originalObject.Longitude);
            $scope.distanceString = "= " + Math.round(distance * 1000) / 1000 + " km";
        }

        $scope.setStop1 = function (stop) {
            $scope.stop1 = stop;
            actualizeDistance();
        }

        $scope.setStop2 = function (stop) {
            $scope.stop2 = stop;
            actualizeDistance();
        }
    }

    angular
        .module('app')
        .controller('distanceController', distanceController);

    distanceController.$inject = ['$scope', 'stopsResource', 'distanceCalculatorService'];
})();
