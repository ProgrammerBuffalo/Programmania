$(document).ready(function () {
    initModal(document.getElementById('loginModal'),
        document.getElementById('enterLoginModal'),
        document.getElementById('closeLoginModal'));

    initModal(document.getElementById('registerModal'),
        document.getElementById('enterRegisterModal'),
        document.getElementById('closeRegisterModal'));
});

$(document).ready(function () {
    $('#enterRegisterModal').click(function () {

        $('.error').css('display', 'none');
        $('.modal__input').val('');
    });
});

$(document).ready(function () {
    $('#enterLoginModal').click(function () {

        $('.error').css('display', 'none');
        $('.modal__input').val('');
    });
});

$(document).ready(function () {
    $('#registerBtnOut').click(function () {

        $('.error').css('display', 'none');

        var data = new FormData();
        data.append('Nickname', $('#nicknameInputReg').val());
        data.append('Email', $('#emailInputReg').val());
        data.append('Password', $('#passwordInputReg').val());
        data.append('PasswordConfirmation', $('#passwordConfirmationInputReg').val());
        data.append('FormFile', $('#avatarInputReg').get(0).files[0]);

        $.ajax({
            type: 'POST',
            url: 'Account/registration',
            processData: false,
            contentType: false,
            data: data,
            success: function () {
                window.location.href = '/main';
            },
            error: function (jqXHR) {
                var errors = JSON.parse(jqXHR.responseText);
                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel + 'LabelReg').next()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
                if (errors['error'] != null) {
                    $('#registerBtnOut').after()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
            }
        });
    });
});

$(document).ready(function () {
    $('#loginBtnOut').click(function () {

        $('.error').css('display', 'none');

        var data = new FormData();
        data.append('Email', $('#emailInputLog').val());
        data.append('Password', $('#passwordInputLog').val());

        $.ajax({
            type: 'POST',
            url: 'Account/authorization',
            processData: false,
            contentType: false,
            data: data,
            success: function () {
                window.location.href = '/main';
            },
            error: function (jqXHR) {
                var errors = JSON.parse(jqXHR.responseText);
                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel + 'LabelLog').next()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
                if (errors['error'] != null) {
                    $('#loginBtnOut').next()
                        .css('display', 'inline')
                        .text(errors['error']);
                }
            }
        });
    });
});