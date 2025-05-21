window.submitContactForm = async function (name, email, subject, message) {
    const form = document.getElementById('contact-form');
    const loading = form.querySelector('.loading');
    const errorMessage = form.querySelector('.error-message');
    const sentMessage = form.querySelector('.sent-message');

    // Show loading
    loading.style.display = 'block';
    errorMessage.style.display = 'none';
    sentMessage.style.display = 'none';

    try {
        const response = await fetch('https://formspree.io/f/xvgalnyw', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name, email, subject, message })
        });

        if (response.ok) {
            sentMessage.style.display = 'block';
            form.reset();
        } else {
            errorMessage.textContent = 'Failed to send message. Please try again.';
            errorMessage.style.display = 'block';
        }
    } catch (error) {
        errorMessage.textContent = 'An error occurred: ' + error.message;
        errorMessage.style.display = 'block';
    } finally {
        loading.style.display = 'none';
    }
};