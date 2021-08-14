﻿$(document).ready(function () {
    $('#registrBtnIn').click(function () {

        $('#nicknameInputReg').val('');
        $('#emailInputReg').val('');
        $('#passwordInputReg').val('');
        $('#confirmPasswordInput').val('');

        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();
        $('#confirmPasswordError').remove();
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

        var hasError = false;

        var nickname = $('#nicknameInputReg');
        var email = $('#emailInputReg');
        var password = $('#passwordInputReg');
        var confirmPassword = $('#confirmPasswordInput');

        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();
        $('#confirmPasswordError').remove();

        if (!checkNickname(nickname.val())) {
            hasError = true;
            $('#nicknameLabelReg').after("<label id='nicknameErrorReg' class='error'>nickname is to short</label>");
        }

        if (!checkEmail(email.val())) {
            hasError = true;
            $('#emailLabelReg').after("<label id='emailErrorReg' class='error'>input error</label>");
        }

        if (!checkPassword(password.val())) {
            hasError = true;
            $('#passwordLabelReg').after("<label id='passwordErrorReg' class='error'>length 8-16. allowed symbols (!,#,$,%,^,&)</label>");
        }

        if (password.val() != confirmPassword.val()) {
            hasError = true;
            confirmPassword.val('');
            $('#confirmPasswordLabel').after("<label id='confirmPasswordError' class='error'>Password not matches</label>");
        }

        if (!hasError) {

            var data = new FormData();
            data.append('Nickname', nickname.val());
            data.append('Email', email.val());
            data.append('Password', password.val());

            $.ajax({
                type: 'POST',
                url: '/Registrate',
                processData: false,
                contentType: false,
                data: data,
                success: function () {
                    alert('registration successful');
                },
                error: function () {
                    $('#registerBtnOut').after("<label id='registerError' class='error'> </label>");
                }
            });
        }
    });
});

$(document).ready(function () {
    $('#loginBtnOut').click(function () {

        var hasError = false;
        var email = $('#emailInputLog');
        var password = $('#passwordInputLog');

        $('#emailErrorLog').remove();
        $('#passwordErrorLog').remove();

        if (!checkEmail(email.val())) {
            hasError = true;
            $('#emailLabelLog').after("<label id='emailErrorLog' class='error'>input error</label>");
        }

        if (!checkPassword(password.val())) {
            hasError = true;
            $('#passwordLabelLog').after("<label id='passwordErrorLog' class='error'>length 8-16. allowed symbols (!,#,$,%,^,&)</label>");
        }

        if (!hasError) {
            var data = new FormData();
            data.append('Email', email.val());
            data.append('Password', password.val());

            $.ajax({
                type: 'POST',
                url: '/Main',
                processData: false,
                contentType: false,
                data: data,
                success: function () {

                },
                error: function () {
                    $('loginBtnOut').after("<label id='loginError' class='error'> </label>");
                }
            });
        }
    });
});

function checkNickname(str) {
    str = str.trim();
    if (str.length < 2) return false;
    return true;
}

function checkEmail(str) {
    str = str.trim();
    var regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regex.test(str);
}

function checkPassword(str) {
    str = str.trim();
    var regex = /^[a-z,A-Z,0-9,!,#,$,%,^,&]{8,16}$/;
    return regex.test(str);
}