var btn = document.getElementById('hide-btn');
var content = document.getElementById('menu-content');
var arrow = document.getElementById('arrow');

btn.onclick = function () {
	content.classList.toggle("active");
	if (content.classList.contains("active")) {
		arrow.classList.remove("arrow-right");
		arrow.classList.add("arrow-left");
	}
	else {
		arrow.classList.remove("arrow-left");
		arrow.classList.add("arrow-right");
	}

}