/// <reference path="jquery-1.8.2.min.js" />
/// <reference path="jquery-1.8.2.intellisense.js" />

(function ($) {
    $("#to-top-link").on("click", function (event) {
        event.preventDefault();
        $('html, body').animate({ scrollTop: 0 }, 1000)
    })

    $(window).on("scroll", function () {
        var $this = $(this);
        var currentScroolHeight = $this.scrollTop();

        //console.log(currentScroolHeight)
        if (currentScroolHeight > 1000) {
            $("#to-top").show(300);
        }
        else if (currentScroolHeight < 1000) {
            $("#to-top").hide(100);
        }
    })
})(jQuery)