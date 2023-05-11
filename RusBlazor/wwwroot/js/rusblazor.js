// Used by Menu to scroll to item
window.scrollToElementInContainer = (containerId, elementId, scrollDuration) => {
    const container = document.getElementById(containerId);
    const element = document.getElementById(elementId);
    if (container && element) {
        const start = container.scrollTop;
        const end = element.offsetTop;
        const change = end - start;
        const duration = scrollDuration; // Duration in ms
        let startTimestamp;

        const easeInOutQuad = (t, b, c, d) => {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t + b;
            t--;
            return -c / 2 * (t * (t - 2) - 1) + b;
        };

        const animateScroll = (timestamp) => {
            if (!startTimestamp) startTimestamp = timestamp;
            const elapsed = timestamp - startTimestamp;
            const progress = Math.min(elapsed / duration, 1);
            container.scrollTop = start + easeInOutQuad(progress, 0, change, 1);
            if (progress < 1) {
                window.requestAnimationFrame(animateScroll);
            }
        };

        window.requestAnimationFrame(animateScroll);
    }
};


// Focus element by ID
window.focusElement = (elementId) => {
    console.log("focusing " + elementId);
    document.getElementById(elementId).focus();
};