$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $.ajax({
        type: "GET",
        url: "/api/StatisticsApi",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
               appendRowForTable(item);
            }); 
        },  
        error: function (request, message, error) {
            handleException(request, message, error);
        } //End of AJAX error function

    });
});

// Append one row for list table
function appendRowForTable(record) {
    var rowNumber = $("#tableRecordList").find("tr").length;

    var row = "<tr>" +
        "<td>" + rowNumber + "</td>" +
        "<td>" + record.id + "</td>" +
        "<td>" + record.firstName + "</td>" +
        "<td>" + record.lastName + "</td>" +
        "<td>" + (record.avgReviewScore).toFixed(2) + "</td>" +
        "<td>" + (record.bestReviewScore).toFixed(2) + "</td>" +
        "<td>" + record.reviewComment + "</td>" +
        "<td>" + (record.totalAvgReviewScore).toFixed(2)  + "</td>" +
        "</tr>";

    $("#tableRecordList").append(row);
}

// Handle Ajax Exception
function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}