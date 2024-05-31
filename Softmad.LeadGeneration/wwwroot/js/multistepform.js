document.addEventListener("DOMContentLoaded", function () {
    // Competitor fields logic
    const competitorDropdown = document.getElementById("competitorDropdown");
    const addCompetitorBtn = document.getElementById("addCompetitorBtn");
    const competitorContainer = document.getElementById("competitorContainer");
    let competitorCount = 0;

    function toggleCompetitorFields() {
        if (competitorDropdown.value === "True") {
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

    // Multi-step form logic
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

    // Dropdown with "Other" selection logic
    const reqDropdown = document.getElementById("requirementsDropdown");
    const reqOtherInputDiv = document.getElementById("add-input-req");
    const reqOtherInput = document.getElementById("otherRequirementsInput");

    const specializationDropdown = document.getElementById("Specialization");
    const specializationOtherInputDiv = document.getElementById("add-input-spec");
    const specializationOtherInput = document.getElementById("otherSpecializationInput");

    reqDropdown.addEventListener("change", function () {
        toggleOtherInputField(reqDropdown, reqOtherInputDiv, reqOtherInput);
    });

    reqOtherInput.addEventListener('input', function () {
        reqDropdown.value = this.value;
        specializationDropdown.ariaPlaceholder = 'Other';
    });

    specializationDropdown.addEventListener("change", function () {
        toggleOtherInputField(specializationDropdown, specializationOtherInputDiv, specializationOtherInput);
    });

    specializationOtherInput.addEventListener('input', function () {
        specializationDropdown.value = this.value;
        specializationDropdown.ariaPlaceholder = 'Other';
    });

    function toggleOtherInputField(dropdown, otherInputDiv, otherInput) {
        var selectedValue = dropdown.value;
        if (selectedValue === 'other') {
            otherInputDiv.style.display = 'block';
            otherInput.style.display = 'block';
        } else {
            otherInputDiv.style.display = 'none';
            otherInput.value = '';
        }
    }

    // Show fields based on selected type logic
    const chooseType = document.getElementById("chooseType");
    chooseType.addEventListener("change", showFields);

    function showFields() {
        var selectedValue = chooseType.value;
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

    // Update Pincode based on city selection
    function updatePincode() {
        const citiesDataElement = document.getElementById('cities-data');
        if (!citiesDataElement) {
            console.error('cities-data element not found');
            return;
        }
        const cities = JSON.parse(citiesDataElement.textContent);
        console.log('Cities data:', cities); // Debugging statement

        const selectedCity = document.getElementById('citySelect').value;
        console.log('Selected city:', selectedCity); // Debugging statement

        const city = cities.find(c => c.CityName === selectedCity);
        console.log('Matching city:', city); // Debugging statement

        const pincodeField = document.getElementById('pincode');
        if (city) {
            pincodeField.value = city.Pincode;
        } else {
            pincodeField.value = '';
        }
    }

    document.getElementById('citySelect').addEventListener('change', updatePincode);
});
