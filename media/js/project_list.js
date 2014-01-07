var PROJECTS = function () {
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
                    oTable.fnUpdate(aData[i + 1], nRow, i + 1, false);
                }

                oTable.fnDraw();
            }

            var memSelect = function () {
                /*
				    <div class="controls">

				    	<select id="memSelect" class="small m-wrap span6 select2" multiple>

				    		<!--option value=""></option-->

				    	</select>

				    </div>
                */
            }

            function getArray(str) {
                if (str == null)
                {
                    return new Array();
                }
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

                //brief name
                jqTds[0].innerHTML = '<input type="text" class="input-small" value="' + $(aData[1]).text() + '">';

                //full name
                jqTds[1].innerHTML = '<input type="text" class="input-small" value="' + aData[2] + '">';

                //participants
                jqTds[2].innerHTML = memSelect.getMultiLine();
                $('#memSelect').select2({
                    placeholder: "Select Participants",
                    allowClear: true
                });
                var selected = getArray(aData[3]);
                getGroupUser(selected);

                //description
                var str = aData[4].replace(new RegExp('</p><p>', 'g'), '\n').replace('<p>', "").replace('</p>', "");
                jqTds[3].innerHTML = '<textarea class="input-large">' + str + '</textarea>';

                //hyperlink
                jqTds[4].innerHTML = '<input type="text" class="input-small" value="' + $(aData[5]).text() + '">';

                //save and cancel
                jqTds[5].innerHTML = '<a class="edit" href="">Save</a>';
                if (jqTds[6].innerHTML.indexOf("new") < 0) {
                    jqTds[6].innerHTML = '<a class="cancel" href="">Cancel</a>';
                }
            }

            //save row after server return
            function saveRow2(oTable, nRow, pro) {
                oTable.fnUpdate(pro["Id"], nRow, 0, false);
                var name = '<a href="./project_detail.aspx?id=' + pro["Id"] + '">' + getStr(pro["Name"]) + '</a>';

                var htp = pro["Link"];
                if (htp.indexOf("http") < 0) {
                    htp = "http://" + htp;
                }

                oTable.fnUpdate(name, nRow, 1, false);
                oTable.fnUpdate(pro["FullName"], nRow, 2, false);
                oTable.fnUpdate(pro["Participator"], nRow, 3, false);
                oTable.fnUpdate(pro["Description"], nRow, 4, false);
                oTable.fnUpdate('<a href="' + htp + '" target="_blank">' + pro["Link"] + '</a>', nRow, 5, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 7, false);
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

            //get project from form
            function getProject(oTable, nRow) {
                var jqInputs = $(':input', nRow);
    
                var mems = new Array();
                $('#memSelect', nRow).find("option:selected").each(function (index, mem) {
                    mems.push(mem.text);
                })

                var aData = oTable.fnGetData(nRow);
                var pro = {};
                pro["Id"] = getInt(aData[0]);
                pro["BriefName"] = jqInputs[0].value;
                pro["FullName"] = jqInputs[1].value;
                pro["Participator"] = mems.join();

                var lst = jqInputs[3].value.split('\n');
                var d = "";
                for (var i = 0; i < lst.length; i++) {
                    d += '<p>' + lst[i] + '</p>';
                }
                pro["Description"] = jqInputs[4].value;;
                pro["HyperLink"] = jqInputs[5].value;;

                return pro;
            }

            //get project from table
            function getRecord2(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var pro = {};
                pro["Id"] = getInt(aData[0]);
                pro["BriefName"] = $(aData[1]).text();
                pro["FullName"] = aData[2];
                pro["Participator"] = aData[3];

                pro["Description"] = aData[4];
                pro["HyperLink"] = aData[5];
                var json = JSON.stringify(rec);
                return json;
            }

            function get_project_settings() {
                $.ajax({
                    url: "data/project_list.ashx",
                    type: "POST",
                    dataType: "json",
                    data: {
                        command: "get_project_settings"
                    },
                    success: function (rec) {
                        for (var i = 0; i < rec.length; i++) {
                            var htp = rec[i].Hyperlink;
                            if (rec[i].Hyperlink.indexOf("http") < 0) {
                                htp = "http://" + htp;
                            }
                            oTable.fnAddData([parseInt(rec[i].Id)
                                , '<a href="./project_detail.aspx?id=' + rec[i].Id + '">' + rec[i].BriefName + '</a>'
                                , rec[i].FullName
                                , rec[i].Participator
                                , rec[i].Description
                                , '<a href="' + htp + '" target="_blank">' + rec[i].Hyperlink + '</a>'
                                , '<a class="edit" href="javascript:">Edit</a>'
                                , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                            ]);
                        }
                    },
                    error: function () {
                        alert("get_project_settings error!");
                    }
                });
            }

            get_project_settings();

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


            function editProject(dataStr, oTable, nEditing) {
                $.ajax({
                    type: "POST",
                    url: "data/project_list.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'edit_project_settings',
                        id:dataStr['Id'],
                        briefname: dataStr['BriefName'],
                        fullname:dataStr['FullName'],
                        description:dataStr['Description'],
                        hyperlink: dataStr['HyperLink'],
                        participator: dataStr['Participator'],
                        creator: $(".username").html()
                    },
                    success: function (data, textStatus) {
                        saveRow2(oTable, nEditing, data);
                    },

                    error: function (rec) {

                    }
                });
            }

            function deleteProject(pid, oTable, nRow) {
                $.ajax({
                    type: "POST",
                    url: "data/project_list.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'delete_project_settings',
                        id: pid,
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
                ],
            });

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialzie select2 dropdown

            var nEditing = 0;

            var isCreate = true;

            $('#sample_editable_1_new').click(function (e) {
                if ($("#sample_editable_1_new").hasClass("gury")) {
                    alert("add New one by one.");
                    return;
                }
                e.preventDefault();

                var aiNew = oTable.fnAddData(['', '', '', '', '', '',
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
                var aData = oTable.fnGetData(nRow);
                deleteProject(aData[0],oTable, nRow);
                oTable.fnDeleteRow(nRow);
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

                    if (jqInputs[0].value == '') {
                        alert("The Brief Name cannot be empty!");
                    } else {
                        var pro = getProject(oTable, nEditing);
                        if (pro["Participator"] == "") {
                            alert("Please select participants");
                            return;
                        }
                        editProject(pro, oTable, nEditing);
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


        }
    };
}();