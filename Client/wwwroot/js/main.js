(function () {
    "use strict";

    window.initMobileNav = function () {
        /**
         * Mobile nav toggle
         */
        const mobileNavToggleBtn = document.querySelector('.mobile-nav-toggle');
        if (!mobileNavToggleBtn) return;

        function mobileNavToogle() {
            document.querySelector('body').classList.toggle('mobile-nav-active');
            mobileNavToggleBtn.classList.toggle('bi-list');
            mobileNavToggleBtn.classList.toggle('bi-x');
        }

        mobileNavToggleBtn.addEventListener('click', mobileNavToogle);

        /**
         * Hide mobile nav on same-page/hash links
         */
        document.querySelectorAll('#navmenu a').forEach(navmenu => {
            navmenu.addEventListener('click', () => {
                if (document.querySelector('.mobile-nav-active')) {
                    mobileNavToogle();
                }
            });
        });

        /**
         * Toggle mobile nav dropdowns
         */
        document.querySelectorAll('.navmenu .toggle-dropdown').forEach(navmenu => {
            navmenu.addEventListener('click', function (e) {
                e.preventDefault();
                this.parentNode.classList.toggle('active');
                this.parentNode.nextElementSibling.classList.toggle('dropdown-active');
                e.stopImmediatePropagation();
            });
        });
    };

    window.initScrollTop = function () {
        /**
         * Scroll top button
         */
        let scrollTop = document.querySelector('.scroll-top');
        if (!scrollTop) return;

        function toggleScrollTop() {
            window.scrollY > 100 ? scrollTop.classList.add('active') : scrollTop.classList.remove('active');
        }

        scrollTop.addEventListener('click', (e) => {
            e.preventDefault();
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });

        window.addEventListener('scroll', toggleScrollTop);
        toggleScrollTop();
    };

    window.initStickyHeader = function () {
        /**
         * Apply .scrolled class to the body as the page is scrolled down
         */
        function toggleScrolled() {
            const selectBody = document.querySelector('body');
            const selectHeader = document.querySelector('#header');
            if (!selectHeader) return;
            if (!selectHeader.classList.contains('scroll-up-sticky') &&
                !selectHeader.classList.contains('sticky-top') &&
                !selectHeader.classList.contains('fixed-top')) return;

            window.scrollY > 100 ? selectBody.classList.add('scrolled') : selectBody.classList.remove('scrolled');
        }

        document.addEventListener('scroll', toggleScrolled);
        window.addEventListener('load', toggleScrolled);
    };

    window.initAOS = function () {
        /**
         * Animation on scroll
         */
        if (window.AOS) {
            AOS.init({
                duration: 600,
                easing: 'ease-in-out',
                once: true,
                mirror: false
            });
        }
    };

    window.initGLightbox = function () {
        /**
         * Lightbox
         */
        if (typeof GLightbox !== 'undefined') {
            GLightbox({ selector: '.glightbox' });
        }
    };

    window.initPureCounter = function () {
        if (typeof PureCounter !== 'undefined') {
            new PureCounter();
        }
    };

    window.initFAQToggle = function () {
        /**
         * Frequently Asked Questions Toggle
         */
        document.querySelectorAll('.faq-item h3, .faq-item .faq-toggle').forEach((faqItem) => {
            faqItem.addEventListener('click', () => {
                faqItem.parentNode.classList.toggle('faq-active');
            });
        });
    };

    window.initSwiperSliders = function () {
        /**
         * Swiper sliders
         */
        if (typeof Swiper === 'undefined') return;

        document.querySelectorAll(".init-swiper").forEach(function (swiperElement) {
            let config = JSON.parse(swiperElement.querySelector(".swiper-config").innerHTML.trim());

            if (swiperElement.classList.contains("swiper-tab")) {
                initSwiperWithCustomPagination(swiperElement, config);
            } else {
                new Swiper(swiperElement, config);
            }
        });
    };

    window.initScrollToHash = function () {
        /**
         * Correct scroll for hash links
         */
        if (window.location.hash && document.querySelector(window.location.hash)) {
            setTimeout(() => {
                let section = document.querySelector(window.location.hash);
                let scrollMarginTop = getComputedStyle(section).scrollMarginTop;
                window.scrollTo({
                    top: section.offsetTop - parseInt(scrollMarginTop),
                    behavior: 'smooth'
                });
            }, 100);
        }
    };

    window.initScrollSpy = function () {
        /**
         * ScrollSpy
         */
        let navmenulinks = document.querySelectorAll('.navmenu a');

        function navmenuScrollspy() {
            navmenulinks.forEach(navmenulink => {
                if (!navmenulink.hash) return;
                let section = document.querySelector(navmenulink.hash);
                if (!section) return;

                let position = window.scrollY + 200;
                if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
                    document.querySelectorAll('.navmenu a.active').forEach(link => link.classList.remove('active'));
                    navmenulink.classList.add('active');
                } else {
                    navmenulink.classList.remove('active');
                }
            });
        }

        window.addEventListener('scroll', navmenuScrollspy);
        window.addEventListener('load', navmenuScrollspy);
    };

    window.initPreloader = function () {
        const preloader = document.querySelector('#preloader');
        if (preloader) {
            window.addEventListener('load', () => {
                preloader.remove();
            });
        }
    };

})();
