////////////////////////////// Alerts //////////////////////
function successAlert(message) {

    Swal.fire({
        position: "top",
        title: 'Done',
        text: message,
        icon: 'success',
        confirmButtonText: 'Ok'
    });

    $("#swal2-content").css("background-color", '#FFEBD8');
}

function warningAlert(message, IsCloseAfterShowing) {

    Swal.fire({
        icon: "warning",
        title: "Warning...",
        text: message,
    });

}

function errorAlert(message, IsCloseAfterShowing, Size) {

    Swal.fire({
        title: 'Error !!!',
        text: message,
        icon: 'error',
        confirmButtonText: 'Close'
    });

    if (Size == undefined) { Size = 400; }
    $("#swal2-content").css("background-color", "rgb(234, 56, 56)");
    $(".swal2-popup.swal2-modal.swal2-show").width(Size);

}

///////////////////////// Confirmation ////////////////////////
function saveConfirmation(message, CallbackMethod) {
    Swal.fire({
        title: message,
        showCancelButton: true,
        confirmButtonText: "Save",
    })
        .then((result) => {
            if (result.isConfirmed) {
                try {
                    CallbackMethod();
                } catch (error) {
                    console.error(error);
                    errorAlert("An error occurred while saving.");
                }
            }
        })
}

function deleteConfirmation(message, CallbackMethod) {
    Swal.fire({
        title: message,
        showCancelButton: true,
        confirmButtonText: "Delete",

    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            CallbackMethod();
        }
    });
}

//////////////////////////////////// Get Data/////////////////////////////////////////////

async function sendRequest(args, requestUrl) {

    let result;

    try {
        result = await $.ajax({
            url: requestUrl,
            type: 'POST',
            data: args
        });
        return result;
    } catch (error) {
        if (error.status == 400) // Bad
            return error.responseJSON

        errorAlert(error.statusText);
    }
}

////////////////////////////////////// Operations ///////////////////////////////////

function fillDropdownData(dropdownId, URL, args) {
    if (args == undefined || args == null) {
        args = {};
    }
    sendRequest(args, URL).then((response) => {
        if (response != undefined) {
            let dropdown = $("#" + dropdownId);

            let options = `<option value="" disabled selected>Select</option>`
            for (item of response.data) {
                options += `<option value="${item.id}">${item.name}</option>`;
            }
            dropdown.html(options);
        }
    });
}

function save(dataObj, URL) {

    return sendRequest(dataObj, URL)
        .then((response) => {
            if (response !== undefined) {
                if (response.succeeded) {
                    successAlert('Saved Successfully');
                    clear();
                } else {
                    errorAlert(response.errorMessage);
                }
            }
        })
        .catch((error) => {
            console.error(error);
            errorAlert("An error occurred while saving.");
            throw error;
        });
}

function deleteMethod(dataObj, URL) {
    sendRequest(dataObj, URL).then((response) => {
        if (response != undefined) {
            if (response.succeeded) {
                successAlert('Deleted Successfully');
                Clear();
            }
            else {
                errorAlert(response.errorMessage);
            }
        }
    });
}
///////////////////////////////// Validation ///////////////////////////
function isValidEmail(mail) {
    let mailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    if ((mail != undefined && mail.trim().length > 4 && mailPattern.test(mail)) || mail.length == 0)
        return true;

    return false;
}

function isValidPhoneNumber(number) {

    let numberPattern = /^[0-9]$/;
    if (number != undefined && number.trim().length > 2 && numberPattern.test(number))
        return true;

    return false;
}

//////////////////////// DataTable //////////////////////////////////////
let mydatatable;
var searchInput;
function initiateServerSideDataTable(tableId, URL, columnsArr, columnDefs) {

    $('#btnSearch').on('click', function () {
        if (mydatatable != undefined) {
            mydatatable.clear().draw();
        } else {
            dataTableDefention(tableId, URL, columnsArr, columnDefs);
            searchInput = $('#' + tableId + '_filter input');
            changeSearchBehavior();
        }

    });
}

