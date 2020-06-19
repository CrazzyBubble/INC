var button_menu_open = document.querySelector(".top__menu");
var left_menu = document.querySelector(".menu");

button_menu_open.addEventListener("click", () => {
    if (left_menu.classList.contains('hidden')) {
        left_menu.classList.remove('hidden');
    } else {
        left_menu.classList.add('hidden');
    }
});

//---------- account options button ----------

var show_account_options = document.querySelector(".top__account");
var account_options = document.querySelector(".account_options");

show_account_options.addEventListener("click", () => {
    if (account_options.classList.contains('hidden')) {
        account_options.classList.remove('hidden');
    } else {
        account_options.classList.add('hidden');
    }
});

//---------- like clicker ----------

var liked = document.querySelector(".image_like");
if (liked != null) {
    liked.addEventListener("click", () => {
        if (liked.classList.contains('on')) {
            liked.classList.remove('on');
        }
        else {
            liked.classList.add('on');
        }
    });
}