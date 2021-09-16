new Swiper('.swiper', {
    autoHeight: true,
    slidesPerView: 1,
    autoplay: {
        delay: 5000,
        stopOnLastSlide: true,
        disableOnInteraction: false
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