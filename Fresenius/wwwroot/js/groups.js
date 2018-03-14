//Получить запрос
var xhr = new XMLHttpRequest();
xhr.open('GET', 'http://localhost:51457/Hospitals/GetDataGhart', false);
xhr.send();
if (xhr.status != 200) {

    alert(xhr.status + 'Ошибка в запросе : ' + xhr.statusText); // пример вывода: 404: Not Found
} else {

    alert(xhr.responseText+'всё ок '); // responseText -- текст ответа.

    var groups = JSON.parse(xhr.responseText);

}

var groups = JSON.parse(xhr.responseText);
alert(groups);

