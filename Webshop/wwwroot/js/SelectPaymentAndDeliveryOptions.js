

function ToggleDelivery() {
    if (document.getElementById("twoToFiveDay").checked == true) {
        document.getElementById("fiveToTenDays").disabled = true;
        document.getElementById("thirtyDays").disabled = true;
        if (
            document.getElementById("swish").checked == true ||
            document.getElementById("invoice").checked == true
        ) {
            document.getElementById("submit").disabled = false;
        }
    }
    if (document.getElementById("twoToFiveDay").checked == false) {
        document.getElementById("fiveToTenDays").disabled = false;
        document.getElementById("thirtyDays").disabled = false;
        document.getElementById("submit").disabled = true;
    }

};
function ToggleDelivery2() {
    if (document.getElementById("fiveToTenDays").checked == true) {
        document.getElementById("twoToFiveDay").disabled = true;
        document.getElementById("thirtyDays").disabled = true;
        if (
            document.getElementById("swish").checked == true ||
            document.getElementById("invoice").checked == true
        ) {
            document.getElementById("submit").disabled = false;
        }
    }
    if (document.getElementById("fiveToTenDays").checked == false) {
        document.getElementById("twoToFiveDay").disabled = false;
        document.getElementById("thirtyDays").disabled = false;
        document.getElementById("submit").disabled = true;
    }
}
function ToggleDelivery3() {

    if (document.getElementById("thirtyDays").checked == true) {
        document.getElementById("twoToFiveDay").disabled = true;
        document.getElementById("fiveToTenDays").disabled = true;
        if (
            document.getElementById("swish").checked == true ||
            document.getElementById("invoice").checked == true
        ) {
            document.getElementById("submit").disabled = false;
        }
    }
    if (document.getElementById("thirtyDays").checked == false) {
        document.getElementById("twoToFiveDay").disabled = false;
        document.getElementById("fiveToTenDays").disabled = false;
        document.getElementById("submit").disabled = true;
    }
}
function TogglePayment() {
    if (document.getElementById("invoice").checked == true) {
        document.getElementById("swish").disabled = true;
    } else if (document.getElementById("swish").checked == true) {
        document.getElementById("invoice").disabled = true;
    }
    else if (document.getElementById("swish").checked == false) {
        document.getElementById("invoice").disabled = false;
    }
    if (document.getElementById("invoice").checked == false) {
        document.getElementById("swish").disabled = false;
    }
}
