// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function addDiv(firstHalf, SecondHalf, screenWidth) {
    var content = document.getElementById('content');
    var div;
    //alert(param1);
    if ($(window).width() < screenWidth && !document.getElementById("scrollable")) {

        div = document.createElement("div");
        var formHalf = document.getElementById(firstHalf);
        var notebookHalf = document.getElementById(SecondHalf)
        //var formHalf = document.getElementById('lotteryFormHalf');
        //var notebookHalf = document.getElementById('santaListNotebook');
        div.setAttribute("id", "scrollable");
        content.appendChild(div);
        div.appendChild(formHalf);
        div.appendChild(notebookHalf);

        //alert($(window).width());
    } else if (document.getElementById("scrollable") && $(window).width() >= screenWidth) {
        var content = $("#scrollable").contents();
        $("#scrollable").replaceWith(content);
    }


}
//const swup = new Swup();
