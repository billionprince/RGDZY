var FTP = function () {
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

            function get_ftp_settings(func) {
                $.ajax({
                    url: "data/ftp.ashx",
                    type: "POST",
                    dataType: "json",
                    data: {
                        command: "get_ftp_settings"
                    },
                    success: function (rec) {
                        for (var i = 0; i < rec.length; i++) {
                            oTable.fnAddData([rec[i].username
                                , rec[i].ftpusername
                                , rec[i].ftppassword
                                , '<a class="edit" href="#form_modal1" data-toggle="modal">Edit</a>'
                            ]);
                        }
                        /*$("#ftptable").html("");
                        for (var i = 0; i < rec.length; i++) {
                            $("#ftptable").append(function () {
                                return "<tr class=''>" +
                                            "<td>" + rec[i].username + "</td>" +
                                            "<td>" + rec[i].ftpusername + "</td>" +
                                            "<td>" + rec[i].ftppassword + "</td>" +
                                            "<td>'<a class=\"edit\" href=\"#form_modal1\" data-toggle=\"modal\">Edit</a>'</td>" +
                                       "</tr>";
                            });
                        }*/
                    },
                    error: function () {
                        alert("get_ftp_settings error!");
                    }
                });
            }

            get_ftp_settings(null);

            var nEditing = 0;

            $('#save').click(function (e) {
                $.ajax({
                    type: "POST",
                    url: "data/ftp.ashx",
                    cache: false,
                    dataType: 'json',
                    data: {
                        command: 'edit_ftp_account',
                        username: $('#username').val(),
                        ftpusername: $('#ftpusername').val(),
                        ftppassword: CryptoJS.SHA1($('#ftppassword').val()).toString()
                    },
                    success: function (rec) {
                        var aData = oTable.fnGetData(nEditing);
                        //alert(aData[1]);
                        aData[0] = rec.Name;
                        aData[1] = rec.FTPUsername;
                        aData[2] = rec.FTPPassword;

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

                $("#ftp_form")[0].reset();

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];
                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                $('#username').val(aData[0]);
                $('#ftpusername').val(aData[1]);
                //$('#ftppassword').val(aData[2]);
            });
        }
    };
}();

$(document).ready(function () {
});