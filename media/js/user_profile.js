var Profile = function () {

    // author: http://www.cnblogs.com/jiekk/archive/2011/06/28/2092444.html
    function GetRequest() {
        var url = location.search; // fetch strings after "?" in url
        var theRequest = new Object();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            }
        }
        return theRequest;
    }

    // original author: http://blog.sina.com.cn/s/blog_5a08ac3d0100dx03.html
    function fillSpanWithTextNode(spanId, text) {
        var tSpan = document.getElementById(spanId);
        // remove all built Text Nodes first
        while (tSpan.hasChildNodes()) {
            tSpan.removeChild(tSpan.childNodes[0]);
        }

        var tNode = document.createTextNode(text);
        tSpan.appendChild(tNode);
    }

    var Request = new Object();
    Request = GetRequest();
    var param1;
    param1 = Request["action"];

    if (param1 == "redirect") {
        fillSpanWithTextNode("responseSpan", "Please Login.");
        $('.alert-login', $('.profile-form')).show();
    }
    else if (param1 == "failed") {
        fillSpanWithTextNode("responseSpan", "Wrong Username/Password Combination. Try again.");
        $('.alert-login', $('.profile-form')).show();
    }

    return {
        //main function to initiate the module
        init: function () {
        }
    };
}();

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

$(document).ready(function () {
    var myname;
    jcrop_init = false;
    $.ajax({
        url: "data/login.ashx",
        type: "POST",
        dataType: "json",
        data: {
            command: "load_user_settings"
        },
        success: function (rec) {
            $('#RealName').attr("value", rec.RealName);
            $('#StudentId').attr("value", rec.StudentId);
            $('#Email').attr("value", rec.Email);
            $('#Phone').attr("value", rec.Phone);
            $('#Hometown').attr("value", rec.Hometown);
            $('#Birthday').attr("value", ChangeDateFormat(rec.Birthday));
            $('#Link').attr("value", rec.Link);
            $('#University').attr("value", rec.University);
            $('#Introduction').text(rec.Introduction);
            d = new Date();
            myname = rec.Name;
            $('#profile-avatar-img').attr("src", "user_data/" + rec.Name + "/a_" + rec.Name + ".jpg?" + d.getTime());
            $('#header-logo').attr("src", "user_data/" + rec.Name + "/a_" + rec.Name + ".jpg?" + d.getTime());
        },
        error: function () {
            alert("load user error!");
        }
    });
    $(".submit-btn .btn.green.button_changepwd").on('click', function () {
        $.ajax({
            url: "data/login.ashx",
            type: "POST",
            dataType: "json",
            data: {
                command: "change_password",
                oldpwd: CryptoJS.SHA1($('#oldpwd').val()).toString(),
                newpwd1: CryptoJS.SHA1($('#newpwd1').val()).toString(),
                newpwd2: CryptoJS.SHA1($('#newpwd2').val()).toString()
            },
            success: function (rec) {
                alert(rec);
            },
            error: function (rec) {
                alert(rec);
            }
        });
    });
    $(".submit-btn .btn.green.button_submit").on('click', function () {
        $.ajax({
            url: "data/login.ashx",
            type: "POST",
            dataType: "json",
            data: {
                command: "save_user_settings",
                RealName: $('#RealName').val(),
                StudentId: $('#StudentId').val(),
                Email: $('#Email').val(),
                Phone: $('#Phone').val(),
                Hometown: $('#Hometown').val(),
                Birthday: $('#Birthday').val(),
                Link: $('#Link').val(),
                University: $('#University').val(),
                Introduction: $('#Introduction').val()
            },
            success: function (rec) {
                var rtv =
					"<div class=\"span8 profile-info\">" +
                        "<h1>" + rec.RealName + "</h1>" +
                        "<p>" + rec.Introduction + "</p>" +
					    "<p><a href=\"" + rec.Link + "\">" + rec.Link + "</a></p>" +
                        "<ul class=\"unstyled inline\">" +
                            "<li><i class=\"icon-home\"></i>" + rec.Hometown + "</li>" +
                            "<li><i class=\"icon-calendar\"></i>" + ChangeDateFormat(rec.Birthday) + "</li>" +                            "<li><i class=\"icon-calendar\"></i>" + rec.University + "</li>" +                            "<li><i class=\"icon-envelope-alt\"></i>" + rec.Email + "</li>" +                            "<li><i class=\"icon-th\"></i>" + rec.Phone + "</li>" +
                        "</ul>" +
                    "</div>";
                var oc = $("#userinfo").html("");
                $("#userinfo").append(rtv);
            },
            error: function () {
                alert("save user error!");
            }
        });
    });
    $(".submit-btn .btn.green.avatar_submit").on('click', function () {
        if (jcrop_init == false) {
            alert("No Avatar!");
            return;
        }
        $('#avatar-form').submit();
    });
});

