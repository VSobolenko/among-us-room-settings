function setAccessCookie(key, minutes) {
    const d = new Date();
    d.setTime(d.getTime() + (minutes*60*1000));
    const expires = "expires=" + d.toUTCString();
    document.cookie = "accessKey=" + key + ";" + expires + ";path=/";
}

function checkAccessCookie() {
    const name = "accessKey=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const ca = decodedCookie.split(';');
    for(let i = 0; i < ca.length; i++) {
        let c = ca[i].trim();
        if (c.indexOf(name) === 0) return true;
    }
    return false;
}

async function checkKey() {
    
    // Fast unlock for development
    if (window.location.hostname === "localhost") {
        document.getElementById("mainUI").style.display = "block";
        document.getElementById("accessBlock").style.display = "none";
        // alert("You are logged in for free as a developer")
        return;
    }
    
    const key = document.getElementById("keyInput").value;

    const response = await fetch("/api/access/check", {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({key})
    });

    const data = await response.json();

    if (response.ok && data.success) {
        document.getElementById("mainUI").style.display = "block";
        document.getElementById("accessBlock").style.display = "none";
    } else {
        alert("Неверный ключ! Спроси у @gexetr какой ключ");
        document.getElementById("mainUI").style.display = "none";
    }
}
    