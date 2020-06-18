var button_changes = document.querySelectorAll(".register_button");
var login_page = document.querySelector(".login_form");
var register_page = document.querySelector(".registration_form");

for (i = 0; i < button_changes.length; i++) {
    button_changes[i].addEventListener("click", () => {
        if (register_page.classList.contains('hidden')) {
            register_page.classList.remove('hidden');
            login_page.classList.add('hidden');
        } else {
            login_page.classList.remove('hidden');
            register_page.classList.add('hidden');
        }
    });
}

/*$(function () {
    $('.registration_form .submit_button').click(function (e) {
        e.preventDefault();
        var data = $('.registration_form')

        $.ajax({
            type: 'POST',
            url: '/registration',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).success(function (data) {
            alert("Регистрация пройдена");
        }).fail(function (data) {
            alert("В процесе регистрации возникла ошибка");
        });

        var tokenKey = "tokenInfo";
        $('.login_form .submit_button').click(function (e) {
            e.preventDefault();
            var loginData = $('.login_form')

            $.ajax({
                type: 'POST',
                url: '/login',
                data: loginData
            }).success(function (data) {
                alert('Логин прошел отлично');
                // сохраняем в хранилище sessionStorage токен доступа
                sessionStorage.setItem(tokenKey, data.access_token);
                console.log(data.access_token);
            }).fail(function (data) {
                alert('При логине возникла ошибка');
            });
        });

        $('#logOut').click(function (e) {
            e.preventDefault();
            sessionStorage.removeItem(tokenKey);
        });
    });
})*/