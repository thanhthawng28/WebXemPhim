document.addEventListener("DOMContentLoaded", function () {
    var confirmPasswordInput = document.getElementById("ConfirmPassword");
    var passwordInput = document.getElementById("UserPassword");
    var passwordMismatchMessage = document.getElementById("passwordMismatch");

    confirmPasswordInput.addEventListener("input", function () {
        if (confirmPasswordInput.value !== passwordInput.value) {
            passwordMismatchMessage.style.display = "block";
        } else {
            passwordMismatchMessage.style.display = "none";
        }
    });

    var registrationForm = document.querySelector("form");
    registrationForm.addEventListener("submit", function (event) {
        if (confirmPasswordInput.value !== passwordInput.value) {
            passwordMismatchMessage.style.display = "block";
            event.preventDefault();
        }
    });
});