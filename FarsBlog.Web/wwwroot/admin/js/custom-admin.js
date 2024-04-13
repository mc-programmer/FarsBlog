//#region Waiting

function open_waiting(selector = 'body') {
    $.blockUI({
        message: '<div class="spinner-border text-white" role="status"></div>',

        css: {
            backgroundColor: 'transparent',
            border: '0'
        },
        overlayCSS: {
            opacity: 0.5
        }
    });
}

function close_waiting(selector = 'body') {
    $.unblockUI();
}

//#endregion 

async function ShowMessage(title, message, icon) {
    Swal.fire({
        title: await jsLocalizer(title),
        icon: icon,
        text: await jsLocalizer(message),
        confirmButtonText: await jsLocalizer("Ok"),
    });
}

async function DeleteConfirm(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("ConfirmDeleteMessage")}</b>`,
        showDenyButton: true,
        icon: 'question',
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}

async function ConfirmCooperating(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("ConfirmCooperatingMessage")}</b>`,
        showDenyButton: true,
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}

async function confirmBankAccountCard(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("ConfirmBankAccoutnCardMessage")}</b>`,
        showDenyButton: true,
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}

function kamaDatePickerCustom(selectorId) {
    kamaDatepicker(selectorId, {
        twodigit: true,
        closeAfterSelect: true,
        forceFarsiDigits: true,
        markToday: true,
        markHolidays: true,
        highlightSelectedDay: true,
        sync: true,
        gotoToday: true,
        buttonsColor: "#6c757d",
        nextButtonIcon: "fa-solid fa-chevron-right",
        previousButtonIcon: "fa-solid fa-chevron-left"
    });
}

//#region change order status

const changeOrderStatus = (url, status) => {

    $.ajax({
        url: url,
        data: { orderId: status },
    });
}

//#endregion

function showOrderItem(url) {
    $.ajax({
        type: 'get',
        url: url,
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer('ViewTitle.Admin.OrderItems'));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal('show');
        },
        error: async function (response) {
            close_waiting();
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                confirmButtonText: await jsLocalizer("Ok"),
            });
        }
    });
}

function openInNewTab(event) {
    event.preventDefault();
    event.stopPropagation();

    if (event.target.classList.contains('file-upload')) {
        selectedFileInput = document.getElementById(event.target.parentElement.getAttribute("for"));
    }

    var url = event.currentTarget.getAttribute("href"); // Get the URL from the href attribute

    //if (selectedFileInput != null) {
    //    url += `?targetId="${selectedFileInput.id}"`
    //}
    const windowFeatures = 'width=1200,height=600,modal=yes';
    window.open(url, '_blank', windowFeatures); // Open the URL in a new tab/window
}


window.addEventListener("load", () => {
    var input = document.querySelector("#Price");

    if (input) {

        if (input.value.length > 0) {
            input.value = parseFloat(convertToEnglish(input.value.replaceAll(",", ""))) / usdRate;
            //showConvertedPrice();
        }

        input.addEventListener("keyup", () => {
            //showConvertedPrice();
        })

        input.form.addEventListener("submit", (e) => {
            e.preventDefault();
            e.stopPropagation();
            //input.value = convertToToman(input.value);
            input.value = convertToEnglish(input.value.replaceAll(",", ""))
            e.target.submit();
        })
    }

})

//var toman = '';

//jsLocalizer("Toman").then(res => toman = res);
//function showConvertedPrice() {
//    let priceInput = document.querySelector("#Price");

//    priceInput.parentElement
//        .querySelector("span")
//        .innerHTML = convertToToman(priceInput.value) + ' ' + toman;
//}

//function convertToToman(value) {
//    value = convertToEnglish(value.replaceAll(",", ""));
//    value = parseInt(parseFloat(value) * usdRate);
//    var rgx = /(\d+)(\d{3})/;
//    while (rgx.test(value)) {
//        value = value.toString().replace(rgx, '$1' + ',' + '$2');
//    }

//    value = value.toString().replace("NaN", "0")

//    return value;
//}


function fillImageSrc(element) {
    if (element) {
        document.getElementById("Image-name").value = element.getAttribute("src");
    }
}

//create file and assign them to input with type file
function fileCreator(imgTagId, fileInputTagId, imageNamehiddenInputId) {
    var imagesrc = document.getElementById(imageNamehiddenInputId).value;
    if (imagesrc !== '' && imagesrc !== null) {

        var imgTag = document.getElementById(imgTagId);

        if (imgTag) {
            imgTag.src = imagesrc;
        }

        var inputFile = document.getElementById(fileInputTagId);

        if (inputFile) {

            var contentType = imagesrc.split(';')[0].split(':')[1];
            var fileExtention = imagesrc.split(';')[0].split('/')[1];

            if (contentType.includes('svg')) {
                fileExtention = 'svg';
            }
            if (contentType.includes('vnd.microsoft.icon')) {
                fileExtention = 'ico';
            }

            fetch(imagesrc)
                .then(response => response.blob())
                .then(blob => {

                    const file = new File([blob], `file.${fileExtention}`, { type: contentType });

                    const fileList = new DataTransfer();
                    fileList.items.add(file);

                    inputFile.files = fileList.files;

                })
                .catch(error => {
                    console.error('Error fetching image:', error);
                });

        }
    }
}


//function hideDatePicker() {

//    document.querySelectorAll("div[class='datepicker-container']").forEach((e) => {
//        e.classList = 'datepicker-container pwt-hide';
//    });
//}

//window.addEventListener('load', () => {
//    //var tdElements = document.querySelectorAll("td[data-unix]");

//    //tdElements.forEach((element) => {
//    //    element.addEventListener('click', hideDatePicker());
//    //});
//    //document.getElementById("BirthDate").addEventListener('input', console.log('changed'))

////    document.getElementById("BirthDate").addEventListener('change', hideDatePicker());
//});


// back button
function backUrl() {
    var stringifiedRequestsHistory = getCookie("requestsHistory");
    var requestsHistory = JSON.parse(stringifiedRequestsHistory);

    // Remove the current URL from the requestsHistory list
    requestsHistory = requestsHistory.filter(function (url) {
        return url !== window.location.href && url !== previousUrl;
    });

    // Set the updated requestsHistory list in the cookie
    document.cookie = "requestsHistory=" + JSON.stringify(requestsHistory) + "; path=/Admin;";

    // Redirect to the previous URL
    window.location.href = previousUrl;
}

var previousUrl = '';
if (window.location.href !== null && window.location.href !== "") {
    var stringifiedRequestsHistory = getCookie("requestsHistory");
    var requestsHistory = [];
    try {
        var requestsHistory = JSON.parse(stringifiedRequestsHistory);
        previousUrl = requestsHistory[requestsHistory.length - 1];
        if (requestsHistory[requestsHistory.length - 1] === window.location.href) {
            previousUrl = requestsHistory[requestsHistory.length - 2];
        }
    }
    catch {
        console.log('has error')
    }
    if (requestsHistory[requestsHistory.length - 1] !== window.location.href) {
        requestsHistory.push(window.location.href);
        document.cookie = "requestsHistory=" + JSON.stringify(requestsHistory) + "; path=/Admin;";
    }
}

function getCookie(name) {
    var cookieName = name + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(";");

    for (var i = 0; i < cookieArray.length; i++) {
        var cookieValue = cookieArray[i];
        while (cookieValue.charAt(0) === " ") {
            cookieValue = cookieValue.substring(1);
        }

        if (cookieValue.indexOf(cookieName) === 0) {
            return cookieValue.substring(cookieName.length, cookieValue.length);
        }
    }

    return "";
}

// withdraw request
function rejectWithdrawRequest(id) {
    document.getElementById("toRejectWithdrawRequestId").value = id;

    $("#rejectModal").modal("show");
}

function acceptWithdrawRequest(id) {
    document.getElementById("toAcceptWithdrawRequestId").value = id;

    $("#acceptModal").modal("show");
}


function showBankAccountCard(id) {
    $.ajax({
        type: 'get',
        url: `/admin/getbankaccountcard/${id}`,
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer('DestinationBankAccountCard'));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal('show');
        },
        error: async function (response) {
            close_waiting();
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                confirmButtonText: await jsLocalizer("Ok"),
            });
        }
    });
}

function showRejectModal(url) {
    $.ajax({
        type: 'get',
        url: url,
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer('ViewTitle.Admin.BankCardNotApproved'));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal('show');
        },
        error: async function (response) {
            close_waiting();
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                confirmButtonText: await jsLocalizer("Ok"),
            });
        }
    });
}


//#region Select teachers

var TeacherArray = [];

function createGuid() {
    function s4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }

    return (s4().replace("-", "") + s4().replace("-", "") + s4().replace("-", "") + s4().replace("-", ""));
}

function checkNotEmpty(item) {
    if (item === "" || item === null || item === undefined || !item.length) {
        return true;
    }

    return false;
}

//#region Teacher Search

$("#Teacher-Display").on("change", function () {
    var TeacherId = $("#Teacher-Input").val();
    var TeacherTitle = $("#Teacher-Display").val();

    if (checkNotEmpty(TeacherId) || checkNotEmpty(TeacherTitle)) {
        return;
    } 

    if ($(`#TeacherTableBody tr`).length >= 1) {
        ShowMessage("Notification", "CantChooseMoreThanOneTeacher", "warning");
        return;
    }

    //if ($(`#Teacher_${TeacherId}`).length) {
    //    ShowMessage("Notification", "TeacherAlreadySelected", "warning");
    //    return;
    //}

    var Teacher = {
        id: TeacherId,
        title: TeacherTitle
    }

    TeacherArray.push(Teacher);

    $("#TeacherTableDiv").removeClass("display-none");

    $("#TeacherTableBody").append(TeacherRowHtmlCode(Teacher));

    updateTeachersJsonInput();

});

