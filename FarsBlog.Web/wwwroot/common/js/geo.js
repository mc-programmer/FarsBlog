//#region Get cities
let provincesSelect = document.getElementById("provinces");
let citiesSelect = document.getElementById("cities");
if (provincesSelect) {

    if (citiesSelect) {
        provincesSelect.addEventListener("change", () => updateCities(provincesSelect.value, citiesSelect));
    }

    updateProvinces(provincesSelect).then(async _ => {
        if (provincesSelect.getAttribute("value") != null) {
            Array.from(provincesSelect.querySelectorAll("option")).find(o => o.value == provincesSelect.getAttribute("value")).selected = "selected";
            await updateCities(provincesSelect.value, citiesSelect)
            Array.from(citiesSelect.querySelectorAll("option")).find(o => o.value == citiesSelect.getAttribute("value")).selected = "selected";
        }
    })
}

async function updateProvinces(provincesSelect) {
    var responseText = await fetch("/provinces");

    var response = await responseText.json();

    if (response.isSuccess == true) {
        if (provincesSelect) {
            provincesSelect.innerHTML = "";
            provincesSelect.innerHTML += `<option value="">${await jsLocalizer("Select")}</option>`;

            Array.from(response.data).forEach(province => {
                provincesSelect.innerHTML += `<option value="${province.id}">${province.title}</option>`;
            })
        }
    }
    else {
        alert(response.message);
    }
}

async function updateCities(provinceId, citiesSelect) {

    if (provinceId == "" && citiesSelect) {
        citiesSelect.innerHTML = "";
        citiesSelect.innerHTML += `<option value="">${await jsLocalizer("Select")}</option>`;
        return;
    }

    var responseText = await fetch("/cities/" + provinceId);

    var response = await responseText.json();

    if (response.isSuccess == true) {
        if (citiesSelect) {
            citiesSelect.innerHTML = "";
            citiesSelect.innerHTML += `<option value="">${await jsLocalizer("Select")}</option>`;

            Array.from(response.data).forEach(city => {
                citiesSelect.innerHTML += `<option value="${city.id}">${city.title}</option>`;
            })
        }
    }
    else {
        alert(response.message);
    }
}