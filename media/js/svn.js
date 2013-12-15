var SVN = function () {
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
            });

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialzie select2 dropdown

            function get_svn_settings(func) {
                $.ajax({
                    url: "data/svn.ashx",
                    type: "POST",
                    dataType: "json",
                    data: {
                        command: "get_svn_settings"
                    },
                    success: function (rec) {
                        for (var i = 0; i < rec.length; i++) {
                            oTable.fnAddData([rec[i].username
                                , rec[i].svnusername
                                , rec[i].svnpassword
                                , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                            ]);
                        }
                        /*$("#svntable").html("");
                        for (var i = 0; i < rec.length; i++) {
                            $("#svntable").append(function () {
                                return "<tr class=''>" +
                                            "<td>" + rec[i].username + "</td>" +
                                            "<td>" + rec[i].svnusername + "</td>" +
                                            "<td>" + rec[i].svnpassword + "</td>" +
                                            "<td>'<a class=\"edit\" href=\"#form_modal1\" data-toggle=\"modal\">Edit</a>'</td>" +
                                       "</tr>";
                            });
                        }*/
                    },
                    error: function () {
                        alert("get_svn_settings error!");
                    }
                });
            }

            get_svn_settings(null);

            var nEditing = 0;

            $('#save').click(function (e) {
                $.ajax({
                    type: "POST",
                    url: "data/svn.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'edit_svn_account',
                        username: $('#username').val(),
                        svnusername: $('#svnusername').val(),
                        svnpassword: CryptoJS.SHA1($('#svnpassword').val()).toString()
                    },
                    success: function (rec) {
                        var aData = oTable.fnGetData(nEditing);
                        //alert(aData[1]);
                        aData[0] = rec.Name;
                        aData[1] = rec.SVNUsername;
                        aData[2] = rec.SVNPassword;

                        for (var i = 0, iLen = aData.length; i < iLen; i++) {
                            oTable.fnUpdate(aData[i], nEditing, i, false);
                        }
                    },

                    error: function (rec) {
                        //alert(rec.responseText);
                    }
                });
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

                $("#svn_form")[0].reset();

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                $('#username').val(aData[0]);
                $('#svnusername').val(aData[1]);
                //$('#svnpassword').val(aData[2]);
            });
        }
    };
}();

$(document).ready(function () {
});