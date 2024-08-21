$(document).ready(function () {
    $('#CountryDropdown').change(function () {
        var countryId = $(this).val();
        if (countryId) {
            $.getJSON('@Url.Action("GetStates", "EmployeeMasters")', { countryId: countryId }, function (data) {
                var stateDropdown = $('#StateDropdown');
                stateDropdown.empty();
                stateDropdown.append('<option value="">Select State</option>');
                $.each(data, function (index, state) {
                    stateDropdown.append('<option value="' + state.rowId + '">' + state.stateName + '</option>');
                });
            });
        } else {
            $('#StateDropdown').empty().append('<option value="">Select State</option>');
            $('#CityDropdown').empty().append('<option value="">Select City</option>');
        }
    });

    $('#StateDropdown').change(function () {
        var stateId = $(this).val();
        if (stateId) {
            $.getJSON('@Url.Action("GetCities", "EmployeeMasters")', { stateId: stateId }, function (data) {
                var cityDropdown = $('#CityDropdown');
                cityDropdown.empty();
                cityDropdown.append('<option value="">Select City</option>');
                $.each(data, function (index, city) {
                    cityDropdown.append('<option value="' + city.rowId + '">' + city.cityName + '</option>');
                });
            });
        } else {
            $('#CityDropdown').empty().append('<option value="">Select City</option>');
        }
    });
});

function fileValidation() {
    var fileInput = document.getElementById('file');

    var filePath = String(fileInput.value);

    const fsize = fileInput.files[0].size;
    const fileKb = Math.round((fsize / 1024));

    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;


    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type, Please select .jpg ot .png format.');
        fileInput.value = '';
        return false;
    }
    else {
        if (fileKb > 0 && fileKb < 200) {
            // Image preview
            if (fileInput.files && fileInput.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById(
                        'imagePreview').src = e.target.result;
                };

                reader.readAsDataURL(fileInput.files[0]);
            }
        }
        else {
            alert('Please select file size less than 200 kb');
            fileInput.value = '';
            return false;
        }

    }
}
