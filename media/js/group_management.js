
var TableEditable = function () {

    return {

        //main function to initiate the module
        init: function () {

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                "bAutoWidth": false,
                "iDisplayLength": 5,
                "sScrollX": "100%",
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r><'datatable-scroll't><'row-fluid'<'span6'i><'span6'p>>",
                //"sDom": "r<'H'lf><'datatable-scroll't><'F'ip>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [
                {
                    "aTargets": ["_all"],
                    /*"mRender": function (data, type, full) {
                        return 200;
                    }*/
                }]
            });

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialzie select2 dropdown

            var nEditing = 0;

            var isCreate = true;//mark create a new user or edit a user

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
                    // For server side json parser logic (in login.ashx addUser()), DataTime cannot be null, or throwing an exception..
                    //alert('/Date(' + '0' + '+0800)/');
                    return ('/Date(' + '0' + '+0800)/');
                    //return jsondate;
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

            function getAllGroups(func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllGroups',
                        parameter: null
                    },
                    success: function (data, textStatus) {
                        $(data).each(function (index, u) {
                            // A hidden column when borrowing device_list, filling a blank col. First col will hide even without style
                            oTable.fnAddData([
                                  getStr(u["Groupname"])
                                , getStr(u["Username"])
                                , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                                //, '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
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

            getAllGroups(null);

            //add G
            function addGroup(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addGroup',
                        parameter: dataStr,
                        create: isCreate 
                    },
                    success: function (u, textStatus) {

                        if (func != null)
                            func();

                        if (u["r"] != "s")
                        {
                            alert("Add group failed...");
                            return;
                        }

                        oTable.fnAddData([
                                   getStr(u["Groupname"])
                                 , getStr(u["Username"])
                                 , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                                 //, '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                        ]);
                    },

                    error: function (rec) {
                        alert("Adding new group-user relation failed...\nReason: " + rec.responseText);
                        //alert(rec.responseText);
                    }
                });
            }

            //edit G
            function editUser(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editGroup',
                        parameter: dataStr,
                    },
                    success: function (u, textStatus) {

                        if (func != null)
                            func();

                        var aData = oTable.fnGetData(nEditing);
                        
                        aData[0] = getStr(u["Groupname"]);;
                        aData[1] = getStr(u["Username"]);

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            //delete G
            function deleteGroup(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deleteGroup',
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
                var u = {};
                u['Username'] = $('#UserName').val();
                u['Groupname'] = $('#GroupName').val();
                
                var data = JSON.stringify(u);
                //alert("save live:" + data);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                //alert(data);
                if (isCreate) {
                    addGroup(data, null);
                } else {
                    editGroup(data, null);
                }
            });

            $('#sample_editable_1_new').live('click', function (e) {
                e.preventDefault();
                $("#u_form")[0].reset();
                isCreate = true;
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                var aData = oTable.fnGetData(nRow);
                var u = {};
                u['Groupname'] = aData[0];
                u['Username'] = aData[1];

                var data = JSON.stringify(u);
                
                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                //alert(data);

                deleteGroup(data);

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

                $("#u_form")[0].reset();
                isCreate = false;

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                $('#GroupName').val(aData[0]);
                $('#UserName').val(aData[1]);
            });
        }

    };

}();

var Group = function () {
    return {
        init: function () {
            TableEditable.init();
        }
    }
}();