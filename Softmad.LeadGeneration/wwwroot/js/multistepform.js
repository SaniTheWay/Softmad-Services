document.addEventListener("DOMContentLoaded", function () {
    const competitorDropdown = document.getElementById("competitorDropdown");
    const addCompetitorBtn = document.getElementById("addCompetitorBtn");
    const competitorContainer = document.getElementById("competitorContainer");
    let competitorCount = 0;

    function toggleCompetitorFields() {
        if (competitorDropdown.value === "Yes") {
            competitorContainer.style.display = "block";
        } else {
            competitorContainer.style.display = "none";
        }
    }

    function addCompetitorFields() {
        competitorCount++;
        const newFields = document.createElement('div');
        newFields.classList.add('competitor-fields', 'mb-4'); // Added spacing class
        newFields.innerHTML = `
            <h4 class="mt-4">Competitor ${competitorCount}</h4>
            <div class="form-group">
                <label for="competitorName${competitorCount}">Competitor Name</label>
                <input type="text" class="form-control" id="competitorName${competitorCount}" name="competitorName${competitorCount}">
            </div>
            <div class="form-group">
                <label for="competitorModel${competitorCount}">Competitor Model</label>
                <input type="text" class="form-control" id="competitorModel${competitorCount}" name="competitorModel${competitorCount}">
            </div>
        `;
        competitorContainer.appendChild(newFields);
    }

    competitorDropdown.addEventListener("change", toggleCompetitorFields);
    addCompetitorBtn.addEventListener("click", addCompetitorFields);

    // Initialize the display based on the default selected option in the dropdown
    toggleCompetitorFields();

    const form = document.getElementById('leadForm');
    const steps = form.querySelectorAll('.step');
    let currentStep = 0;

    function showStep(index) {
        steps.forEach(step => step.classList.remove('active'));
        steps[index].classList.add('active');
        currentStep = index;
    }

    function nextStep() {
        if (currentStep < steps.length - 1) {
            showStep(currentStep + 1);
        }
    }

    function prevStep() {
        if (currentStep > 0) {
            showStep(currentStep - 1);
        }
    }

    form.querySelectorAll('.btn-next').forEach(btn => {
        btn.addEventListener('click', nextStep);
    });

    form.querySelectorAll('.btn-prev').forEach(btn => {
        btn.addEventListener('click', prevStep);
    });

    function toggleOtherInput() {
        var dropdown = document.getElementById("requirementsDropdown");
        var otherInput = document.getElementById("otherRequirementsInput");
        if (dropdown.value === "other") {
            otherInput.style.display = "block";
        } else {
            otherInput.style.display = "none";
        }
    }

    function toggleOtherInput22() {
        var dropdown = document.getElementById("Specialization");
        var otherInput = document.getElementById("otherRequirementsInput1");
        if (dropdown.value === "other") {
            otherInput.style.display = "block";
        } else {
            otherInput.style.display = "none";
        }
    }

    const chooseType = document.getElementById("chooseType");
    chooseType.addEventListener("change", showFields);

    function showFields() {
        var selectedValue = chooseType.options[chooseType.selectedIndex].value;
        var purchaseHeadFields = document.getElementById("purchaseHeadFields");
        var bioMedicalFields = document.getElementById("bioMedicalFields");
        var ownerFields = document.getElementById("ownerFields");

        if (selectedValue === "Purchase Head") {
            purchaseHeadFields.style.display = "block";
            bioMedicalFields.style.display = "none";
            ownerFields.style.display = "none";
        } else if (selectedValue === "Bio Medical") {
            purchaseHeadFields.style.display = "none";
            bioMedicalFields.style.display = "block";
            ownerFields.style.display = "none";
        } else if (selectedValue === "Owner") {
            purchaseHeadFields.style.display = "none";
            bioMedicalFields.style.display = "none";
            ownerFields.style.display = "block";
        } else {
            purchaseHeadFields.style.display = "none";
            bioMedicalFields.style.display = "none";
            ownerFields.style.display = "none";
        }
    }

    const requirementsDropdown = document.getElementById("requirementsDropdown");
    requirementsDropdown.addEventListener("change", toggleOtherInput);

    const Specialization = document.getElementById("Specialization");
    Specialization.addEventListener("change", toggleOtherInput22);
});
