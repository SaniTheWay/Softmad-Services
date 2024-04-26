
// JavaScript to handle multi-step form navigation
document.addEventListener('DOMContentLoaded', function () {
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
});