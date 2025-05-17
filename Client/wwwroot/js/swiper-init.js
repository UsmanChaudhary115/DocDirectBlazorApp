window.initializeSwiper = function () {
    document.querySelectorAll('.init-swiper').forEach(swiperElement => {
        new Swiper(swiperElement, {
            loop: true,
            speed: 400, // reduced transition speed (milliseconds)
            autoplay: {
                delay: 3000 // reduced delay between slides (milliseconds)
            },
            slidesPerView: 'auto',
            pagination: {
                el: '.swiper-pagination',
                type: 'bullets',
                clickable: true
            }
        });
    });
};
