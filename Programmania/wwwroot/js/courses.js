$(document).ready(function () {
    $('.course').click(function () {
        let img = $(this).find('.course__image')[0].childNodes[1];
        let rgb = imageColorAnylizer(img);
        sessionStorage.setItem('courseColor', rgbToHex(rgb));
        //sessionStorage.setItem('courseImage', img.src);
        let id = $(this).attr('data-id');
        
        window.location.href = `disciplines?id=${id}`;
    });

});в