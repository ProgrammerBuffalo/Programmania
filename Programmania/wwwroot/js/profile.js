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
    initTab(document.getElementById('challenges'),
        document.getElementById('challengesTab'));

    //initTab(document.getElementById('achievments'),
    //    document.getElementById('achievmentsTab'));
});

$(document).ready(function () {
    $('#userInfo').click(function () {

        $.ajax({
            type: 'GET',
            url: 'Profile/get-user-info',
            processData: true,
            dataType: 'json',
            success: function (data) {
                $('#totalXp').text(data.coursesEndedCount);
                $('#coursesEnded').text(data.totalXp);
            },
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
            success: function (data) {
                $('#gamesPlayed').text(data.gamesWined);
                $('#gamesWined').text(data.gamesPlayed);
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

        let formData = new FormData(document.getElementById('nicknameForm'));

        $.ajax({
            type: 'POST',
            url: 'Profile/change-nickname',
            processData: false,
            contentType: false,
            data: formData,
            success: function () {
                $('.error').css('display', 'none');
                $('#userNickname').val($('#profileNickname').val());
                $('#nicknameInput').val('');
            },
            error: function (errors) {
                $('.error').css('display', 'none');

                var errors = JSON.parse(jqXHR.responseText);
                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel + 'Label').next()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
                if (errors['error'] != null) {
                    $('#nicknameChangeBtn').after()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
            }
        });
    });
});

//$(document).ready(function () {
//    $('#avatarInput').change(function () {
//        let canvas = document.getElementById('avatarCanvas');
//        canvas.getContext('2d').clearRect(0, 0, 200, 200);

//        if (this.files[0].size > 1000000) {
//            alert('error image');
//            $(this).parent().find('.error');
//            $(this).val('');
//        }
//        else {
//            cropImage(canvas, this.files[0], 200, 200);
//        }
//    });
//});

$(document).ready(function () {
    $('#avatarChangeBtn').click(function () {
        //$('#avatar').val(document.getElementById('avatarCanvas').toDataURL());

        let formData = new FormData(document.getElementById('avatarForm'));
        //formData.append('Nickname', ('#nicknameInput').val());

        $.ajax({
            type: 'POST',
            url: 'Profile/change-avatar',
            processData: false,
            contentType: false,
            data: formData,
            success: function () {
                $('.error').css('display', 'none');

                var reader = new FileReader();
                reader.onload = function (event) {
                    imgtag.src = event.target.result;
                };
                reader.readAsDataURL($('#avatarInput').get(0).files[0]);

                $('#profileImage').val('');
            },
            error: function () {
                $('.error').css('display', 'none');

                for (var key in errors) {
                    var camel = camelize(key);
                    $('#' + camel + 'Label').next()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
                if (errors['error'] != null) {
                    $('#nicknameChangeBtn').after()
                        .css('display', 'inline')
                        .text(errors[key]);
                }
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