jQuery(function ($) {

    // Create variables (in this scope) to hold the API and image size
    var jcrop_api,
        boundx,
        boundy;

    var $preview,
        $pcnt,
        $pimg,
        xsize,
        ysize;
    jcrop_init = false;

    $('#avatar-preview').change(function () {
        $('#avatar-preview').show();
        $('#avatar-submit').show();
        initJcrop();
        $('.avatar_submit').show();
        $('.avatar_cancel').show();
    });

    //initJcrop();
    function initJcrop()//{{{
    {
        if (jcrop_init == true) {
            destroyJcrop();
        }
        //$(".jcrop-assist-avatar-preview img").Jcrop({
        $("#target-avatar").Jcrop({
            onChange: updateCoord,
            onSelect: updateCoord,
            //aspectRatio: xsize / ysize
            aspectRatio: 1
        }, function () {
            // Use the API to get the real image size
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            jcrop_api = this;

            jcrop_api.setOptions({ allowResize: true });
            jcrop_api.setOptions({ allowSelect: true });
            jcrop_api.setOptions({ aspectRatio: 1 / 1 });
            jcrop_api.animateTo([0, 0, 200, 200]);
            jcrop_api.focus();
            jcrop_init = true;
        });
    };

    function destroyJcrop() {
        jcrop_api.destroy();
        jcrop_init = false;
    }

    function updateCoord(c) {
        $('#x1').val(c.x);
        $('#y1').val(c.y);
        $('#x2').val(c.x2);
        $('#y2').val(c.y2);
        $('#w').val(c.w);
        $('#h').val(c.h);
    };
});

$(document).ready(function () {
    var opts = {
        success: function (name) {
            $("#avatar_save_info").html("Avatar Uploaded Successfully.");
            $("#Info-Show").click();
            setTimeout("$('#Info-Close').click();", 1000);
            // force image reload
            d = new Date();
            $('#profile-avatar-img').attr("src", "user_data/" + name + "/a_" + name + ".jpg?" + d.getTime());
            $('#header-logo').attr("src", "user_data/" + name + "/a_" + name + ".jpg?" + d.getTime());
            $('#tab-Overview').click();
        },
        error: function (data) {
            $("#avatar_save_info").html("Oops.. Avatar Uploading Failed!");
            $("#Info-Show").click();
            setTimeout("$('#Info-Close').click();", 1000);
        }
    };
    $('.avatar_submit').hide();
    $('.avatar_cancel').hide();
    $('#Info-Show-Div').hide();
    $('#avatar-preview').hide();
    $('#avatar-submit').hide();
    $('#avatar-upload-info').hide();
    $('#avatar-form').ajaxForm(opts);

    $('#avatar-input').change(function () {
        //alert("avatar-input changed!");
        var file = this.files[0];
        var name = file.name;
        var size = file.size;
        var type = file.type;
        //validation
    });
});

