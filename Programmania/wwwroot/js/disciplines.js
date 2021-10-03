$(document).ready(function () {
    let color = sessionStorage.getItem('courseColor');
    console.log(color);
    $('.header-text').css('background', color);
    $('.course__img > img').attr('src', sessionStorage.getItem('courseImage'));
    $('.header-text__content > h3').val(sessionStorage.getItem('courseName'));

    sessionStorage.removeItem('courseImage');
    sessionStorage.removeItem('courseColor');
    sessionStorage.removeItem('courseName');
});

$(document).ready(function () {
    $('.discipline').click(function () {

    });
});