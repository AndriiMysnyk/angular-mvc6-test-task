/**
 * @module distanceCalculatorService
 * @see http://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula
 * @overview Haversine formula implementation for distance calculation
 */

(function () {
    'use strict';

    function distanceCalculatorService() {

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
        
        return {
            calculate: getDistanceFromLatLonInKm
        };
    }

    angular
        .module('app')
        .factory('distanceCalculatorService', distanceCalculatorService);
})();