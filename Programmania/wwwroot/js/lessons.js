var lessonIndex;

$(document).ready(function () {
    $('#disciplineImage').attr('src', sessionStorage.getItem('disciplineImage'));
    $('#disciplineName').text(sessionStorage.getItem('disciplineName'));

    let lessonEl = $('.burger-content').find('.burger-content-elem_unread').first();
    lessonIndex = lessonEl.index();
    if (lessonIndex == -1)
        lessonIndex = 0;

    let lessonId = lessonEl.attr('data-id');
    requestLesson(lessonEl[0], sessionStorage.getItem('disciplineId'), lessonId, false);
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

        if (answerIndex != null) {
            $.ajax({
                type: 'GET',
                url: 'Lessons/check-test',
                data: {
                    'testIndex': answerIndex,
                    'disciplineId': sessionStorage.getItem('disciplineId'),
                    'lessonId': $('.burger-content')[0].children[lessonIndex].getAttribute('data-id')
                },
                dataType: 'json',
                processData: true,
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

$(document).ready(function () {
    window.onunload = function () {
        sessionStorage.removeItem('disciplineId');
        sessionStorage.removeItem('disciplineName');
        sessionStorage.removeItem('disciplineImage');
    }
});

function findChekedAnswer() {
    let answerInputs = document.querySelectorAll('.rad-input');
    for (var i = 0; i < answerInputs.length; i++) {
        if (answerInputs[i].checked == true)
            return i;
    }
}

function getLesson(lesson, disciplineId, lessonId) {
    $.ajax({
        type: 'GET',
        url: 'Disciplines/Lessons',
        data: { 'disciplineId': sessionStorage.getItem('disciplineId'), 'lessonId': elem.attr('data-id') },
        success: function (data) {
            lesson.removeClass('burger-content-elem_unread');
            lesson.addClass('burger-content-elem_read');

            $('#lessonName').val(data.name);
            $('#lessonContent').val(data.html);
            $('#testContent').val(createTest(data.test));
        },
        error: function () {

        }
    });
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

function changeLessonName(lessonEl) {
    $('#lessonName').text(lessonEl.children[0].textContent);
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

//$.ajax({
            //    type: 'GET',
            //    url: 'Lessons',
            //    dataType: 'json',
            //    processData: true,
            //    data: { 'disciplineId': sessionStorage.getItem('disciplineId'), 'lessonId': $(this).attr('data-id') },
            //    success: function (data) {
            //        lessonIndex = $(this).index();
            //        changeLessonName();
            //        $('#lessonContent').html(data.html);
            //        $('#testContent').html(createTest(data.test));
            //    },
            //    error: function () {

            //    }
            //});
        }
        //else if (lessonIndex == 0 && $(this).before().find('burger-content-elem_read')) {
        //    $.ajax({
        //        type: 'GET',
        //        url: 'Disciplines/Lessons',
        //        dataType: 'json',
        //        processData: true,
        //        data: { 'disciplineId': sessionStorage.getItem('disciplineId'), 'lessonId': $(this).attr('data-id') },
        //        success: function (data) {

        //            changeLessonName();
        //            $('#lessonContent').val(data.html);
        //            $('#testContent').val(createTest(data.test));

        //            lessonIndex = $(this).index();
        //            $(this).removeClass('burger-content-elem_unread')
        //            $(this).addClass('burger-content-elem_read');
        //        },
        //        error: function () {

        //        }
        //    });
        //}