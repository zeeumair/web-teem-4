function search() {
    var searchValue = document.getElementById("searchbar").value;
    var url = "Products?search=" + searchValue;
    window.location.href = window.location.origin + '/' + url;
}