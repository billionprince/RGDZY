var PROJECTS = function () {
    return {
        //main function to initiate the module
        init: function () {
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

            function getUserProjects() {
                $.ajax({
                    url: "data/project_list.ashx",
                    type: "POST",
                    dataType: "json",
                    data: {
                        command: "get_project_settings"
                    },
                    success: function (rec) {
                        for (var i = 0; i < rec.length; i++) {
                            var htp = rec[i].Hyperlink;
                            if (rec[i].Hyperlink.indexOf("http") < 0) {
                                htp = "http://" + htp;
                            }
                            oTable.fnAddData([parseInt(rec[i].Id)
                                , '<a href="./project_detail.aspx?id=' + rec[i].Id + '">' + rec[i].BriefName + '</a>'
                                , rec[i].FullName
                                , rec[i].Description
                                , '<a href="' + htp + '" target="_blank">' + rec[i].Hyperlink + '</a>'
                            ]);
                        }
                    },
                    error: function () {
                        alert("get user projects error!");
                    }
                });
            }

            getUserProjects();

            //add Project
            function addProject() {
                $.ajax({
                    type: "POST",
                    url: "data/project_list.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'add_project_settings',
                        briefname: $('#briefname').val(),
                        fullname: $('#fullname').val(),
                        description: $('#description').val(),
                        hyperlink: $('#hyperlink').val()
                    },
                    success: function (rec) {
                        oTable.fnAddData([parseInt(rec.Id)
                            , '<a href="./project_detail.aspx?id=' + rec.Id + '">' + rec.Name + '</a>'
                            , rec.FullName
                            , rec.Description
                            , rec.Link
                            , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                            , '<a class="delete" data-mode="new" href = "javascript:">Delete</a>'
                        ]);
                    },

                    error: function (rec) {
                        alert("add_project_settings error!");
                    }
                });
            }

            //edit Project
            function editProject() {
                $.ajax({
                    type: "POST",
                    url: "data/project_list.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'edit_project_settings',
                        id: $('#projectid').val(),
                        briefname: $('#briefname').val(),
                        fullname: $('#fullname').val(),
                        description: $('#description').val(),
                        hyperlink: $('#hyperlink').val()
                    },
                    success: function (rec) {
                        var aData = oTable.fnGetData(nEditing);
                        //alert(aData[1]);
                        aData[1] = '<a href="./project_detail.aspx?id=' + rec.Id + '">' + rec.Name + '</a>';
                        //aData[1] = rec.Name;
                        aData[2] = rec.FullName;
                        aData[3] = rec.Description;
                        aData[4] = '<a href="' + rec.Link + '" target="_blank">' + rec.Link + '</a>';

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        alert("edit_project_settings error!");
                    }
                });
            }

            //delete Project
            function deleteProject(id) {
                $.ajax({
                    type: "POST",
                    url: "data/project_list.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'delete_project_settings',
                        id: parseInt(id)
                    },
                    success: function (rec) {
                        //alert("delete_success");
                    },

                    error: function (rec) {
                        alert("delete_project_settings error!");
                    }
                });
            }

            $('#save').click(function (e) {
                if (isCreate) {
                    addProject();
                } else {
                    editProject();
                }
            });

            $('#sample_editable_1_new').click(function (e) {
                e.preventDefault();
                $("#project_form")[0].reset();
                isCreate = true;
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                var aData = oTable.fnGetData(nRow);
                deleteProject(aData[0]);
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

                $("#project_form")[0].reset();
                isCreate = false;

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                $('#projectid').val(aData[0]);
                $('#briefname').val(aData[1].substr(aData[1].indexOf('>') + 1, aData[1].indexOf('<', aData[1].indexOf('>')) - aData[1].indexOf('>') - 1));
                $('#fullname').val(aData[2]);
                $('#description').val(aData[3]);
                $('#hyperlink').val(aData[4].substr(aData[4].indexOf('>') + 1, aData[4].indexOf('<', aData[4].indexOf('>')) - aData[4].indexOf('>') - 1));
            });

            var handlePortletTools = function () {

                jQuery('body').on('click', '.portlet .tools a.reload', function (e) {
                    e.preventDefault();
                    var el = jQuery(this).parents(".portlet");
                    App.blockUI(el);
                    getUserProjects();
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