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

var SearchResult = function () {

    var disableall = function () {
        $("#tab_2_2, #tab_2_3, #tab_2_5, #tab_1_2, #tab_1_3").hide();
    }

    var schedulesearch = function (rec) {
    }

    var devicesearch = function (rec) {
    }

    var projectsearch = function (rec) {
    }

    var filesearch = function (rec) {
    }

    var accountsearch = function (rec) {
    }

    return {

        init: function () {
            var para = getparameter();
            if (para.hasOwnProperty('s') == false || para[s].length == 0) {
                disableall();
            }
            $.ajax({
                url: "data/search.ashx",
                type: "POST",
                datatype: "json",
                data: {
                    command: "get_search_result",
                    str: para['id']
                },
                success : function (rec) {
                    schedulesearch(rec);
                    devicesearch(rec);
                    projectsearch(rec);
                    filesearch(rec);
                    accountsearch(rec);
                },
                error: function () {
                    alert("put project detail chat events error!");
                }
            });

        }

    };
}();

$(document).ready(function () {
});