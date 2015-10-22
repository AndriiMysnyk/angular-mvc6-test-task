(function () {
    'use strict';

    function stopsResource($resource) {

        function getAll() {
            return $resource('/stops').query();
        }
        return {
            getAll: getAll
        };
    }

    angular
        .module('app')
        .factory('stopsResource', stopsResource);

    stopsResource.$inject = ['$resource'];
})();