////$.ajax({
////    type: 'POST',
////    url: '/Challenge/send-answers',
////    contentType: 'application/json',
////    dataType: 'json',
////    data: JSON.stringify(answers),
////    success: function () {
////        window.location.href = '';
////    }
////});

//public int DiscipineId { get; set; }
//   public string Name { get; set; }
//   public int Order { get; set; }
//   public string Content { get; set; }

//$(...).off();


$(document).ready(function () {

});

//#region Course
$(document).ready(function () {
    var formData = {
        discipineId: 1,
        name: 'aaa',
        content: 'content1'
    }

    $.ajax({
        type: 'POST',
        url: 'Lesson',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(formData),
        success: function () {

        },
        error: function () {

        }
    });
});
//#endregion

//#region Discipline
$(document).ready(function () {
    var formData = {
        discipineId: 1,
        name: 'aaa',
        content: 'content1'
    }

    $.ajax({
        type: 'POST',
        url: 'Lesson',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(formData),
        success: function () {

        },
        error: function () {

        }
    });
});
//#endregion

//#region Lesson
$(document).ready(function () {
    var formData = {
        discipineId: 1,
        name: 'aaa',
        content: 'content1'
    }

    $.ajax({
        type: 'POST',
        url: 'Lesson',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(formData),
        success: function () {

        },
        error: function () {

        }
    });
});
//#endregion

//#region Test

//#endregion

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});

$(document).ready(function () {

});