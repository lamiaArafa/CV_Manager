let materialPurchaseObj = {};
let today = getTodayDate();

$('#btnRemove').css('visibility', 'hidden');
$('#btnSearch').css('visibility', 'hidden');

(function FillClientsData() {
    fillDropdownData('clientId', '/GetClients');
})();
(function FillMaterialssData() {
    fillDropdownData('materialId', '/GetMaterials');
})();

(function initiateOperationDateDatePicker() {
    datepickerBuilder.SetMaxDays(0).Build('operationDate');
    addLessThanOrEqualToDateValidation('operationDate', today, 'Operation Date', 'Today');

})();

$("#btnSave").on("click", function () {

    fillMaterialPurchaseObj();

    if (validatePaymentData()) {
        saveConfirmation("Do you want to save?!", saveMaterialPurchase);
    }

})

function fillMaterialPurchaseObj() {
    materialPurchaseObj =
    {
        ClientId: $('#clientId').val(),
        MaterialId: $('#materialId').val(),
        Cost: $('#cost').val(),
        OperationDate: $('#operationDate').val()
    }

}
function validatePaymentData() {

    if (materialPurchaseObj.ClientId < 1) {
        warningAlert("Please choose a client first");
        return false;
    }

    if (materialPurchaseObj.MaterialId < 1) {
        warningAlert("Please choose a client first");
        return false;
    }

    if (materialPurchaseObj.Cost < 1) {
        warningAlert("Paid Amount must be >= 1");
        return false;
    }

    if (!isDateLessThanOrEqualTo(materialPurchaseObj.OperationDate, today)) {
        warningAlert("OperationDate should be less than or equal to Today");
        return false;
    }

    return true;
}


function saveMaterialPurchase() {

    save(materialPurchaseObj, '/SaveMaterialPurchase');
}

function clear() {


    $("#clientId").val("").trigger("change");
    $("#materialId").val("").trigger("change");
    $("#cost").val("0").trigger("change");
    $('#operationDate').val(getDatePickerStringFormatFromDate(today));

    materialPurchaseObj = {};
}
