﻿window.removePreloader = () => {
    const preloader = document.querySelector('#preloader');
    if (preloader) {
        preloader.remove();
    }
};