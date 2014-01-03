
var TableEditable = function () {

    return {

        //main function to initiate the module
        init: function () {

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                "bAutoWidth": false,  //自适应宽度
                "iDisplayLength": 5,
                "sScrollX": "10%",    //Scroll on x-axis
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
                return  month + "/" + currentDate + "/" + date.getFullYear();
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

            function getInt(data) {
                if (data == null || data == '')
                    return 0;
                else
                    return data;
            }

            function getAllDevices(func) {
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
                                , getStr(device["MAC"])
                                , getStr(device["Cpu"])
                                , getStr(device["Memory"])
                                , getStr(device["Disk"])
                                , ChangeDateFormat(device["PurchaseDate"])
                                , getStr(deviceUse["UserId"])
                                , ChangeDateFormat(deviceUse["StartDate"])
                                , ChangeDateFormat(deviceUse["EndDate"])
                                , getStr(device["Remark"])
                                , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                                , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                            ]);
                        })

                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        //
                        (rec.responseText);
                    }
                });
            }

            getAllDevices(null);

            function getAllUsers(func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllUsers',
                        parameter: null
                    },
                    success: function (data, textStatus) {
                        $("#user").empty();
                        $("#user").append("<option value=''></option>");
                        $(data).each(function (index, item) {
                            $("#user").append("<option value='"+item.Name+"'>"+item.Name+"</option>");
                        })

                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }
            getAllUsers(null);

            //add device
            function addDevice(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addDevice',
                        parameter: dataStr,
                        create: isCreate 
                    },
                    success: function (data, textStatus) {
                        var device = data["dev"];
                        var deviceUse = data["devUse"];

                        if (func != null)
                            func();

                        var startDate = ChangeDateFormat(deviceUse["StartDate"]);
                        var endDate = ChangeDateFormat(deviceUse["EndDate"]);
                        if (deviceUse["UserId"] == "") {
                            startDate = "";
                            endDate = "";
                        }
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
                            , startDate
                            , endDate
                            , getStr(device["Remark"])
                            , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                            , '<a class="delete" href = "javascript:">Delete</a>'
                        ]);

                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            //edit device
            function editDevice(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editDevice',
                        parameter: dataStr,
                    },
                    success: function (data, textStatus) {
                        var device = data["dev"];
                        var deviceUse = data["devUse"];

                        if (func != null)
                            func();

                        var startDate = ChangeDateFormat(deviceUse["StartDate"]);
                        var endDate = ChangeDateFormat(deviceUse["EndDate"]);
                        if (deviceUse["UserId"] == "") {
                            startDate = "";
                            endDate = "";
                        }
                        var aData = oTable.fnGetData(nEditing);
                        //alert(aData[1]);
                        aData[0] = parseInt(device["Id"])
                        aData[1] = getStr(device["AssetNum"]);
                        aData[2] = getStr(device["Type"]);
                        aData[3] = getStr(device["Version"]);
                        aData[4] = getStr(device["MAC"]);
                        aData[5] = getStr(device["Cpu"]);
                        aData[6] = getStr(device["Memory"]);
                        aData[7] = getStr(device["Disk"]);
                        aData[8] = ChangeDateFormat(device["PurchaseDate"]);
                        aData[9] = getStr(deviceUse["UserId"]);
                        aData[10] = startDate;
                        aData[11] = endDate;
                        aData[12] = getStr(device["Remark"]);

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            //edit device
            function deleteDevice(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/DeviceHandler.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deleteDevice',
                        parameter: dataStr,
                    },
                    success: function (data, textStatus) {
                        //alert(JSON.stringify(data));

                        if (func != null)
                            func();
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            $('#save').live('click', function (e) {
                var dev = {};
                var devUse = {};
                dev['Id'] = getInt($('#devId').val());
                dev['AssetNum'] = $('#assetNum').val();
                dev['Type'] = $('#type').val();
                dev['Version'] = $('#version').val();
                dev['MAC'] = $('#MAC').val();
                dev['Cpu'] = $('#cpu').val();
                dev['Memory'] = $('#memory').val();
                dev['Disk'] = $('#disk').val();
                dev['PurchaseDate'] = ChangeDateFormat2(getDate('#purchaseDate'));
                dev['Remark'] = $('#remark').val();
                devUse['DeviceId'] = getInt($('#devId').val());
                devUse['UserId'] = $('#user').find('option:selected').text();
                devUse['StartDate'] = ChangeDateFormat2(getDate('#startDate'));
                devUse['EndDate'] = ChangeDateFormat2(getDate('#endDate'));

                var union = {};
                union['dev'] = dev;
                union['devUse'] = devUse;

                var data = JSON.stringify(union);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                //alert(data);
                if (isCreate) {
                    addDevice(data, null);
                } else {
                    editDevice(data,null);
                }
            });

            $('#sample_editable_1_new').live('click', function (e) {
                e.preventDefault();
                $("#dev_form")[0].reset();
                isCreate = true;
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                var aData = oTable.fnGetData(nRow);
                var dev = {};
                var devUse = {};
                dev['Id'] = parseInt(aData[0]);
                dev['AssetNum'] = aData[1];
                dev['Type'] = aData[2];
                dev['Version'] = aData[3];
                dev['MAC'] = aData[4];
                dev['Cpu'] = aData[5];
                dev['Memory'] = aData[6];
                dev['Disk'] = aData[7];
                dev['PurchaseDate'] = ChangeDateFormat2(getDate2(aData[8]));
                
                devUse['DeviceId'] = parseInt(aData[0]);
                devUse['UserId'] = aData[9];
                devUse['StartDate'] = ChangeDateFormat2(getDate2(aData[10]));
                devUse['EndDate'] = ChangeDateFormat2(getDate2(aData[11]));
                dev['Remark'] = aData[12];
                var union = {};
                union['dev'] = dev;
                union['devUse'] = devUse;

                var data = JSON.stringify(union);
                
                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                //alert(data);

                deleteDevice(data);

                oTable.fnDeleteRow(nRow);
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

                $("#dev_form")[0].reset();
                isCreate = false;

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                $('#devId').val(aData[0]);
                $('#assetNum').val(aData[1]);
                $('#type').val(aData[2]);
                $('#version').val(aData[3]);
                $('#MAC').val(aData[4]);
                $('#cpu').val(aData[5]);
                $('#memory').val(aData[6]);
                $('#disk').val(aData[7]);
                $('#purchaseDate').val(aData[8]);
                $('#user').val(aData[9]);
                $('#startDate').val(aData[10]);
                $('#endDate').val(aData[11]);
                $('#remark').val(aData[12]);
                $("#user").change();
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