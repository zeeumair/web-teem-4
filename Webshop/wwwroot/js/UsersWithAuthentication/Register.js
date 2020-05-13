
function CheckUserEmail() {
    var access_key = 'db3925fc36c9fccb1a8dc9b251ee6462';
    var email_address = document.getElementById("EmailAddress").value; 

    // verify email address via AJAX call
    $.ajax({
        url: 'https://apilayer.net/api/check?access_key=' + access_key + '&email=' + email_address,
        dataType: 'jsonp',
        success: function (json) {


            if (json.format_valid == false || json.smtp_check == false) {
                document.getElementById("EmailAddress").value = ""; 
                alert("Email address does not exist, please enter a valid email address")
            }
        }
    });

}