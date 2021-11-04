var _URL = window.URL || window.webkitURL;

$(document).ready(function () {
    initModal(document.getElementById('nicknameModal'),
        document.getElementById('enterNicknameModal'),
        document.getElementById('closeNicknameModal'));

    initModal(document.getElementById('avatarModal'),
        document.getElementById('enterAvatarModal'),
        document.getElementById('closeAvatarModal'));

    initTab(document.getElementById('userInfo'),
        document.getElementById('userInfoTab'));

    initTab(document.getElementById('games'),
        document.getElementById('gamesTab'));

    initTab(document.getElementById('achievments'),
        document.getElementById('achievmentsTab'));
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

        let dataForm = new FormData(document.getElementById('nicknameForm'));

        $.ajax({
            type: 'POST',
            url: 'Profile/change-nickname',
            processData: false,
            contentType: false,
            data: dataForm,
            success: function () {

            }
        })
    });
});

$(document).ready(function () {
    $('#avatarInput').change(function () {
        let canvas = document.getElementById('avatarCanvas');
        context.clearRect(0, 0, 200, 200);

        if (this.files[0].size > 100000) {
            alert('error image');
            $(this).parent().find('.error');
            $(this).val('');
        }
        else {
            cropImage(canvas, this.files[0], 200, 200);

            let img = new Image();
            img.naturalWidth
            let objUrl = _URL.createObjectURL(this.files[0]);
            img.onload = function () {
                _URL.revokeObjectURL(objUrl);
            }
            img.src = objUrl;
        }
    });
});

$(document).ready(function () {
    $('#avatarChangeBtn').click(function () {
        $('#avatar').val(document.getElementById('avatarCanvas').toDataURL());

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