function dataTableDefention(tableId, URL, columnsArr, columnDefs) {

    columnsArr.push({ data: "id", name: "id" });

    if (columnDefs == undefined) {
        columnDefs = [];
    }
    columnDefs.push({
        targets: -1, // The last column
        render: function (data, type, row, meta) {
            return '<button class="btn btn-edit" style="padding:0px;width:auto;color:#0d6efd;font-size: small; " data-row-index="' + meta.row + '"> <i class="fa-solid fa-pen-to-square"></i> </button>';
        }
    });
    columnDefs.push({ "className": "dt-center", "targets": "_all" });

    mydatatable = $('#' + tableId).DataTable(
        {
            ajax: {
                url: URL,
                type: "POST",
            },
            processing: true,
            serverSide: true,
            filter: true,
            columns: columnsArr,
            columnDefs: columnDefs,
            ordering: false,
        }
    );

}

function changeSearchBehavior() {

    searchInput.unbind();

    searchInput.bind('keyup', function (e) {
        if (e.keyCode == 13) {
            checkSearch(this.value);
        }
    });

    searchInput.on('blur', function () {
        checkSearch(this.value);
    });
}

let oldSearch = '';
function checkSearch(newSearch) {
    if (newSearch != oldSearch) {
        oldSearch = newSearch;
        mydatatable.search(newSearch).draw();
    }
}


/////////////////////////DateTimePicker ///////////////////

datepickerBuilder = {
    minDays: -36500,
    maxDays: 365,
    SetMinDays(_minDays) {
        this.minDays = _minDays;
        return this;
    },
    SetMaxDays(_maxDays) {
        this.maxDays = _maxDays;
        return this;
    },
    Build(datePickerId) {
        $("#" + datePickerId).datepicker(
            {
                dateFormat: "yy-mm-dd",
                minDate: this.minDays,
                maxDate: "" + this.maxDays + "D"
            });
    }
}
function getDatePickerStringFormatFromDate(yourDate) {
    return yourDate.toISOString().split('T')[0]
}
function getTodayDate() {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const day = String(today.getDate()).padStart(2, '0');
    return new Date(year + "-" + month + "-" + day);
}
function addDays(yourDate, daysCount) {
    return new Date(yourDate.setDate(yourDate.getDate() + daysCount));
}

function addLessThanOrEqualToDateValidation(datePickerId, compareToValue, datePickerDisplayValue, compareToId) {
    $('#' + datePickerId).on('change', function () {
        if (!isDateLessThanOrEqualTo(this.value, compareToValue)) {
            warningAlert(datePickerDisplayValue + " should be less than or equal to " + compareToId);
            $('#' + datePickerId).val(getDatePickerStringFormatFromDate(compareToValue));
        }
    });
}

function isDateLessThanOrEqualTo(_datePickerValue, compareTo) {
    let value = new Date(_datePickerValue);
    if (value > compareTo) {
        return false;
    }
    return true;
}

function isValidDate(_date) {
    return _date instanceof Date && !isNaN(_date);
}


function addUpdateDatePickerMinDate(startDatePickerId, endDatePickerId) {

    $('#' + startDatePickerId).on('change', function () {

        var endDatePicker = $('#' + endDatePickerId);
        endDatePicker.datepicker("option", "minDate", this.value);
    });
}

// Event handler for start date change


//////////////////////////Notes///////////////////////
//$(function () {
//    $("#from").datepicker({
//        defaultDate: "+1w",
//        changeMonth: true,
//        numberOfMonths: 2,
//        minDate: 4,
//        onSelect: function (selectedDate) {
//            //set #to date +4 days in the future, starting from #from date
//            var fromDate = new Date(selectedDate);
//            var minDate = new Date(fromDate.setDate(fromDate.getDate() + 4));

//            $("#to").datepicker("option", "minDate", minDate);
//        },
//        onClose: function (selectedDate) {
//            //alternatively call it in onSelect
//            totalDays();
//        }
//    });
//    $("#to").datepicker({
//        defaultDate: "+1w",
//        changeMonth: true,
//        numberOfMonths: 2,
//        minDate: 4,
//        onClose: function (selectedDate) {
//            //alternatively call it in onSelect
//            totalDays();
//        }
//    });

//    function totalDays() {
//        //subtract the two Date objects, convert seconds to days
//        var from = $('#from').datepicker('getDate');
//        var to = $('#to').datepicker('getDate');
//        var seconds = to - from;
//        var days = Math.ceil(seconds / (1000 * 3600 * 24));

//        //dont fill in value if only #to has a valid
//        if (days > 0 && from) {
//            $('#totaldays').val(days);
//        }
//    }

//});
// search-box open close js code
