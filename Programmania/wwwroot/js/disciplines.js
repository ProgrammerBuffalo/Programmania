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

        if ($(this).find('discipline_selected').length == 0) {
            let formData = new FormData();
            formData.append('disciplineId', id);

            $.ajax({
                type: 'POST',
                url: 'Disciplines/discipline-begin',
                processData: false,
                contentType: false,
                data: formData,
                success: function () {
                    $(this).addClass('discipline_selected');
                    window.location.href = `Disciplines/Lessons?disciplineId=${id}`;
                }
            });
        }
        else
            window.location.href = `Disciplines/Lessons?disciplineId=${id}`;
    });
});