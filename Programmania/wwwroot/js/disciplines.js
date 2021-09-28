$(document).ready(function () {
    let color = sessionStorage.getItem('courseColor');
    console.log(color);
    $('.header-text').css('background', color);
    $('.course__img > img').attr('src', sessionStorage.getItem('courseImage'));

    sessionStorage.removeItem('courseImage');
    sessionStorage.removeItem('courseColor');
});

$(document).ready(function () {
    $('.discipline').click(function () {

    });
});