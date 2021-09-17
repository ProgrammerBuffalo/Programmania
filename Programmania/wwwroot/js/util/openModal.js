var logModal = document.getElementById("loginModal");
var regModal = document.getElementById("registerModal");
var regSpan = document.getElementsByClassName("close")[0];
var logSpan = document.getElementsByClassName("close")[1];

$("#loginBtnIn").click(function () {
    logModal.style.display = "block";
});

logSpan.onclick = function() {
    logModal.style.display = "none";
}

$("#registrBtnIn").click(function(){
    regModal.style.display = "block";
})

regSpan.onclick = function() {
    regModal.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == logModal || event.target == regModal) {
        logModal.style.display = "none";
        regModal.style.display = "none";
    }
}





