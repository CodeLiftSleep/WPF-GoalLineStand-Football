'use strict';

angular.module('app.eCommerce').controller('OrdersDemoCtrl', function ($scope, orders) {

    $scope.orders = orders.data;

    $scope.tableOptions =  {
        "data": orders.data.data,
//            "bDestroy": true,
        "iDisplayLength": 15,
        columns: [
            {data: "orderId"},
            {data: "customerId"},
            {data: "purchase"},
            {data: "delivery"},
            {data: "basePrice"},
            {data: "postalZip"},
            {data: "status"}
        ],
        "order": [[1, 'asc']]
    }
});