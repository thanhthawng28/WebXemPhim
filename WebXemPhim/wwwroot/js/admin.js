function toggleSidebar() {
    if (window.matchMedia("(min-width: 769px)").matches) {
        const sidebar = document.getElementById("sidebar");
        const main_content = document.getElementById("main-content");
        sidebar.classList.toggle("active");
        if (sidebar.classList.contains("active")) {
            main_content.style.marginLeft = "0";
        } else {
            main_content.style.marginLeft = "200px";
        }
    }
    else {
        const sidebar = document.getElementById("sidebar");
        const main_content = document.getElementById("main-content");
        sidebar.classList.toggle("active");

        if (sidebar.classList.contains("active")) {
            main_content.style.marginTop = "0";
        } else {
            main_content.style.marginTop = "200px";
            main_content.style.marginLeft = "0";
        }
    }
}
function handleWindowResize() {
    const sidebar = document.getElementById("sidebar");
    const main_content = document.getElementById("main-content");
    if (window.innerWidth < 769) {
        if (sidebar.classList.contains("active")) {
            main_content.style.marginTop = "0";
            main_content.style.marginLeft = "0";
        } else {
            main_content.style.marginTop = "200px";
            main_content.style.marginLeft = "0";
        }

    } else {
        if (sidebar.classList.contains("active")) {
            main_content.style.marginTop = "0";
            main_content.style.marginLeft = "0";
        } else {
            main_content.style.marginTop = "0";
            main_content.style.marginLeft = "200px";
        }

    }
}

window.addEventListener("resize", handleWindowResize);
handleWindowResize();
