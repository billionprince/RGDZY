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
                     
                     	<select class="small m-wrap event_week" tabindex="2">

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

				    		<input type="text" value="" data-format="hh:mm A" class="m-wrap small clockface_1 clockface-open">

				    	</div>
                */
            }

            function editRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text" class="m-wrap small" value="' + $(aData[1]).text() + '">';
                jqTds[1].innerHTML = daySelect.getMultiLine();
                jqTds[2].innerHTML = timeSelect.getMultiLine();
                jqTds[3].innerHTML = timeSelect.getMultiLine();

                var jqInputs = $('input', nRow);
                jqInputs[1].value = aData[3];
                jqInputs[2].value = aData[4];

                $("select :contains('" + aData[2] + "')", nRow).attr("selected", true);

                jqTds[4].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[5].innerHTML = '<a class="cancel" href="">Cancel</a>';
            }

            //save row after server return
            function saveRow2(oTable, nRow, sem) {
                oTable.fnUpdate(sem["Id"], nRow, 0, false);
                var name = '<a href="./seminar_record.aspx?id=' + sem["Id"] + '">' + getStr(sem["Name"]) + '</a>';
                oTable.fnUpdate(name, nRow, 1, false);
                oTable.fnUpdate(sem["Day"], nRow, 2, false);
                oTable.fnUpdate(sem["BeginTime"], nRow, 3, false);
                oTable.fnUpdate(sem["EndTime"], nRow, 4, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 5, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 6, false);
                oTable.fnDraw();
            }

            function saveRow(oTable, nRow, sem) {
                oTable.fnUpdate(jqInputs[0].value, nRow, 1, false);
                oTable.fnUpdate(jqSelect.find("option:selected").text(), nRow, 2, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 3, false);
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
                var jqSelect = $('select', nRow);

                var aData = oTable.fnGetData(nRow);
                var sem = {};
                sem["Id"] = getInt(aData[0]);
                sem["Name"] = jqInputs[0].value;
                sem["Day"] = jqSelect.find("option:selected").text();
                sem["BeginTime"] = jqInputs[2].value;
                sem["EndTime"] = jqInputs[3].value;
                var json = JSON.stringify(sem);
                //alert(json);
                return json;
            }

            //get seminar from table
            function getSeminar2(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var sem = {};
                sem["Id"] = getInt(aData[0]);
                sem["Name"] = $(aData[1]).text();
                alert(sem["Name"])
                sem["Day"] = aData[2];
                sem["BeginTime"] = aData[3];
                sem["EndTime"] = aData[4];
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
                        $(data).each(function (index, sem) {
                            oTable.fnAddData([parseInt(sem["Id"])
                                //, getStr(sem["Name"])
                                , '<a href="./seminar_record.aspx?id=' + sem["Id"] + '">' + getStr(sem["Name"]) + '</a>'
                                , getStr(sem["Day"])
                                , getStr(sem["BeginTime"])
                                , getStr(sem["EndTime"])
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
                    },
                    success: function (data, textStatus) {
                        saveRow2(oTable, nEditing, data);
                    },

                    error: function (rec) {

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
                e.preventDefault();

                $('.clockface').hide();
                var aiNew = oTable.fnAddData(['', '', '', '','',
                        '<a class="edit" href="">Edit</a>', '<a class="cancel" data-mode="new" href="">Cancel</a>'
                ]);
                var nRow = oTable.fnGetNodes(aiNew[0]);
                editRow(oTable, nRow);
                nEditing = nRow;
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
                    if (jqInputs[0].value == '' || jqInputs[2].value == '' || jqInputs[3].value == '') {
                        alert("Please input the empty fields!");
                    } else {
                        //saveRow(oTable, nEditing);
                        editSeminar(getSeminar(oTable, nEditing), oTable, nEditing);
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