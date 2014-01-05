$(function () {
    $("#create-tweet .tweet").attr({ disabled: "disabled" });

    $("#tweetTextArea").on("keyup keypress", function () {
        $this = $(this);
        var statusTextLength = 140 - $this.val().length;
        if (statusTextLength < 140 && statusTextLength >= 0) {
            $("#create-tweet .tweet").removeAttr("disabled");
        }
        else {
            $("#create-tweet .tweet").attr({ disabled: "disabled" });
        }

        $(".tweet-counter").text(statusTextLength);

        if (statusTextLength < 0) {
            $(".tweet-counter").css({ color: "red" })
        } else {
            $(".tweet-counter").css({ color: "black" })
        }
    });
});