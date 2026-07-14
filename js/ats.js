document.addEventListener("DOMContentLoaded", function () {

    const resumeUpload = document.getElementById("resumeUpload");
    const checkATS = document.getElementById("checkATS");
    const applyBtn = document.getElementById("applyBtn");
    const popup = document.getElementById("applyPopup");

    if (!resumeUpload || !checkATS || !applyBtn || !popup) {
        return;
    }

    applyBtn.disabled = true;

    checkATS.addEventListener("click", function () {

        if (resumeUpload.files.length === 0) {
            alert("Please upload your resume first.");
            return;
        }

        const file = resumeUpload.files[0];

        document.getElementById("resumeName").textContent = file.name;

        const score = Math.floor(Math.random() * 21) + 80;

        document.getElementById("atsResult").style.display = "block";
        document.getElementById("atsScore").textContent = score + "%";
        document.getElementById("progressBar").style.width = score + "%";

        let feedback = "";

        if (score >= 90) {
            feedback = "Excellent Resume. Highly ATS Friendly.";
        } else if (score >= 80) {
            feedback = "Good Resume. Add more technical keywords.";
        } else {
            feedback = "Resume needs improvement.";
        }

        document.getElementById("feedback").textContent = feedback;

        applyBtn.disabled = false;

    });

    applyBtn.addEventListener("click", function () {

        if (resumeUpload.files.length === 0) {
            alert("Please upload your resume first.");
            return;
        }

        popup.style.display = "flex";

    });

});

function closePopup() {

    const popup = document.getElementById("applyPopup");

    if (popup) {
        popup.style.display = "none";
    }

}

let savedJobs = JSON.parse(localStorage.getItem("savedJobs")) || [];

function removeJob(id) {

    savedJobs = savedJobs.filter(job => job.id !== id);

    localStorage.setItem("savedJobs", JSON.stringify(savedJobs));

    if (typeof displaySavedJobs === "function") {
        displaySavedJobs();
    }

}