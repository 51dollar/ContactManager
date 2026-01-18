document.addEventListener('DOMContentLoaded', () => {
    const toastEl = document.getElementById('appToast');
    if (toastEl) {
        new bootstrap.Toast(toastEl, {
            delay: 3000
        }).show();
    }
});