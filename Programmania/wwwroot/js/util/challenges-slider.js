new Swiper('.challenges-swiper', {
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev'
	},
	slidesPerView: 1,
	spaceBetween: 30,
	speed: 800,
	initialSlide: 1,
	centeredSlides: true,
	breakpoints: {
		1600: {
			slidesPerView: 4
        },
		1330: {
			slidesPerView: 3
		},
		950: {
			slidesPerView: 2
		}
	},
});