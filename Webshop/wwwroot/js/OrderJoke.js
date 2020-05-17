window.onload = function () {
    this.GetRandomJoke();
}
function GetRandomJoke() {
    var url = 'https://api.chucknorris.io/jokes/random';
    var request = new XMLHttpRequest();
    request.open('GET', url);
    request.onload = function () {
        var data = JSON.parse(request.responseText);
        renderHTML(data);
    };
    request.send();
}

function renderHTML(data) {
    var jokeContainer = document.getElementById("jokeContainer");
    var htmlString = "<p>" + data.value + "</p>";
    jokeContainer.insertAdjacentHTML('beforeend', htmlString);

}