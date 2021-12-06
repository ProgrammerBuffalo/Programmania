var tests = [];
var answers = [];
var index = 0;

function changeAnswer(event) {
    if ($(event.currentTarget)[0].checked) {
        answers[index].answerId = parseInt($(event.currentTarget).attr('data-id'));

        $('#answersNav')
            .children()
            .eq(index)
            .removeClass('answer_unchoose')
            .addClass('answer_choose');
    }
}

function changeQuestion() {
    $('#question').text(tests[index].question);
    $('#answer1').text(tests[index].a1);
    $('#answer2').text(tests[index].a2);
    $('#answer3').text(tests[index].a3);
    $('#answer4').text(tests[index].a4);
}

function changeTest() {
    changeQuestion();

    $('.rad-input').unbind('change', changeAnswer);

    $('.rad-input').prop('checked', false);

    if (answers[index].answerId != -1)
        $('#rad' + (answers[index].answerId + 1)).prop('checked', true);

    $('.rad-input').bind('change', changeAnswer);
}

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Challenge/tests',
        processData: true,
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                tests = data;
                answers = new Array(data.length);

                for (var i = 0; i < data.length; i++) {
                    answers[i] = { 'questionId': data[i].id, 'answerId': -1 };

                    $('#answersNav').append("<div class='answer answer_unchoose'></div>");
                }

                changeQuestion();

                $('.rad-input').bind('change', changeAnswer);

                $('.rad-input').prop('checked', false);

                runTimer(data.length * 15);
            }
        }
    })
});

$(document).ready(function () {
    $('#btnNext').click(function () {
        if (index + 1 != tests.length) {
            index++;
            changeTest();
        }
    });
});

$(document).ready(function () {
    $('#btnPrev').click(function () {
        if (index != 0) {
            index--;
            changeTest();
        }
    });
});

$(document).ready(function () {
    $('.answer').click(function () {
        index = $(this).attr('data-id');
        changeTest();
    });
});

$(document).ready(function () {
    $('#endTest').click(function () {

        $.ajax({
            type: 'POST',
            url: '/Challenge/send-answers',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(answers),
            success: function () {
                window.location.href = '';
            }
        });
    });
});