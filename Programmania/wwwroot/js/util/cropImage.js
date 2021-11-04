function cropImage(canvas, file, width, height) {
    let context = canvas.getContext('2d');
    context.clearRect(0, 0, width, height);

    const reader = new FileReader();
    reader.onload = function (e) {
        let img = new Image();
        img.onload = function () {

            if (this.naturalWidth > this.naturalHeight) {
                let dx = 0;
                if (this.naturalWidth > width) {
                    canvas.width = this.naturalWidth * width / height;
                    dx = (this.naturalWidth - canvas.width) / 2;
                }
                else
                    canvas.width = width;

                if (this.naturalHeight > height)
                    canvas.height = this.naturalHeight;
                else
                    canvas.height = height;

                context.drawImage(this, dx, 0, this.naturalWidth - dx * 2, this.naturalHeight, 0, 0, width, height);
            }
            else {
                let dy = 0;
                if (this.naturalWidth > width)
                    canvas.width = this.naturalWidth;
                else
                    canvas.width = width;

                if (this.naturalHeight > height) {
                    canvas.height = this.naturalHeight * height / width;
                    dy = (this.naturalHeight - canvas.height) / 2;
                }
                else
                    canvas.height = height;

                context.drawImage(this, 0, dy, this.naturalWidth, this.naturalHeight - dy * 2, 0, 0, width, height);
            }

            //canvas.width = res.width;
            //canvas.height = res.height;


            //if (this.width > this.height) {
            //    let dx = (this.width - this.height) / 2;
            //    context.drawImage(img, dx, 0, this.width - dx, this.height, 0, 0, width, height);
            //}
            //else {
            //    let dy = (this.height - this.width) / 2;
            //    context.drawImage(img, 0, dy, this.width, this.height - dy, 0, 0, width, height);
            //}

        };
        img.setAttribute('src', e.target.result);
    }
    reader.readAsDataURL(file);
}

//function temp(naturalWidth, naturalHeight, width, height) {
//    if (naturalWidth > naturalHeight) {
//        res.height = naturalHeight;
//        res.width = naturalHeight * x / y;
//    }
//    else {
//        res.width = naturalWidth;
//        res.height = naturalWidth * y / x;
//    }
//}

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