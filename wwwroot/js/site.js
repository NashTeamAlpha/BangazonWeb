// Write your Javascript code.
$(document).ready(function() {
    $("#ListOfCustomers").on("change", function(e) {
        $.ajax({
            url: `/Customers/Activate/${$(this).val()}`,
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
            $("#Product_SubProductTypeId").html("");
            $("#Product_SubProductTypeId").append("<option value=null> Choose a Sub Category </option>");
            subTypes.forEach((option) => {
                console.log(option);
                $("#Product_SubProductTypeId").append(`<option value="${option.subProductTypeId}">${option.name}</option>`)
            });
        });
    });
// Adds an item to the users active shopping cart. Then redirects the user to the index file.    
    $("#AddToCart").on("click", function(e) {
        $.ajax({
            url: `/Products/AddToCart/${$(this).val()}`,
            method: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done((id) => {
            console.log(id);
            window.location.replace("http://localhost:5000"); 
        });
    });
});