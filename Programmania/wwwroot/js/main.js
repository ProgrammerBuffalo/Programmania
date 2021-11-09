$(document).ready(function () {
    //getUserInfo();
    //getCurrentCourse();
    //getAllCourses();
    //getPerformance();
    //getPossibleChallenges();
    //getOfferedChallenges();

    //$('#nickname').text('test')
    //$('#courseBackground').css('background-color', 'red');
    //$('#courseImage').attr('src', '/images/AngularLogo.png');
    //$('#courseDescription').text('test')
    //$('#courseName').text('test');
    //$('#courseDiagram').attr('data-value', '10');
    //$('#levelDiagram').attr('data-value', '10');
    //$('#level').text('test');
    //$('#expiriance').text('test');
    //$('#gamesRate').text('test');
});

function getUserInfo() {
    $.ajax({
        type: 'GET',
        url: '',
        processData: true,
        dataType: 'json',
        success: function (data) {
            //data.nickname;
            //data.expierence;
            //data.level;
            //data.expToNextLevelPercentage
        }
    });
}

function getCurrentCourse() {
    $.ajax({
        type: 'GET',
        url: '',
        processData: true,
        dataType: 'json',
        success: function (data) {
            //data.currentCourse
            //data.currentDiscipline.name
        }
    });
}

function getAllCourses() {
    $.ajax({
        type: 'GET',
        url: '',
        processData: true,
        dataType: 'json',
        success: function (data) {
            return Json(userCourses);
            for (var i = 0; i < data.lenght; i++) {

            }
        }
    });
}

function getPossibleChallenges() {
    $.ajax({
        type: 'GET',
        url: '',
        processData: true,
        dataType: 'json',
        success: function (data) {
            return Json(possibleChallenges);
            for (var i = 0; i < data.lenght; i++) {

            }
        }
    });
}

function getOfferedChallenges() {
    $.ajax({
        type: 'GET',
        url: '',
        processData: true,
        dataType: 'json',
        success: function (data) {
            return Json(offeredChallenges);
            for (var i = 0; i < data.lenght; i++) {

            }
        }
    });
}

function getPerformance() {
    $.ajax({
        type: 'GET',
        url: '',
        processData: true,
        dataType: 'json',
        success: function (data) {
            for (var i = 0; i < data.lenght; i++) {

            }
        }
    });
}