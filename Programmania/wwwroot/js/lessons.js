var lessonIndex;

$(document).ready(function () {
    //$('#disciplineImage').attr('src', sessionStorage.getItem('disciplineImage'));
    //$('#disciplineName').text(sessionStorage.getItem('disciplineName'));

    let lessonEl = $('.burger-content').find('.burger-content-elem_unread').first();
    lessonIndex = lessonEl.index();
    if (lessonIndex == -1)
        lessonIndex = 0;

    $.ajax({
        type: 'GET',
        url: 'Lessons/get-discipline-description',
        processData: true,
        dataType: 'json',
        data: { 'disciplineId': sessionStorage.getItem('disciplineId') },
        success: function (data) {
            $('#disciplineName').text(data.disciplineName);
            $('#disciplineImage').attr('src', 'data:image*;base64,' + data.disciplineImage);
        }
    });

    //let lessonId = lessonEl.attr('data-id');
    //requestLesson(lessonEl[0], sessionStorage.getItem('disciplineId'), lessonId, false);
});

$(document).ready(function () {
    $('.burger-content-elem').click(function () {
        if ($(this).find('burger-content-elem_read'))
            requestLesson($(this)[0], sessionStorage.getItem('disciplineId'), $(this).attr('data-id'), false);
        else
            alert('you cant read this lesson until you finish previous');
    });
});

$(document).ready(function () {
    $('#next').click(function () {
        let curLesson = $('.burger-content')[0].children[lessonIndex];
        if ($(curLesson).next().find('.burger-content-elem_unread'))
            requestLesson($(curLesson).next()[0], sessionStorage.getItem('disciplineId'), $(curLesson).next().attr('data-id'), true);
        else
            requestLesson($(curLesson).next()[0], sessionStorage.getItem('disciplineId'), $(curLesson).next().attr('data-id'), false);
    });
});

$(document).ready(function () {
    $('#checkTest').click(function () {
        let answerIndex = findChekedAnswer();
        if (answerIndex == null) {
            let formData = new FormData();
            formData.append('lessonId', $('.burger-content')[0].children[lessonIndex].getAttribute('data-id'));
            formData.append('disciplineId', sessionStorage.getItem('disciplineId'));
            formData.append('testIndex', answerIndex);

            $.ajax({
                type: 'POST',
                url: 'Lessons/check-test',
                data: formData,
                contentType: false,
                processData: false,
                success: function () {
                    alert('correct answer');
                },
                error: function () {
                    alert('wrong answer')
                }
            });
        }
    });
});

function findChekedAnswer() {
    let answerInputs = document.querySelectorAll('.rad-input');
    for (var i = 0; i < answerInputs.length; i++) {
        if (answerInputs[i].checked == true)
            return i;
    }
}

function requestLesson(lessonEl, disciplineId, lessonId, isNext) {
    $.ajax({
        type: 'GET',
        url: 'Lesson',
        dataType: 'json',
        processData: true,
        data: { 'disciplineId': disciplineId, 'lessonId': lessonId },
        success: function (data) {
            changeLesson(data, lessonEl, isNext);
        },
        error: function () {

        }
    });
}

function changeLesson(data, lessonEl, isNext) {
    $('#lessonName').text(lessonEl.children[0].textContent);
    $('#lessonContent').html(data.html);
    $('#test').html(createTest(data.test));

    lessonIndex = $(lessonEl).index();

    if (isNext) {
        $(lessonEl).prev().removeClass('burger-content-elem_unread')
        $(lessonEl).prev().addClass('burger-content-elem_read');
    }
}

$(document).ready(function () {
    window.onunload = function () {
        //sessionStorage.removeItem('disciplineId');
        //sessionStorage.removeItem('disciplineName');
        //sessionStorage.removeItem('disciplineImage');
    }
});