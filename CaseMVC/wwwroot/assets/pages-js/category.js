function subCatCheckToggle(operation) {
    if (operation === "add") {
        if ($("#subCatCheck").is(":checked") == true)
            $("#upChooseMainCat").removeClass("d-none");
        else
            $("#upChooseMainCat").addClass("d-none");
    }
    else if ("update") {
        if ($("#upSubCatCheck").is(":checked") == true)
            $("#upChooseMainCat").removeClass("d-none");
        else
            $("#upChooseMainCat").addClass("d-none");
    }

}

function createCategory() {
    var categoryName = $("#categoryName").val();
    var desc = $("#desc").val();
    var line = parseInt($("#line").val());
    var mainCategoryId = 0;
    if ($("#subCatCheck").is(":checked") == true)
        var mainCategoryId = parseInt($("#mainCategoryId").val());

    $.ajax({
        url: '@Url.Action("CreateCategory", "Category")',
        type: 'POST',
        dataType: 'json',
        data: {
            "_categoryName": categoryName,
            "_desc": desc,
            "_mainCategoryId": mainCategoryId,
            "_line": line
        },
        success: function (data) {
            if (data) {
                debugger;
                $(function () {
                    $('#createModal').modal('toggle');
                });
                Swal.fire({
                    title: "Başarılı!",
                    text: "Kategori başarıyla eklendi.",
                    icon: "success"
                });
                updateTable();
            }
            else {
                $(function () {
                    $('#createModal').modal('toggle');
                });
                Swal.fire({
                    title: "Hata!",
                    text: "Bir şeyler ters gitti lütfen tekrar deneyiniz!",
                    icon: "error"
                });

                return;
            }
        }
    });
}

function deleteCategory(_id) {
    Swal.fire({
        title: "Seçtiğiniz kategori silinecektir. Onaylıyor musunuz?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Evet, sil!",
        cancelButtonText: "Kapat"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '@Url.Action("DeleteCategory", "Category")',
                type: 'POST',
                dataType: 'json',
                data: {
                    "_id": _id
                },
                success: function (data) {
                    if (data) {
                        Swal.fire({
                            title: "Silindi!",
                            text: "Kategori başarıyla silindi.",
                            icon: "success"
                        });
                        updateTable();
                    }
                    else {
                        $(function () {
                            $('#modal').modal('toggle');
                        });
                        Swal.fire({
                            title: "Hata!",
                            text: "Bir şeyler ters gitti lütfen tekrar deneyiniz!",
                            icon: "error"
                        });

                        return;
                    }
                }
            });
        }
    });

}

function getCategoryById(_id) {
    $.ajax({
        url: '@Url.Action("GetCategoryById", "Category")',
        type: 'POST',
        dataType: 'json',
        data: {
            "_id": _id
        },
        success: function (data) {
            if (data) {
                $("#id").val(data.id);
                $("#upCategoryName").val(data.name);
                $("#upDesc").val(data.description);
            }
            else {
                $(function () {
                    $('#modal').modal('toggle');
                });
                Swal.fire({
                    title: "Hata!",
                    text: "Bir şeyler ters gitti lütfen tekrar deneyiniz!",
                    icon: "error"
                });

                return;
            }
        }
    });
}

function updateCategory() {
    var id = $("#id").val();
    var name = $("#upCategoryName").val();
    var desc = $("#upDesc").val();
    var line = parseInt($("#upLine").val());
    var mainCategoryId = 0;
    if ($("#upSubCatCheck").is(":checked") == true)
        var mainCategoryId = parseInt($("#upMainCategoryId").val());

    $.ajax({
        url: '@Url.Action("UpdateCategory", "Category")',
        type: 'POST',
        dataType: 'json',
        data: {
            "_id": id,
            "_categoryName": name,
            "_desc": desc,
            "_mainCategoryId": mainCategoryId,
            "_line": line

        },
        success: function (data) {
            if (data) {
                $(function () {
                    $('#updateModal').modal('toggle');
                });
                Swal.fire({
                    title: "Başarılı!",
                    text: "Kategori başarıyla güncellendi.",
                    icon: "success"
                });
                updateTable();
            }
            else {
                $(function () {
                    $('#updateModal').modal('toggle');
                });
                Swal.fire({
                    title: "Hata!",
                    text: "Bir şeyler ters gitti lütfen tekrar deneyiniz!",
                    icon: "error"
                });

                return;
            }
        }
    });
}

function updateTable() {
    $.ajax({
        url: '@Url.Action("UpdateTable", "Category")',
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data) {
                $("#categoryTableBody").empty();
                var trHtml = "";
                for (var item of data) {
                    trHtml += '<tr>';
                    trHtml += '    <td class="d-none"> ' + item.id + '</td>';
                    trHtml += '        <td> ' + item.name + '</td>'
                    trHtml += '        <td> ' + item.description + '</td>'
                    if (item.mainCategoryId != 0)
                        trHtml += '<td><span class="badge rounded-pill bg-primary">' + data.find(x => x.id === item.mainCategoryId).name + '</span></td>'
                    else
                        trHtml += '<td><span class="badge rounded-pill bg-success">Ana Kategori</span></td>'
                    trHtml += '        <td> ' + item.line + '</td>'
                    trHtml += '        <td>'
                    trHtml += '        <div class="dropdown" >'
                    trHtml += '            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">'
                    trHtml += '                <i class="bx bx-dots-vertical-rounded" > </i>'
                    trHtml += '            </button>'
                    trHtml += '            <div class="dropdown-menu">'
                    trHtml += '                 <a onclick="getCategoryById(' + item.id + ')" data-bs-toggle="modal" data-bs-target="#updateModal" class="dropdown-item" > <i class="bx bx-edit-alt me-1"> </i>Edit</a>'
                    trHtml += '                 <a onclick="deleteCategory(' + item.id + ')" class="dropdown-item" > <i class="bx bx-trash me-1" > </i> Delete</a>'
                    trHtml += '            </div>'
                    trHtml += '        </div>'
                    trHtml += '     </td>'
                    trHtml += '</tr>';
                }
                $("#categoryTableBody").append(trHtml);
            }
            else {
                Swal.fire({
                    title: "Hata!",
                    text: "Tablo yüklenirken bir şeyler ters gitti lütfen tekrar deneyiniz!",
                    icon: "error"
                });

                return;
            }
        }
    });
}

