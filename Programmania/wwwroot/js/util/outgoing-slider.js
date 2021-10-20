new Swiper('.outgoing-challenges', {
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev'
	},
	slidesPerView: 1,
	centeredSlides: true,
	spaceBetween: 30,
	speed: 800,
	breakpoints: {
		1252: {
			slidesPerView: 3
		},
		887: {
			slidesPerView: 2
		}
	}
})