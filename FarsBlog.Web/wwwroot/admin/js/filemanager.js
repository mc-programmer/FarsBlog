const locale = true ? "en-US" : "fa-IR";
kendo.culture(locale);

function jsLocalizer(resourceName) {

    var request = new XMLHttpRequest();

    request.open("GET", `/Home/GetResource?resourceName=${resourceName}`, false);
    request.send();

    return request.responseText;
}

const previewTemplate = kendo.template(`
        <div class="k-file-info">
            <div class="k-file-preview">
                #if (selection[0].extension === ".jpg" || selection[0].extension === ".png" || selection[0].extension === ".gif") {#
                    <img class="img-fluid w-50" src="/shared/UserFiles/Folders/#=selection[0].path#" alt="#=selection[0].name#" />
                #} else {#
                    <span class="k-icon k-i-#=kendo.getFileGroup(selection[0].extension, true)#"></span>
                #}#
            </div>
            <h5>#=selection[0].name#</h5>
            #if(metaFields){#
                <div class="text-start mt-3">
                    #for(var i = 0; i < metaFields.length; i+=1){#
                        #var field = metaFields[i]#
                        <div class="row">
                            <div class="col-12">
                                <label class="form-label">#=messages[field]#: </label>
                                <span>
                                    #if(field == "size"){#
                                        #=kendo.getFileSizeMessage(selection[0][field])#
                                    #} else if(selection[0][field] instanceof Date) {#
                                        #=new Date(kendo.toString(selection[0][field], "yyyy/MM/dd")).toLocaleString('${locale}', { year: 'numeric', month: '2-digit', day: '2-digit' })#
                                    #} else if(field == "extension") {#
                                        #=kendo.getFileGroup(selection[0].extension)#
                                    #} else {#
                                        #=selection[0][field]#
                                    #}#
                                </span>
                            </div>
                        </div>
                    # } #
                </div>
            #}#
        </div>
`);

$("#filemanager").kendoFileManager({
    dataSource: {
        schema: kendo.data.schemas.filemanager,
        transport: {
            read: {
                url: "/Admin/FileManager/Read",
                method: "POST"
            },
            create: {
                url: "/Admin/FileManager/Create",
                method: "POST"
            },
            update: {
                url: "/Admin/FileManager/Update",
                method: "POST"
            },
            destroy: {
                url: "/Admin/FileManager/Destroy",
                method: "POST"
            }
        }
    },
    previewPane: {
        singleFileTemplate: previewTemplate,
        enabled: true,
        metaFields: ["extension", "size", "created", "modified"],
    },
    uploadUrl: "/Admin/FileManager/Upload",
    toolbar: {
        items: [
            { name: "createFolder" },
            { name: "upload" },
            { name: "sortDirection" },
            { name: "sortField" },
            { name: "spacer" },
            { name: "details" },
            { name: "search" },
        ]
    },
    contextMenu: {
        items: [
            { name: "rename", text: jsLocalizer("Rename"), command: "RenameCommand" },
            { name: "delete", text: jsLocalizer("Delete"), command: "DeleteCommand" }
        ]
    },
    open: (e) => {
        if (e.entry.isDirectory != true) {
            selectFile(e.entry.path)
        }
    },
    draggable: true,
    resizable: true,
    messages: {
        toolbar: {
            createFolder: jsLocalizer("NewFolder"),
            upload: jsLocalizer("Upload"),
            sortDirection: jsLocalizer("SortDirection"),
            sortDirectionAsc: jsLocalizer("SortDirectionAsc"),
            sortDirectionDesc: jsLocalizer("SortDirectionDesc"),
            sortField: jsLocalizer("SortField"),
            nameField: jsLocalizer("NameField"),
            sizeField: jsLocalizer("SizeField"),
            typeField: jsLocalizer("TypeField"),
            dateModifiedField: jsLocalizer("DateModifiedField"),
            dateCreatedField: jsLocalizer("DateCreatedField"),
            listView: jsLocalizer("ListView"),
            gridView: jsLocalizer("GridView"),
            search: jsLocalizer("Search"),
            details: jsLocalizer("Details"),
            detailsChecked: jsLocalizer("DetailsChecked"),
            detailsUnchecked: jsLocalizer("DetailsUnchecked"),
            delete: jsLocalizer("Delete"),
            rename: jsLocalizer("Rename")
        },
        views: {
            nameField: jsLocalizer("Name"),
            sizeField: jsLocalizer("FileSize"),
            typeField: jsLocalizer("FileType"),
            dateModifiedField: jsLocalizer("LastModified"),
            dateCreatedField: jsLocalizer("DateCreated"),
            items: jsLocalizer("Items")
        },
        dialogs: {
            upload: {
                title: jsLocalizer("UploadFiles"),
                clear: jsLocalizer("ClearList"),
                done: jsLocalizer("Done")
            },
            moveConfirm: {
                title: jsLocalizer("Confirmation"),
                content: "<p style='text-align: center;'>" + jsLocalizer("MoveOrCopy") + "</p>",
                okText: jsLocalizer("Copy"),
                cancel: jsLocalizer("Move"),
                close: jsLocalizer("Close")
            },
            deleteConfirm: {
                title: jsLocalizer("Confirmation"),
                content: "<p style='text-align: center;'>" + jsLocalizer("AreYouSureToDelete") + "<br/>" + jsLocalizer("ThisActionIsNotReversible") + "</p>",
                okText: jsLocalizer("Delete"),
                cancel: jsLocalizer("Cancel"),
                close: jsLocalizer("Close")
            },
            renamePrompt: {
                title: jsLocalizer("Message"),
                content: "<p style='text-align: center;'>" + jsLocalizer("EnterNewName") + "</p>",
                okText: jsLocalizer("Rename"),
                cancel: jsLocalizer("Cancel"),
                close: jsLocalizer("Close")
            },
            contextMenu: {
                rename: jsLocalizer("Rename")
            }

        },
        previewPane: {
            noFileSelected: jsLocalizer("NoFileSelected"),
            extension: jsLocalizer("FileType"),
            size: jsLocalizer("Size"),
            created: jsLocalizer("CreatedDate"),
            modified: jsLocalizer("ModifiedDate"),
            items: jsLocalizer("Items"),
            rename: jsLocalizer("Rename")
        }
    }
});

var filemanager = $("#filemanager").getKendoFileManager();

filemanager.executeCommand({ command: "TogglePaneCommand", options: { type: "preview" } });
$("#details-toggle").getKendoSwitch().toggle();



function selectFile(path) {

    if (window.opener.selectedFileInput != null) {
        window.opener.selectedFileInput.value = path;
        var imgTag = window.opener.selectedFileInput.parentElement.querySelector("img");
        if (imgTag) {
            imgTag.src = "/shared/UserFiles/Folders/" + path;
        }
        window.close();
    }
}