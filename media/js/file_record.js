var FileRecord = function () {

    var LoadFileRecord = function (dt) {
        var oTable = $('#sample_2').dataTable({
            "aaData": dt,
            "aoColumnDefs": [
                { "aTargets": [0] }
            ],
            "aaSorting": [[1, 'asc']],
            "aLengthMenu": [
               [5, 15, 20, -1],
               [5, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "iDisplayLength": 10,
        });

        jQuery('#sample_2_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
        jQuery('#sample_2_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
        jQuery('#sample_2_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown

        $('#sample_2_column_toggler input[type="checkbox"]').change(function () {
            /* Get the DataTables object again - this is not a recreation, just a get of the object */
            var iCol = parseInt($(this).attr("data-column"));
            var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
            oTable.fnSetColumnVis(iCol, (bVis ? false : true));
        });
    }

    var initCharts = function (dt) {

        if (!jQuery.plot) {
            return;
        }

        var d1 = [];
        var d2 = [];
        var tmp = {};
        for (var i = 0; i < dt.length; i += 1) {
            if (!tmp.hasOwnProperty(dt[i][0])) tmp[dt[i][0]] = 0;
            tmp[dt[i][0]] += 1;
        }
        var sp = 0;
        $.each(tmp, function (key, value) {
            d1.push([sp, value]);
            d2.push([sp, key]);
            sp += 1;
        });
        $.plot($("#chart_5"), [d1], {
            xaxis: {
                ticks: d2
            },
            series: {
                stack: 0,
                lines: {
                    show: false,
                    fill: true,
                    steps: false
                },
                bars: {
                    show: true,
                    barWidth: 0.6
                }
            }
        });

    }

    return {

        //main function to initiate the module
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }

            $.ajax({
                type: "POST",
                url: "data/file_record.ashx",
                cache: false,
                dataType: 'json',
                data: {
                    command: 'get_file_record'
                },
                success: function (rec) {
                    LoadFileRecord(rec);
                    initCharts(rec);
                },

                error: function (rec) {
                    alert("load file record error!");
                }
            });
        }

    };

}();