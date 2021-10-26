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

        sessionStorage.setItem('disciplineId', id);
        sessionStorage.setItem('disciplineImage', $(this).find('.discipline__image > img').first().attr('src'));
        sessionStorage.setItem('disciplineName', $(this).find('.discipline__title').first().text());

        //$.ajax({
        //    type: 'POST',
        //    url: 'Disciplines/discipline-begin',
        //    processData: true,
        //    dataType: 'json',
        //    data: { 'disciplineId': id },
        //    success: function (xhr) {

        //    },
        //    error: function (error) {

        //    }
        //});

        if ($(this).find('discipline_selected').length != 0) {
            //let xhr = new XMLHttpRequest();
            //xhr.open('POST', 'Disciplines/discipline-begin', true);
            //xhr.setRequestHeader("Content-Type", "application/json");
            //xhr.send(JSON.stringify({ 'disciplineId': id }));

            //xhr.onreadystatechange = function () {
            //    if (xhr.readyState === 4) {
            //        console.log(xhr.status);
            //        console.log(xhr.responseText);
            //    }
            //};
            //window.location.href = `Disciplines/discipline-begin?disciplineId=${id}`;
        }
        else
            window.location.href = `Disciplines/Lessons?disciplineId=${id}`;
    });
});