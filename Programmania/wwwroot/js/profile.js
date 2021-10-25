$(document).ready(function () {
    $('#profileImage').click(function () {
        $('#profileImageInput').click();
    });
});

$(document).ready(function () {
    $('#profileImageInput').change(function () {
        if (this.files[0].size < 100000) {
            $.ajax({
                type: 'POST',
                url: 'change-image',
                processData: true,
                dataType: 'json',
                data: { 'image': this.files[0] },
                success: function () {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('#profileImage').setAttribute('src', e.target.result);
                    };

                    reader.readAsDataURL(this.files[0]);
                },
                error: function () {
                    alert('error try again');
                }
            })
        }
        else {
            alert('image size is too large');
        }
    });
});

var profileModal = document.getElementById("profileModal");
var regModal = document.getElementById("registerModal");
var regSpan = document.getElementsByClassName("close");
var logSpan = document.getElementsByClassName("close");

$("#loginBtnIn").click(function () {
    logModal.style.display = "block";
});

logSpan.onclick = function() {
    logModal.style.display = "none";
}

$("#registrBtnIn").click(function(){
    regModal.style.display = "block";
})

regSpan.onclick = function() {
    regModal.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == logModal || event.target == regModal) {
        logModal.style.display = "none";
        regModal.style.display = "none";
    }
}