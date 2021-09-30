$(document).ready(function () {
    $('#mainBtn').click(function () {
        $.ajax({
            type: 'POST',
            url: '/account/authorization',
            processData: false,
            contentType: false,
            headers: { 'Authorization': 'Bearer ' + getCookie('JwtToken') },
            success: function () {
                window.location.href = '/main';
            }            
        });
    });
});