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

        //var hasError = true;

        //var nickname = ;
        //var email = ;
        //var password = ;
        //var confirmPassword = ;
        //var avatar = ;

        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();
        $('#passwordConfirmationErrorReg').remove();

        //if (!checkNickname(nickname.val())) {
        //    hasError = true;
        //    $('#nicknameLabelReg').after("<label id='nicknameErrorReg' class='error'>nickname is to short</label>");
        //}

        //if (!checkEmail(email.val())) {
        //    hasError = true;
        //    $('#emailLabelReg').after("<label id='emailErrorReg' class='error'>email input error</label>");
        //}

        //if (!checkPassword(password.val())) {
        //    hasError = true;
        //    $('#passwordLabelReg').after("<label id='passwordErrorReg' class='error'>length 8-16. allowed symbols (!, #, $, %, ^, &)</label>");
        //}

        //if (password.val() != confirmPassword.val() || stringIsEmty(password.val())) {
        //    hasError = true;
        //    confirmPassword.val('');
        //    $('#confirmPasswordLabel').after("<label id='confirmPasswordError' class='error'>Password doesn't match</label>");
        //}

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

        //var hasError = false;
        //var email = ;
        //var password = ;

        $('#emailErrorLog').remove();
        $('#passwordErrorLog').remove();

        //if (!checkEmail(email.val())) {
        //    hasError = true;
        //    $('#emailLabelLog').after("<label id='emailErrorLog' class='error'>input error</label>");
        //}

        //if (!checkPassword(password.val())) {
        //    hasError = true;
        //    $('#passwordLabelLog').after("<label id='passwordErrorLog' class='error'>length 8-16. allowed symbols (!,#,$,%,^,&)</label>");
        //}

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

//function checkNickname(str) {
//    str = str.trim();
//    if (str.length < 2) return false;
//    return true;
//}

//function checkEmail(str) {
//    str = str.trim();
//    var regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
//    return regex.test(str);
//}

//function checkPassword(str) {
//    str = str.trim();
//    var regex = /^[a-z,A-Z,0-9,!,#,$,%,^,&]{8,16}$/;
//    return regex.test(str);
//}

//function stringIsEmty(str) {
//    return str == "" || str == null;
//}