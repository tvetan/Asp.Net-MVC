﻿$(function () {

    function init() {
        $("#create-tweet .tweet").attr({ disabled: "disabled" });
        var tweetTextAreaIniationTextLength = $("#tweetTextArea").val().length;
        $("#tweetTextArea").on("blur focus", function (event) {
            
            $this = $(this)
            var statusTextLength = $this.val().length;
            if (statusTextLength === tweetTextAreaIniationTextLength) {
                $(this).toggleClass("tweetTextArea-open");
            }
        });

        // Calculating the length of the tweet.
        $("#tweetTextArea").on("keyup keypress", function () {
            $this = $(this);
            var statusTextLength = 140 - $this.val().length;
            if (statusTextLength < 140 && statusTextLength >= tweetTextAreaIniationTextLength) {
                $("#create-tweet .tweet-button").removeAttr("disabled");
            }
            else {
                $("#create-tweet .tweet-button").attr({ disabled: "disabled" });
            }

            $(".tweet-counter").text(statusTextLength);

            if (statusTextLength < 0) {
                $(".tweet-counter").css({ color: "red" });
            } else {
                $(".tweet-counter").css({ color: "black" });
            }
        });
    }

    init();
});