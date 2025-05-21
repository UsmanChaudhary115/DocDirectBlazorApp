window.initScrollTop = function () {
    const scrollTopBtn = document.querySelector('.scroll-top');

    if (!scrollTopBtn) return;

    /**
     * Toggle visibility based on scroll position
     */
    function toggleScrollTop() {
        window.scrollY > 100
            ? scrollTopBtn.classList.add('active')
            : scrollTopBtn.classList.remove('active');
    }

    /**
     * Smooth scroll to top when clicked
     */
    scrollTopBtn.addEventListener('click', (e) => {
        e.preventDefault();
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });

    window.addEventListener('load', toggleScrollTop);
    document.addEventListener('scroll', toggleScrollTop);
};