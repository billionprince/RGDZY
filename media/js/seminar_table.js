var SeminarTable = function () {

    return {

        //main function to initiate the module
        init: function () {

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

            var daySelect = function () {
                /* 
                     <div class="controls">
                     
                     	<select id = "daySelect" class="input-small" tabindex="2">

                            <option value="0">MON</option>
                     
                     		<option value="1">TUE</option>
                     
                     		<option value="2">WED</option>
                     
                     		<option value="3">THUR</option>
                     
                     		<option value="4">FRI</option>

                            <option value="5">SAT</option>

                            <option value="6">SUN</option>
                     
                     	</select>
                     
                     </div>
                */
            }

            var timeSelect = function () {
                /*
				    	<div class="controls">

				    		<input type="text" value="" data-format="H:mm" class="input-small clockface_1 clockface-open">

				    	</div>
                */
            }

            var memSelect = function () {
                /*
				    <div class="controls">

				    	<select id="memSelect" class="large m-wrap span6 select2" multiple>

				    		<!--option value=""></option-->

				    	</select>

				    </div>
                */
            }

            function getArray(str)
            {
                var tmp = str.split(",");
                if (tmp.length == 1 && tmp[0] == "") {
                    return new Array();
                }
                else {
                    return tmp;
                }
            }

            function editRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text" class="input-small" value="' + $(aData[1]).text() + '">';
                jqTds[1].innerHTML = daySelect.getMultiLine();
                jqTds[2].innerHTML = timeSelect.getMultiLine();
                jqTds[3].innerHTML = timeSelect.getMultiLine();
                jqTds[4].innerHTML = memSelect.getMultiLine();

                var jqInputs = $('input', nRow);
                jqInputs[1].value = aData[3];
                jqInputs[2].value = aData[4];

                $("#daySelect :contains('" + aData[2] + "')", nRow).attr("selected", true);

                jqTds[5].innerHTML = '<a class="edit" href="">Save</a>';
                if (jqTds[6].innerHTML.indexOf("new") < 0) {
                    jqTds[6].innerHTML = '<a class="cancel" href="">Cancel</a>';
                }

                $('#memSelect').select2({
                    placeholder: "Select Participator",
                    allowClear: true
                });

                var selected = getArray(aData[5]);
                getGroupUser(selected);
            }

            //save row after server return
            function saveRow2(oTable, nRow, sem) {
                oTable.fnUpdate(sem["Id"], nRow, 0, false);
                var name = '<a href="./seminar_record.aspx?id=' + sem["Id"] + '">' + getStr(sem["Name"]) + '</a>';
                oTable.fnUpdate(name, nRow, 1, false);
                oTable.fnUpdate(sem["Day"], nRow, 2, false);
                oTable.fnUpdate(sem["BeginTime"], nRow, 3, false);
                oTable.fnUpdate(sem["EndTime"], nRow, 4, false);
                oTable.fnUpdate(sem["Participator"], nRow, 5, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 7, false);
                oTable.fnDraw();
            }

            function saveRow(oTable, nRow, sem) {
                var jqInputs = $(':input', nRow);
                var dayText = $('#daySelect', nRow).find("option:selected").text();
                var mems = new Array();
                $('#memSelect', nRow).find("option:selected").each(function (index, mem) {
                    mems.push(mem.text);
                })

                oTable.fnUpdate(jqInputs[0].value, nRow, 1, false);
                oTable.fnUpdate(dayText, nRow, 2, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 3, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 4, false);
                oTable.fnUpdate(mems.join(), nRow, 5, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 7, false);
                oTable.fnDraw();
            }

            function cancelEditRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 5, false);
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

            //get seminar from form
            function getSeminar(oTable, nRow) {
                var jqInputs = $(':input', nRow);
                var dayText = $('#daySelect', nRow).find("option:selected").text();
                var mems = new Array();
                $('#memSelect', nRow).find("option:selected").each(function (index, mem) {
                    mems.push(mem.text);
                })

                var aData = oTable.fnGetData(nRow);
                var sem = {};
                sem["Id"] = getInt(aData[0]);
                sem["Name"] = jqInputs[0].value;
                sem["Day"] = dayText;
                sem["BeginTime"] = jqInputs[2].value;
                sem["EndTime"] = jqInputs[3].value;
                sem["Participator"] = mems.join();
                //var json = JSON.stringify(sem);
                return sem;
            }

            //get seminar from table
            function getSeminar2(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var sem = {};
                sem["Id"] = getInt(aData[0]);
                sem["Name"] = $(aData[1]).text();
                sem["Day"] = aData[2];
                sem["BeginTime"] = aData[3];
                sem["EndTime"] = aData[4];
                sem["Participator"] = aData[5];
                var json = JSON.stringify(sem);
                //alert(json);
                return json;
            }

            function getAllSeminars(func) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllSeminars',
                        parameter: null
                    },
                    success: function (data, textStatus) {
                        //alert(data);
                        oTable.fnClearTable();
                        $(data).each(function (index, sem) {
                            oTable.fnAddData([parseInt(sem["Id"])
                                , '<a href="./seminar_record.aspx?id=' + sem["Id"] + '">' + getStr(sem["Name"]) + '</a>'
                                , getStr(sem["Day"])
                                , getStr(sem["BeginTime"])
                                , getStr(sem["EndTime"])
                                , getStr(sem["Participator"])
                                , '<a class="edit" href="">Edit</a>'
                                , '<a class="delete" href="">Delete</a>'
                            ]);
                        })
                        oTable.fnDraw();

                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        //
                        (rec.responseText);
                    }
                });
            }

            getAllSeminars(null);

            members = null;

            function getGroupUser(selected) {
                if (members == null) {
                    $.ajax({
                        url: "data/user.ashx",
                        type: "POST",
                        datatype: "json",
                        data: {
                            command: "get_user_group"
                        },
                        success: function (rec) {
                            //alert(JSON.stringify(rec));
                            for (var i = 0; i < rec.length; i++) {
                                $("#memSelect").append(function () {
                                    var str = "<optgroup label='" + rec[i].groupname + "'>";
                                    for (var j = 0; j < rec[i].username.length; j++) {
                                        str += "<option>" + rec[i].username[j] + "</option>";
                                    }
                                    str += "</optgroup>";
                                    return str;
                                });
                            }
                            members = rec;

                            for (m in selected) {
                                $("#memSelect :contains('" + selected[m] + "')").attr("selected", true);
                            }

                            $('#memSelect').select2({
                                placeholder: "Select Participator",
                                allowClear: true
                            });
                            
                        },
                        error: function () {
                            alert("userlist load events error!");
                        }
                    });
                } else {
                    for (var i = 0; i < members.length; i++) {
                        $("#memSelect").append(function () {
                            var str = "<optgroup label='" + members[i].groupname + "'>";
                            for (var j = 0; j < members[i].username.length; j++) {
                                str += "<option>" + members[i].username[j] + "</option>";
                            }
                            str += "</optgroup>";
                            return str;
                        });
                    }
                    for (m in selected) {
                        $("#memSelect :contains('" + selected[m] + "')").attr("selected", true);
                    }

                    $('#memSelect').select2({
                        placeholder: "Select Participator",
                        allowClear: true
                    });
                }


            };

            function addSeminar(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addSeminar',
                        parameter: dataStr,
                        create: isCreate
                    },
                    success: function (data, textStatus) {
                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        alert("addSeminar error!");
                    }
                });
            }

            function editSeminar(dataStr, oTable, nEditing) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editSeminar',
                        parameter: dataStr,
                        creator: $(".username").html()
                    },
                    success: function (data, textStatus) {
                        saveRow2(oTable, nEditing, data);
                    },

                    error: function (rec) {
                        alert("editSeminar error!");
                    }
                });
            }

            function deleteSeminar(dataStr, oTable, nRow) {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deleteSeminar',
                        parameter: dataStr,
                    },
                    success: function (data, textStatus) {
                        oTable.fnDeleteRow(nRow);
                    },
                    error: function (rec) {
                        alert("deleteSeminar error!");
                    }
                });
            }

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                "bAutoWidth": false,  //自适应宽度
                "iDisplayLength": 5,
                "sScrollX": "100%",

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
                    'bSearchable': false,
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
                if ($("#sample_editable_1_new").hasClass("gury")) {
                    alert("add New one by one.");
                    return;
                }
                e.preventDefault();

                $('.clockface').hide();
                var aiNew = oTable.fnAddData(['', '', '', '','','',
                        '<a class="edit" href="">Edit</a>', '<a class="cancel" data-mode="new" href="">Cancel</a>'
                ]);
                var nRow = oTable.fnGetNodes(aiNew[0]);
                editRow(oTable, nRow);
                nEditing = nRow;
                $("#sample_editable_1_new").removeClass("green").addClass("gury");
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                $('.clockface').hide();
                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];

                deleteSeminar(getSeminar2(oTable, nRow), oTable, nRow);
                
                //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
            });

            $('#sample_editable_1 a.cancel').live('click', function (e) {
                e.preventDefault();

                $('.clockface').hide();
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

                $('.clockface').hide();

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
                    var reg = /^(\d{1,2}):(\d{1,2})$/;
                    if (jqInputs[0].value == '' || jqInputs[2].value == '' || jqInputs[3].value == '') {
                        alert("Please input the empty fields!");
                    } else if (!(reg.test(jqInputs[2].value) && reg.test(jqInputs[3].value))) {
                        alert("Invalid time");
                    } else if (Date.parse(jqInputs[3].value) - Date.parse(jqInputs[2].value) < 0) {
                        alert("Invalid time");
                    } else {
                        //saveRow(oTable, nEditing);
                        var sem = getSeminar(oTable, nEditing);
                        if (sem["Participator"] == "")
                        {   
                            alert("Please select participants");
                            return;
                        }
                        editSeminar(JSON.stringify(sem), oTable, nEditing);
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
                    getAllSeminars(null);
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