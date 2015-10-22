(function () {
    'use strict';

    function deg2Rad(deg) {
        return deg * (Math.PI / 180);
    }

    function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
        var r = 6371; // Radius of the earth in km
        var dLat = deg2Rad(lat2 - lat1);  // deg2rad below
        var dLon = deg2Rad(lon2 - lon1);
        var a =
          Math.sin(dLat / 2) * Math.sin(dLat / 2) +
          Math.cos(deg2Rad(lat1)) * Math.cos(deg2Rad(lat2)) *
          Math.sin(dLon / 2) * Math.sin(dLon / 2);
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var d = r * c; // Distance in km
        return d;
    }

    function distancesController($scope, stopsResource) {
        $scope.title = 'distancesController';

        stopsResource.getAll().$promise.then(function (result) {
            $scope.stops = result;
        });

        function actualizeDistance() {
            if (!$scope.stop1 || !$scope.stop2) {
                $scope.distanceString = "";
                return;
            }

            var distance = getDistanceFromLatLonInKm(
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
        .controller('distancesController', distancesController);

    distancesController.$inject = ['$scope', 'stopsResource'];
})();
