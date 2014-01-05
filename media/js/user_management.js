
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
                "aoColumnDefs": [{
                    'aTargets': [0],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    "aTargets": [9],
                    "type": 'textarea', // not activated
                    "sWidth": '30%', // not activated
                    "height": '3', // not activated
                },
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

            function getAllUsers(func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllUsers',
                        parameter: null
                    },
                    success: function (data, textStatus) {
                        $(data).each(function (index, u) {
                            // A hidden column when borrowing device_list, filling a blank col. First col will hide even without style
                            oTable.fnAddData([parseInt("0")
                                , getStr(u["Name"])
                                , getStr(u["RealName"])
                                , getStr(u["StudentId"])
                                , getStr(u["Email"])
                                , getStr(u["Phone"])
                                , getStr(u["Hometown"])
                                , ChangeDateFormat(u["Birthday"])
                                , getStr(u["University"])
                                , '<div style="max-height: 80px; overflow-y: auto;">' + getStr(u["Introduction"]) + '</div>'
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

            getAllUsers(null);

            //add User
            function addUser(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addUser',
                        parameter: dataStr,
                        create: isCreate 
                    },
                    success: function (u, textStatus) {

                        if (func != null)
                            func();

                        if (u["r"] != "s")
                        {
                            alert("Add user failed...");
                            return;
                        }

                        oTable.fnAddData([parseInt("0") 
                                 , getStr(u["Name"])
                                 , getStr(u["RealName"])
                                 , getStr(u["StudentId"])
                                 , getStr(u["Email"])
                                 , getStr(u["Phone"])
                                 , getStr(u["Hometown"])
                                 , ChangeDateFormat(u["Birthday"])
                                 , getStr(u["University"])
                                 , '<div style="max-height: 80px; overflow-y: auto;">' + getStr(u["Introduction"]) + '</div>'
                                 , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                                 , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                        ]);
                    },

                    error: function (rec) {
                        alert("Adding new user failed...\nReason: " + rec.responseText);
                        //alert(rec.responseText);
                    }
                });
            }

            //edit User
            function editUser(dataStr, func) {
                var pedited = "true";
                if ($('#Password').val() === null || $('#Password').val() == "undefined" || $('#Password').val() == "")
                {
                    pedited = "false";
                }
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editUser',
                        parameter: dataStr,
                        passedit: pedited,
                    },
                    success: function (u, textStatus) {

                        if (func != null)
                            func();

                        var aData = oTable.fnGetData(nEditing);
                        //alert(aData[1]);
                        
                        aData[0] = 0;
                        aData[1] = getStr(u["Name"]);
                        aData[2] = getStr(u["RealName"]);
                        aData[3] = getStr(u["StudentId"]);
                        aData[4] = getStr(u["Email"]);
                        aData[5] = getStr(u["Phone"]);
                        aData[6] = getStr(u["Hometown"]);
                        aData[7] = ChangeDateFormat(u["Birthday"]);
                        aData[8] = getStr(u["University"]);
                        aData[9] = '<div style="max-height: 80px; overflow-y: auto;">' + getStr(u["Introduction"]) + '</div>'

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            //delete User
            function deleteUser(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deleteUser',
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

            //$('#save').attr('disabled', 'true');
            $('#Retype-info').hide();
            var retype = true;
            function RetypeWarning()
            {
                if ($('#RetypePassword').val() != $('#Password').val()) {
                    if (retype == true) {
                        $('#Retype-info').fadeIn();
                        $('#save').attr('disabled', 'true');
                    }
                    retype = false;
                }
                else {
                    if (retype == false) {
                        $('#Retype-info').fadeOut();
                        $('#save').removeAttr('disabled');
                    }
                    retype = true;
                }
            }
            $('#Password').live('change mouseleave', function (e) {
                RetypeWarning(null);
            });
            $('#RetypePassword').live('change focus mouseleave', function (e) {
                RetypeWarning(null);
            });
            $('#save').live('focus', function (e) {
                RetypeWarning(null);
            });

            $('#save').live('click', function (e) {
                var u = {};
                u['Name'] = $('#UserName').val();
                u['Password'] = CryptoJS.SHA1($('#Password').val()).toString(),
                u['RealName'] = $('#RealName').val();
                u['StudentId'] = $('#StudentId').val();
                u['Email'] = $('#Email').val();
                u['Phone'] = $('#Phone').val();
                u['Hometown'] = $('#Hometown').val();
                u['Birthday'] = ChangeDateFormat2(getDate('#Birthday'));
                u['University'] = $('#University').val();
                // remove wrap
                var intro_tmp = $('#Introduction').val();
                var last_tmp = intro_tmp.indexOf('</div>');
                var first_tmp = intro_tmp.indexOf('>') + 1;
                if (first_tmp != 0) {
                    var trunc_intro = intro_tmp.substring(first_tmp, last_tmp);
                    u['Introduction'] = trunc_intro;
                }
                else {
                    u['Introduction'] = intro_tmp;
                }
                var data = JSON.stringify(u);
                //alert("save live:" + data);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                //alert(data);
                if (isCreate) {
                    addUser(data, null);
                } else {
                    editUser(data, null);
                }
            });

            $('#sample_editable_1_new').live('click', function (e) {
                e.preventDefault();
                $("#u_form")[0].reset();
                isCreate = true;
                $("#UserName").removeAttr("disabled");
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                var aData = oTable.fnGetData(nRow);
                var u = {};
                //u['BlankId'] = aData[0];
                u['Name'] = aData[1];
                u['RealName'] = aData[2];
                u['StudentId'] = aData[3];
                u['Email'] = aData[4];
                u['Phone'] = aData[5];
                u['Hometown'] = aData[6];
                u['Birthday'] = ChangeDateFormat2(getDate2(aData[7]));
                u['University'] = aData[8];
                // remove wrap
                var intro_tmp = aData[9];
                var last_tmp = intro_tmp.indexOf('</div>');
                var first_tmp = intro_tmp.indexOf('>') + 1;
                if (first_tmp != 0) {
                    var trunc_intro = intro_tmp.substring(first_tmp, last_tmp);
                    u['Introduction'] = trunc_intro;
                }
                else {
                    u['Introduction'] = intro_tmp;
                }
                var data = JSON.stringify(u);
                
                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");
                //alert(data);

                deleteUser(data);

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
                $("#UserName").attr('disabled', 'disabled');

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                $('#BlankId').val(aData[0]);
                $('#UserName').val(aData[1]);
                $('#RealName').val(aData[2]);
                $('#StudentId').val(aData[3]);
                $('#Email').val(aData[4]);
                $('#Phone').val(aData[5]);
                $('#Hometown').val(aData[6]);
                $('#Birthday').val(aData[7]);
                $('#University').val(aData[8]);
                // remove wrap
                var intro_tmp = aData[9];
                var last_tmp = intro_tmp.indexOf('</div>');
                var first_tmp = intro_tmp.indexOf('>') + 1;
                if (first_tmp != 0) {
                    var trunc_intro = intro_tmp.substring(first_tmp, last_tmp);
                    $('#Introduction').val(trunc_intro);
                }
                else {
                    $('#Introduction').val(intro_tmp);
                }

            });
        }

    };

}();

var UserMan = function () {
    return {
        init: function () {
            TableEditable.init();
        }
    }
}();