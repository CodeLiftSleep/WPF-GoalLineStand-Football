'use strict';

angular.module('app.tables').controller('JqGridCtrl', function ($scope) {
    $scope.gridData = {
        data: [
            {
                id: "1",
                date: "2007-10-01",
                name: "test",
                note: "note",
                amount: "200.00",
                tax: "10.00",
                total: "210.00"
            },
            {
                id: "2",
                date: "2007-10-02",
                name: "test2",
                note: "note2",
                amount: "300.00",
                tax: "20.00",
                total: "320.00"
            },
            {
                id: "3",
                date: "2007-09-01",
                name: "test3",
                note: "note3",
                amount: "400.00",
                tax: "30.00",
                total: "430.00"
            },
            {
                id: "4",
                date: "2007-10-04",
                name: "test",
                note: "note",
                amount: "200.00",
                tax: "10.00",
                total: "210.00"
            },
            {
                id: "5",
                date: "2007-10-05",
                name: "test2",
                note: "note2",
                amount: "300.00",
                tax: "20.00",
                total: "320.00"
            },
            {
                id: "6",
                date: "2007-09-06",
                name: "test3",
                note: "note3",
                amount: "400.00",
                tax: "30.00",
                total: "430.00"
            },
            {
                id: "7",
                date: "2007-10-04",
                name: "test",
                note: "note",
                amount: "200.00",
                tax: "10.00",
                total: "210.00"
            },
            {
                id: "8",
                date: "2007-10-03",
                name: "test2",
                note: "note2",
                amount: "300.00",
                tax: "20.00",
                total: "320.00"
            },
            {
                id: "9",
                date: "2007-09-01",
                name: "test3",
                note: "note3",
                amount: "400.00",
                tax: "30.00",
                total: "430.00"
            },
            {
                id: "10",
                date: "2007-10-01",
                name: "test",
                note: "note",
                amount: "200.00",
                tax: "10.00",
                total: "210.00"
            },
            {
                id: "11",
                date: "2007-10-02",
                name: "test2",
                note: "note2",
                amount: "300.00",
                tax: "20.00",
                total: "320.00"
            },
            {
                id: "12",
                date: "2007-09-01",
                name: "test3",
                note: "note3",
                amount: "400.00",
                tax: "30.00",
                total: "430.00"
            },
            {
                id: "13",
                date: "2007-10-04",
                name: "test",
                note: "note",
                amount: "200.00",
                tax: "10.00",
                total: "210.00"
            },
            {
                id: "14",
                date: "2007-10-05",
                name: "test2",
                note: "note2",
                amount: "300.00",
                tax: "20.00",
                total: "320.00"
            },
            {
                id: "15",
                date: "2007-09-06",
                name: "test3",
                note: "note3",
                amount: "400.00",
                tax: "30.00",
                total: "430.00"
            },
            {
                id: "16",
                date: "2007-10-04",
                name: "test",
                note: "note",
                amount: "200.00",
                tax: "10.00",
                total: "210.00"
            },
            {
                id: "17",
                date: "2007-10-03",
                name: "test2",
                note: "note2",
                amount: "300.00",
                tax: "20.00",
                total: "320.00"
            },
            {
                id: "18",
                date: "2007-09-01",
                name: "test3",
                note: "note3",
                amount: "400.00",
                tax: "30.00",
                total: "430.00"
            }
        ],
        colNames: ['Actions', 'Inv No', 'Date', 'Client', 'Amount', 'Tax', 'Total', 'Notes'],
        colModel: [
            {
                name: 'act',
                index: 'act',
                sortable: false
            },
            {
                name: 'id',
                index: 'id'
            },
            {
                name: 'date',
                index: 'date',
                editable: true
            },
            {
                name: 'name',
                index: 'name',
                editable: true
            },
            {
                name: 'amount',
                index: 'amount',
                align: "right",
                editable: true
            },
            {
                name: 'tax',
                index: 'tax',
                align: "right",
                editable: true
            },
            {
                name: 'total',
                index: 'total',
                align: "right",
                editable: true
            },
            {
                name: 'note',
                index: 'note',
                sortable: false,
                editable: true
            }
        ]
    }


    $scope.getSelection = function(){
        alert(jQuery('table').jqGrid('getGridParam', 'selarrrow'));
    };

    $scope.selectRow = function(row){
       jQuery('table').jqGrid('setSelection', row);

    }
});