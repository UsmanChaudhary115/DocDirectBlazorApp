window.submitFormspreeForm = async (formElement, event) => {
    event.preventDefault(); // Explicitly prevent default form submission
    console.log("Form submission intercepted"); // Debug log

    const form = formElement;
    const loading = form.querySelector('.loading');
    const errorMessage = form.querySelector('.error-message');
    const sentMessage = form.querySelector('.sent-message');
    const submitButton = form.querySelector('button[type="submit"]');

    // Show loading state
    loading.style.display = 'block';
    errorMessage.style.display = 'none';
    sentMessage.style.display = 'none';
    submitButton.disabled = true;
    console.log("Loading state displayed"); // Debug log

    try {
        const response = await fetch(form.action, {
            method: 'POST',
            body: new FormData(form),
            headers: {
                'Accept': 'application/json'
            }
        });

        console.log("Response received:", response.status); // Debug log

        if (response.ok) {
            sentMessage.style.display = 'block';
            form.reset(); // Clear the form
            console.log("Success message displayed"); // Debug log
        } else {
            const data = await response.json();
            throw new Error(data.error || 'Something went wrong');
        }
    } catch (error) {
        errorMessage.style.display = 'block';
        errorMessage.textContent = error.message || 'An error occurred. Please try again.';
        console.error("Error during submission:", error.message); // Debug log
    } finally {
        loading.style.display = 'none';
        submitButton.disabled = false;
        console.log("Submission complete"); // Debug log
    }
};