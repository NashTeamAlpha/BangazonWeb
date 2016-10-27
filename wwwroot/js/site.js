// Write your Javascript code.
$(document).ready(function() {
    $("#ListOfCustomers").on("change", function(e) {
        $.ajax({
            url: `/Customer/Activate/${$(this).val()}`,
            method: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done(() => {
            location.reload();
        });
    });

    $("#ProductTypesList").on("change", function(e) {
        $.ajax({
            url: `/Products/GetSubTypes/${$(this).val()}`,
            method: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done((subTypes) => {
            console.log("info", subTypes);
            $("#SubProductTypesList").html("");
            $("#SubProductTypesList").append("<option> Select a Sub Category </option>");
            subTypes.forEach((option) => {
                console.log(option);
                $("#SubProductTypesList").append(`<option value="${option.subProductTypeId}">${option.name}</option>`)
            });
            // location.reload();
        });
    });
});