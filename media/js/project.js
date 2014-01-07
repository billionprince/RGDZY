var getparameter = function () {
    var query = location.search.substr(1);
    var data = query.split("&");
    var result = {};
    for (var i = 0; i < data.length; i++) {
        var item = data[i].split("=");
        result[item[0]] = item[1];
    }
    return result;
}

Dropzone.options.myAwesomeDropzone = {
    dictRemoveFile:"remove",
    addRemoveLinks: true,
    clickable: true,
    dictRemoveFileConfirmation: "Are you sure to remove it?",
    init: function () {

        myDropzone = this;

        //show files already stored on server
        function getAllFiles() {
            var para = getparameter();
            $.ajax({
                type: "POST",
                url: "data/FileUploader.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'getAllFiles',
                        id: para['id']
                },
                success: function (data, textStatus) {
                    //alert(JSON.stringify(data));
                        
                    for (var key in data) {
                        $(".dz-message").hide();
                        // Create the mock file:
                        var mockFile = { name: data[key].OriginName, size: data[key].Size };

                        // Call the default addedfile event handler
                        myDropzone.emit("addedfile", mockFile);

                        // And optionally show the thumbnail of the file:
                        myDropzone.emit("thumbnail", mockFile, data[key].ThumbnailUrl);

                        var space = Dropzone.createElement("&nbsp;");
                        var downloadButton = Dropzone.createElement("<a>download</a>");
                        var fileName = Dropzone.createElement("<label class='fileName' style='display:none'>" + data[key].Name + "</label>");
                        // Add the button to the file preview element.

                        // Listen to the click event
                        downloadButton.addEventListener("click", function (e) {
                            // Make sure the button click doesn't submit the form:
                            e.preventDefault();
                            e.stopPropagation();
                            //alert(this.nextSibling.innerHTML)
                            downloadFile(this.nextSibling.innerHTML);
                        });

                        mockFile.previewElement.appendChild(space);
                        mockFile.previewElement.appendChild(downloadButton);
                        mockFile.previewElement.appendChild(fileName);

                        // If you use the maxFiles option, make sure you adjust it to the
                        // correct amount:
                        var existingFileCount = 1; // The number of files already uploaded
                        myDropzone.options.maxFiles = myDropzone.options.maxFiles - existingFileCount;
                    }
                },

                error: function (rec) {
                    //alert(rec.responseText);
                }
            });
        }

        getAllFiles();

        function uploadThumbnail(fileName, dataUrl) {
            var para = getparameter();
            $.ajax({
                type: "POST",
                url: "data/FileUploader.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'uploadThumbnail',
                    thumbnail: dataUrl,
                    fileName: fileName,
                    id: para['id']
                },
                success: function (data, textStatus) {
                    //alert(textStatus);
                },

                error: function (rec) {
                    //alert(rec.responseText);
                }
            });
        }

        function removeFile(fileName) {
            var para = getparameter();
            $.ajax({
                type: "POST",
                url: "data/FileUploader.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'removeFile',
                    fileName: fileName,
                    id: para['id']
                },
                success: function (data, textStatus) {
                    //alert(textStatus);
                },

                error: function (rec) {
                    //alert(rec.responseText);
                }
            });
        }

        function downloadFile(fileName) {
            var para = getparameter();
            var form = $("<form>");  //==>jQuery创建隐藏表单,实现ajax下载
            form.attr('style', 'display:none');
            form.attr('target', 'downloadframe');
            form.attr('action', 'data/FileUploader.ashx');
            form.attr('method', 'post');
            form.append("<input name='fileName' value='" + fileName + "'></input>");
            form.append("<input name='id' value='" + para['id'] + "'></input>");
            form.append("<input name='command' value='downloadFile'></input>")
            $('body').append(form);

            form.submit();
            form.remove();
        }

        this.on("success", function (file, json) {

            //alert(JSON.stringify(json));
            // Create the remove button
            var space = Dropzone.createElement("&nbsp;");
            var downloadButton = Dropzone.createElement("<a>download</a>");
            var fileName = Dropzone.createElement("<label class='fileName' style='display:none'>" + json['Name'] + "</label>");

            // Capture the Dropzone instance as closure.
            var _this = this;

            // Listen to the click event
            downloadButton.addEventListener("click", function (e) {
                // Make sure the button click doesn't submit the form:
                e.preventDefault();
                e.stopPropagation();
                downloadFile(fileName.innerHTML);
            });

            // Add the button to the file preview element.
            file.previewElement.appendChild(space);
            file.previewElement.appendChild(downloadButton);
            file.previewElement.appendChild(fileName);

            uploadThumbnail(json['Name'], file.previewElement.querySelector("[data-dz-thumbnail]").src);
            //alert(file.previewElement.querySelector(".fileName").innerHTML);
            //alert(file.previewElement.querySelector("[data-dz-thumbnail]").src);
        });

        this.on("removedfile", function (file) {
            removeFile(file.previewElement.querySelector(".fileName").innerHTML);
        });

        this.on("thumbnail", function (file, dataUrl) {
        });

        this.on("sending", function (file, xhr, formData) {
            formData.append("command", "uploadFile");
        });

        myDropzone.confirm = function (question, accepted, rejected) {
            if (confirm(question))
                accepted;
        };

    }
};
