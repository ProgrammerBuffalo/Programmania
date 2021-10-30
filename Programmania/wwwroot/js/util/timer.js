$(document).ready(function () {
	let timerShow = document.getElementById("timer-show");
	let time = 89;
	let timer = setInterval(function () {
		let seconds = time % 60;
		let minutes = parseInt(time / 60);
		console.log(minutes);
		console.log(seconds);
		if (time <= 0) {
			clearInterval(timer);
		}
		else {
			let strTimer = minutes.toString() + ":" + seconds.toString();
			timerShow.innerHTML = strTimer;
		}
		--time;
	}, 1000)
})