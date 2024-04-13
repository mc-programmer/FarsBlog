$(document).ready(function () {

    function checkAll(id) {
        if ($("input[name='ProductIds'], input[name='GameIds']").length == $("input[name='ProductIds']:checked, input[name='GameIds']:checked").length) {
            var checkboxes = document.querySelectorAll('input[type="checkbox"]');
            checkboxes.forEach(function (checkbox) {
                checkbox.checked = true;
            });
        }

        else {
            if (id != null) {

                $(`input[id='SelectAll'],input[dataId*=${id}]`).prop("checked", false);
            }
        }
    }

    function expandAll() {
        $(".accordion-button").removeClass("collapsed");
        $(".accordion-collapse").addClass("show");
        $(".accordion-button, .accordion-collapse").attr("aria-expanded", true);
        $(".accordion-collapse").css("height", "auto");
    }

    function collapseAll() {
        $(".accordion-button").addClass("collapsed");
        $(".accordion-collapse").removeClass("show");
        $(".accordion-button, .accordion-collapse").attr("aria-expanded", false);
        $(".accordion-collapse").css("height", "0");
    }

    function setCheckboxState(id, state) {
        var input = $(`input[dataId*=${id}]`);
        input.prop("checked", state);
    }

    function onSelectAllClick() {
        if (this.checked) {
            expandAll();
            $("input[name=ProductIds],input[name=GameIds],input[id*='ManufactorerId']").each(function () {
                setCheckboxState($(this).attr("dataId"), true);
            });
        } else {
            collapseAll();
            $("input[name=ProductIds],input[name=GameIds],input[id*='ManufactorerId']").each(function () {
                setCheckboxState($(this).attr("dataId"), false);
            });
        }
    }

    checkAll();

    $("#SelectAll").on("click", onSelectAllClick);

    $("input[name='ProductIds'], input[name='GameIds'],input[id*='ManufactorerId']").on("change", function (e) {

        var elementChecked = e.target.checked;
        var element = e.target;


        if (element.getAttribute("dataId").includes("ManufactorerId")) {

            var newId = element.getAttribute("dataId").substring(element.getAttribute("dataId").indexOf("-") + 1, element.getAttribute("dataId").lastIndexOf("-"));
            var inputs = $(`input[dataId*='${newId}']`);

            inputs.splice(0, 1);

            inputs.prop('checked', true);

            if (elementChecked) {

                for (var i = 0; i < inputs.length; i++) {

                    inputs[i].checked = elementChecked;
                }
            }

            if (!elementChecked) {

                for (var i = 0; i < inputs.length; i++) {

                    inputs[i].checked = elementChecked;
                }
            }
            checkAll();
        }

        if (element.getAttribute("dataId").includes("GameId")) {

            let gameId = element.getAttribute("dataId");

            let startIndex = gameId.indexOf("-") + 1;

            let endIndex = gameId.lastIndexOf("-");

            var newId = gameId.substring(startIndex, endIndex);

            var inputs = $(`input[dataId*='ProductId-${newId}']`);

            var manufactorerId = "ManufactorerId-" + gameId.split("-")[2];

            inputs.prop('checked', true);

            if (elementChecked) {

                for (var i = 0; i < inputs.length; i++) {

                    inputs[i].checked = elementChecked;
                }
                checkAll();
            }

            if (!elementChecked) {
                for (var i = 0; i < inputs.length; i++) {

                    inputs[i].checked = false;
                }
                checkAll(manufactorerId);
            }

        }

        if (element.getAttribute("dataId").includes("ProductId")) {

            let productId = element.getAttribute("dataId");

            let startIndex = productId.indexOf("-") + 1;

            let endIndex = productId.lastIndexOf("-");

            var newId = productId.substring(startIndex, endIndex);

            var inputs = $(`input[dataId*='ProductId-${newId}']`);

            var manufactorerId = "ManufactorerId-" + productId.split("-")[2];

            checkAll(manufactorerId);

        }
    });

});




