//---------- log in / registration switcher ----------

var button_changes = document.querySelectorAll(".register_button");
var login_page = document.querySelector(".login");
var register_page = document.querySelector(".registration");

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