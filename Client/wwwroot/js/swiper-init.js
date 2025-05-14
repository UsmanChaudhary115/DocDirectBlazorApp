window.initializeSwiper = () => {
    const swipers = document.querySelectorAll('.init-swiper');
    swipers.forEach(el => {
        const configElement = el.querySelector('.swiper-config');
        if (configElement) {
            const config = JSON.parse(configElement.textContent);
            new Swiper(el, config);
        }
    });
};
