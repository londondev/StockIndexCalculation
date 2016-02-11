angular.module('app')
    .factory('stockPriceFactory', ['$q','$http', function ($q,$http) {
        var stockFactory = {};

        stockFactory.upload = function (addStockRequest) {
            $http.post('/api/IndexData', addStockRequest);
        };

        var getModelAsFormData = function (data) {
            var dataAsFormData = new FormData();
            angular.forEach(data, function (value, key) {
                dataAsFormData.append(key, value);
            });
            return dataAsFormData;
        };

        stockFactory.saveModel = function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/IndexData',
                method: "POST",
                data: getModelAsFormData(data),
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function (result, status) {
                deferred.reject(status);
            });
            return deferred.promise;
        };
        return stockFactory;
    }]);