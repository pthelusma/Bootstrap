var ReCaptcha = (function () {

    var challenge;
    var response;

    var init = function () {
        createReCaptcha();
    }

    var createReCaptcha = function () {
        Recaptcha.create("6LcXkfUSAAAAACBhoS9s68a6Ivu_kCfoo8RU1-hg", "ReCaptcha", {
            theme: "red",
            callback: Recaptcha.focus_response_field
        });
    }

    var destroyReCaptcha = function () {
        Recaptcha.destroy();
    }

    var recreateRecaptcha = function () {
        destroyReCaptcha();
        createReCaptcha();
    }

    function isValidResponse() {
        return (reponse && response != "");
    }

    function storeRecaptchaParameters() {
        challenge = Recaptcha.get_challenge();
        reponse = Recaptcha.get_response();
    }

    function isValidReCaptcha() {

        var remoteip;

        var success = false;

        $.getJSON("http://jsonip.com",
            function (res) {
                remoteip = res.ip;
            });

        $.ajax({
            type: "POST",
            url: "/Home/Submit",
            async: false,
            data: {
                remoteip: remoteip,
                challenge: Recaptcha.get_challenge(),
                response: Recaptcha.get_response()
            },
            success: function (resp) {
                if (resp.result === "true") {
                    success = true;
                } else {
                    recreateRecaptcha();
                }
            }
        });

        return success;
    }

    return {
        init: init,
        isValidReCaptcha: isValidReCaptcha,
        isValidResponse: isValidResponse,
        storeRecaptchaParameters: storeRecaptchaParameters
    };
})();