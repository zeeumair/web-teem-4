
function GetServiceCodes() {
    var apikey = '87a7d90ab3e0cb85d3a18ab17afd31d6';
    var country = document.getElementById("Country").value;

    // get the 
    $.ajax({
        url: 'https://atapi2.postnord.com/rest/shipment/v3/edi/servicecodes?apikey=' + apikey,
        dataType: 'json',
        contentType: 'application/json',
        success: function (json) {
            var servicesInCountry = json.filter(x => x.data.issuerCountry == country);
            const html = json['data'].map(result => result.json().data{ return `<option value="${}">${}</option>` }).join('');
            document.querySelector('#output-Books').insertAdjacentHTML('afterbegin', html);
            document.getElementById("EmailAddress").value = "";

        }, 
        error: function() {
            document.getElementById("").value = "No available mailing services in your country"
        }
    });
}