function imageColorAnylizer(image) {
    var pixelData;
    var pixelDatas = [];

    var canvas = document.createElement('canvas');
    canvas.width = image.width;
    canvas.height = image.height;
    canvas.getContext('2d').drawImage(image, 0, 0, image.width, image.height);

    for (var i = 0; i < image.height; i += 5) {
        outerLoop:
        for (var j = 0; j < image.width; j += 5) {
            pixelData = canvas.getContext('2d').getImageData(i, j, 1, 1).data;
            for (var k = 0; k < pixelDatas.length; k++) {
                if (pixelDatas[k].color[0] == pixelData[0] &&
                    pixelDatas[k].color[1] == pixelData[1] &&
                    pixelDatas[k].color[2] == pixelData[2]) {
                    pixelDatas[k].count++;
                    continue outerLoop;
                }
            }
            pixelDatas.push({ 'color': pixelData, 'count': 1 });
        }
    }

    var count = pixelDatas[0].count;
    for (var i = 0; i < pixelDatas.length; i++) {
        if (count < pixelDatas[i].count) {
            pixelData = pixelDatas[i].color;
            count = pixelDatas[i].count;
        }
    }
    return pixelData
}

function componentToHex(c) {
    var hex = c.toString(16);
    return hex.length == 1 ? "0" + hex : hex;
}

function isColorsEqual(color1, color2) {
    return color1[0] == color2[0] && color1[1] == color2[1] && color1[2] == color2[2];
}

function rgbToHex(rgb) {
    return "#" + componentToHex(rgb[0]) + componentToHex(rgb[1]) + componentToHex(rgb[2]);
}