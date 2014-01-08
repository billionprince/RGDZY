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

var SeminarRecord = function () {

    return {

        //main function to initiate the module
        init: function () {

            var seminarId = getparameter()['id'];

            Function.prototype.getMultiLine = function () {
                var lines = new String(this);
                lines = lines.substring(lines.indexOf("/*") + 3, lines.lastIndexOf("*/"));
                return lines;
            }

            function restoreRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTable.fnUpdate(aData[i+1], nRow, i+1, false);
                }

                oTable.fnDraw();
            }


            var datePicker = function () {
                /* 
                    <div class="controls">

						<input class="input-small date-picker" readonly="" size="16" type="text" value="">

					</div>
                */
            }

            var fileSelect = function(){
                /*
                    <div class="controls">

						<input id="tags_2" type="text" class="m-wra tags small" value="tag1,tag2" style="display: none;"><div id="tags_2_tagsinput" class="tagsinput" style="width: 240px; height: 100px;"><span class="tag"><span>tag1&nbsp;&nbsp;</span><a href="#" title="Removing tag">x</a></span><span class="tag"><span>tag2&nbsp;&nbsp;</span><a href="#" title="Removing tag">x</a></span><div id="tags_2_addTag"><input id="tags_2_tag" value="" data-default="add a tag" style="color: rgb(102, 102, 102); width: 80px;"></div><div class="tags_clear"></div></div>

					</div>
                */
            }

            var editor = function () {
                /*
                <div class="controls">

						<ul class="wysihtml5-toolbar" style=""><li class="dropdown"><a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><i class="icon-font"></i>&nbsp;<span class="current-font">Normal text</span>&nbsp;<b class="caret"></b></a><ul class="dropdown-menu"><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="div" tabindex="-1" href="javascript:;" unselectable="on">Normal text</a></li><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="h1" tabindex="-1" href="javascript:;" unselectable="on">Heading 1</a></li><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="h2" tabindex="-1" href="javascript:;" unselectable="on">Heading 2</a></li><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="h3" tabindex="-1" href="javascript:;" unselectable="on">Heading 3</a></li><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="h4" href="javascript:;" unselectable="on">Heading 4</a></li><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="h5" href="javascript:;" unselectable="on">Heading 5</a></li><li><a data-wysihtml5-command="formatBlock" data-wysihtml5-command-value="h6" href="javascript:;" unselectable="on">Heading 6</a></li></ul></li><li><div class="btn-group"><a class="btn" data-wysihtml5-command="bold" title="CTRL+B" tabindex="-1" href="javascript:;" unselectable="on">Bold</a><a class="btn" data-wysihtml5-command="italic" title="CTRL+I" tabindex="-1" href="javascript:;" unselectable="on">Italic</a><a class="btn" data-wysihtml5-command="underline" title="CTRL+U" tabindex="-1" href="javascript:;" unselectable="on">Underline</a></div></li><li><div class="btn-group"><a class="btn" data-wysihtml5-command="insertUnorderedList" title="Unordered list" tabindex="-1" href="javascript:;" unselectable="on"><i class="icon-list"></i></a><a class="btn" data-wysihtml5-command="insertOrderedList" title="Ordered list" tabindex="-1" href="javascript:;" unselectable="on"><i class="icon-th-list"></i></a><a class="btn" data-wysihtml5-command="Outdent" title="Outdent" tabindex="-1" href="javascript:;" unselectable="on"><i class="icon-indent-right"></i></a><a class="btn" data-wysihtml5-command="Indent" title="Indent" tabindex="-1" href="javascript:;" unselectable="on"><i class="icon-indent-left"></i></a></div></li><li><div class="bootstrap-wysihtml5-insert-link-modal modal hide fade"><div class="modal-header"><a class="close" data-dismiss="modal"></a><h3>Insert link</h3></div><div class="modal-body"><input type="text" value="http://" class="bootstrap-wysihtml5-insert-link-url1 m-wrap large"><label class="checkbox"> <input type="checkbox" class="bootstrap-wysihtml5-insert-link-target" checked="">Open link in new window</label></div><div class="modal-footer"><a href="#" class="btn" data-dismiss="modal">Cancel</a><a href="#" class="btn green btn-primary" data-dismiss="modal">Insert link</a></div></div><a class="btn" data-wysihtml5-command="createLink" title="Insert link" tabindex="-1" href="javascript:;" unselectable="on"><i class="icon-share"></i></a></li><li><div class="bootstrap-wysihtml5-insert-image-modal modal hide fade"><div class="modal-header"><a class="close" data-dismiss="modal"></a><h3>Insert image</h3></div><div class="modal-body"><input type="text" value="http://" class="bootstrap-wysihtml5-insert-image-url m-wrap large"></div><div class="modal-footer"><a href="#" class="btn" data-dismiss="modal">Cancel</a><a href="#" class="btn green btn-primary" data-dismiss="modal">Insert image</a></div></div><a class="btn" data-wysihtml5-command="insertImage" title="Insert image" tabindex="-1" href="javascript:;" unselectable="on"><i class="icon-picture"></i></a></li></ul><textarea class="span12 wysihtml5 m-wrap" rows="6" style="display: none;"></textarea><input type="hidden" name="_wysihtml5_mode" value="1"><iframe class="wysihtml5-sandbox" security="restricted" allowtransparency="true" frameborder="0" width="0" height="0" marginwidth="0" marginheight="0" style="background-color: rgba(0, 0, 0, 0); border-collapse: separate; border: 1px solid rgb(204, 204, 204); clear: none; display: inline-block; float: none; margin: 0px; outline: rgb(51, 51, 51) none 0px; outline-offset: 0px; padding: 6px; position: static; top: auto; left: auto; right: auto; bottom: auto; z-index: auto; vertical-align: top; text-align: start; box-sizing: border-box; -webkit-box-shadow: none; box-shadow: none; border-top-right-radius: 0px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px; border-top-left-radius: 0px; width: 876px; height: 134px;"></iframe>

				</div>
                */
            }

            function editRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = datePicker.getMultiLine();


                jqTds[1].innerHTML = '<input type="text" class="input-small" value="' + aData[2] + '">';

                var str = aData[3].replace(new RegExp('</p><p>', 'g'), '\n').replace('<p>', "").replace('</p>', "");
                jqTds[2].innerHTML = '<textarea class="input-xxlarge">' + str + '</textarea>';

                //jqTds[2].innerHTML = editor.getMultiLine();
                jqTds[3].innerHTML = '<a class="edit" href="">Save</a>';
                if (jqTds[4].innerHTML.indexOf("new") < 0) {
                    jqTds[4].innerHTML = '<a class="cancel" href="">Cancel</a>';
                }

                //initialize datepicker
                $('.date-picker').each(function () {
                    $(this).datepicker();
                });

                var jqInputs = $('input', nRow);
                jqInputs[0].value = aData[1];
            }

            //save row after server return
            function saveRow2(oTable, nRow, rec) {
                oTable.fnUpdate(rec["Id"], nRow, 0, false);
                oTable.fnUpdate(rec["Date"], nRow, 1, false);
                oTable.fnUpdate(rec["Recorder"], nRow, 2, false);
                oTable.fnUpdate(rec["Agenda"], nRow, 3, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 4, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 5, false);
                oTable.fnDraw();
            }

            function saveRow(oTable, nRow) {
                var jqInputs = $(':input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 2, false);
                var lst = jqInputs[2].value.split('\n');
                var d = "";
                for (var i = 0; i < lst.length; i++) {
                    d += '<p>' + lst[i] + '</p>';
                }
                oTable.fnUpdate(d, nRow, 3, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 4, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 5, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 6, false);
                oTable.fnDraw();
            }

            function cancelEditRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 4, false);
                oTable.fnDraw();
            }

            function getStr(data) {
                if (data == null)
                    return '';
                else
                    return data;
            }

            function getInt(data) {
                if (data == null || data == '')
                    return 0;
                else
                    return data;
            }

            //get record from form
            function getRecord(oTable, nRow) {
                var jqInputs = $(':input', nRow);
                var jqSelect = $('select', nRow);

                var aData = oTable.fnGetData(nRow);
                var rec = {};
                rec["Id"] = getInt(aData[0]);
                rec["SeminarId"] = seminarId;
                rec["Date"] = jqInputs[0].value;
                //rec["Recorder"] = jqSelect.find("option:selected").text();

                rec["Recorder"] = jqInputs[1].value;

                var lst = jqInputs[2].value.split('\n');
                var d = "";
                for (var i = 0; i < lst.length; i++) {
                    d += '<p>' + lst[i] + '</p>';
                }

                rec["Agenda"] = d;
                var json = JSON.stringify(rec);
                //alert(json);
                return json;
            }

            //get record from table
            function getRecord2(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var rec = {};
                rec["Id"] = getInt(aData[0]);
                rec["SeminarId"] = seminarId;
                rec["Date"] = aData[1];
                rec["Recorder"] = aData[2];
                rec["Agenda"] = aData[3];
                var json = JSON.stringify(rec);
                //alert(json);
                return json;
            }

            function getAllRecords() {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarRecordHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllSeminarRecords',
                        parameter: null,
                        id: seminarId
                    },
                    success: function (data, textStatus) {
                        //alert(data);
                        oTable.fnClearTable();
                        $(data).each(function (index, rec) {
                            oTable.fnAddData([parseInt(rec["Id"])
                                , getStr(rec["Date"])
                                , getStr(rec["Recorder"])
                                , getStr(rec["Agenda"])
                                , '<a class="edit" href="">Edit</a>'
                                , '<a class="delete" href="">Delete</a>'
                            ]);
                        })
                        oTable.fnDraw();
                    },

                    error: function (rec) {
                        //
                        (rec.responseText);
                    }
                });
            }

            getAllRecords();

            function getSeminar() {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getSeminar',
                        id: seminarId
                    },
                    success: function (data, textStatus) {
                        $(".semName").html(data["Name"]);
                    },

                    error: function (rec) {
                        alert(data["getSeminar error!"]);
                    }
                });
            }

            getSeminar();

            function addRecord(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarRecordHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addSeminarRecord',
                        parameter: dataStr,
                        create: isCreate,
                        id: seminarId
                    },
                    success: function (data, textStatus) {
                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                    }
                });
            }

            function editRecord(dataStr, oTable, nEditing) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarRecordHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editSeminarRecord',
                        parameter: dataStr,
                        id: seminarId
                    },
                    success: function (data, textStatus) {
                        saveRow2(oTable, nEditing, data);
                    },

                    error: function (rec) {

                    }
                });
            }

            function deleteRecord(dataStr, oTable, nRow) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarRecordHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deleteSeminarRecord',
                        parameter: dataStr,
                        id: seminarId
                    },
                    success: function (data, textStatus) {
                        oTable.fnDeleteRow(nRow);
                    },
                    error: function (rec) {
                    }
                });
            }


            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],

                "sScrollX": "100%",
                "bAutoWidth": false,  //自适应宽度
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bVisible': false,
                    "bSearchable": false,
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialzie select2 dropdown

            var nEditing = null;

            $('#sample_editable_1_new').click(function (e) {
                e.preventDefault();
                if ($("#sample_editable_1_new").hasClass("gury")) {
                    alert("add New one by one.");
                    return;
                }
                var aiNew = oTable.fnAddData(['', '', '','',
                        '<a class="edit" href="">Edit</a>', '<a class="cancel" data-mode="new" href="">Cancel</a>'
                ]);
                var nRow = oTable.fnGetNodes(aiNew[0]);
                editRow(oTable, nRow);
                nEditing = nRow;
                $("#sample_editable_1_new").removeClass("green").addClass("gury");
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                //oTable.fnDeleteRow(nRow);
                deleteRecord(getRecord2(oTable, nRow), oTable, nRow);
                //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
            });

            $('#sample_editable_1 a.cancel').live('click', function (e) {
                e.preventDefault();
                if ($(this).attr("data-mode") == "new") {
                    var nRow = $(this).parents('tr')[0];
                    oTable.fnDeleteRow(nRow);
                    $("#sample_editable_1_new").removeClass("gury").addClass("green");
                    nEditing = null;
                } else {
                    restoreRow(oTable, nEditing);
                    nEditing = null;
                }
            });

            $('#sample_editable_1 a.edit').live('click', function (e) {
                e.preventDefault();

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];

                if (nEditing !== null && nEditing != nRow) {
                    /* Currently editing - but not this row - restore the old before continuing to edit mode */
                    restoreRow(oTable, nEditing);
                    editRow(oTable, nRow);
                    nEditing = nRow;
                } else if (nEditing == nRow && this.innerHTML == "Save") {
                    /* Editing this row and want to save it */
                    var jqInputs = $(':input', nRow);
                    if (jqInputs[0].value == '' || jqInputs[1].value == '' || jqInputs[2].value == '') {
                        alert("Please input the empty fields!");
                    } else {
                        //saveRow(oTable, nEditing);
                        editRecord(getRecord(oTable, nEditing), oTable, nEditing);
                        $("#sample_editable_1_new").removeClass("gury").addClass("green");
                        nEditing = null;
                        //alert("Updated! Do not forget to do some ajax to sync with backend :)");
                    }
                } else {
                    /* No edit in progress - let's start one */
                    editRow(oTable, nRow);
                    nEditing = nRow;
                }
            });

            var handlePortletTools = function () {

                jQuery('body').on('click', '.portlet .tools a.reload', function (e) {
                    e.preventDefault();
                    var el = jQuery(this).parents(".portlet");
                    App.blockUI(el);
                    getAllRecords(null);
                    window.setTimeout(function () {
                        App.unblockUI(el);
                    }, 1000);
                });

                jQuery('body').on('click', '.portlet .tools .collapse, .portlet .tools .expand', function (e) {
                    e.preventDefault();
                    var el = jQuery(this).closest(".portlet").children(".portlet-body");
                    if (jQuery(this).hasClass("collapse")) {
                        jQuery(this).removeClass("collapse").addClass("expand");
                        el.slideUp(200);
                    } else {
                        jQuery(this).removeClass("expand").addClass("collapse");
                        el.slideDown(200);
                    }
                });
            }
            handlePortletTools();
        }
    };

}();