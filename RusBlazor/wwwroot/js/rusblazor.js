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
    document.getElementById(elementId).focus();
};

// Select text in element by ID
window.selectElement = (elementId) => {
    setTimeout(function () {
        var element = document.getElementById(elementId);
        element.focus();
        element.select();
    }, 10);
}





// Animate a CSS property of an element
window.animateProperty = (elementId, property, startValue, endValue, durationMs, unit = '') => {
    let startTimestamp = null;
    const element = document.getElementById(elementId);

    function frame(timestamp) {
        if (startTimestamp === null) startTimestamp = timestamp;
        const elapsed = timestamp - startTimestamp;
        const progress = Math.min(elapsed / durationMs, 1);
        const currentValue = startValue + (endValue - startValue) * progress;
        element.style[property] = currentValue + unit;
        if (progress < 1) {
            window.requestAnimationFrame(frame);
        }
    }

    window.requestAnimationFrame(frame);
};


window.toggleDropdownHeight = (elementId, isOpen, height, durationMs) => {
    const startHeight = isOpen ? 0 : height;
    const endHeight = isOpen ? height : 0;
    window.animateProperty(elementId, 'height', startHeight, endHeight, durationMs, 'px'); 
};

window.toggleDropdownOpacity = (elementId, isOpen, durationMs) => {
    const startOpacity = isOpen ? 0 : 1;
    const endOpacity = isOpen ? 1 : 0;
    window.animateProperty(elementId, 'opacity', startOpacity, endOpacity, durationMs);
};