//get details of selected row using AJAX
function details(row, controller) {
        event.preventDefault();
        var id = parseInt(row.find("#responseId").text());
        var url = "/" + controller + "/details/";
        $.ajax({
            url: url,
            type: "GET",
            data: { "id": id },
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
    if (method == "Create") {
        switch(controller) {
            case "Response":
                var id = parseInt(row.find("#projectId").text());
        }
    }
    else {
        var id = parseInt(row.find("#responseId").text());
    }
    var url = "/" + controller + "/" + method + "/";
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
                title: method,
                autoOpen: false,
                minWidth: 1000,
                resizable: false,
                modal: true,
                show: {
                    effect: "fade",
                    duration: 1000
                },
                buttons: {
                    "Submit": {
                        id: "submit",
                        text: "Submit",
                        click: function () {
                            if (validateForm(method) == true || (method != "Create" && method != "Edit")) 
                            {
                                var parameters = setUpData(method);
                                $.ajax({
                                    url: url,
                                    type: "POST",
                                    data: parameters,
                                    success: function (response) {
                                        if (method != "Create") {
                                            updateDivs(row, response);
                                        }
                                        else {
                                            $(function () {
                                                if (controller == "Response")
                                                {
                                                    $("#response").dialog();
                                                    $("#response").dialog("open");
                                                }
                                                else
                                                {
                                                    $("#submit").dialog();
                                                    $("#submit").dialog("open");
                                                }
                                                
                                            });
                                        }
                                        $("#dialog").dialog('destroy');
                                        $("#dialog").empty();
                                    },
                                    error: function () {
                                        alert("LOL");
                                    }
                                });
                            }
                        }
                    },
                    "Cancel": function () {
                        $("#dialog").dialog('destroy');
                        $("#dialog").empty();
                    }
                }
            });
            if (method == "Delete") {
                $("#submit").button("option", "label", "Delete");
            }
            $("#dialog").dialog("open");
        }
    })
}
//validates the form before sending it off to the server
function validateForm(method) {
    var valid = true;
    switch (method) {
        case "Create":
            $(".form-input").each(function (index, item) {
                var hold = checkEmptyInput($(item));
                if (hold == false) {
                    valid = false;
                }
            });  
            break;
        case "Edit":
            break;
    }
    return valid;
}

function checkEmptyInput(element) {
    if (element.val() == "" || element.val() == null || element.val() == "Select Project") {
        element.hide();
        element.css({ "border-style": "solid", "border-color": "red" }).html();
        element.fadeIn('fast');
        return false;
    }
    return true;
}

function checkPhoneNumber() {

}

function checkEmail() {

}

//sets up data models to be passed to action methods through AJAX
function setUpData(method) {
    var parameter;
    switch(method) {
        case "Create":
            parameter = 
                {
                   "__RequestVerificationToken": $('[name=__RequestVerificationToken]').val(),
                   "response":
                   {
                       "FirstName": $("#dialog").find("#firstName").val(),
                       "LastName": $("#dialog").find("#lastName").val(),
                       "Email": $("#dialog").find("#email").val(),
                       "PhoneNumber": $("#dialog").find("#phoneNumber").val()
                   },
                   "name": $("#dialog").find("#projectName option:selected").text()
                };
            break;
        case "Edit":
            parameter =
                {
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
                    "name": $("#dialog").find("#projectName option:selected").text()
                };
            break;
        case "Delete":
            parameter =
            {
                "__RequestVerificationToken": $('[name=__RequestVerificationToken]').val(),
                "id": parseInt($("#dialog").find("#responseId").text()),
                
            }
            break;
    }
     return parameter;
}

function updateDivs(row, response) {
    if (response > 0 && typeof response == "string") {
        row.empty()
    }
    else {
        var updatedResponse = $.parseJSON(response);
        if (updatedResponse[0].userId == "" || null)
            updatedResponse[0].userId = "New";
        row.find("#responseId").replaceWith("<td id='responseId'>" + updatedResponse[0].ResponseId + "</td>");
        row.find("#id").replaceWith("<td id='id'>" + "New" + "</td>");
        row.find("#project").replaceWith("<td id='project'>" + updatedResponse[0].Project.Name + "</td>");
        row.find("#firstName").replaceWith("<td id='firstName'>" + updatedResponse[0].FirstName + "</td>");
        row.find("#lastName").replaceWith("<td id='lastName'>" + updatedResponse[0].LastName + "</td>");
        row.find("#email").replaceWith("<td id='email'><a href='mailto:" + updatedResponse[0].Email + "'>" + updatedResponse[0].Email + "</a></td>");
        row.find("#phoneNumber").replaceWith("<td id='phoneNumber'>" + updatedResponse[0].PhoneNumber + "</td>");
        if (updatedResponse[0].Checked == true) {
            row.find("#check").replaceWith("<td id='check'><input checked='checked' class='check-box' disabled='disabled' type='checkbox'></td>");
        }
        else {
            row.find("#check").replaceWith("<td id='check'><input class='check-box' disabled='disabled' type='checkbox'></td>");
        }
    }
    
}