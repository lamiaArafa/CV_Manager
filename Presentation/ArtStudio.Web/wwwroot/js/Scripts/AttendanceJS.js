let attendanceObj = 0;
let today = getTodayDate();


(function fillClientsData() {
    fillDropdownData('clients', '/GetClients');
})();

(function initiateOperationDateDatePicker() {
    datepickerBuilder.SetMaxDays(0).Build('operationDate');
    addLessThanOrEqualToDateValidation('operationDate', today, 'Operation Date', 'Today');

})();

$("#clients").on("change", function () {
    let clientId = Number(this.value);
    fillDropdownData('courses', '/GetAllowedCoursesByClientId', { clientId: clientId });
})



$("#btnSave").on("click", function () {
    fillattendanceObj();
    if (validateAttendanceData()) {
        saveConfirmation("Do you want to save?!", saveAttendance);
    }

})
function fillattendanceObj() {
    attendanceObj = {
        clientId: $('#clients').val(),
        courseId: $('#courses').val(),
        attendanceDate: $('#operationDate').val(),
        takenSessionsCount: $('#takenSessionsCount').val(),
    }
}
function validateAttendanceData() {
    if (attendanceObj.clientId < 1) {
        warningAlert("You should to select client");
        return false;
    }
    if (attendanceObj.courseId < 1) {
        warningAlert("You should to select course");
        return false;
    }
    if (attendanceObj.takenSessionsCount < 1) {
        warningAlert("Taken sessions count should be greater than 0");
        return false;
    }

    if (!isDateLessThanOrEqualTo(attendanceObj.attendanceDate, today)) {
        warningAlert("Operation Date should be less than or equal to Today");
        return false;
    }
    return true;
}
function saveAttendance() {

    save(attendanceObj, '/SaveAttendance');
}

function clear() {

    $('#operationDate').val(getDatePickerStringFormatFromDate(today));
    $('#takenSessionsCount').val(1);
    $('#clients').val("").trigger("change");
    $('#courses').val("").trigger("change");
    attendanceObj = {};
}

$('#btnRemove').css('visibility', 'hidden');
$('#btnSearch').css('visibility', 'hidden');