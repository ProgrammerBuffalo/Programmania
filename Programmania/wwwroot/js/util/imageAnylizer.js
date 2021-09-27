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
                if (colorsFamiliarPercent(pixelDatas[k].color, pixelData) <= 10) {
                    mergeColor(pixelDatas[k].color, pixelData);
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

function colorsFamiliarPercent(color1, color2) {
    let rDiff = Math.abs(color1[0] - color2[0]) / 255;
    let gDiff = Math.abs(color1[1] - color2[1]) / 255;
    let bDiff = Math.abs(color1[2] - color2[2]) / 255;
    return (rDiff + gDiff + bDiff) / 3 * 100;
}

function mergeColor(sourceColor, addColor) {
    sourceColor[0] = (sourceColor[0] + addColor[0]) / 2;
    sourceColor[1] = (sourceColor[1] + addColor[1]) / 2;
    sourceColor[2] = (sourceColor[2] + addColor[2]) / 2;
}

function rgbToHex(rgb) {
    return "#" + componentToHex(rgb[0]) + componentToHex(rgb[1]) + componentToHex(rgb[2]);
}

//function temp() {

//    var color1 = [255, 26, 26];
//    var color2 = [111, 79, 121];
//    console.log(colorsFamiliarPercent(color1, color2));

//    color1 = [230, 25, 25];
//    color2 = [217, 38, 38];
//    console.log(colorsFamiliarPercent(color1, color2));

//    color1 = [230, 25, 25];
//    color2 = [164, 49, 72];
//    console.log(colorsFamiliarPercent(color1, color2));

//    color1 = [230, 25, 25];
//    color2 = [255, 49, 72];
//    console.log(colorsFamiliarPercent(color1, color2));

//    color1 = [230, 25, 25];
//    color2 = [255, 152, 146];
//    console.log(colorsFamiliarPercent(color1, color2));
//}
