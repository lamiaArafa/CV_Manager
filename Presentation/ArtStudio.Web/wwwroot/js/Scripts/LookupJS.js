let lookupId = 0;
let lookupObj = {};


(function FillLookupTypesData() {
    fillDropdownData('types', '/GetLookupTypes');
})();

$('#tblShowData').on('click', '.btn-edit', function () {

    var rowIndex = $(this).data('row-index');
    var rowData = $('#tblShowData').DataTable().row(rowIndex).data();

    lookupId = rowData.lookupId;
    $("#types").val(rowData.typeId).trigger("change");
    $("#name").val(rowData.lookupName);

    $('#types').prop('disabled', true);


    $("#btnCloseModalShowData").trigger("click");
});

$("#btnSave").on("click", function () {
    FillLookupObj();
    if (ValidateLookupData()) {
        saveConfirmation("Do you want to save?!", SaveLookup);
    }

})
function FillLookupObj() {
    lookupObj =
    {
        id: lookupId,
        typeId: $("#types").val(),
        lookupName: $("#name").val().trim(),
    }

}
function ValidateLookupData() {
    if (lookupObj.lookupName.length < 3 || lookupObj.lookupName.length > 200) {
        warningAlert("Name should be greater than 2 chars and less than or equal to 200");
        return false;
    }
    return true;
}
function SaveLookup() {

    save(lookupObj, '/SaveLookup');
}

$("#btnRemove").on("click", function () {
    if (lookupId != 0) {
        deleteConfirmation("Are You Sure, You Want To Delete This Lookup Name?!", DeleteLookup);
    }
    else {
        warningAlert("Please Select A Lookup First");
    }
})
function DeleteLookup() {

    deleteMethod({ lookupId: lookupId }, '/DeleteLookups');
}
function clear() {
    $("#types").val(1).trigger("change");
    $("#name").val("").trigger("change");

    lookupId = 0;
    lookupObj = {};
    $('#types').prop('disabled', false);

}


//////////////////////////////////////////// DataTable Defention ///////////////////////////////
let datatableColumns = [
    { data: "typeName", name: "typeName" },
    { data: "lookupName", name: "lookupName" },
]


initiateServerSideDataTable('tblShowData', "/GetPaginationLookups", datatableColumns);
