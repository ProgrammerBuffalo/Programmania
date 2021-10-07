new Swiper('.courses', {
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev'
	},
	slidesPerView: 1,
	centeredSlides: true,
	spaceBetween: 30,
	speed: 800,
	breakpoints: {
		1200: {
			slidesPerView: 3
		},
		800: {
			slidesPerView: 2
		}
	}
});