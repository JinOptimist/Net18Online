const rangeInput = document.getElementById('customRange2');
const rangeValue = document.getElementById('rangeValue');

rangeInput.addEventListener('input', () => {
    rangeValue.textContent = rangeInput.value;
});