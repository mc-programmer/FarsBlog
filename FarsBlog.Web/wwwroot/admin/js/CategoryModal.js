var currentStep = -1;

function ShowSearchProductCategoryModal(step) {
    statusChanged = false;
    $.ajax({
        url: `/Admin/Product/FilterProductCategory`,
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            currentStep = step;
            close_waiting();
            $("#MediumModalTitle").html(await jsLocalizer("ProductCategories"));
            $("#MediumModalBody").html(response);
            $("#MediumModal").modal("show");
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
    return false;
}

function FillPartialFilterProductCategory(pageId) {
    $("#PartialFilterProductCategoryPageId").val(pageId);
    $(`#filterProductCategory-Form`).submit();
}

function SelectProductCategory(categoryId, title) {
    $(`#ProductCategory-Input-${currentStep}`).val(categoryId).trigger("change");
    $(`#ProductCategory-Display-${currentStep}`).val(title).trigger("change");

    $("#MediumModal").modal("hide");
}