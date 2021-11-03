$(document).ready(function () {
    var btnNext = $(".btn__next")[0];
    btnNext.addEventListener('click', e => {
        var answer = document.querySelector('input[name="rad"]:checked').value;
        localStorage.setItem("answer", answer);
        console.log("True");
    });
})
