// site.js
window.toggleContainer = (isSignIn) => {
    const container = document.getElementById('container');
    const registerBtn = document.getElementById('register');
    const loginBtn = document.getElementById('login');

    if (isSignIn) {
        container.classList.remove("active");
    } else {
        container.classList.add("active");
    }
}
