


$("#btnGenerateReport").on("click", function () {

    let searchObj = {
        startDate: $('#startDate').val(),
        endDate: $('#endDate').val()
    }

    sendRequest(searchObj, '/GetExpensesReport').then((response) => {

        $('#tblReportData').DataTable().clear().draw();

        if (response != undefined) {
            response.data.forEach(function (item) {
                $('#tblReportData').DataTable().row.add([
                    item.employeeName,
                    item.expenseName,
                    item.amount,
                    item.operationDate
                ]).draw(false);
            });
        }
    });
});
