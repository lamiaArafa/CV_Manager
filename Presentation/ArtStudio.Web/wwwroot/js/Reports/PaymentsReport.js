


$("#btnGenerateReport").on("click", function () {

    let searchObj = {
        startDate: $('#startDate').val(),
        endDate: $('#endDate').val()
    }

    sendRequest(searchObj, '/GetPaymentReport').then((response) => {

        $('#tblReportData').DataTable().clear().draw();

        if (response != undefined) {
            response.data.forEach(function (item) {
                $('#tblReportData').DataTable().row.add([
                    item.clientName,
                    item.amount,
                    item.operationDate
                ]).draw(false);
            });
        }
    });
});
