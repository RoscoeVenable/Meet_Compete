// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function ($) {
    if ($("#FileUploadId").length) {
        $("#FormId").submit(submitImage);
    }
})(jQuery);

function submitImage(e) {
    e.preventDefault();
    let formData = new FormData();
    let file = $("#myImage").prop("files")[0];
    formData.append("file", file);
    $.ajax({
        url: 'https://localhost:44317/EventPlaners/Upload',
        type: "Put",
        data: formData,
        cache: false,
        processData: false,
        contentType: false,
        success: function (result, textStatus, jQxhr) {
            alert("Image Upload sucessful");
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

