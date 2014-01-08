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
                        command: "get_user_project",
                        user: $(".username").html()
                    },
                    success: function (rec) {

                        oTable.fnClearTable();
                        for (var i = 0; i < rec.length; i++) {
                            var htp = rec[i].Hyperlink;
                            if (rec[i].Hyperlink.indexOf("http") < 0) {
                                htp = "http://" + htp;
                            }
                            oTable.fnAddData([parseInt(rec[i].Id)
                                , '<a href="./project_detail.aspx?id=' + rec[i].Id + '">' + rec[i].BriefName + '</a>'
                                , rec[i].FullName
                                , rec[i].Participator
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