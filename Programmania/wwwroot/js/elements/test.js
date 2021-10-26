function createTest(test) {
    return `<h3 class='content-tests__question text-normal'>${test.question}</h3>` +
        createAnswer(test.a1) +
        createAnswer(test.a2) +
        createAnswer(test.a3) +
        createAnswer(test.a4);
}

function createAnswer(answer) {
    return "<label class='rad-label'>" +
        "<input type='radio' class='rad-input' name='rad' />" +
        "<div class='rad-design'></div>" +
        `<div class='rad-text text-light'>${answer}</div>` +
        "</label>";
}