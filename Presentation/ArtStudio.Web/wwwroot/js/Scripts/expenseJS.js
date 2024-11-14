let expenseObj = {};
let expenseId = 0;
let today = getTodayDate();


(function fillEmployeeData() {

    fillDropdownData('employeeId', '/GetEmployees');
})();

(function FillExpenseTypesData() {

    fillDropdownData('expenseTypeId', '/GetExpenseTypes');
})();

(function initiateOperationDateDatePicker() {
    datepickerBuilder.SetMaxDays(0).Build('operationDate');
    addLessThanOrEqualToDateValidation('operationDate', today, 'Operation Date', 'Today');

})();

//----------------------------------------------

$("#btnSave").on("click", function () {

    fillExpensesObj();

    if (isValidExpenseData()) {
        saveConfirmation("Do you want to save?!", saveExpense);
    }

})


function fillExpensesObj() {
    expenseObj =
    {
        Id: expenseId,
        ExpenceTypeId: $("#expenseTypeId").val(),
        EmployeeId: $("#employeeId").val(),
        Amount: $("#amount").val(),
        OperationDate: $("#operationDate").val()
    }

}

function isValidExpenseData() {

    if (expenseObj.ExpenceTypeId < 1) {
        warningAlert("Please choose expense type");
        return false;
    }
    if (expenseObj.EmployeeId < 1) {
        warningAlert("Please choose employee");
        return false;
    }

    if (expenseObj.Amount < 1) {
        warningAlert("Amount must be greater than or equal to one");
        return false;
    }

    if (!isDateLessThanOrEqualTo(expenseObj.OperationDate, today)) {
        warningAlert("OperationDate should be less than or equal to Today");
        return false;
    }

    return true;
}


function saveExpense() {

    save(expenseObj, '/SaveExpenses');
}

function clear() {

    $("#expenseTypeId").val("").trigger("change");
    $("#employeeId").val("").trigger("change");
    $("#amount").val("1").trigger("change");
    $('#operationDate').val(getDatePickerStringFormatFromDate(today));
    expenseObj = {};
    expenseId = 0;
}


$('#tblShowData').on('click', '.btn-edit', function () {

    var rowIndex = $(this).data('row-index');
    var rowData = $('#tblShowData').DataTable().row(rowIndex).data();

    expenseId = rowData.id;
    $("#expenseTypeId").val(rowData.expenseTypeId).trigger("change");
    $("#employeeId").val(rowData.employeeId).trigger("change");
    $("#amount").val(rowData.amount);
    $("#operationDate").val(rowData.operationDateString).trigger("change");

    $("#btnCloseModalShowData").trigger("click");
});

$("#btnRemove").on("click", function () {
    if (expenseId != 0) {
        deleteConfirmation("Are You Sure, You Want To Delete This Expense Operation?!", deleteExpense);
    }
    else {
        warningAlert("Please Select Expense Operation First");
    }
})

function deleteExpense() {

    deleteMethod({ expenseId: expenseId }, '/DeleteExpense');
}

//////////////////////////////////////////// DataTable Defention ///////////////////////////////
let datatableColumns = [
    { data: "employeeName", name: "employeeName" },
    { data: "expenseName", name: "expenseName" },
    { data: "amount", name: "amount" },
    { data: "operationDateString", name: "operationDateString" },
]

initiateServerSideDataTable('tblShowData', "/GetPaginationExpensesHistory", datatableColumns);
