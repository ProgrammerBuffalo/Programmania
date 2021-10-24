var lessonIndex;

$(document).ready(function () {
    $('#disciplineImage').attr('src', sessionStorage.getItem('disciplineImage'));
    $('#disciplineName').text(sessionStorage.getItem('disciplineName'));

    var lessons = $('.burger-content');
    for (var i = 0; i < lessons[0].children.length; i++) {
        if (lessons[0].children[i].classList.contains('burger-content-elem_unread')) {
            lessonIndex = i;
            return;
        }
    }
    lessonIndex = 0;
});

$(document).ready(function () {
    $('.burger-content-elem').click(function () {
        if ($(this).find('burger-content-elem_read')) {
            $.ajax({
                type: 'GET',
                url: '',
                dataType: 'json',
                processData: true,
                data: { 'disciplineId': sessionStorage.getItem('disciplineId'), 'lessonId': $(this).attr('data-id') },
                success: function (data) {
                    lessonIndex = $(this).index();
                    $('#lessonName').val(data.name);
                    $('#lessonContent').val(data.html);
                    $('#testContent').val(createTest(data.test));
                },
                error: function () {

                }
            });
        }
        else if (lessonIndex == 0 && $(this).before().find('burger-content-elem_read')) {
            $.ajax({
                type: 'GET',
                url: '',
                dataType: 'json',
                processData: true,
                data: { 'disciplineId': sessionStorage.getItem('disciplineId'), 'lessonId': $(this).attr('data-id') },
                success: function (data) {
                    $('#lessonName').val(data.name);
                    $('#lessonContent').val(data.html);
                    $('#testContent').val(createTest(data.test));

                    lessonIndex = $(this).index();
                    $(this).removeClass('burger-content-elem_unread')
                    $(this).addClass('burger-content-elem_read');
                },
                error: function () {

                }
            });
        }
        else
            alert('you cant read this lesson until you finish previous');
    });
});

$(document).ready(function () {
    $('#next-button').click(function () {
        let curLesson = $('.burger-content')[0].children[lessonIndex];

        if (curLesson.classList.contains('.burger-content-elem_unread')) {

            $.ajax({
                type: 'GET',
                url: '',
                data: { 'disciplineId': sessionStorage.getItem('disciplineId'), 'lessonId': elem.attr('data-id') },
                success: function (data) {
                    curLesson.removeClass('burger-content-elem_unread');
                    curLesson.addClass('burger-content-elem_read');

                    $('#lessonName').val(data.name);
                    $('#lessonContent').val(data.html);
                    $('#testContent').val(createTest(data.test));
                },
                error: function () {

                }
            });
        }
    });
});

$(document).ready(function () {
    $('#checkTest').click(function () {
        let answerIndex = findChekedAnswer();

        if (answerIndex != null) {
            $.ajax({
                type: 'GET',
                url: '',
                data: { 'answer': answerIndex },
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