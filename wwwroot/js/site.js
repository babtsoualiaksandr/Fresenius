//// Write your JavaScript code.
$(document).ready(function () {
    $('#IndexTable').DataTable();
});

/// Предварительный просмотр картинок
$('#img').change(function () {
    var input = $(this)[0];
    if (input.files && input.files[0]) {
        if (input.files[0].type.match('image.*')) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#img-preview').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        } else {
            console.log('ошибка, не изображение');
        }
    } else {
        console.log('хьюстон у нас проблема');

    }
    //заполнить строку ввода пути после изменения файла картинки в Edit
    //<input type="text" class="bginput" name="searchuser" id="userfield_txt" size="35" value="" style="width:250px" autocomplete="off" />
    //<input asp-for="Image" class="form-control" id="InputImage" />
    (function bookmark() {
        document.getElementById('InputImage').value = '~/images/'+input.files[0].name

    })();
});

$('#reset-img-preview').click(function () {
    $('#img').val('');
    $('#img-preview').attr('src', 'default-preview.jpg');
});

$('#form').bind('reset', function () {
    $('#img-preview').attr('src', 'default-preview.jpg');
});
//// **конец





