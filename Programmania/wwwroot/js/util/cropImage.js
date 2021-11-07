function cropImage(canvas, file, width, height, maxSize) {
    let context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);

    const reader = new FileReader();
    reader.onload = function (e) {
        let img = new Image();
        img.onload = function () {
            if (this.width > this.height) {
                let dx = (this.width - this.height) / 2;
                context.drawImage(img, dx, 0, this.width - dx, this.height, 0, 0, width, height);
            }
            else {
                let dy = (this.height - this.width) / 2;
                context.drawImage(img, 0, dy, this.width, this.height - dy, 0, 0, width, height);
            }

        };
        img.setAttribute('src', e.target.result);
    }
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