//get details of selected row using AJAX
function details(id, controller) {
        event.preventDefault();
        var url = "/" + controller + "/details/";
        $.ajax({
            url: url,
            type: "GET",
            data: { "id": parseInt(id) },
            success: function (result) {
                $("#dialog").html(result);
            },
            complete: function (xmlHttpRequest, textStatus) {
                $("#dialog").dialog({
                    title: "Details",
                    autoOpen: false,
                    minWidth: 450,
                    resizable: false,
                    modal: true,
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    buttons: {
                    }
                });
                $("#dialog").dialog("open");
            }
        });
}

//form submit using AJAX
function ajaxCRUD(row, controller, method) {
    event.preventDefault();
    var id = parseInt(row.find("#responseId").text());
    var url = "/" + controller + "/" + method + "/";
    if (method != "create") 
        url = url + id
    var model;
    $.ajax({
        url: url,
        type: "GET",
        data: { "id": id },
        success: function (result) {
            $("#dialog").html(result);
        },
        complete: function (xmlHttpRequest, textStatus) {
            $("#dialog").dialog({
                title: "Edit",
                autoOpen: false,
                minWidth: 1000,
                resizable: false,
                modal: true,
                show: {
                    effect: "fade",
                    duration: 1000
                },
                buttons: {
                    "Submit": function () {
                        var parameters = setUpData(method);  
                        $.ajax({
                            url: url,
                            type: "POST",
                            data: parameters,
                            success: function (response) {
                                updateDivs(row, response);
                                $("#dialog").dialog('close');
                                $("#dialog").empty();
                            },
                            error: function () {
                                alert("LOL");
                            }
                        });
                    },
                    "Cancel": function () {
                        $("#dialog").dialog('close');
                        $("#dialog").empty();
                    }
                }
            });
            $("#dialog").dialog("open");
        }
    })
}
//sets up data models to be passed to action methods through AJAX
function setUpData(method) {
    var parameter = null;
    switch(method) {
        case "Create":
            break;
        case "Edit":
            parameter = {
                "__RequestVerificationToken": $('[name=__RequestVerificationToken]').val(),
                "response":
                    {
                        "ResponseId": $("#dialog").find("#responseId").val(),
                        "FirstName": $("#dialog").find("#firstName").val(),
                        "LastName": $("#dialog").find("#lastName").val(),
                        "Email": $("#dialog").find("#email").val(),
                        "PhoneNumber": $("#dialog").find("#phoneNumber").val(),
                        "Checked": $("#dialog").find("#checked").is(":checked"),
                        "UserId": $("#dialog").find("#userId").val()
                    },
                "name": $("#dialog").find("#projectName").val()
            };
            break;
        case "Delete":
            break;
    }
     return parameter;
}

function updateDivs(row, response) {
    var updatedResponse = $.parseJSON(response);
    row.find("#responseId").replaceWith("<td id='responseId'>" + updatedResponse[0].ResponseId + "</td>");
    row.find("#id").replaceWith("<td id='id'>" + updatedResponse[0].UserId + "</td>");
    row.find("#project").replaceWith("<td id='project'>" + updatedResponse[0].Project.Name + "</td>");
    row.find("#firstName").replaceWith("<td id='firstName'>" + updatedResponse[0].FirstName + "</td>");
    row.find("#lastName").replaceWith("<td id='lastName'>" + updatedResponse[0].LastName + "</td>");
    row.find("#email").replaceWith("<td id='email'>" + updatedResponse[0].Email + "</td>");
    row.find("#phoneNumber").replaceWith("<td id='phoneNumber'>" + updatedResponse[0].PhoneNumber + "</td>");
    if (updatedResponse[0].Checked == true) {
        row.find("#check").replaceWith("<td id='check'><input checked='checked' class='check-box' disabled='disabled' type='checkbox'></td>");
    } 
    else {
        row.find("#check").replaceWith("<td id='check'><input class='check-box' disabled='disabled' type='checkbox'></td>");
    }
}