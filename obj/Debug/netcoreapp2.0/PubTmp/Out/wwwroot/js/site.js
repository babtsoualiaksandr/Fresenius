//// Write your JavaScript code.
$(document).ready(function () {
    $('#IndexTable').DataTable();
    $('#IndexTable').attr('id', 'notable');
    

});

$(document).ready(function () {
    $('#IndexTableInvoice').DataTable({
        paging: false,
        searching: false});
    $('#IndexTableInvoice').attr('id', 'notable');

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
        document.getElementById('InputImage').value = '~/images/' + input.files[0].name

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




//$('.mask').click(function () {
//    let id = $(this).attr('name');

//    $(id).css("backgroundColor", "red");
//    $(".myPartialViewInvoice").show("slow");
//    let url = "EditMaskPartial/"+id;

//    $(".myPartialViewInvoice").load(url, function (responseTxt, statusTxt, xhr) {
//        if (statusTxt == "error")
//            alert("Error: " + xhr.status + ": " + xhr.statusText);
//    });
//});

//$(function () {
//    $(".mask").mouseover(function () {
//        $(this).animate({ height: '+=20', width: '+=20' });
//    });
//    $(".mask").mouseout(function () {
//        $(this).animate({ height: '-=20', width: '-=20' });
//    });
//});

//$('.mask').click(function () {
//    let id = $(this).attr('name');
//    let urll = "EditMaskPartial/" + id;
//    $.ajax({
//        url: urll, // адрес, на который будет отправлен запрос
//        context: $(".myPartialViewInvoice"), // новый контекст исполнения функции
//        success: function () { // если запрос успешен вызываем функцию
//            $(this).html("Всё ок"); // добавлем текстовое содержимое в элемент с классом .myClass
//        }
//    });
//});


///Спрятать показать колонки в выходной форме


$(document).ready(function () {
    var checkBoxes = $("#select input[type='checkbox']");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (!checkBoxes[i].checked) {
            $('#table td:nth-child(' + $(checkBoxes[i]).val() + ')').toggleClass("hide");
        }
    }
    $("#select input[type='checkbox']").click(function () {
        $('#table td:nth-child(' + $(this).val() + ')').toggleClass("hide");
    });
});










