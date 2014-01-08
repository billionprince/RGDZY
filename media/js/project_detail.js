var getparameter = function () {
    var query = location.search.substr(1);
    var data = query.split("&");
    var result = {};
    for (var i = 0; i < data.length; i++) {
        var item = data[i].split("=");
        result[item[0]] = item[1];
    }
    return result;
}

var Project_Detail = function () {

    var getchatcontent = function () {
        var para = getparameter();
        $.ajax({
            url: "data/project.ashx",
            type: "POST",
            datatype: "json",
            data: {
                command: "get_project_detail",
                id: para['id']
            },
            success: function (rec) {
                $("div#chats ul.chats").html("");
                for (var i = 0; i < rec.length; i++) {
                    $("div#chats ul.chats").append(function () {
                        var str;
                        if (i % 2) str = "<li class='in'>";
                        else str = "<li class='out'>";
                        str += "<img class='avatar' alt='' src='" + rec[i].image + "'/>" +
                            "<div class='message'>" +
                                "<span class='arrow'></span>" +
                                "<a href='#' class='name'>" + rec[i].name + "</a>" +
                                "<span class='datetime'>at " + rec[i].time + "</span>" +
                                "<span class='body'>" + rec[i].content + "</span>" +
                            "</div></li>";
                        return str;
                    });
                }
            },
            error: function () {
                alert("project detail load events error!");
            }
        });
    };

    return {
        //main function to initiate the module
        init: function () {
            getchatcontent();
            var para = getparameter();
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

            function get_milestone_settings() {
                $.ajax({
                    url: "data/project.ashx",
                    type: "POST",
                    dataType: "json",
                    data: {
                        project_id: para['id'],
                        command: "get_milestone_settings"
                    },
                    success: function (rec) {
                        for (var i = 0; i < rec.length; i++) {
                            oTable.fnAddData([parseInt(rec[i].Id)
                                , rec[i].Description
                                , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                                , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                            ]);
                        }
                    },
                    error: function () {
                        alert("get_milestone_settings error!");
                    }
                });
            }

            get_milestone_settings();

            //add milestone
            function addmilestone() {
                $.ajax({
                    type: "POST",
                    url: "data/project.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        project_id: para['id'],
                        command: 'add_milestone_settings',
                        description: $('#description').val()
                    },
                    success: function (rec) {
                        oTable.fnAddData([parseInt(rec.Id)
                            , rec.Description
                            , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                            , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                        ]);

                        // logo upload through submitting form
                        project_id = getparameter()['id'];
                        handler_path = "data/project.ashx?command=save_milestone_logo&project_id=" + project_id + "&id=" + rec.Id;
                        $('#milestone_form').attr('action', handler_path);
                        $('#milestone_form').submit();
                    },

                    error: function (rec) {
                        alert("add_milestone_settings error!");
                    }
                });
            }

            //edit milestone
            function editmilestone() {
                $.ajax({
                    type: "POST",
                    url: "data/project.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'edit_milestone_settings',
                        project_id: para['id'],
                        id: $('#milestoneid').val(),
                        description: $('#description').val()
                    },
                    success: function (rec) {
                        var aData = oTable.fnGetData(nEditing);
                        aData[1] = rec.Description;

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }

                        // logo upload through submitting form
                        project_id = getparameter()['id'];
                        handler_path = "data/project.ashx?command=save_milestone_logo&project_id=" + project_id + "&id=" + rec.Id;
                        $('#milestone_form').attr('action', handler_path);
                        $('#milestone_form').submit();
                    },

                    error: function (rec) {
                        alert("edit_milestone_settings error!");
                    }
                });
            }

            //delete milestone
            function deletemilestone(id) {
                $.ajax({
                    type: "POST",
                    url: "data/project.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        project_id: para['id'],
                        command: 'delete_milestone_settings',
                        id: parseInt(id)
                    },
                    success: function (rec) {
                        //alert("delete_success");
                    },

                    error: function (rec) {
                        alert("delete_milestone_settings error!");
                    }
                });
            }

            $('#save').click(function (e) {
                if (isCreate) {
                    addmilestone();
                } else {
                    editmilestone();
                }
            });

            $('#sample_editable_1_new').click(function (e) {
                e.preventDefault();
                $("#milestone_form")[0].reset();
                isCreate = true;
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                var aData = oTable.fnGetData(nRow);
                deletemilestone(aData[0]);
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

                $("#milestone_form")[0].reset();
                isCreate = false;

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                $('#milestoneid').val(aData[0]);
                $('#description').val(aData[1]);
            });
        }
    };

}();

$(document).ready(function () {
    function get_project() {
        $.ajax({
            type: "POST",
            url: "data/project_list.ashx",
            cache: false,
            dataType: 'json',
            data: {
                command: 'get_project',
                id: getparameter()["id"]
            },
            success: function (data, textStatus) {
                $(".proName").html(data["Name"]);
            },

            error: function (rec) {
                alert("get_project error!");
            }
        });
    }

    get_project();


    var chatinput = function () {
        var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        var d = new Date();
        var time = month[parseInt(d.toString('MM')) - 1] + d.toString(' dd, yyyy HH:ss');
        var content = $("div.chat-form input").val();
        var name = $(".username").html();
        $("div#chats ul.chats").append(function () {
            var str;
            if ($("div#chats ul.chats li").length % 2) str = "<li class='in'>";
            else str = "<li class='out'>";
            str += "<img class='avatar' alt='' src='" + "user_data/" + name + "/a_" + name + ".jpg" + "'/>" +
                "<div class='message'>" +
                    "<span class='arrow'></span>" +
                    "<a href='#' class='name'>" + name + "</a>" +
                    "<span class='datetime'>at " + time + "</span>" +
                    "<span class='body'>" + content + "</span>" +
                "</div></li>";
            return str;
        });
        var para = getparameter();
        $.ajax({
            url: "data/project.ashx",
            type: "POST",
            datatype: "json",
            data: {
                command: "put_project_detail_chat",
                name: name,
                time: time,
                content: content,
                project_id: para['id']
            },
            success: function () {
                $("div.chat-form input").val("");
            },
            error: function () {
                alert("put project detail chat events error!");
            }
        });
    }

    $("div.chat-form input").keypress(function (e) {
        if (e.keyCode == 13) {
            chatinput();
        }
    });

    $("div.chat-form a").on("click", function (e) {
        e.preventDefault();
        var content = $("div.chat-form input").val();
        if (content.length > 0) {
            chatinput();
        }
        else {
            alert("please input content!");
        }
    });
});


$(document).ready(function () {
    var opts = {
        success: function (name) {
            //$("#avatar_save_info").html("Avatar Uploaded Successfully.");
            //$("#Info-Show").click();
            //setTimeout("$('#Info-Close').click();", 1000);
            // force image reload
            //d = new Date();
            //$('#profile-avatar-img').attr("src", "user_data/" + name + "/a_" + name + ".jpg?" + d.getTime());
            //$('#header-logo').attr("src", "user_data/" + name + "/a_" + name + ".jpg?" + d.getTime());
            //$('#tab-Overview').click();
            //alert('Img Suc!');
        },
        error: function (data) {
            //$("#avatar_save_info").html("Oops.. Avatar Uploading Failed!");
            //$("#Info-Show").click();
            //setTimeout("$('#Info-Close').click();", 1000);
            alert('Upload Logo Failed');
        }
    };
    $('#milestone_form').ajaxForm(opts);//.submit(function () { return false; });

    $('#logo-input').change(function () {
        //alert("logo-input changed!");
        var file = this.files[0];
        var name = file.name;
        var size = file.size;
        var type = file.type;
        //validation
    });


});