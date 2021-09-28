var redirectTo;

document.ready(function () {
    var req = new XMLHttpRequest();
    redirectTo = req.getResponseHeader('RedirectTo');
});

$(document).ready(function () {
    $('#mainBtn').click(function () {
        window.location.href = '/home/index';
    });
});

$(document).ready(function () {
    $('#acceptBtn').click(function () {
        $('')

        var data = new FormData();
        data.append('Email', $('#email').val());
        data.append('Password', $('#password').val());

        $.ajax({
            type: 'GET',
            url: 'account/authorization',
            processData: false,
            contentType: false,
            headers: {},
            data: data,
            success: function () {
                window.location.href = redirectTo;
            },
            error: function (jqXML) {
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