$(document).ready(function () {

    ReCaptcha.init();

    $('#Materials').multiselect({
        nonSelectedText: 'Materials'
    });

    $('#myModal').on('hidden.bs.modal', function (e) {

        $("#FirstName").val("");
        $("#FirstName").closest('.form-group').removeClass('has-success');
        $("#FirstName1").removeClass('glyphicon-ok');

        $("#LastName").val("");
        $("#LastName").closest('.form-group').removeClass('has-success');
        $("#LastName1").removeClass('glyphicon-ok');

        $("#Email").val("");
        $("#Email").closest('.form-group').removeClass('has-success');
        $("#Email1").removeClass('glyphicon-ok');

        $("#PhoneNumber").val("");
        $("#PhoneNumber").closest('.form-group').removeClass('has-success');
        $("#PhoneNumber1").removeClass('glyphicon-ok');

        $("#Message").val("");
        $("#Message").closest('.form-group').removeClass('has-success');
        $("#Message1").removeClass('glyphicon-ok');

        $("#ContactMethod").val("");
        $("#ContactMethod").closest('.form-group').removeClass('has-success');

        $("#DiscoveryMethod").val("");
        $("#DiscoveryMethod").closest('.form-group').removeClass('has-success');

        $('#Materials option:selected').each(function () {
            $(this).prop('selected', false);
        });

        $('#Materials').multiselect('refresh');

        ReCaptcha.recreateRecaptcha();
        window.scrollTo(0, 0);
    })

    jQuery.validator.addMethod("validateRecaptcha", (function () {
        var validation = ReCaptcha.isValidReCaptcha();
        return validation;
    }), "");

    $('form').validate({
        rules: {
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            PhoneNumber: {
                required: true,
                phoneUS: true
            },
            Email: {
                required: true,
                email: true
            },
            Message: {
                required: true,
                maxlength: 1000
            },
            ContactMethod: {
                required: true
            },
            DiscoveryMethod: {
                required: true
            },
            recaptcha_response_field: {
                validateRecaptcha: true,
                required: true
            }
        },
        onkeyup: false,
        onfocusout: false,
        onclick: false,
        messages: {
            recaptcha_response_field: {
                validateRecaptcha: "Captcha response was incorrect",
                required: "Captcha response is required"
            }
        },
        highlight: function (element) {
            var id_attr = "#" + $(element).attr("id") + "1";
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            $(id_attr).removeClass('glyphicon-ok').addClass('glyphicon-remove');
        },
        unhighlight: function (element) {
            var id_attr = "#" + $(element).attr("id") + "1";
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            $(id_attr).removeClass('glyphicon-remove').addClass('glyphicon-ok');
        },
        errorElement: 'em',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if(element.attr('id') == 'recaptcha_response_field')
            {
                $("#ReCaptcha").append(error);
            } else {
                error.insertAfter(element);
            }
        }
    });

    $("#btnSubmit").click(function () {
        ReCaptcha.storeRecaptchaParameters();
        var validator = $("#myform").validate();
        if (validator.form()) {

            var materials = "";

            $.each($('select#Materials').val(), function (index, value) {
                materials = materials + " " + value;
            });

            var communication = {
                FirstName: $("#FirstName").val(),
                LastName: $("#LastName").val(),
                Email: $("#Email").val(),
                PhoneNumber: $("#PhoneNumber").val(),
                ContactMethod: $("#ContactMethod option:selected").text(),
                Materials: materials,
                Message:$("#Message").val(),
                DiscoveryMethod: $("#DiscoveryMethod option:selected").text()
            }

            $.ajax({
                type: "POST",
                url: "/Home/SendMail",
                async: false,
                data: communication,
                success: function (resp) {
                    $('#myModal').modal('show');
                }
            });
        } else {
            validator.focusInvalid();
            ReCaptcha.recreateRecaptcha();
        }
    });
});