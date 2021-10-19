var redirectTo;

$(document).ready(function () {
    var req = new XMLHttpRequest();
    redirectTo = req.getResponseHeader('RedirectTo');
});

$(document).ready(function () {
    $('#mainBtn').click(function () {
        window.location.href = '/index';
    });
});

$(document).ready(function () {
    $('#signInBtn').click(function () {

        var data = new FormData();
        data.append('Email', $('#email').val());
        data.append('Password', $('#password').val());
        document.cookie = 'JwtToken=abcdefg;';
        
        $.ajax({
            type: 'POST',
            url: '/account/authorization',
            processData: false,
            contentType: false,
            headers: { 'Authorization': 'Bearer ' + getCookie('JwtToken')},
            data: data,
            success: function () {
                window.location.href = redirectTo;
            },
            error: function (jqXHR) {
                var errors = JSON.parse(jqXHR.responseText);
                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel).before(`<label id='${camel + 'Error'}' class='error__input'>${errors[key]}</label>`);
                }
                if (errors['error'] != null)
                    $('#password').after(`<label id='loginError' class='error__input'>${errors['error']}</label>`);
            }
        });
    });
});