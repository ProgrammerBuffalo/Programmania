$(document).ready(function () {
    $('.course').click(function () {
        let img = $(this).find('.course__image')[0].children[0];
        let rgb = imageColorAnylizer(img);

        sessionStorage.setItem('courseImage', img.src);
        sessionStorage.setItem('courseColor', rgbToHex(rgb));
        sessionStorage.setItem('courseName', $(this).find('.course__title')[0].innerText);

        let id = $(this).attr('data-id');
        window.location.href = `Courses/Disciplines?courseId=${id}`;
    });

});