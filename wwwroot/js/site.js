//This method waits for the "ListOfCustomers" select drop down list to change, then actives the Activate method in the Customers Controller and sets the active customer to the customer selected. 
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

//This method waits for the Category select drop down in the add new product form to be changed- when it is- this method gets all SubProductTypes inside the selected category and populates the subcategory drop down list thats just below the select category drop down list in the add product form. 
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
            // location.reload();
        });
    });
});