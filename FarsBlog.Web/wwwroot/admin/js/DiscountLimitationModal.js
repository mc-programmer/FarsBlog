function ShowCreateDiscountLimitationModal(discountId) {
    statusChanged = false;
    $.ajax({
        url: `/Admin/Discount/CreateDiscountLimitation?discountId=${discountId}`,
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer("CreateDiscountLimitation"));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
    return false;
}

function ShowUpdateDiscountLimitationModal(id) {
    statusChanged = false;
    $.ajax({
        url: `/Admin/Discount/UpdateDiscountLimitation/${id}`,
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer("UpdateDiscountLimitation"));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
    return false;
}

function ShowSearchUserModal() {
    $.ajax({
        url: "/Admin/Discount/FilterUsers",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("UsersList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);
                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

function ShowSearchGameModal() {
    $.ajax({
        url: "/Admin/Discount/FilterGames",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("GamesList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);
                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

function ShowSearchProductModal() {
    $.ajax({
        url: "/Admin/Discount/FilterProducts",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("ProductList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);
                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

function ShowSearchManufacturerModal() {
    $.ajax({
        url: "/Admin/Discount/FilterManufacturers",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("ManufacturersList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);
                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            ShowMessage(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

var selectedUserIds = [];
var selectedGameIds = [];
var selectedProductIds = [];
var selectedManufacturerIds = [];

function SelectUsers() {
    Array.from(document.querySelectorAll("input[name='userId']:checked"))
        .forEach(input => {
            selectedUserIds.push(input.value);
            $("#users tbody").append(`
                                        <tr for="${input.value}">
                                            <td>${input.getAttribute("username")}</td>
                                            <td>
                                                <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteUser('${input.value}')">
                                                    <i class="bx bx-trash me-1"></i>
                                                </a>
                                            </td>
                                        </tr>
                                    `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteUser(userId) {
    document.querySelector(`#users tbody tr[for='${userId}']`).remove();
    selectedUserIds = selectedUserIds.filter(id => id != userId);
}

function SelectGames() {
    Array.from(document.querySelectorAll("input[name='gameId']:checked"))
        .forEach(input => {
            selectedGameIds.push(input.value);
            $("#games tbody").append(`
                                        <tr for="${input.value}">
                                            <td>${input.getAttribute("title")}</td>
                                            <td>
                                                <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteGame('${input.value}')">
                                                    <i class="bx bx-trash me-1"></i>
                                                </a>
                                            </td>
                                        </tr>
                                    `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteGame(gameId) {
    document.querySelector(`#games tbody tr[for='${gameId}']`).remove();
    selectedGameIds = selectedGameIds.filter(id => id != gameId);
}

function SelectProducts() {
    Array.from(document.querySelectorAll("input[name='productId']:checked"))
        .forEach(input => {
            selectedProductIds.push(input.value);
            $("#products tbody").append(`
                                            <tr for="${input.value}">
                                                <td>${input.getAttribute("title")}</td>
                                                <td>
                                                    <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteProduct('${input.value}')">
                                                        <i class="bx bx-trash me-1"></i>
                                                    </a>
                                                </td>
                                            </tr>
                                        `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteProduct(productId) {
    document.querySelector(`#products tbody tr[for='${productId}']`).remove();
    selectedProductIds = selectedProductIds.filter(id => id != productId);
}

function SelectManufacturers() {
    Array.from(document.querySelectorAll("input[name='manufacturerId']:checked"))
        .forEach(input => {
            selectedManufacturerIds.push(input.value);
            $("#manufacturers tbody").append(`
                                                <tr for="${input.value}">
                                                    <td>${input.getAttribute("title")}</td>
                                                    <td>
                                                        <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteManufacturer('${input.value}')">
                                                            <i class="bx bx-trash me-1"></i>
                                                        </a>
                                                    </td>
                                                </tr>
                                            `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteManufacturer(manufacturerId) {
    document.querySelector(`#manufacturers tbody tr[for='${manufacturerId}']`).remove();
    selectedManufacturerIds = selectedManufacturerIds.filter(id => id != manufacturerId);
}

function FillPartialPageId(pageId) {
    $("#PartialPageId").val(pageId);
    $(`#search-form`).submit();
}

try {
    document.getElementById("discount-limitation-form").addEventListener("submit", (e) => {
        e.preventDefault();
        e.stopPropagation();

        if (e.target["UsageCount.Count"]) {
            var usageCountValue = e.target["UsageCount.Count"].value;
            if (usageCountValue === null || usageCountValue === undefined || usageCountValue.trim() == '') {
                e.target["UsageCount.Count"].value = 0;
            }
        }

        var counter = 0;
        selectedUserIds.forEach(userId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Users[${counter}].UserId`);
            input.setAttribute("value", userId);
            e.target.appendChild(input);
            counter++;
        });

        counter = 0;
        selectedGameIds.forEach(gameId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Games[${counter}].GameId`);
            input.setAttribute("value", gameId);
            e.target.appendChild(input);
            counter++;
        });

        counter = 0;
        selectedProductIds.forEach(productId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Products[${counter}].ProductId`);
            input.setAttribute("value", productId);
            e.target.appendChild(input);
            counter++;
        });

        counter = 0;
        selectedManufacturerIds.forEach(manufacturerId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Manufacturers[${counter}].ManufacturerId`);
            input.setAttribute("value", manufacturerId);
            e.target.appendChild(input);
            counter++;
        });

        e.target.submit();
    });
} catch (e) {
    console.log(e);
}