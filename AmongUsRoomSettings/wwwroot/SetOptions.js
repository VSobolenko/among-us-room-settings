function setOptions(values) {
    for (let key in values) {
        const el = document.getElementById(key);
        if (!el) continue;
        
        if (el.type === "checkbox") {
            el.checked = values[key];
        } else {
            el.value = values[key];
        }
    }
}