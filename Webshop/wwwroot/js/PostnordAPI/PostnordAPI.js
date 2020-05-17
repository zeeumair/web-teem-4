GetServiceCodes();

function GetServiceCodes() {

    var apikey = '87a7d90ab3e0cb85d3a18ab17afd31d6';
    //var country = document.getElementById("Country").value;
    var baseUrl = 'https://atapi2.postnord.com/test?';
    var request = new XMLHttpRequest();

    request.open('GET', baseUrl + 'apikey=' + apikey);
    request.onload = function () {
        console.log(request.responseText);
    };
    request.send();

    //$.ajax({
    //    url: 'https://atapi2.postnord.com/rest/shipment/v3/edi/servicecodes?apikey=' + apikey,
    //    dataType: 'json',
    //    contentType: 'application/json',
    //    success: function (data) {
    //        var html = '';
    //        var serviceArray = data.map(function (item) {
    //            return { value: item.serviceCodeDetails.ServiceName, name: item.serviceCodeDetails.ServiceName};
    //        });
    //        serviceArray.each(serviceArray.value,
    //            html = html + '<option value="' + value + '">' + data.issuerCountry + '</option>' + '');
    //        document.querySelector('#deliveryTime').insertAdjacentHTML('afterbegin', html);
    //    }, 
    //    error: function() {
    //        document.getElementById("Country").value = "No available mailing services in your country"
    //    }
    //});
}