let enrollmentId = 0;
let enrollmentObj = 0;
let today = getTodayDate();


(function FillClientsData() {
    fillDropdownData('clients', '/GetClients');
})();
(function FillCoursesData() {
    fillDropdownData('courses', '/GetCourses');
})();
(function InitiateStartDateDatePicker() {
    datepickerBuilder.SetMinDays(-365)
        .Build('startDate');

})();

$('#startDate').on('change', function () {

    let value = new Date(this.value);
    //if ((!isValidDate(value) || value < today) && enrollmentId == 0) {

    //    warningAlert("Start date should be greater than or equal to today");
    //    $('#startDate').val(getDatePickerStringFormatFromDate(today));
    //    value = today;
    //}
    let allowedWeeks = parseInt($('#allowedWeeks').val());
    let expirationdate = addDays(value, 7 * allowedWeeks);

    $('#expirationDate').val(getDatePickerStringFormatFromDate(expirationdate));
});
$('#allowedWeeks').on('blur', function () {
    let startDate = new Date($('#startDate').val());
    let allowedWeeks = parseInt($('#allowedWeeks').val());
    let expirationdate = addDays(startDate, 7 * allowedWeeks);

    $('#expirationDate').val(getDatePickerStringFormatFromDate(expirationdate));
});

$('#tblShowData').on('click', '.btn-edit', function () {

    $('#startDate').prop('disabled', false);
    $('#btnRemove').css('visibility', 'visible');
    $('#clients').prop('disabled', true);
    $('#courses').prop('disabled', true);

    var rowIndex = $(this).data('row-index');
    var rowData = $('#tblShowData').DataTable().row(rowIndex).data();

    enrollmentId = rowData.id;
    $('#clients').val(rowData.clientId).trigger("change");
    $('#courses').val(rowData.courseId).trigger("change");
    $('#startDate').val(rowData.startDateString);
    $('#allowedWeeks').val(rowData.allowedWeeks);
    $('#sessionsCount').val(rowData.sessionsCount);
    $('#expirationDate').val(rowData.expirationDateString);
    $('#takenSessionsCount').val(rowData.takenSessionsCount);
    $('#cost').val(rowData.cost);


    let value = new Date(rowData.startDate);

    if (value < today) {
        $('#startDate').prop('disabled', true);
    }

    if (rowData.takenSessionsCount > 0) {
        $('#btnRemove').css('visibility', 'hidden');
    }
    $("#btnCloseModalShowData").trigger("click");
});

$("#btnSave").on("click", function () {
    fillEnrollmentObj();
    if (validateEnrollmentData()) {
        saveConfirmation("Do you want to save?!", saveEnrollment);
    }

})
function fillEnrollmentObj() {
    enrollmentObj = {
        id: enrollmentId,
        clientId: $('#clients').val(),
        courseId: $('#courses').val(),
        startDate: $('#startDate').val(),
        sessionsCount: $('#sessionsCount').val(),
        allowedWeeks: $('#allowedWeeks').val(),
        cost: $('#cost').val(),
    }
}
function validateEnrollmentData() {
    if (enrollmentObj.clientId == "") {
        warningAlert("You should To Select Client First");
        return false;
    }
    if (enrollmentObj.courseId == "") {
        warningAlert("You should To Select Course First");
        return false;
    }
    if (enrollmentObj.cost < 1) {
        warningAlert("Cost should be greater than 0");
        return false;
    }

    if (enrollmentObj.sessionsCount < 1) {
        warningAlert("Sessions Count should be greater than 0");
        return false;
    }

    if (enrollmentObj.allowedWeeks < 1) {
        warningAlert("Allowed Weeks should be greater than 0");
        return false;
    }
    return true;
}
function saveEnrollment() {

    save(enrollmentObj, '/SaveEnrollment');
}

$("#btnRemove").on("click", function () {
    if (enrollmentId != 0) {
        deleteConfirmation("Are You Sure, You Want To Delete This Enrollment?!", deleteEnrollment);
    }
    else {
        warningAlert("Please Select An Enrollment First");
    }
})

function deleteEnrollment() {

    deleteMethod({ enrollmentId: enrollmentId }, '/DeleteEnrollment');
}
function clear() {

    let today = getTodayDate();
    $('#startDate').val(getDatePickerStringFormatFromDate(today));
    $('#sessionsCount').val(1);
    $('#allowedWeeks').val(1);
    $('#cost').val(1);
    $('#expirationDate').val(getDatePickerStringFormatFromDate(today));
    enrollmentId = 0;
    enrollmentObj = {};
;

    $('#startDate').prop('disabled', false);
    $('#clients').prop('disabled', false);
    $('#courses').prop('disabled', false);
    $('#btnRemove').css('visibility', 'visible');
}


////cost').val(),//////////////////////////////////////// DataTable Defention ///////////////////////////////
let datatableColumns = [
    { data: "clientName", name: "clientName" },
    { data: "courseName", name: "courseName" },
    { data: "startDateString", name: "startDateString" },
    { data: "expirationDateString", name: "expirationDateString" },
    { data: "takenSessionsCount", name: "takenSessionsCount" },

]

initiateServerSideDataTable('tblShowData', "/GetPaginationActiveEnrollments", datatableColumns);