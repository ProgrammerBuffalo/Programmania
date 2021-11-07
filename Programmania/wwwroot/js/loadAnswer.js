$(document).ready(function () {
    var answer = localStorage.getItem("answer");
    if (answer != null) {
        var answers = $('.rad-input');
        Array.prototype.forEach.call(answers, item => {
            if (item.value == answer) {
                item.checked = true;
                
                console.log(answer);
            }
        })
    }
});