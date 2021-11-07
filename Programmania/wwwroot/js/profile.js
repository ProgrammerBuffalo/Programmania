$(document).ready(function () {
    initModal(document.getElementById('nicknameModal'),
        document.getElementById('enterNicknameModal'),
        document.getElementById('closeNicknameModal'));

    initModal(document.getElementById('avatarModal'),
        document.getElementById('enterAvatarModal'),
        document.getElementById('closeAvatarModal'));

    //$.ajax({
    //    type: 'GET',
    //    url: 'temp',
    //    processData: true,
    //    dataType: 'json',
    //    data: { 'data1': 'aaaa' },
    //    success: function (data) {
    //        alert('ok');
    //    },
    //    error: function () {

    //    }
    //});
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
    $('#nicknameChangeBtn').click(function () {

        //let dataForm = new FormData();
        //dataForm.append('Nickname', 'aa');

        //$.ajax({
        //    type: 'POST',
        //    url: 'Profile/change-nickname',
        //    processData: false,
        //    contentType: false,
        //    data: dataForm,
        //    success: function () {

        //    },
        //    error: function () {

        //    }
        //});

        $.ajax({
            type: 'GET',
            url: 'change-nickname',
            processData: true,
            dataType: 'json',
            data: { a: 'hello' },
            success: function () {

            }
        })
    });
});

$(document).ready(function () {
    $('#avatarInput').change(function () {
        let canvas = document.getElementById('canvas');
        if (this.files[0].size > 100000) {
            alert('error image');
            $(this).parent().find('.error');
        }
        else {
            cropImage(canvas, this.files[0], canvas.width, canvas.height);
        }
    });
});

$(document).ready(function () {
    $('#avatarChangeBtn').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-achivments',
            processData: true,
            dataType: 'json',
            success: function () {

            },
            error: function () {

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