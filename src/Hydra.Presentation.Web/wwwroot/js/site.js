// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//menu toggle
let toggle = document.querySelector('.toggle');
let navigation = document.querySelector('.navigation');
let main = document.querySelector('.main');

toggle.onclick = function () {
    navigation.classList.toggle('active');
    main.classList.toggle('active');
}

// add hovered class in selected list item
let list = document.querySelectorAll('.navigation li');
function activeLink() {
    list.forEach((item) =>
        item.classList.remove('hovered')
    );
    this.classList.add('hovered');
}
list.forEach((item) =>
    item.addEventListener('mouseover', activeLink));


let role = document.querySelector('.role');
if (role != null) {
    let roleName = role.textContent;
    let formatted = roleName.replace('[', '').replace(']', '');
    role.textContent = formatted;
    let mainBody = document.querySelector('body');
    mainBody.setAttribute('data-role', formatted);
}



