function createTest(test) {
    return "" +
        "<div class='content-tests'>" +
        "<div class='content-tests-test' data-aos='fade-right' data-aos-duration='1000'>" +
        `<h3 class='content-tests__question text-normal'>${test.Question}</h3>` +
        createAnswer(test.A1) +
        createAnswer(test.A2) +
        createAnswer(test.A3) +
        createAnswer(test.A3) +
        "</div>" +
        "</div>";
}

function createAnswer(answer) {
    return "<label class='rad-label'>" +
        "<input type='radio' class='rad-input' name='rad' />" +
        "<div class='rad-design'></div>" +
        `<div class='rad-text text-light'>${answer}</div>` +
        "</label>";
}