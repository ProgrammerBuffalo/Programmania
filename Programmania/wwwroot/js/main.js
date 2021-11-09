$(document).ready(function () {
    getUserInfo();
    getCurrentCourse();
    getAllCourses();
    getPerformance();
    getPossibleChallenges();
    getOfferedChallenges();
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
            return Json(new { Expierence = user.Exp, Level = (int)(System.Math.Sqrt(user.Exp) / 150) });
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
            return Json(new { CurrentCourse = userCourseVM, CurrentDiscipline = userDiscipline.Discipline.Name });
            data.currentCourse
            data.currentDiscipline.name
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