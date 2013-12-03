
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

            var nEditing = null;

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

            function getALLDevices(func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllDevices',
                        parameter: null
                    },
                    success: function (data, textStatus) {

                        $(data).each(function (index, item) {

                            var device = item["dev"];
                            var deviceUse = item["devUse"];
                            oTable.fnAddData([parseInt(device["Id"])
                                , getStr(device["AssetNum"])
                                , getStr(device["Type"])
                                , getStr(device["Version"])
                                , getStr(device["Cpu"])
                                , getStr(device["Memory"])
                                , getStr(device["Disk"])
                                , ChangeDateFormat(device["PurchaseDate"])
                                , getStr(deviceUse["UserId"])
                                , ChangeDateFormat(deviceUse["StartDate"])
                                , ChangeDateFormat(deviceUse["EndDate"])
                                , getStr(device["Remark"])
                                , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>', '<a class="delete" data-mode="new" href="#form_modal1" data-toggle="modal">Delete</a>'
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

            getALLDevices(null);

            function addDevice(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addDevice',
                        parameter: dataStr
                    },
                    success: function (data, textStatus) {

                        var device = data["dev"];
                        var deviceUse = data["devUse"];

                        if (func != null)
                            func();

                        oTable.fnAddData([parseInt(device["Id"])
                            , getStr(device["AssetNum"])
                            , getStr(device["Type"])
                            , getStr(device["Version"])
                            , getStr(device["Cpu"])
                            , getStr(device["Memory"])
                            , getStr(device["Disk"])
                            , ChangeDateFormat(device["PurchaseDate"])
                            , getStr(deviceUse["UserId"])
                            , ChangeDateFormat(deviceUse["StartDate"])
                            , ChangeDateFormat(deviceUse["EndDate"])
                            , getStr(device["Remark"])
                            ,'<a class="edit" href="">Edit</a>', '<a class="delete" data-mode="new" href="">Delete</a>'
                        ]);
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }


            $('#save').click(function (e) {
                var dev = {};
                var devUse = {};
                dev['AssetNum'] = $('#assetNum').val();
                dev['Type'] = $('#type').val();
                dev['Version'] = $('#version').val();
                dev['Cpu'] = $('#cpu').val();
                dev['Memory'] = $('#memory').val();
                dev['Disk'] = $('#disk').val();
                dev['PurchaseDate'] = ChangeDateFormat2(getDate('#purchaseDate'));
                dev['Remark'] = $('#remark').val();
                devUse['UserId'] = $('#user').find('option:selected').text();
                devUse['StartDate'] = ChangeDateFormat2(getDate('#startDate'));
                devUse['EndDate'] = ChangeDateFormat2(getDate('#endDate'));

                var union = {};
                union['dev'] = dev;
                union['devUse'] = devUse;

                var data = JSON.stringify(union);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                addDevice(data, null);

            });

            $('#sample_editable_1_new').click(function (e) {
                e.preventDefault();
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                oTable.fnDeleteRow(nRow);
                alert("Deleted! Do not forget to do some ajax to sync with backend :)");
            });

            $('#sample_editable_1 a.cancel').live('click', function (e) {
                e.preventDefault();
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

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];

                if (nEditing !== null && nEditing != nRow) {
                    /* Currently editing - but not this row - restore the old before continuing to edit mode */
                    restoreRow(oTable, nEditing);
                    editRow(oTable, nRow);
                    nEditing = nRow;
                } else if (nEditing == nRow && this.innerHTML == "Save") {
                    /* Editing this row and want to save it */
                    saveRow(oTable, nEditing);
                    nEditing = null;
                    alert("Updated! Do not forget to do some ajax to sync with backend :)");
                } else {
                    /* No edit in progress - let's start one */
                    editRow(oTable, nRow);
                    nEditing = nRow;
                }
            });
        }

    };

}();

var Device = function () {
    return {
        init: function () {
            TableEditable.init();
        }
    }
}();