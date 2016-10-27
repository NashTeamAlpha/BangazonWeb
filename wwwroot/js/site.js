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
        }).done(() => {
            location.reload();
        });
    });
});