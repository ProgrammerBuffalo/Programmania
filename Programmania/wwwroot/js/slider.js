new Swiper('.image-slider',{
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    },
    pagination: {
        el: '.swiper-pagination',
        type: 'fraction',

        renderFraction: function (currClass, totalClass){
            return '<span class="' + currClass + '"></span>' + '/' + '<span class="' + totalClass + '"></span>'
        }
    },

    scrollbar: {
        el: '.swiper-scrollbar',
        draggable: true
    },

    keyboard: {
        enabled: true,

        onlyInViewport: true,
        pageUpDown: true,
    },

    autoplay: {
        delay: 5000,

        stopOnLastSlide: true,
        disableOnInteraction: false
    },

    speed: 800,

    effect: 'flip',
    flipEffect: {
        slideShadows: true,
        limitRotation: true
    },

    preloadImages: false,

    lazy: {
        loadOnTransitionStart: false,
        loadPrevNext: true,
    },

    watchSlidesProgress: true,
    watchSlidesVisibility: true,

});