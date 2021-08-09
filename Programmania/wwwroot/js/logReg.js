$(document).ready(function () {
    $('#registrBtnIn').click(function () {

        $('#nameInputReg').val('');
        $('#nicknameInputReg').val('');
        $('#ageInputReg').val('');
        $('#emailInputReg').val('');
        $('#passwordInputReg').val('');

        $('#nameErrorReg').remove();
        $('#ageErrorReg').remove();
        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();
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

        var name = $('#nameInputReg');
        var age = $('#ageInputReg');
        var nickname = $('#nicknameInputReg');
        var email = $('#emailInputReg');
        var password = $('#passwordInputReg');

        $('#nameErrorReg').remove();
        $('#ageErrorReg').remove();
        $('#nicknameErrorReg').remove();
        $('#emailErrorReg').remove();
        $('#passwordErrorReg').remove();

        if (!checkName(name.val())) {
            hasError = true;
            $('#nameLabelReg').after("<label id='nameErrorReg' class='error'>name is to short</label>");
        }

        if (!checkAge(age.val())) {
            hasError = true;
            $('#ageLabelReg').after("<label id='ageErrorReg' class='error'>input error</label>");
        }

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

        if (!hasError) {
            var data = {
                name: name.val(),
                age: age.val(),
                nickname: nickname.val(),
                email: email.val(),
                password: password.val()

            }
            data = JSON.stringify(data);

            $.ajax({
                type: 'POST',
                url: '/Registrate',
                processData: false,
                dataType: 'json',
                data: 'data=' + data,
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
            var data = { email: email.val(), password: password.val() };
            data = JSON.stringify(data);

            $.ajax({
                type: 'POST',
                url: '/Main',
                processData: false,
                dataType: 'json',
                data: 'data=' + data,
                success: function () {

                },
                error: function () {
                    $('loginBtnOut').after("<label id='loginError' class='error'> </label>");
                }
            });
        }
    });
});

function checkName(str) {
    str = str.trim();
    if (str.length < 2) return false;
    return true;
}

function checkNickname(str) {
    str = str.trim();
    if (str.length < 2) return false;
    return true;
}

function checkAge(str) {
    str = str.trim();
    var number = Number.parseInt(str);
    if (Number.isNaN(number)) return false;
    if (number <= 6 || number >= 100) return false;
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