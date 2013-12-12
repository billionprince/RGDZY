var Project_Detail = function () {
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
        }
    };

}();

$(document).ready(function () {

});
