$(document).ready(function () {
    initModal(document.getElementById('profileModal'),
        document.getElementById('enterProfileModal'),
        document.getElementById('closeProfileModal'));
});

$(document).ready(function () {
    $('#userInfo').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-user-info',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

$(document).ready(function () {
    $('#games').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-games',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

$(document).ready(function () {
    $('#achievments').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-achivments',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

$(document).ready(function () {
    $('#avatar').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-achivments',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

$(document).ready(function () {
    $('#nickname').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-achivments',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

$(document).ready(function () {
    $('#change-nickname').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-achivments',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

$(document).ready(function () {
    $('#change-avatar').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-achivments',
            processData: true,
            dataType: 'json',
            success: function () {

            }
        });
    });
});

//$(document).ready(function () {
//    $('#profileImageInput').change(function () {
//        if (this.files[0].size < 100000) {
//            $.ajax({
//                type: 'POST',
//                url: 'change-image',
//                processData: true,
//                dataType: 'json',
//                data: { 'image': this.files[0] },
//                success: function () {
//                    var reader = new FileReader();
//                    reader.onload = function (e) {
//                        $('#profileImage').setAttribute('src', e.target.result);
//                    };
//                    reader.readAsDataURL(this.files[0]);
//                },
//                error: function () {
//                    alert('error try again');
//                }
//            })
//        }
//        else {
//            alert('image size is too large');
//        }
//    });
//});