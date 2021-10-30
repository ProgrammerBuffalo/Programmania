function cropAvatarImage(img, file) {
    const reader = new FileReader();
    reader.onload = function (e) {
        let img = document.createElement('img');
       
        img.width = 200;
        img.height = 200;
        img.setAttribute('imgPreview');
        img.setAttribute('src', e.target.result);
        return img;
    };
    reader.readAsDataURL(file);
}

//const canvas = document.getElementById('canvas');
//const ctx = canvas.getContext('2d');
//image = document.getElementById("source");
//ctx.drawImage(image, 33, 71, 104, 124, 21, 20, 87, 104);

//function readURL() {
//    var myimg = document.getElementById("source");
//    var input = document.getElementById("myfile");
//    if (input.files && input.files[0]) {
//        var reader = new FileReader();
//        reader.onload = function (e) {
//            console.log("changed");
//            myimg.src = e.target.result;
//            drawimg(e.target.result);
//        }
//        reader.readAsDataURL(input.files[0]);
//    }
//}

//document.querySelector('#myfile').addEventListener('change', function () {
//    readURL()
//});

//function drawimg(idata) {
//    var img = new Image();
//    img.onload = function () {
//        ctx.drawImage(img, 33, 71, 104, 124, 21, 20, 87, 104);
//    };
//    img.src = idata;
//}