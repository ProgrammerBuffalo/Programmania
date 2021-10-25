$(document).ready(function () {
    $('#disciplineImage').attr('src', sessionStorage.getItem('disciplineImage'));
    $('#disciplineName').text(sessionStorage.getItem('disciplineName'));

    sessionStorage.removeItem('disciplineImage');
    sessionStorage.removeItem('disciplineName');
});

$(document).ready(function () {
    $('.burger-content-elem').click(function () {
        //let ids = JSON.parse($(this).attr('data-id'));

        //$.ajax({
            
        //});
    });
});