// =========================
// JobFinder Authentication
// =========================

// ===============================
// JobFinder Authentication
// ===============================

const JobFinderAuth = (function () {

    const STORAGE_KEY = "jobfinder_user";

    function setUser(user) {
        localStorage.setItem(STORAGE_KEY, JSON.stringify(user));
    }

    function getUser() {
        const data = localStorage.getItem(STORAGE_KEY);
        return data ? JSON.parse(data) : null;
    }

    function logout() {
        localStorage.removeItem(STORAGE_KEY);
        window.location.href = "login.html";
    }

    return {
        setUser,
        getUser,
        logout
    };

})();

document.addEventListener("DOMContentLoaded", function () {

    const user = JobFinderAuth.getUser();

    const guestLink = document.getElementById("navGuestLink");
    const guestCta = document.getElementById("navGuestCta");
    const userLink = document.getElementById("navUserLink");
    const userCta = document.getElementById("navUserCta");

    if (user) {

        if (guestLink) guestLink.style.display = "none";
        if (guestCta) guestCta.style.display = "none";

        if (userLink) userLink.style.display = "";
        if (userCta) userCta.style.display = "";

        document.querySelectorAll("[data-user-first-name]").forEach(function (el) {
            el.textContent = user.name.split(" ")[0];
        });

    }

    document.querySelectorAll(".js-logout").forEach(function (btn) {

        btn.addEventListener("click", function (e) {

            e.preventDefault();

            JobFinderAuth.logout();

        });

    });

});