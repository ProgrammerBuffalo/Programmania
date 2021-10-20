$(document).ready(function () {
    $('#disciplineImage').attr('src', sessionStorage.getItem('disciplineImage'));
    $('#disciplineName').text(sessionStorage.getItem('disciplineName'));

    sessionStorage.removeItem('disciplineImage');
    sessionStorage.removeItem('disciplineName');
});