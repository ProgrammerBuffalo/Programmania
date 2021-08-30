"use strict";

var buttons = document.getElementsByClassName("denyBtn");

console.log(buttons);

Array.prototype.forEach.call(buttons, button => {
    button.addEventListener('click', function (e) {
        e = e || window.event;
        var target = e.target || e.srcElement;
        target = target.parentElement.parentElement;
        target.style.animation = "denyAnimation 0.5s ease-in-out 0s";
        console.log(target);
        console.log(target.style.animation);
        target.addEventListener('animationend', () => {
            target.remove();
        })
    }, false);
})