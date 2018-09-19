// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets


$(document).ready(function () {

    //Hide the spinner when document finished loading.
    $(".lds-roller").css("display", "none");

    //Get all the books.
    let books = document.getElementsByClassName("book");
    //When the user stops writing, hide all not matching books by adding or removing d-none class.
    $("#Filter").keyup(function () {

        let text = $("#Filter").val().toLowerCase();

        for (var i = 0; i < books.length; i++) {
            var contains = false;
            if ((books[i].id.toLowerCase()).indexOf(text) !== -1) {
                contains = true;
            }

            if (!contains) {
                $(books[i]).addClass("d-none");
            }
            else {
                var check = $(books[i]).hasClass("d-none");
                if (check) {
                    $(books[i]).removeClass("d-none");
                }
            }
        }
    });

    /* -- Loading config -- */

    //If the user already visited the catalog, then don't display again the landing page.
    //This item lasts until the user closes the tab or browser.
    var data = sessionStorage.getItem('visited');
    if (data) {
        $("#Landing").css("opacity", "0");
        $("#Landing").css("display", "none");
        $("#Library").css("display", "flex");
        $("#Navbar").css("display", "flex");
        setTimeout(function () {
            $("#Library").css("opacity", "1");
            $("#Navbar").css("opacity", "1");
        }, 50);
    }
    else {
        $("#Landing").css("opacity", "1");
    }

    //When the user clicks the button, animate transition between #Landing div and book #Catalog.
    $("#Catalog").click(function () {
        //Store in session storage that the user visited the catalog.
        sessionStorage.setItem('visited', true);
        $("#Landing").css("opacity", "0");
        setTimeout(function () {
            $("#Landing").css("display", "none");
            $("#Library").css("display", "flex");
            $("#Navbar").css("display", "flex");
            setTimeout(function () {
                $("#Library").css("opacity", "1");
                $("#Navbar").css("opacity", "1");
            }, 50);
        }, 1000)
    });
});