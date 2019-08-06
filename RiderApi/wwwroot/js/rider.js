$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#tableRidersList  tr:not(:first)").remove("");
    getAllRiders();
});

function getAllRiders() {
    $.ajax({
        type: "GET",
        url: "/api/RidersApi",
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (data) {
            //alert(JSON.stringify(data));
            try {
                var output = JSON.parse(data);
                $.each(output, function (i, item) {

                    appendRowForTableOfRider(item);
                }); 
            } catch (e) {
                alert("Output is not valid JSON: " + data);
            }
        },  

        error: function (request, message, error) {
            alert(error);
            alert(request.status);
            handleException(request, message, error);
        } //End of AJAX error function

    });
}

// After the add new rider link is clicked
function addRiderClick() {
    $("#addOrEditSubmit").html("Add");
    clearInputFields();
}

//after the Add submit button is clicked when adding a new rider
function addRiderSubmit() {

    // Build Rider object from inputs
    var rider = new Object();
    rider.FirstName = $("#firstName").val();
    rider.LastName = $("#lastName").val();
    rider.PhoneNumber = $("#phoneNumber").val();
    rider.Email = $("#email").val();
    rider.StartDate = $("#startDate").val();

    var ret = validateRiderObject(rider);
    if (!ret) return;

    $.ajax({
        url: "/api/RidersApi",
        type: 'POST',
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(rider),
        success: function (responseData) {
            riderAddSuccess(responseData);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

//After Edit link is clicked
function reviseRiderClick(ctl) {
    $("#addOrEditSubmit").html("Update");
    clearInputFields();

    var rowNo = $(ctl).data("number");

    var rowRecord = $("#tableRidersList tr").eq(rowNo);

    var id = rowRecord.find("td").eq(1).text();
    var firstName = rowRecord.find("td").eq(2).text();
    var lastName = rowRecord.find("td").eq(3).text();
    var phoneNumber = rowRecord.find("td").eq(4).text();
    var email = rowRecord.find("td").eq(5).text();
    var startDate = rowRecord.find("td").eq(6).text();

    $("#riderId").val(id);
    $("#myModalLabel").html("Revising Rider (ID:" + id + ")");
    $("#firstName").val(firstName);
    $("#lastName").val(lastName);
    $("#phoneNumber").val(phoneNumber);
    $("#email").val(email);
    $("#rowNo").val(rowNo);

    var from = startDate.toString().split("/");
    var s = from[2] + "-" + from[1] + "-" + from[0];
    $("#startDate").val(s);

}

//After Update Submit button is clicked
function reviseRiderSubmit() {
    var rowNo = $("#rowNo").val();
    // Build Rider object from inputs
    var rider = new Object();
    rider.Id = $("#riderId").val();
    rider.FirstName = $("#firstName").val();
    rider.LastName = $("#lastName").val();
    rider.PhoneNumber = $("#phoneNumber").val();
    rider.Email = $("#email").val();
    rider.StartDate = $("#startDate").val();

    var ret = validateRiderObject(rider);
    if (!ret) return;

    $.ajax({
        url: "/api/RidersApi/" + rider.Id,
        type: "PUT",
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(rider),
        success: function () {
            riderUpdateSuccess(rider, rowNo);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function addOrUpdateRiderSubmit() {
    $("#errorMessage").text("");
    if ($("#addOrEditSubmit").html() == "Add")
        addRiderSubmit();
    else
        reviseRiderSubmit();
}

//Update the riders list after updating one rider successfully
function riderUpdateSuccess(rider, rowNo) {
    var rowRecord = $("#tableRidersList tr").eq(rowNo);
    var firstNameTd = rowRecord.find("td").eq(2);
    var lastNameTd = rowRecord.find("td").eq(3);
    var phoneNumberTd = rowRecord.find("td").eq(4);
    var emailTd = rowRecord.find("td").eq(5);
    var startDataTd = rowRecord.find("td").eq(6);

    firstNameTd.text(rider.FirstName);
    lastNameTd.text(rider.LastName);
    phoneNumberTd.text(rider.PhoneNumber);
    emailTd.text(rider.Email);
    startDataTd.text(rider.StartDate);

    $("#modelRiders").modal("hide");
}

function deleteRider(ctl) {

    if (confirm("Are you sure to delete?") == false)
        return false;

    var rowNo = $(ctl).data("number");
    var riderId = $(ctl).data("id");
    $.ajax({
        url: "/api/RidersApi/" + riderId,
        type: "DELETE",
        contentType:
            "application/json;charset=utf-8",
        success: function () {
            riderDeleteSuccess(rowNo);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

    return true;
}

// Update the rider table list after deleting one rider
function riderDeleteSuccess(rowNo) {
    $("#tableRidersList  tr:not(:first)").remove("");
    getAllRiders();
}

function validateRiderObject(rider) {
    var ret1 = validatePhoneNo(rider.PhoneNumber);
    var ret2 = validateEmail(rider.Email);
    var ret3 = validateFirstName(rider.FirstName);
    var ret4 = validateLastName(rider.LastName);
    var ret5 = validateStartDate(rider.StartDate);
    return ret1 && ret2 && ret3 && ret4 && ret5;
}

// Validate the phone number
function validatePhoneNo(phone) {
    var pattern = /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/;
    var result = pattern.test(phone);
    if (!result) {
        $("#errorMessage").html("<h6>PhoneNumber error.</h6>");
        $("#errorMessage").css('color', 'red');
        return false;
    }
    if (phone.length > 100) {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6>phone number should be less than 100.<h6>");
        return false;
    }
    return true;
}

// Validate the phone number
function validateEmail(email) {
    var pattern = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    var result = pattern.test(email);
    if (!result) {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6>Email error.<h6>");
        return false;
    }
    if (email.length > 200) {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6>Email length should be less than 200.<h6>");
        return false;
    }

    return true;
}

function validateFirstName(firstName) {
    if (firstName == "") {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6> FirstName could not be null.<h6>");
        return false;
    }
    if (firstName.length > 20) {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6> FirstName length should be less than 20.<h6>");
        return false;
    }
    return true;
}

function validateLastName(lastName) {
    if (lastName == "") {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6> LastName could not be null.<h6>");
        return false;
    }
    if (lastName.length > 20) {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6> LastName length should be less than 20.<h6>");
        return false;
    }
    return true;
}

function validateStartDate(startDate) {
    if (startDate == "") {
        $("#errorMessage").css('color', 'red');
        $("#errorMessage").append("<h6> StartDate could not be null.<h6>");
        return false;
    }

    return true;
}

//after ajax return success when adding a new rider
function riderAddSuccess(rider) {

    appendRowForTableOfRider(rider);

    clearInputFields();

    $("#modelRiders").modal("hide");
}

//Clear input fields
function clearInputFields() {
    $("#firstName").val("");
    $("#lastName").val("");
    $("#phoneNumber").val("");
    $("#email").val("");
    $("#startDate").val("");
    $("#errorMessage").text("");
}

// Append one row for riders list table
function appendRowForTableOfRider(rider) {
    var rowNumber = $("#tableRidersList").find("tr").length;
    var dateObj = new Date(rider.startDate);
    var dateNew = pad(dateObj.getDate()) + "/" + pad(dateObj.getMonth() + 1) + "/" + dateObj.getFullYear();

    var row = "<tr>" +
        "<td class='rowno'>" + rowNumber + "</td>" +
        "<td>" + rider.id + "</td>" +
        "<td>" + rider.firstName + "</td>" +
        "<td>" + rider.lastName + "</td>" +
        "<td>" + rider.phoneNumber + "</td>" +
        "<td>" + rider.email + "</td>" +
        "<td>" + dateNew + "</td>" +
        "<td>" +
        "<button type='button' class='btn btn-link' " +
        "onclick='reviseRiderClick(this);' " +
        "data-toggle='modal' " +
        "data-target='#modelRiders' " +
        "data-number='" + rowNumber + "'>" +
        "Edit</button>" +
        "<button type='button' " +
        "onclick='getJobs(this);' " +
        "class='btn btn-link' " +
        "data-toggle='modal' " +
        "data-target='#modelJobs' " +
        "data-id='" + rider.id + "'>" +
        "Jobs</button>" +
        "<button type='button' class='btn btn-link' " +
        "onclick='deleteRider(this);' " +
        "data-number='" + rowNumber + "'" +
        "data-id='" + rider.id + "'>" +
        "Delete</button>" +
        "</td >" +
        "</tr>";

    $("#tableRidersList").append(row);
}

//Get Jobs for one rider
function getJobs(ctl) {

    // Get rider id from data- attribute
    var id = $(ctl).data("id");

    var titleString = "Jobs of Rider:" + id;
    $("#jobsModalLabel").html(titleString);

    // Need to clean the TableJobs's job list first 
    $("#tableJobs  tr:not(:first)").remove("");

    // Call Web API to get a list of Phone Numbers of this Customer
    $.ajax({
        url: "api/StatisticsApi/GetJobsByRiderIdAsync/" + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (i, item) {
                appendRowForTableOfJobs(item);
            }); //End of foreach Loop
        },
        error: function (request, message, error) {
            if (request.status != 404)
                handleException(request, message, error);
        }
    });
}

function appendRowForTableOfJobs(item) {
    var rowNumber = $("#tableJobs").find("tr").length;
    var row = "<tr>" +
        "<td>" + rowNumber + "</td>" +
        "<td>" + item.id + "</td>" +
        "<td>" + item.jobDateTime + "</td>" +
        "<td>" + item.riderId + "</td>" +
        "<td>" + item.reviewScore + "</td>" +
        "<td>" + item.reviewComment + "</td>" +
        "<td>" + item.completedAt + "</td>" +
        "</tr>";

    $('#tableJobs').append(row);
}

function pad(n) {
     return n < 10 ? "0" + n : n;
}


// Handle Ajax Exception
function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Status: " + request.statusText + "\n";
    msg += "Message: " + message + "\n";
    msg += "Error: " + error + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}