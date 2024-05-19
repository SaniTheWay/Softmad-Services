document.addEventListener("DOMContentLoaded", function () {
    const competitorDropdown = document.getElementById("competitorDropdown");
    const addCompetitorBtn = document.getElementById("addCompetitorBtn");
    const competitorContainer = document.getElementById("competitorContainer");
    let competitorCount = 0;

    function toggleCompetitorFields() {
        if (competitorDropdown.value === "True") {
            competitorContainer.style.display = "block";
        }
        else {
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

    competitorDropdown.addEventListener("change", function () { toggleCompetitorFields(); });
    addCompetitorBtn.addEventListener("click", function () { addCompetitorFields(); });

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

    // Add Addition Field with "Other" selection

    var reqDropdown = document.getElementById("requirementsDropdown"); // dropdown
    var reqOtherInputDiv = document.getElementById("add-input-req");   // dropdown additional input "div"
    var reqOtherInput = document.getElementById("otherRequirementsInput"); // dropdown additional input

    var specializationDropdown = document.getElementById("Specialization"); // dropdown
    var specializationOtherInputDiv = document.getElementById("add-input-spec");   // dropdown additional input "div"
    var specializationOtherInput = document.getElementById("otherSpecializationInput"); // dropdown additional input

    // Requirements Dropdown
    reqDropdown.addEventListener("change", function () {
        toggleOtherInputField(reqDropdown, reqOtherInputDiv, reqOtherInput);
    });
    reqOtherInput.addEventListener('input', function () {
        // Set the selected option value based on the input field value
        reqDropdown.value = this.value;
        specializationDropdown.ariaPlaceholder = 'Other';
    });

    // Specialization Dropdown
    specializationDropdown.addEventListener("change", function () {
        toggleOtherInputField(specializationDropdown, specializationOtherInputDiv, specializationOtherInput);
    });
    specializationOtherInput.addEventListener('input', function () {
        // Set the selected option value based on the input field value
        specializationDropdown.value = this.value;
        specializationDropdown.ariaPlaceholder = 'Other';
    });

    // Method to change display of input field
    function toggleOtherInputField(dropdown, otherInputDiv, otherInput) {
        var selectedValue = dropdown.value;
        console.log("toggle");
        if (selectedValue === 'other') {
            console.log("other clicked.");
            otherInputDiv.style.display = 'block';
            otherInput.style.display = 'block';
        } else {
            otherInputDiv.style.display = 'none';
            // Clear the input field when a different option is selected
            otherInput.value = '';
        }
    }



    //Add Addition Field with "Other" selection for Lead.Doctor.Specialization
    //const Specialization = document.getElementById("Specialization");
    //Specialization.addEventListener("change", toggleOtherInput22);

    //function toggleOtherInput22() {
    //    var dropdown = document.getElementById("Specialization");
    //    var otherInput = document.getElementById("otherSpecializationInput");
    //    if (dropdown.value === "other") {
    //        otherInput.style.display = "block";
    //    } else {
    //        otherInput.style.display = "none";
    //    }
    //}
    
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


});
