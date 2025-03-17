// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let InputSearch = document.getElementById("SearchEmployee");

InputSearch.addEventListener("keyup", () => {

    var xhr = new XMLHttpRequest();
    let url = `https://localhost:7001/Employee?SearchEmployee=${InputSearch.value}`;

    xhr.open("GET", url, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log(JSON.parse(xhr.responseText));
        }
    };
    xhr.send();

});