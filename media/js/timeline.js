var TIMELINE = function () {
    return {
        //main function to initiate the module
        init: function () {
            function get_all_timeline() {
                $.ajax({
                    url: "data/timeline.ashx",
                    type: "POST",
                    dataType: "json",
                    data: {
                        command: "get_all_timeline"
                    },
                    success: function (rec) {
                        var oc = $("#usertl").html("");
                        $("#usertl").append(rec);
                    },
                    error: function () {
                        alert("get_all_timeline error!");
                    }
                });
            }
            get_all_timeline();
        }
    };
}();

$(document).ready(function () {
});