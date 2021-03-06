new Swiper('.swiper', {
    autoHeight: true,
    slidesPerView: 1,
    autoplay: {
        delay: 5000,
        stopOnLastSlide: true,
        disableOnInteraction: false
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    breakpoints: {
        320: {
            slidesPerView: 1
        },
        640: {
            slidesPerView: 2
        },
        980: {
            slidesPerView: 3
        }
    }
})