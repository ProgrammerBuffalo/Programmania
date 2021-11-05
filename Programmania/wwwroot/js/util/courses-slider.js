new Swiper('.courses', {
	autoplay: {
		delay: 5000,
		stopOnLastSlide: false,
		disableOnInteraction: false
	},
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev',
    },
	speed: 500,
	effect: 'fade',
});