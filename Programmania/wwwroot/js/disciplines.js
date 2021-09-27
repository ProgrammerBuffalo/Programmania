$(document).ready(function () {
    let color = sessionStorage.getItem('courseColor');
    console.log(color);
    $('.header-text').css('background', color);
    sessionStorage.removeItem('courseColor');
});

$(document).ready(function () {
    $('.discipline').click(function () {
        
    });
});