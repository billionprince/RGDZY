var FilePrint = function () {
    var handleToggleButtons = function () {
        if (!jQuery().toggleButtons) {
            return;
        }
        $('.single-double').toggleButtons({
            width: 180,
            height: 35,
            label: {
                enabled: "Single-sided",
                disabled: "Double-sided"
            },
            style: {
                enabled: "danger",
                disabled: "success"
            },
            font: {
                'font-size': '20px',
             }
        });
    }
    var FormFileUpload = function () {
        $('#fileupload').fileupload({
            // Uncomment the following to send cross-domain cookies:
            //xhrFields: {withCredentials: true},
            url: 'data/file_print.ashx'
        });

        // Load existing files:
        // Demo settings:
        $.ajax({
            // Uncomment the following to send cross-domain cookies:
            //xhrFields: {withCredentials: true},
            url: $('#fileupload').fileupload('option', 'url'),
            dataType: 'json',
            context: $('#fileupload')[0],
            maxFileSize: 5000000,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            process: [{
                action: 'load',
                fileTypes: /^image\/(gif|jpeg|png)$/,
                maxFileSize: 20000000 // 20MB
            }, {
                action: 'resize',
                maxWidth: 1440,
                maxHeight: 900
            }, {
                action: 'save'
            }
            ]
        }).done(function (result) {
            $(this).fileupload('option', 'done')
                .call(this, null, {
                    result: result
                });
        });

        // Upload server status check for browsers with CORS support:
        if ($.support.cors) {
            $.ajax({
                url: 'file_print.ashx',
                type: 'HEAD'
            }).fail(function () {
                return;
                $('<span class="alert alert-error"/>')
                    .text('Upload server currently unavailable - ' +
                    new Date())
                    .appendTo('#fileupload');
            });
        }

    }

    return {
        //main function to initiate the module
        init: function () {
            handleToggleButtons();
            FormFileUpload();
        }

    };

}();

$(document).ready(function () {    $(".print").on("click", function (e) {        var isSelected = $(".single-double .toggle").is(':checked');        $.ajax({
            type: "POST",
            url: "data/file_print.ashx",
            dataType: 'json',
            data: {
                flag: isSelected
            },
            success: function () {
                alert("print files succeed!");
            },
            error: function (rec) {
                console.log(rec);
                alert("print files error!");
            }
        });
    });
});