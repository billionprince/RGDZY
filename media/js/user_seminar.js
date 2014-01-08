var SeminarTable = function () {

    return {

        //main function to initiate the module
        init: function () {

            function getArray(str) {
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


            function getUserSeminars() {
                $.ajax({
                    type: "POST",
                    url: "data/SeminarHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getUserSeminars',
                        user: $(".username").html()

                    },
                    success: function (data, textStatus) {
                        //alert(data);
                        oTable.fnClearTable();
                        $(data).each(function (index, sem) {
                            oTable.fnAddData([parseInt(sem["Id"])
                                //, getStr(sem["Name"])
                                , '<a href="./seminar_record.aspx?id=' + sem["Id"] + '">' + getStr(sem["Name"]) + '</a>'
                                , getStr(sem["Day"])
                                , getStr(sem["BeginTime"])
                                , getStr(sem["EndTime"])
                                , getStr(sem["Participator"])
                            ]);
                        })
                        oTable.fnDraw();

                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            getUserSeminars();

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                "bAutoWidth": false,  //自适应宽度
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",

                "sScrollX": "100%",

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

            var handlePortletTools = function () {

                jQuery('body').on('click', '.portlet .tools a.reload', function (e) {
                    e.preventDefault();
                    var el = jQuery(this).parents(".portlet");
                    App.blockUI(el);
                    getUserSeminars();
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