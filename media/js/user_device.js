
var TableEditable = function () {

    return {

        //main function to initiate the module
        init: function () {
            function restoreRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTable.fnUpdate(aData[i], nRow, i, false);
                }

                oTable.fnDraw();
            }

            function editRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text" class="m-wrap small" value="' + aData[0] + '">';
                jqTds[1].innerHTML = '<input type="text" class="m-wrap small" value="' + aData[1] + '">';
                jqTds[2].innerHTML = '<input type="text" class="m-wrap small" value="' + aData[2] + '">';
                jqTds[3].innerHTML = '<input type="text" class="m-wrap small" value="' + aData[3] + '">';
                jqTds[4].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[5].innerHTML = '<a class="cancel" href="">Cancel</a>';
            }

            function saveRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 4, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 5, false);
                oTable.fnDraw();
            }

            function cancelEditRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 4, false);
                oTable.fnDraw();
            }

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "bAutoWidth": false,  //自适应宽度
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

            var isCreate = true;//mark create a new device or edit a deivce

            function ChangeDateFormat(jsondate) {
                if (jsondate == null) {
                    jsondate = "";
                    return jsondate;
                }
                jsondate = jsondate.replace("/Date(", "").replace(")/", "");
                if (jsondate.indexOf("+") > 0) {
                    jsondate = jsondate.substring(0, jsondate.indexOf("+"));
                }
                else if (jsondate.indexOf("-") > 0) {
                    jsondate = jsondate.substring(0, jsondate.indexOf("-"));
                }

                var date = new Date(parseInt(jsondate, 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                return date.getFullYear() + "-" + month + "-" + currentDate;
            }


            function ChangeDateFormat2(jsondate) {
                if (jsondate == null) {
                    return jsondate;
                }
                return '/Date(' + jsondate.getTime() + '+0800)/';
            }

            function getStr(data) {
                if (data == null)
                    return '';
                else
                    return data;
            }

            function getDate(id) {
                if ($(id).val() != '')
                    return new Date($(id).val());
                else
                    return null;
            }

            function getDate2(data) {
                if (data != '')
                    return new Date(data);
                else
                    return null;
            }

            function getUserDevices(func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getUserDevices',
                        name: $('.username').html()
                    },
                    success: function (data, textStatus) {

                        oTable.fnClearTable();

                        $(data).each(function (index, item) {

                            var device = item["dev"];
                            var deviceUse = item["devUse"];
                            oTable.fnAddData([parseInt(device["Id"])
                                , getStr(device["AssetNum"])
                                , getStr(device["Type"])
                                , getStr(device["Version"])
                                , getStr(device["MAC"])
                                , getStr(device["Cpu"])
                                , getStr(device["Memory"])
                                , getStr(device["Disk"])
                                , ChangeDateFormat(device["PurchaseDate"])
                                , getStr(deviceUse["UserId"])
                                , ChangeDateFormat(deviceUse["StartDate"])
                                , ChangeDateFormat(deviceUse["EndDate"])
                                , getStr(device["Remark"])
                                , "<a class='powerOn' href=\"javascript:\">Power On</a>"
                            ]);
                        })

                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            getUserDevices(null);

            function poweronDevice(id) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'PowerOnDevice',
                        id: parseInt(id)
                    },
                    success: function (rec) {
                        //alert("PowerOnDevice success");
                    },

                    error: function (rec) {
                        //alert("PowerOnDevice error!");
                    }
                });
            }

            $('#sample_editable_1 a.powerOn').live('click', function (e) {
                e.preventDefault();                

                var nRow = $(this).parents('tr')[0];
                var aData = oTable.fnGetData(nRow);
                poweronDevice(aData[0]);
            });

            var handlePortletTools = function () {

                jQuery('body').on('click', '.portlet .tools a.reload', function (e) {
                    e.preventDefault();
                    var el = jQuery(this).parents(".portlet");
                    App.blockUI(el);
                    getUserDevices(null);
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

var UserDevice = function () {
    return {
        init: function () {
            TableEditable.init();
        }
    }
}();