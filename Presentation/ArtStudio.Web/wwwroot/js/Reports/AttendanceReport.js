


$("#btnGenerateReport").on("click", function () {

    let searchObj = {
        startDate: $('#startDate').val(),
        endDate: $('#endDate').val()
    }

    sendRequest(searchObj, '/GetAttendanceReport').then((response) => {

        $('#tblReportData').DataTable().clear().draw();

        if (response != undefined) {
            response.data.forEach(function (item) {
                $('#tblReportData').DataTable().row.add([
                    item.clientName,
                    item.courseName,
                    item.sessionsCount,
                    item.operationDate
                ]).draw(false);
            });
        }
    });
});
