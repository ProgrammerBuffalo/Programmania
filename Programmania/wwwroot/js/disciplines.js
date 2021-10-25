$(document).ready(function () {
    $('.header-text').css('background-color', sessionStorage.getItem('courseColor'));
    $('.course__img > img').attr('src', sessionStorage.getItem('courseImage'));
    $('.header-text__content > h3').first().text(sessionStorage.getItem('courseName'));

    sessionStorage.removeItem('courseImage');
    sessionStorage.removeItem('courseColor');
    sessionStorage.removeItem('courseName');
});

$(document).ready(function () {
    $('.discipline').click(function () {
        let id = $(this).attr('data-id');
        sessionStorage.setItem('disciplineImage', $(this).find('.discipline__image > img').first().attr('src'));
        sessionStorage.setItem('disciplineName', $(this).find('.discipline__title').first().text());

        window.location.href = `Disciplines/Lessons?disciplineId=${id}`;
    });
});