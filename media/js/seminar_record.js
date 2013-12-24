
var SeminarRecord = function () {

    return {

        //main function to initiate the module
        init: function () {

            Function.prototype.getMultiLine = function () {
                var lines = new String(this);
                lines = lines.substring(lines.indexOf("/*") + 3, lines.lastIndexOf("*/"));
                return lines;
            }

            function restoreRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTable.fnUpdate(aData[i+1], nRow, i+1, false);
                }

                oTable.fnDraw();
            }


            var datePicker = function () {
                /* 
                    <div class="controls">

						<input class="m-wrap m-ctrl-medium date-picker" readonly="" size="16" type="text" value="">

					</div>
                */
            }

            var fileSelect = function(){
                /*
                    <div class="controls">

						<input id="tags_2" type="text" class="m-wra tags medium" value="tag1,tag2" style="display: none;"><div id="tags_2_tagsinput" class="tagsinput" style="width: 240px; height: 100px;"><span class="tag"><span>tag1&nbsp;&nbsp;</span><a href="#" title="Removing tag">x</a></span><span class="tag"><span>tag2&nbsp;&nbsp;</span><a href="#" title="Removing tag">x</a></span><div id="tags_2_addTag"><input id="tags_2_tag" value="" data-default="add a tag" style="color: rgb(102, 102, 102); width: 80px;"></div><div class="tags_clear"></div></div>

					</div>
                */
            }

            function editRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = datePicker.getMultiLine();
                jqTds[1].innerHTML = '<input type="text" class="m-wrap small" value="' + aData[2] + '">';
                var str = aData[3].replace(new RegExp('</p><p>', 'g'), '\n').replace('<p>', "").replace('</p>', "");
                jqTds[2].innerHTML = '<textarea class="medium m-wrap">' + str + '</textarea>';
                jqTds[3].innerHTML = fileSelect.getMultiLine();
                jqTds[4].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[5].innerHTML = '<a class="cancel" href="">Cancel</a>';

                //initialize datepicker
                $('.date-picker').each(function () {
                    $(this).datepicker();
                });
            }

            function saveRow(oTable, nRow) {
                var jqInputs = $(':input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 2, false);
                var lst = jqInputs[2].value.split('\n');
                var d = "";
                for (var i = 0; i < lst.length; i++) {
                    d += '<p>' + lst[i] + '</p>';
                }
                oTable.fnUpdate(d, nRow, 3, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 4, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 5, false);
                oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 6, false);
                oTable.fnDraw();
            }

            function cancelEditRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 4, false);
                oTable.fnDraw();
            }

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                "bAutoWidth": false,  //自适应宽度
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
                ]
            });

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialzie select2 dropdown

            var nEditing = null;

            $('#sample_editable_1_new').click(function (e) {
                e.preventDefault();
                var aiNew = oTable.fnAddData(['', '', '', '',"",
                        '<a class="edit" href="">Edit</a>', '<a class="cancel" data-mode="new" href="">Cancel</a>'
                ]);
                var nRow = oTable.fnGetNodes(aiNew[0]);
                editRow(oTable, nRow);
                nEditing = nRow;
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
                    var jqInputs = $(':input', nRow);
                    if (jqInputs[0].value == '' || jqInputs[2].value == '' || jqInputs[3].value == '') {
                        alert("Please input the empty fields!");
                    } else {
                        saveRow(oTable, nEditing);
                        nEditing = null;
                        alert("Updated! Do not forget to do some ajax to sync with backend :)");
                    }
                } else {
                    /* No edit in progress - let's start one */
                    editRow(oTable, nRow);
                    nEditing = nRow;
                }
            });
        }

    };

}();