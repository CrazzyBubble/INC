var button_menu_open = document.querySelector(".top__menu");
var left_menu = document.querySelector(".menu")

button_menu_open.addEventListener("click", () => {
    if (left_menu.classList.contains('hidden')) {
        left_menu.classList.remove('hidden');
    } else {
        left_menu.classList.add('hidden');
    }
});