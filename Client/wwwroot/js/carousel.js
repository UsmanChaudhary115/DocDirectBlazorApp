window.initializeCarousel = function (selector) {
    var carouselElement = document.querySelector(selector);
    if (carouselElement) {
        new bootstrap.Carousel(carouselElement, {
            interval: 3000,
            pause: false
        });
    }
};