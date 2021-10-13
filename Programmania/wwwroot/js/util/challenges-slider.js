new Swiper('.challenges-swiper', {
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev'
	},
	slidesPerView: 1,
	spaceBetween: 30,
	speed: 800,
	centeredSlides: true,
	breakpoints: {
		1330: {
			slidesPerView: 3
		},
		950: {
			slidesPerView: 2
		}
	},
});