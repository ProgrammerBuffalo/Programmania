function createCourse(course) {
    return '<a>' +
        "<div class='course'>" +
        "<div class='course__image lifted dark-image'>" +
        `<img src='data:image/*;base64,${course.image}' />` +
        "</div>" +
        "<div class='course-body'>" +
        "<div class='course-info'>" +
        `<h5>${course.courseName}</h5>` +
        `<p> Lessons Count: ${course.lessonsCompleted} / ${course.lessonsCount}</p>` +
        "</div>" +
        `<div class='course-info__text'>${course.description}</div>` +
        "</div>" +
        "</div>" +
        "</a>"
}

function createUserCourse(course) {
    return "<a>" +
        "<div class='course'>" +
        "<div class='course__image lifted'>" +
        `<img src = 'data:image/*;base64,${course.image}' />` +
        "</div>" +
        "<div class='course-body'>" +
        "<div class='course-info'>" +
        `<h5>${course.courseName}</h5>` +
        `<p> Lessons Count: ${course.lessonsCompleted} / ${course.lessonsCount}</p>` +
        "</div>" +
        `<div class='ldBar label-center' data-preset='circle' data-value='${course.percentage}' data-transition-in='1000' style='width: 50%; height: 50%;'></div>` +
        "</div>" +
        "</div>" +
        "</a>";
}