function TeacherRowHtmlCode(Teacher) {
    var code = `
                                <tr class="tc vm" id="Teacher_${Teacher.id}" name="AddedTeacherRow">
                                    <td class="tc vm ellipsis-style" style="max-width: 0">
                                        ${Teacher.title}
                                    </td>
                                    <td class="tc vm">
                                        <a type="button" onclick="deleteTeacherRow('${Teacher.id}')" ><i class="fa-solid fa-trash-can text-danger"></i></a>
                                    </td>
                                </tr>
                            `;

    return code;
}

function updateTeachersJsonInput() {
    var teacherJson = JSON.stringify(TeacherArray);

    $("#SelectedTeacherJson").val(teacherJson);
}

function deleteTeacherRow(TeacherRowId, url = null) {
    let dicountId;

    if ($(`#Teacher_${TeacherRowId}`).length) {
        let discount = document.querySelector('#DiscountId');
        if (url !== null && discount) {
            discountId = document.querySelector('#DiscountId').value;
            $.ajax({
                url: url,
                type: "Post",
                data: { "discountId": discountId, "TeacherId": TeacherRowId },
                beforeSend: function () {
                    open_waiting();
                },
                success: function (response) {
                    close_waiting();
                    if (response.status === "Success") {
                        $(`#Teacher_${TeacherRowId}`).remove();
                        ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
                    }

                    if (response.status === "Error") {
                        ShowMessage("خطا", "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .", "error");
                    }
                },
                error: function () {
                    close_waiting();
                    ShowMessage("خطا", "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .", "error");
                }
            });
        }
        else {
            $(`#Teacher_${TeacherRowId}`).remove();
        }
    }
    debugger;

    if ($('tr[name ="AddedTeacherRow"]').length === 0) {
        $("#TeacherTableDiv").addClass("display-none");
    }
    var Teacher = TeacherArray.find(function (object) {
        return object.id.toString() === TeacherRowId.toString();
    });

    if (Teacher !== undefined) {
        var filteredArray = TeacherArray.filter(function (value, index, arr) {
            return value.id.toString() !== Teacher.id.toString();
        });
        TeacherArray = filteredArray;
    }
    updateTeachersJsonInput();
}

//#endregion

//#region Document Ready
document.addEventListener("DOMContentLoaded", () => {
    var teachersJson = $("#SelectedTeacherJson").val();
    if (!checkNotEmpty(teachersJson)) {
        var selectedTeachers = JSON.parse(teachersJson);

        TeacherArray = selectedTeachers;
        $("#TeacherTableDiv").removeClass("display-none");

        for (var item of selectedTeachers) {
            $("#TeacherTableBody").append(TeacherRowHtmlCode(item));
        }
    }
})
//#endregion