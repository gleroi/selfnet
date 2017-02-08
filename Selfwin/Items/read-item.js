function initialize() {
    var images = document.getElementsByTagName("img")

    for (var i = 0; i < images.length; i++) {
        var image = images[i];
        var desc = image["alt"];
        if (desc) {
            var p = document.createElement("p");
            p.textContent = desc;
            image.insertAdjacentElement("afterend", p);
            console.log("desc", desc);
        }
    }
}

window.onload = initialize;