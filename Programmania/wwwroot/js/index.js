$(document).ready(function () {
    $('#registrBtnIn').click(function () {

        $('#nicknameInputReg').val('');
        $('#emailInputReg').val('');
        $('#passwordInputReg').val('');
        $('#passwordConfirmationInput').val('');
        $('#avatarInputReg').val(null);

        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();
        $('#passwordConfirmationErrorReg').remove();
        $('#registerError').remove();
    });
});

$(document).ready(function () {
    $('#loginBtnIn').click(function () {

        $('#emailInputLog').val('');
        $('#passwordInputLog').val('');

        $('#emailErrorLog').remove();
        $('#passwordErrorLog').remove();
        $('#loginError').remove();
    });
});

$(document).ready(function () {
    $('#registerBtnOut').click(function () {

        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();
        $('#passwordConfirmationErrorReg').remove();

        var data = new FormData();
        data.append('Nickname', $('#nicknameInputReg').val());
        data.append('Email', $('#emailInputReg').val());
        data.append('Password', $('#passwordInputReg').val());
        data.append('PasswordConfirmation', $('#passwordConfirmationInputReg').val());
        data.append('FormFile', $('#avatarInputReg').get(0).files[0]);

        $.ajax({
            type: 'POST',
            url: 'account/registration',
            processData: false,
            contentType: false,
            data: data,
            success: function () {

            },
            error: function (jqXHR) {
                var errors = JSON.parse(jqXHR.responseText);
                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel + 'LabelReg').after(`<label id='${camel + 'ErrorReg'}' class='error'>${errors[key]}</label>`);
                }
                if (errors['error'] != null)
                    $('#registerBtnOut').after(`<label id='registerError' class='error'>${errors['error']}</label>`);
            }
        });
    });
});

$(document).ready(function () {
    $('#loginBtnOut').click(function () {

        $('#emailErrorLog').remove();
        $('#passwordErrorLog').remove();

        var data = new FormData();
        data.append('Email', $('#emailInputLog').val());
        data.append('Password', $('#passwordInputLog').val());

        $.ajax({
            type: 'POST',
            url: 'account/authorization',
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                window.location.href = response.url;
            },
            error: function (jqXHR) {
                var errors = JSON.parse(jqXHR.responseText);
                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel + 'LabelLog').after(`<label id='${camel + 'ErrorLog'}' class='error'>${errors[key]}</label>`);
                }
                if (errors['error'] != null)
                    $('#registerBtnOut').after(`<label id='loginError' class='error'>${errors['error']}</label>`);
            }
        });
    });
});