var TableEditable_1 = function () {

    return {

        //main function to initiate the module
        init: function () {
            var oTable = $('#sample_editable_1').dataTable({
                "bAutoWidth": false,
                "iDisplayLength": 10,
                "sScrollX": "100%",
                "sDom": "<'row-fluid'r><'datatable-scroll't><'row-fluid'<'span6'i><'span6'p>>",
                //"sDom": "r<'H'lf><'datatable-scroll't><'F'ip>",
                "sPaginationType": "bootstrap",
                "aoColumnDefs": [{
                    'aTargets': [0],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    'aTargets': [2],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    'aTargets': [3],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    'aTargets': [4],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    "aTargets": ["_all"],
                    /*"mRender": function (data, type, full) {
                        return 200;
                    }*/
                }]
            });

            var nEditing = 0;

            var isCreate = true;//mark create a new paper or edit a paper
            var isNewFill = true;//mark refill paper or edit a paper

            function showBtn1() {
                $('#sample_editable_1_new').parent().show();
                $('#sample_editable_2_new').parent().hide();
            }

            function showBtn2() {
                $('#sample_editable_2_new').parent().show();
                $('#sample_editable_1_new').parent().hide();
            }
            showBtn1();
            $('#tab_1').click(function () {
                showBtn1();
            });

            $('#tab_2').click(function () {
                showBtn2();
            });

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
                return month + "/" + currentDate + "/" + date.getFullYear();
            }

            function ChangeDateFormat2(jsondate) {
                if (jsondate == null) {
                    // For server side json parser logic (in login.ashx addPaper()), DataTime cannot be null, or throwing an exception..
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

            function getAllPapers(func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllPapers',
                        parameter: null
                    },
                    success: function (data, textStatus) {
                        $(data).each(function (index, u) {
                            // A hidden column when borrowing device_list, filling a blank col. First col will hide even without style
                            oTable.fnAddData([parseInt(u["Id"])
                                , getStr(u["text"])
                                , getStr(u["PaperName"])
                                , getStr(u["Conference"])
                                , parseInt(u["Year"])
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
            getAllPapers(null);

            //add Paper
            function addPaper(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addPaper',
                        parameter: dataStr,
                        create: isCreate
                    },
                    success: function (u, textStatus) {
                        if (func != null)
                            func();
                        if (u["r"] != "s") {
                            alert("Add paper failed...");
                            return;
                        }

                        oTable.fnAddData([parseInt(u["Id"])
                                 , getStr(u["text"])
                                 , getStr(u["PaperName"])
                                 , getStr(u["Conference"])
                                 , parseInt(u["Year"])
                                 , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                                 , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                        ]);
                    },

                    error: function (rec) {
                        alert("Adding new paper failed...\nReason: " + rec.responseText);
                        //alert(rec.responseText);
                    }
                });
            }

            //edit Paper
            function editPaper(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editPaper',
                        parameter: dataStr,
                    },
                    success: function (u, textStatus) {
                        if (func != null)
                            func();

                        var aData = oTable.fnGetData(nEditing);

                        aData[0] = getStr(u["Id"]);
                        aData[1] = getStr(u["text"]);
                        aData[2] = getStr(u["PaperName"]);
                        aData[3] = getStr(u["Conference"]);
                        aData[4] = parseInt(u["Year"]);

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            //delete Paper
            function deletePaper(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deletePaper',
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

            $('#psave').live('click', function (e) {
                var u = {};
                u['Id'] = getInt($('#pId').val());
                if ($('#pPaperName').val() === null || $('#pPaperName').val() == "undefined" || $('#pPaperName').val() == "") {
                    alert("Please fill the name of the paper...");
                    //isNewFill = false;
                    return;
                }
                u['PaperName'] = $('#pPaperName').val();
                u['Conference'] = $('#pConference').val();
                u['Year'] = getInt($('#pYear').val());
                var data = JSON.stringify(u);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");

                if (isCreate) {
                    addPaper(data, null);
                } else {
                    editPaper(data, null);
                }
            });

            $('#sample_editable_1_new').live('click', function (e) {
                e.preventDefault();
                if (isNewFill) {
                    $("#u_form")[0].reset();
                }
                isNewFill = true;
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
                u['Id'] = aData[0];
                u['text'] = aData[1];
                u['PaperName'] = aData[2];
                u['Conference'] = aData[3];
                u['Year'] = aData[4];
                var data = JSON.stringify(u);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");

                deletePaper(data);

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

                $('#pId').val(aData[0]);
                $('#pPaperName').val(aData[2]);
                $('#pConference').val(aData[3]);
                $('#pYear').val(aData[4]);
            });
        }

    };

}();

var TableEditable_2 = function () {

    return {

        //main function to initiate the module
        init: function () {
            var oTable_2 = $('#sample_editable_2').dataTable({
                "bAutoWidth": false,
                "iDisplayLength": 10,
                "sScrollX": "100%",
                "sDom": "<'row-fluid'r><'datatable-scroll't><'row-fluid'<'span6'i><'span6'p>>",
                //"sDom": "r<'H'lf><'datatable-scroll't><'F'ip>",
                "sPaginationType": "bootstrap",
                "aoColumnDefs": [{
                    'aTargets': [0],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    'aTargets': [2],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    'aTargets': [3],
                    'bVisible': false,
                    'bSearchable': false,
                    'bSortable': false
                },
                {
                    "aTargets": ["_all"],
                    /*"mRender": function (data, type, full) {
                        return 200;
                    }*/
                }]
            });

            var nEditing = 0;

            var isCreate = true;//mark create a new award or edit a award
            var isNewFill = true;//mark refill award or edit a award

            /* leave in TableEditable_1
            function showBtn1() {
                $('#sample_editable_1_new').parent().show();
                $('#sample_editable_2_new').parent().hide();
            }

            function showBtn2() {
                $('#sample_editable_2_new').parent().show();
                $('#sample_editable_1_new').parent().hide();
            }
            showBtn1();
            $('#tab_1').click(function () {
                showBtn1();
            });

            $('#tab_2').click(function () {
                showBtn2();
            });
            */

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
                return month + "/" + currentDate + "/" + date.getFullYear();
            }

            function ChangeDateFormat2(jsondate) {
                if (jsondate == null) {
                    // For server side json parser logic (in login.ashx addPaper()), DataTime cannot be null, or throwing an exception..
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

            function getAllAwards(func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'getAllAwards',
                        parameter: null
                    },
                    success: function (data, textStatus) {
                        $(data).each(function (index, u) {
                            // A hidden column when borrowing device_list, filling a blank col. First col will hide even without style
                            oTable_2.fnAddData([parseInt(u["Id"])
                                , getStr(u["text"])
                                , getStr(u["Name"])
                                , parseInt(u["Year"])
                                , '<a class="edit" href="#form_modal2" data-toggle="modal">Edit</a>'
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
            getAllAwards(null);

            //add Award
            function addAward(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'addAward',
                        parameter: dataStr,
                        create: isCreate
                    },
                    success: function (u, textStatus) {
                        if (func != null)
                            func();
                        if (u["r"] != "s") {
                            alert("Add Award failed...");
                            return;
                        }

                        oTable_2.fnAddData([parseInt(u["Id"])
                                 , getStr(u["text"])
                                 , getStr(u["Name"])
                                 , parseInt(u["Year"])
                                 , '<a class="edit" href="#form_modal2" data-toggle="modal">Edit</a>'
                                 , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                        ]);
                    },

                    error: function (rec) {
                        alert("Adding new paper failed...\nReason: " + rec.responseText);
                        //alert(rec.responseText);
                    }
                });
            }

            //edit Award
            function editAward(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'editAward',
                        parameter: dataStr,
                    },
                    success: function (u, textStatus) {
                        if (func != null)
                            func();

                        var aData = oTable_2.fnGetData(nEditing);

                        aData[0] = getStr(u["Id"]);
                        aData[1] = getStr(u["text"]);
                        aData[2] = getStr(u["Name"]);
                        aData[3] = parseInt(u["Year"]);

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable_2.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
            }

            //delete Award
            function deleteAward(dataStr, func) {
                $.ajax({
                    type: "POST",
                    url: "data/login.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'deleteAward',
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

            $('#asave').live('click', function (e) {
                var u = {};
                u['Id'] = getInt($('#aId').val());
                if ($('#aPaperName').val() === null || $('#aPaperName').val() == "undefined" || $('#aPaperName').val() == "") {
                    alert("Please fill the name of the award...");
                    //isNewFill = false;
                    return;
                }
                u['Name'] = $('#aName').val();
                u['Year'] = getInt($('#aYear').val());
                var data = JSON.stringify(u);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");

                if (isCreate) {
                    addAward(data, null);
                } else {
                    editAward(data, null);
                }
            });

            $('#sample_editable_2_new').live('click', function (e) {
                e.preventDefault();
                if (isNewFill) {
                    $("#u_form_2")[0].reset();
                }
                isNewFill = true;
                isCreate = true;
            });

            $('#sample_editable_2 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                var aData = oTable_2.fnGetData(nRow);
                var u = {};
                u['Id'] = aData[0];
                u['text'] = aData[1];
                u['Name'] = aData[2];
                u['Year'] = aData[3];
                var data = JSON.stringify(u);

                data = data.replace("/Date", "\\/Date");
                data = data.replace("+0800)/", "+0800)\\/");

                deleteAward(data);

                oTable_2.fnDeleteRow(nRow);
            });

            $('#sample_editable_2 a.cancel').live('click', function (e) {
                e.preventDefault();
                if ($(this).attr("data-mode") == "new") {
                    var nRow = $(this).parents('tr')[0];
                    oTable_2.fnDeleteRow(nRow);
                } else {
                    restoreRow(oTable, nEditing);
                    nEditing = null;
                }
            });

            $('#sample_editable_2 a.edit').live('click', function (e) {
                e.preventDefault();

                $("#u_form_2")[0].reset();
                isCreate = false;

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable_2.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                $('#aId').val(aData[0]);
                $('#aName').val(aData[2]);
                $('#aYear').val(aData[3]);
            });
        }

    };

}();
