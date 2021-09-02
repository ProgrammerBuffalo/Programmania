var bar = new ldBar(".roundedPb", {
	"preset": "circle",
	"min": 0,
	"max": 100,
	"stroke": "#a1a7ff",
	"stroke-width": 10,
});

$('.ldBar').addClass("label-center");
$('.ldBar').css("width", "70%");
$('.ldBar').css("height", "70%");


$('.coursesList__item__hover').on('animationstart', function() {
	bar.set(50, true);
});
