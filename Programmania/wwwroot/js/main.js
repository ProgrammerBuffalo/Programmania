var challanges = [];

function getData(rewards) {
    let data = [];
    let date1 = new Date(rewards[0].createdAt);
    let date2;
    let j = 0;
    data.push({ day: date1.getDate(), exp: rewards[0].experience });
    for (var i = 1; i < rewards.length; i++) {
        date2 = new Date(rewards[i].createdAt);
        if (date1.getFullYear() == date2.getFullYear() && date1.getMonth() == date2.getMonth() && date1.getDate() == date2.getDate()) {
            data[j].exp += rewards[i].experience;
        }
        else {
            j++;
            data[j] = { exp: rewards[i].experience, day: date2.getDate() }
            date1 = date2;
        }   
    }
    return data;
}

function getLabels() {
    let labels = new Array(30);
    let date = new Date();
    for (let i = 0; i < labels.length; i++) {
        labels[i] = date.getDate();
        date.setDate(date.getDate() - 1);
    }
    return labels;
}

function createChart(ctx, labels, data) {
    return new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: "Rewards",
                data: data,
                fill: false,
                borderColor: "rgb(102, 168, 255)",
                tension: 0.1
            }]
        },
        options: {
            layout: {
                padding: 20
            },
            plugins: {
                legend: {
                    labels: {
                        font: {
                            size: 20
                        }
                    }
                }
            },
            parsing: {
                xAxisKey: 'day',
                yAxisKey: 'exp'
            },
            responsive: true,
            maintainAspectRatio: false
        },
    })
}

$(document).ready(function () {
    getUserInfo();
    getCurrentCourse();
    getAllCourses();
    getPerformance();
    getChallanges();
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
            $('#levelDiagram').attr('data-value', data.expToNextLevelPercentage);
            $('#expiriance').text(data.expierence);
            $('#gamesRate').text(data.challengeStats.wins + ' / ' + data.challengeStats.gamesPlayed);
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
            $('#courseBtn').attr('data-id', data.currentCourse.id);
            $('#courseBackground').css('background-color', 'red');
            $('#courseName').text(data.currentCourse.courseName);
            $('#courseDescription').text(data.currentCourse.description);
            $('#courseImage').attr('src', 'data:image/jpg;base64,' + data.currentCourse.image);
            $('#courseDiagram').attr('data-value', data.currentCourse.percentage);
            $('#disciplineBtn').attr('data-id', data.currentDisciplineId);
            $('#discipline').text(data.currentDiscipline);
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
            for (var i = 0; i < data.length; i++) {
                $('#courses').append(createCourseItemMain(data[i]));
            }
        }
    });
}

function getPerformance() {
    $.ajax({
        type: 'GET',
        url: 'Main/get-user-performance',
        processData: true,
        dataType: 'json',
        success: function (rewards) {
            let data = getData(rewards);
            let labels = getLabels();

            var ctx = document.getElementById('performanceChart').getContext('2d');
            createChart(ctx, labels, data);
        }
    });
}

//async function getChallanges() {
//    let data = await getOfferedChallenges();

//    let i = 0;
//    for (i; i < data.length; i++) {
//        challanges.push(data[i]);
//        $('#challanges').append(createOfferedChallangeItem(challanges[i], i));
//    }

//    if (challanges.length < 7) {
//        data = await getPossibleChallenges();
//        for (var j = 0; j < data.length; j++, i++) {
//            challanges.push(data[j]);
//            $('#challanges').append(createPossibleChallangeItem(challanges[i], i));
//        }
//    }
//}

async function getChallanges() {
    let data = await getPossibleChallenges();

    let i = 0;
    for (i; i < data.length; i++) {
        challanges.push(data[i]);
        $('#challanges').append(createPossibleChallangeItem(challanges[i], i));
    }

    if (challanges.length < 7) {
        data = await getOfferedChallenges();
        for (var j = 0; j < data.length; j++, i++) {
            challanges.push(data[j]);
            $('#challanges').append(createOfferedChallangeItem(challanges[i], i));
        }
    }
}

function getPossibleChallenges() {
    return $.ajax({
        type: 'GET',
        url: 'Main/get-possible-challenges',
        processData: true,
        dataType: 'json',
        success: function (data) {
            return data;
        }
    });
}

function getOfferedChallenges() {
    return $.ajax({
        type: 'GET',
        url: 'Main/get-offered-challenges',
        processData: true,
        dataType: 'json',
        success: function (data) {
            return data;
        }
    });
}

function acceptPosibleChallange(el) {
    let index = $(el).attr('data-id');

    $.ajax({
        type: 'POST',
        url: '/Challenge/create-challenge',
        dataType: 'json',
        data: { 'courseId': challanges[index].courseId, 'userId': challanges[index].opponentDescription.id },
        success: function () {
            window.location.href = '/Challenge';
        }
    });
}

function acceptOfferedChallange(el) {
    let index = $(el).attr('data-id');

    $.ajax({
        type: 'POST',
        url: '/Challenge/accept-challenge',
        dataType: 'json',
        data: { 'challengeId': challanges[index].id },
        success: function () {
            window.location.href = '/Challenge';
        }
    });
}

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