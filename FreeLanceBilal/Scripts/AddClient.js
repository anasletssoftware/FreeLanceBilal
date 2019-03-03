$(document).ready(function () {

    $('#AddClient').click(function () {
        debugger;
        var data = {
            ClientName : $("#ClientName").val(),
            Proprietor: $("#Proprietor").val(),
            CNICNumber: $("#CNICNumber").val(),
            Address: $("#Address").val(),
            RegistrationDate: $("#RegistrationDate").val(),
            Representative: $("#Representative").val(),
            CityId: $("#City").val(),
            CityName: $("#City option:selected").text(),
            StateId: $("#State").val(),
            StateName: $("#State option:selected").text(),
            ReturntypeId: $("#Return").val(),
            ReturnTypeName: $("#Return option:selected").text(),
            ClientTypeId: $("#ClientType").val(),
            ClientTypeName: $("#ClientType option:selected").text(),
            Services: $("#Services").is(":checked"),
            Importer: $("#Importer").is(":checked"),
            Exporter:  $("#Exporter").is(":checked"),
            WholeSeller: $("#WholeSeller").is(":checked"),
            Retailer: $("#Retailer").is(":checked"),
            CommercialImporter: $("#CommercialImporter").is(":checked"),
            SalesTaxNumber : $("#SalesTaxNumber").val(),
            NTNNumber: $("#NTNNumber").val(),
            MobileNumber1: $("#MobileNumber1").val(),
            MobileNumber2: $("#MobileNumber2").val(),
            OfficeNumber1: $("#OfficeNumber1").val(),
            OfficeNumber2: $("#OfficeNumber2").val(),
            EmailAddress: $("#EmailAddress").val(),
            PIN: $("#PIN").val(),
            UserId: $("#UserId").val(),
            Password: $("#Password").val(),
        }
        $.ajax({
            url: '/Admin/SaveClient',
            type: "POST",
            data: JSON.stringify(data),
            dataType: "JSON",
            contentType: "application/json",
            success: function (d) {
                //check is successfully save to database
                if (d.status == true) {
                    alert('Successfully Saved');
                }
                else {
                    alert('Failed');
                }
                $('#submit').val('Save');
            },
            error: function () {
                alert('Error. Please try again.');
                $('#submit').val('Save');
            }
        });


    });

});