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
        }
    };

}();

$(document).ready(function () {
    $("div.chat-form input").keypress(function (e) {
        var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        if (e.keyCode == 13) {
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
                error: function () {
                    alert("put project detail chat events error!");
                }
            });
        }
    });
});
