var challanges = [];

$(document).ready(function () {
    //getUserInfo();
    //getCurrentCourse();
    //getAllCourses();
    //getPerformance();
    //getPossibleChallenges();
    //getOfferedChallenges();
});

$(document).ready(function () {
    $('#disciplineBtn').click(function () {
        console(this);
        console.log($(this).attr('data-id'))
        let disciplineId = $(this).attr('data-id');
        window.location.href = `Course/Disciplines/Lessons?disciplineId${disciplineId}`;
    });
});

$(document).ready(function () {
    $('#courseBtn').click(function () {
        console(this);
        console.log($(this).attr('data-id'))
        let courseId = $(this).attr('data-id');
        window.location.href = `Course/Disciplines?courseId=${courseId}`;
    });
});

function getUserInfo() {
    $.ajax({
        type: 'GET',
        url: 'Main/get-user-info',
        processData: true,
        dataType: 'json',
        success: function (data) {
            $('#nickname').text(data.nickname);
            $('#level').text(data.level);
            $('#expiriance').text(data.expierence);
            $('#gamesRate').text(data.challengeStats.wins + ' / ' + data.challengeStats.gamesPlayed);
            $('#levelDiagram').attr('data-value', data.expToNextLevelPercentage);
        }
    });
}

function getCurrentCourse() {
    $.ajax({
        type: 'GET',
        url: 'Main/get-user-course',
        processData: true,
        dataType: 'json',
        success: function (data) {
            $('#courseBtn').attr('data-id', data.courseId);
            $('#courseBackground').css('background-color', 'red');
            $('#courseName').text(data.courseName);
            $('#courseDescription').text(data.description);
            $('#courseImage').attr('src', 'data:image/*;base64, ' + data.image);
            $('#courseDiagram').attr('data-value', data.percentage);
            $('#disciplineBtn').attr('data-id', data.currentDisciplineId);
        }
    });
}

function getAllCourses() {
    $.ajax({
        type: 'GET',
        url: 'Main/get-all-courses',
        processData: true,
        dataType: 'json',
        success: function (data) {
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
            for (var i = 0; i < data.lenght; i++) {
                $('#aa').add(getChallangeItem(true, data[i]));
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
            if (data.lenght < 7)
                getPossibleChallenges();

            for (var i = 0; i < data.lenght; i++) {
                $('#aa').add(getChallangeItem(false, data[i]))
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