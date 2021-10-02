function getCookie(key) {
    let name = key + "=";
    let pair = document.cookie.split(';');
    for (let i = 0; i < pair.length; i++) {
        let c = pair[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}