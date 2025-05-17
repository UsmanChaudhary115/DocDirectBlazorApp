window.initializeToasts = function () {
    var toasts = document.querySelectorAll('.toast');
    toasts.forEach(function (toastElement) {
        var toast = new bootstrap.Toast(toastElement, {
            autohide: true,
            delay: 5000
        });
        toast.show();
    });
};