// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    if ($("#currencyList li").length < 1) {
        $.get("https://localhost:44304/api/currencies/", function (data) {
            let result = [].concat(data);
            result.forEach(item => {
                let listItem = `<li id="${item.id}" class="list-group-item">${item.currencyCode}</li>`;
                $("#currencyList").append(listItem);
            })
        })
    }
});

$("#currencyList").on("click", "li", function () {
    $.get("https://localhost:44304/api/currencies/" + this.id)
        .then(() => location.reload());
});
