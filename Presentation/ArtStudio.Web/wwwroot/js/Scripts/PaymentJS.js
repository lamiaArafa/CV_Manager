let paymentObj = {};
let clientId = 0;
let today = getTodayDate();

$('#btnRemove').css('visibility', 'hidden');

(function initiateOperationDateDatePicker() {
    datepickerBuilder.SetMaxDays(0).Build('operationDate');
    addLessThanOrEqualToDateValidation('operationDate', today, 'Operation Date', 'Today');

})();

$('#tblShowData').on('click', '.btn-edit', function () {

    var rowIndex = $(this).data('row-index');
    var rowData = $('#tblShowData').DataTable().row(rowIndex).data();

    clientId = rowData.id;
    $("#clientName").val(rowData.name);
    $("#phoneNum").val(rowData.mobile);
    $("#oldDebt").val(rowData.debt);

    $("#btnCloseModalShowData").trigger("click");
});

$("#btnSave").on("click", function () {

    fillPaymentObj();

    if (validatePaymentData()) {
        saveConfirmation("Do you want to save?!", savePayment);
    }

})

function fillPaymentObj() {
    paymentObj =
    {
        ClientId: clientId,
        PaidAmount: $('#paidAmount').val(),
        Date: $('#operationDate').val()
    }

}
function validatePaymentData() {

    if (paymentObj.ClientId < 1) {
        warningAlert("Please choose a client first");
        return false;
    }

    if (paymentObj.PaidAmount < 1 && paymentObj.PaidAmount > -1) {
        warningAlert("Paid Amount must be >= 1 or  <= -1");
        return false;
    }
    if (!isDateLessThanOrEqualTo(paymentObj.Date, today)) {
        warningAlert("OperationDate should be less than or equal to Today");
        return false;
    }

    return true;
}


function savePayment() {

    save(paymentObj, '/SavePayment');
}

function clear() {


    $("#clientName").val("").trigger("change");
    $("#phoneNum").val("").trigger("change");
    $("#oldDebt").val("0").trigger("change");
    $("#paidAmount").val("0").trigger("change");
    $('#operationDate').val(getDatePickerStringFormatFromDate(today));

    paymentObj = {};
    clientId = 0;
}

//////////////////////////////////////////// DataTable Defention ///////////////////////////////
let datatableColumns = [
    { data: "name", name: "name" },
    { data: "mobile", name: "mobile" },
    { data: "email", name: "email" },
    { data: "debt", name: "debt" },

]

initiateServerSideDataTable('tblShowData', "/GetPaginationClients", datatableColumns);
