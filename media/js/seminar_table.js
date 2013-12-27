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

            var memSelect = function () {
                /*
				    	<div class="controls">

								<select data-placeholder="Choose Participators" class="chosen span6 chzn-done" multiple="multiple" tabindex="-1" id="selE04" style="display: none;">

									<option value=""></option>

									<optgroup label="NFC EAST">

										<option>Dallas Cowboys</option>

										<option>New York Giants</option>

										<option>Philadelphia Eagles</option>

										<option>Washington Redskins</option>

									</optgroup>

									<optgroup label="NFC NORTH">

										<option selected="">Chicago Bears</option>

										<option>Detroit Lions</option>

										<option>Green Bay Packers</option>

										<option>Minnesota Vikings</option>

									</optgroup>

									<optgroup label="NFC SOUTH">

										<option>Atlanta Falcons</option>

										<option selected="">Carolina Panthers</option>

										<option>New Orleans Saints</option>

										<option>Tampa Bay Buccaneers</option>

									</optgroup>

									<optgroup label="NFC WEST">

										<option>Arizona Cardinals</option>

										<option>St. Louis Rams</option>

										<option>San Francisco 49ers</option>

										<option>Seattle Seahawks</option>

									</optgroup>

									<optgroup label="AFC EAST">

										<option>Buffalo Bills</option>

										<option>Miami Dolphins</option>

										<option>New England Patriots</option>

										<option>New York Jets</option>

									</optgroup>

									<optgroup label="AFC NORTH">

										<option>Baltimore Ravens</option>

										<option>Cincinnati Bengals</option>

										<option>Cleveland Browns</option>

										<option>Pittsburgh Steelers</option>

									</optgroup>

									<optgroup label="AFC SOUTH">

										<option>Houston Texans</option>

										<option>Indianapolis Colts</option>

										<option>Jacksonville Jaguars</option>

										<option>Tennessee Titans</option>

									</optgroup>

									<optgroup label="AFC WEST">

										<option>Denver Broncos</option>

										<option>Kansas City Chiefs</option>

										<option>Oakland Raiders</option>

										<option>San Diego Chargers</option>

									</optgroup>

								</select><div id="selE04_chzn" class="chzn-container chzn-container-multi" style="width: 427px;"><ul class="chzn-choices"><li class="search-choice" id="selE04_chzn_c_7"><span>Chicago Bears</span><a href="javascript:void(0)" class="search-choice-close" rel="7"></a></li><li class="search-choice" id="selE04_chzn_c_13"><span>Carolina Panthers</span><a href="javascript:void(0)" class="search-choice-close" rel="13"></a></li><li class="search-field"><input type="text" value="" class="" autocomplete="off" style="width: 25px;" tabindex="6"></li></ul><div class="chzn-drop" style="left: -9000px; width: 425px; top: 33px;"><ul class="chzn-results"><li id="selE04_chzn_g_1" class="group-result" style="display: list-item;">NFC EAST</li><li id="selE04_chzn_o_2" class="active-result group-option" style="">Dallas Cowboys</li><li id="selE04_chzn_o_3" class="active-result group-option" style="">New York Giants</li><li id="selE04_chzn_o_4" class="active-result group-option" style="">Philadelphia Eagles</li><li id="selE04_chzn_o_5" class="active-result group-option" style="">Washington Redskins</li><li id="selE04_chzn_g_6" class="group-result" style="display: list-item;">NFC NORTH</li><li id="selE04_chzn_o_7" class="result-selected group-option" style="">Chicago Bears</li><li id="selE04_chzn_o_8" class="active-result group-option" style="">Detroit Lions</li><li id="selE04_chzn_o_9" class="active-result group-option" style="">Green Bay Packers</li><li id="selE04_chzn_o_10" class="active-result group-option" style="">Minnesota Vikings</li><li id="selE04_chzn_g_11" class="group-result" style="display: list-item;">NFC SOUTH</li><li id="selE04_chzn_o_12" class="active-result group-option" style="">Atlanta Falcons</li><li id="selE04_chzn_o_13" class="result-selected group-option" style="">Carolina Panthers</li><li id="selE04_chzn_o_14" class="active-result group-option" style="">New Orleans Saints</li><li id="selE04_chzn_o_15" class="active-result group-option" style="">Tampa Bay Buccaneers</li><li id="selE04_chzn_g_16" class="group-result" style="display: list-item;">NFC WEST</li><li id="selE04_chzn_o_17" class="active-result group-option" style="">Arizona Cardinals</li><li id="selE04_chzn_o_18" class="active-result group-option" style="">St. Louis Rams</li><li id="selE04_chzn_o_19" class="active-result group-option" style="">San Francisco 49ers</li><li id="selE04_chzn_o_20" class="active-result group-option" style="">Seattle Seahawks</li><li id="selE04_chzn_g_21" class="group-result" style="display: list-item;">AFC EAST</li><li id="selE04_chzn_o_22" class="active-result group-option" style="">Buffalo Bills</li><li id="selE04_chzn_o_23" class="active-result group-option" style="">Miami Dolphins</li><li id="selE04_chzn_o_24" class="active-result group-option" style="">New England Patriots</li><li id="selE04_chzn_o_25" class="active-result group-option" style="">New York Jets</li><li id="selE04_chzn_g_26" class="group-result" style="display: list-item;">AFC NORTH</li><li id="selE04_chzn_o_27" class="active-result group-option" style="">Baltimore Ravens</li><li id="selE04_chzn_o_28" class="active-result group-option" style="">Cincinnati Bengals</li><li id="selE04_chzn_o_29" class="active-result group-option" style="">Cleveland Browns</li><li id="selE04_chzn_o_30" class="active-result group-option" style="">Pittsburgh Steelers</li><li id="selE04_chzn_g_31" class="group-result" style="display: list-item;">AFC SOUTH</li><li id="selE04_chzn_o_32" class="active-result group-option" style="">Houston Texans</li><li id="selE04_chzn_o_33" class="active-result group-option" style="">Indianapolis Colts</li><li id="selE04_chzn_o_34" class="active-result group-option" style="">Jacksonville Jaguars</li><li id="selE04_chzn_o_35" class="active-result group-option" style="">Tennessee Titans</li><li id="selE04_chzn_g_36" class="group-result" style="display: list-item;">AFC WEST</li><li id="selE04_chzn_o_37" class="active-result group-option" style="">Denver Broncos</li><li id="selE04_chzn_o_38" class="active-result group-option" style="">Kansas City Chiefs</li><li id="selE04_chzn_o_39" class="active-result group-option" style="">Oakland Raiders</li><li id="selE04_chzn_o_40" class="active-result group-option" style="">San Diego Chargers</li></ul></div></div>

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
                jqTds[4].innerHTML = memSelect.getMultiLine();

                var jqInputs = $('input', nRow);
                jqInputs[1].value = aData[3];
                jqInputs[2].value = aData[4];

                $("select :contains('" + aData[2] + "')", nRow).attr("selected", true);

                jqTds[5].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[6].innerHTML = '<a class="cancel" href="">Cancel</a>';
            }

            //save row after server return
            function saveRow2(oTable, nRow, sem) {
                oTable.fnUpdate(sem["Id"], nRow, 0, false);
                var name = '<a href="./seminar_record.aspx?id=' + sem["Id"] + '">' + getStr(sem["Name"]) + '</a>';
                oTable.fnUpdate(name, nRow, 1, false);
                oTable.fnUpdate(sem["Day"], nRow, 2, false);
                oTable.fnUpdate(sem["BeginTime"], nRow, 3, false);
                oTable.fnUpdate(sem["EndTime"], nRow, 4, false);
                oTable.fnUpdate(sem["Participators"], nRow, 5, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 7, false);
                oTable.fnDraw();
            }

            function saveRow(oTable, nRow, sem) {
                oTable.fnUpdate(jqInputs[0].value, nRow, 1, false);
                oTable.fnUpdate(jqSelect.find("option:selected").text(), nRow, 2, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 3, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 4, false);
                oTable.fnUpdate(jqInputs[4].value, nRow, 5, false);
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
                var jqSelect = $('select', nRow);

                var aData = oTable.fnGetData(nRow);
                var sem = {};
                sem["Id"] = getInt(aData[0]);
                sem["Name"] = jqInputs[0].value;
                sem["Day"] = jqSelect.find("option:selected").text();
                sem["BeginTime"] = jqInputs[2].value;
                sem["EndTime"] = jqInputs[3].value;
                sem["Participators"] = jqInputs[3].value;
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
                                , getStr(sem["Participators"])
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
                var aiNew = oTable.fnAddData(['', '', '', '','','',
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