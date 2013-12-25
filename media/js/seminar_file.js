Dropzone.options.myAwesomeDropzone = {
    dictRemoveFile: "remove",
    addRemoveLinks: true,
    clickable: true,
    dictRemoveFileConfirmation: "Are you sure to remove it?",
    init: function () {

        myDropzone = this;

        //show files already stored on server
        function getAllFiles() {
            $.ajax({
                type: "POST",
                url: "data/FileUploader.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'getAllFiles',
                },
                success: function (data, textStatus) {
                    //alert(JSON.stringify(data));

                    for (var key in data) {
                        // Create the mock file:
                        var mockFile = { name: data[key].origin_name, size: data[key].size };

                        // Call the default addedfile event handler
                        myDropzone.emit("addedfile", mockFile);

                        // And optionally show the thumbnail of the file:
                        myDropzone.emit("thumbnail", mockFile, data[key].thumbnail_url);

                        var space = Dropzone.createElement("&nbsp;");
                        var downloadButton = Dropzone.createElement("<a>download</a>");
                        var fileName = Dropzone.createElement("<label class='fileName' style='display:none'>" + data[key].name + "</label>");
                        // Add the button to the file preview element.

                        // Listen to the click event
                        downloadButton.addEventListener("click", function (e) {
                            // Make sure the button click doesn't submit the form:
                            e.preventDefault();
                            e.stopPropagation();
                            downloadFile(fileName.innerHTML);
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
            $.ajax({
                type: "POST",
                url: "data/FileUploader.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'uploadThumbnail',
                    thumbnail: dataUrl,
                    fileName: fileName
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
            $.ajax({
                type: "POST",
                url: "data/FileUploader.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'removeFile',
                    fileName: fileName
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
            var form = $("<form>");  //==>jQuery创建隐藏表单,实现ajax下载
            form.attr('style', 'display:none');
            form.attr('target', 'downloadframe');
            form.attr('action', 'data/FileUploader.ashx');
            form.attr('method', 'post');
            form.append("<input name='fileName' value='" + fileName + "'></input>");
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
            var fileName = Dropzone.createElement("<label class='fileName' style='display:none'>" + json['name'] + "</label>");

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

            uploadThumbnail(json['name'], file.previewElement.querySelector("[data-dz-thumbnail]").src);
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
