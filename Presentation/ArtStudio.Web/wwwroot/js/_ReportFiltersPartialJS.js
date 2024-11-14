
$('#tblReportData').DataTable({
    ordering: false,
    searching: false,
});

// Your specific page or partial view initialization code
let today = getTodayDate();

// Initialize datepickers
(function initiateDatePickers() {
    datepickerBuilder.SetMaxDays(0).Build('startDate');
    addLessThanOrEqualToDateValidation('startDate', today, 'Start Date', 'Today');

    datepickerBuilder.SetMaxDays(0).Build('endDate');
    addLessThanOrEqualToDateValidation('endDate', today, 'End Date', 'Today');

    addUpdateDatePickerMinDate('startDate', 'endDate');
})();


$('#DivButtons').hide();


