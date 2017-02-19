//get details of selected row using AJAX
function details(id, controller) {
        event.preventDefault();
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
function ajaxCRUD(id, controller, method) {
    event.preventDefault();
        var idInt = + id;
        var url = "/" + controller + "/" + method + "/";
        if (method != "create") 
            url = url + idInt
        var model;
        $.ajax({
            url: url,
            type: "GET",
            data: { "id": idInt },
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
                            var parameters = setUpData(method, $("#dialog").html());  
                            $.ajax({
                                url: url,
                                type: "POST",
                                data: parameters,
                                success: function () {
                                    $("#dialog").dialog('close');
                                    $("#.dialog").empty();
                                },
                                error: function () {
                                    alert("LOL");
                                }
                            });
                        },
                        "Cancel": function () {
                            $("#dialog").dialog('close');
                            $("#.dialog").empty();
                        }
                    }
                });
               $("#dialog").dialog("open");
            }
        })
}
//sets up data models to be passed to action methods through AJAX
function setUpData(method, form) {
    var parameter = null;
    switch(method) {
        case "Create":
            break;
        case "Edit":
            parameter = {
                "__RequestVerificationToken": $('[name=__RequestVerificationToken]').val(),
                "response":
                    {
                        "ResponseId": form.,
                        "FirstName": "Hh",
                        "LastName": "D",
                        "Email": "s@gmail.com",
                        "PhoneNumber": "021 631 50393",
                        "Checked": true,
                        "UserId": 2,
                        "ProjectId": 123
                    }
            };
            break;
        case "Delete":
            break;
    }
     return parameter;
}

function updateDivs(row) {

}