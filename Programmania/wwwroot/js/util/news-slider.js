new Swiper('.news', {
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev'
	},
	slidesPerView: 1,
	spaceBetween: 30,
	centeresSlides: true,
	speed: 800,
	breakpoints: {
		1360: {
			slidesPerView: 3
		},
		920: {
			slidesPerView: 2
		}
	}
});