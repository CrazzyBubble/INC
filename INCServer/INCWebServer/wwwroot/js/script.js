var button_menu_open = document.querySelector(".top__menu");
var left_menu = document.querySelector(".menu");

button_menu_open.addEventListener("click", () => {
    if (left_menu.classList.contains('hidden')) {
        left_menu.classList.remove('hidden');
    } else {
        left_menu.classList.add('hidden');
    }
});

/*var x = document.querySelector("select");
var i;
for (i = 0; i < 10; i++) {
    var option = document.createElement("option");
    option.text = i + 1;
    option.classList.add("selector_style");
    x.add(option);
}*/

var liked = document.querySelector(".image_like");
liked.addEventListener("click", () => {
    if (liked.classList.contains('on')) {
        liked.classList.remove('on');
    } else {
        liked.classList.add('on');
        liked.classList.remove("hover");
        liked.removeAttribute("hover");
        liked.removeAttribute
    }
})