function createCourseItemMain(course) {
    return "<div class='swiper-slide'>"
        + "<div class='course' style='background-color: #fcc9c0;'>"
        + "<div class='course__content'>"
        + "<div class='course__info'>"
        + `<h2 class='course__title title_ruby'>${course.courseName}</h2>`
        + "<div class='course__percent percent_ruby'>"
        + `<div class='ldBar label-center' data-preset='circle' data-id='${course.percentage}' data-transition-in='1000' style='width: 100%; height: 100%'></div>`
        + "</div>"
        + "</div>"
        + "<div class='course__button button_ruby'>"
        + `<a href='Courses' data-value='${course.courseId}'>Go to course</a>`
        + "</div>"
        + "</div>"
        + "<div class='flip'>"
        + "<div class='course__image flip__front'>"
        + `<img src='data:image/*;base64,${course.image}' />`
        + "</div>"
        + "<div class='flip__back'>"
        + `<p>${course.description}</p>`
        + "</div>"
        + "</div>"
        + "</div>"
        + "</div>";
}

//<div class='swiper-slide'>
//    <div class='course' style='background-color: #fcc9c0;'>
//        <div class='course__content'>
//            <div class='course__info'>
//                <h2 class='course__title title_ruby'>Ruby</h2>
//                <div class='course__percent percent_ruby'>
//                    <div class='ldBar label-center' data-preset='circle' data-value='77' data-transition-in='1000' style='width: 100%; height: 100%'></div>
//                </div>
//            </div>

//            <div class='course__button button_ruby'>
//                <a href='Courses' data-value=''>Go to course</a>
//            </div>
//        </div>

//        <div class='flip'>
//            <div class='course__image flip__front'>
//                <img src='~/images/rubyCourse.png' />
//            </div>

//            <div class='flip__back'>
//                <p>Welcome to Ruby Course!</p>
//            </div>
//        </div>
//    </div>
//</div>