$(function () {
    var startURL = "account/";
    var searchURL = "search/";
    var tokenKey = "tokenInfo";
    $('.register_form .submit_button').click(function (e) {
        e.preventDefault();
        var $data = {};
        $('.register_form').find('input').each(function () {
            if (this.type != "button")
                $data[this.name] = $(this).val();
        });
        if (data.password.length >= 8) {
            $.ajax({
                type: 'POST',
                url: startURL + 'register',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify($data),
                success: function (data) {
                    alert('Registration is OK!');
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
        else {
            alert("Short password");
        }
    });

    $('.login_form .submit_button').click(function (e) {
        e.preventDefault();
        var $data = {};
        $('.login_form').find('input').each(function () {
            if (this.type != "button")
                $data[this.name] = $(this).val();
        });
        $.ajax({
            type: 'POST',
            url: startURL+'login',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify($data),
            success: function (data) {
                var res = JSON.parse(data)
                // сохраняем в хранилище sessionStorage токен доступа
                sessionStorage.setItem(tokenKey, res.access_token);
                sessionStorage.setItem("username", res.username);
                alert('Login is OK');
                //Переходим на след страницу
                /*$.ajax({

                    type: 'GET',
                    url: searchURL,
                    beforeSend: function (xhr) {
                        var token = sessionStorage.getItem(tokenKey);
                        xhr.setRequestHeader("Authorization", "Bearer " + token);
                    },
                    success: function (data) {
                        window.location.assign(window.location.hostname + searchURL);
                        //console.log(data);
                    }
                });*/
                var myHeaders = new Headers();
                myHeaders.append('Authorization', "Bearer " + token);

                var myInit = {
                    method: 'GET',
                    headers: myHeaders,
                    mode: 'cors',
                    cache: 'default'
                };
                console.log(window.location.hostname);
                var myRequest = new Request(window.location.hostname + searchURL, myInit);

            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    });

    $('#logOut').click(function (e) {
        e.preventDefault();
        sessionStorage.removeItem(tokenKey);
    });
})
