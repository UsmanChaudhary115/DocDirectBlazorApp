window.initNavbar = function () {
    const body = document.querySelector('body');
    const header = document.querySelector('#header');
    const mobileNavToggleBtn = document.querySelector('.mobile-nav-toggle');

    // Skip if navbar elements are missing
    if (!header || !mobileNavToggleBtn) return;

    /**
     * Toggle header scrolled class
     */
    function toggleScrolled() {
        if (!header.classList.contains('scroll-up-sticky') &&
            !header.classList.contains('sticky-top') &&
            !header.classList.contains('fixed-top')) return;

        window.scrollY > 100
            ? body.classList.add('scrolled')
            : body.classList.remove('scrolled');
    }

    /**
     * Toggle mobile nav
     */
    function toggleMobileNav() {
        body.classList.toggle('mobile-nav-active');
        mobileNavToggleBtn.classList.toggle('bi-list');
        mobileNavToggleBtn.classList.toggle('bi-x');
    }

    // Event: toggle nav when button clicked
    mobileNavToggleBtn.addEventListener('click', toggleMobileNav);

    // Event: close mobile nav on nav link click
    document.querySelectorAll('#navmenu a').forEach(link => {
        link.addEventListener('click', () => {
            if (body.classList.contains('mobile-nav-active')) {
                toggleMobileNav();
            }
        });
    });

    // Event: toggle dropdowns in mobile nav
    document.querySelectorAll('.navmenu .toggle-dropdown').forEach(toggle => {
        toggle.addEventListener('click', function (e) {
            e.preventDefault();
            this.parentNode.classList.toggle('active');
            this.parentNode.nextElementSibling.classList.toggle('dropdown-active');
            e.stopImmediatePropagation();
        });
    });

    // Add scroll event for sticky effect
    document.addEventListener('scroll', toggleScrolled);
    window.addEventListener('load', toggleScrolled);
};
 