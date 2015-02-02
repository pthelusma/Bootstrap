$(function () {

    ReCaptcha.init();

    $("#btnSubmit").click(function () {

        ReCaptcha.storeRecaptchaParameters();

        if (ReCaptcha.isValidResponse()) {
            if (ReCaptcha.isValidReCaptcha()) {
                alert("Success");

                $.ajax({
                    type: "POST",
                    url: "/Home/SendEmail",
                    async: false,
                    success: function (resp) {
                        alert("Send email!");
                    }
                });
            } else {
                alert("Failure");
            }
        } else {
            alert("Please provide a response");
        }
    });
});