window.scrollToBottomOfChat = () => {
    const container = document.getElementById("chatContainer");
    if (container) {
        container.scrollTo({
            top: container.scrollHeight,
            behavior: "smooth"
        });
    }
};
