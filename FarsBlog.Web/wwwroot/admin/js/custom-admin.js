//display loading
function displayWaitMe(selector = 'body') {
    $(selector).waitMe({
        effect: 'bounce',
        text: '',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000'
    });
}

// hide loading
function hideWaitMe(selector = 'body') {
    $(selector).waitMe('hide');
}

//display by swal
function notifAjaxResult(result) {
    if (result.isSuccess == true || result.isSuccess == "true") {
        Swal.fire({
            icon: 'success',
            title: 'عملیات موفق!'
        })
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'عملیات شکست خورد!'
        })
    }

}

// fills pageId id for paging
function FillPageId(pageId) {
    $("#pageId").val(pageId);
    $("#search-form").submit();
}


// Loads Modal html to Base Modal partial
function loadModalElements(url, modalSize, displayWaiting = "body") {
    try {
        $.ajax({
            type: 'GET',
            url: url,
            contentType: false,
            processData: false,
            beforeSend: function () {
                displayWaitMe(displayWaiting);
            },
            success: function (res) {
                let modalSizeElemenet = document.getElementById('modalSize');
                modalSizeElemenet.classList.add(modalSize);

                $('#modal .modal-content').html(res);

                $('#modal-form').data('validator', null);
                $.validator.unobtrusive.parse('#modal-form');

                hideWaitMe(displayWaiting);
                $('#modal').modal('show');
            },
            error: function (err) {
                hideWaitMe(displayWaiting);
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
}

// will called when modal upsert successed (just shows result)
function ModalUpsertResult(result) {
    hideWaitMe('#modal-content');
    if (result.isSuccess === true) {
        $("#modal").modal("hide");

        $("#search-form").submit();

        toastr.success(result.message, 'عملیات موفق');
    }
    else {
        toastr.error(result.message, 'مشکلی پیش آمده');
    }
}

async function ModalDeleteConfirm(url) {
    Swal.fire({
        html: `<b>ایا از حذف اطمینان دارید؟</b>`,
        showDenyButton: true,
        icon: 'question',
        confirmButtonText: 'بله',
        denyButtonText: 'خیر'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "post",
                contentType: false,
                processData: false,
                beforeSend: function () {
                    displayWaitMe("#modal-content");
                },
                success: function (response) {
                    hideWaitMe("#modal-content");
                    if (response.isSuccess) {
                        $("#modal").modal("hide");

                        if (result.listToReload) {
                            $(result.listToReload).load(location.href + " " + result.listToReload);
                        }

                        Swal.fire({
                            title: "حذف شد!",
                            icon: "success",
                            button: "باشه"
                        });
                    }
                    else {
                        Swal.fire({
                            title: "خطا",
                            text: "عملیات ناموفق بود!",
                            icon: "error",
                            button: "باشه"
                        });
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            });
        }
    });
}



async function ModalRecoverConfirm(url) {
    Swal.fire({
        html: `<b>از بازسازی اطمینان دارید؟</b>`,
        showDenyButton: true,
        icon: 'warning',
        confirmButtonText: 'بازیابی',
        denyButtonText: 'لغو'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "post",
                contentType: false,
                processData: false,
                beforeSend: function () {
                    displayWaitMe("#modal-content");
                },
                success: function (response) {
                    hideWaitMe("#modal-content");
                    if (response.isSuccess) {
                        $("#modal").modal("hide");

                        if (result.listToReload) {
                            $(result.listToReload).load(location.href + " " + result.listToReload);
                        }

                        Swal.fire({
                            title: "بازیابی شد!",
                            icon: "success",
                            button: "باشه"
                        });
                    }
                    else {
                        Swal.fire({
                            title: "خطا",
                            text: "عملیات ناموفق بود!",
                            icon: "error",
                            button: "باشه"
                        });
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            });
        }
    });
}



