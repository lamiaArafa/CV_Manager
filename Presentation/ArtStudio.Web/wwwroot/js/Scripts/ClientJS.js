let clientObj = {};
let clientId = 0;


(function fillAgeGroupData() {

    fillDropdownData('ageGroupId', '/GetAgeGroups');
})();

(function fillCountryData() {
    fillDropdownData('countryId', '/GetCountries');
})();



$('#tblShowData').on('click', '.btn-edit', function () {

    var rowIndex = $(this).data('row-index');
    var rowData = $('#tblShowData').DataTable().row(rowIndex).data();

    clientId = rowData.id;
    $("#clientName").val(rowData.name);
    $("#phoneNum").val(rowData.mobile);
    $("#mail").val(rowData.email);
    $("#countryId").val(rowData.countryId).trigger("change");
    $("#ageGroupId").val(rowData.ageGroupId).trigger("change");
    $("#debt").val(rowData.debt);
    $('#isActive').prop('checked', rowData.isActive);
    $("#btnCloseModalShowData").trigger("click");
});

$("#btnSave").on("click", function () {

    fillClientObj();

    if (isValidClientData()) {
        saveConfirmation("Do you want to save?!", saveClient);
    }

})

function fillClientObj() {
    clientObj =
    {
        Id: clientId,
        Name: $("#clientName").val().trim(),
        Mobile: $("#phoneNum").val().trim(),
        Email: $("#mail").val().trim(),
        CountryId: $("#countryId").val(),
        AgeGroupId: $("#ageGroupId").val(),
        isActive: $('#isActive').prop('checked')

    }

}
function isValidClientData() {

    if (clientObj.Name.length < 3 || clientObj.Name.length > 200) {
        warningAlert("Name should be greater than 3 chars and less than or equal to 200");
        return false;
    }

    if (clientObj.Mobile.length < 6 || clientObj.Mobile.length > 15) {
        warningAlert("Mobile should be greater than 6 numbers and less than or equal to 15");
        return false;
    }
    if (isValidPhoneNumber(clientObj.Mobile)) {
        warningAlert("Invalid Mobile Number");
        return false;
    }

    if (!isValidEmail(clientObj.Email)) {
        warningAlert("Invalid Email");
        return false;
    }
    if ($("#countryId").val() == null) {
        warningAlert("You have to select at least one country");
        return false;
    }
    if ($("#ageGroupId").val() == null) {
        warningAlert("You have to select at least one age group");
        return false;
    }
    return true;
}


function saveClient() {

    save(clientObj, '/SaveClient');
}

$("#btnRemove").on("click", function () {
    if (clientId != 0) {
        deleteConfirmation("Are You Sure, You Want To Delete This Client?!", deleteClient);
    }
    else {
        warningAlert("Please Select  Client First");
    }
})
function deleteClient() {

    deleteMethod({ clientId: clientId }, '/DeleteClient');
}

function clear() {
    $("#clientName").val("").trigger("change");
    $("#phoneNum").val("").trigger("change");
    $("#mail").val("").trigger("change");

    clientObj = {};
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
