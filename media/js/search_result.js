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
        if (rec.hasOwnProperty('schedule') == false || rec['schedule'].length == 0) {
            $("#tab_2_2").hide();
            $("li a[href$='#tab_2_2']").hide();
        }
        else {
            for (var i = 0; i < rec['schedule'].length; i += 1) {
                $("#tab_2_2 table tbody").append(function () {
                    var str = "<tr>" +
                                "<td>" + (i + 1) + "</td>" +
                                "<td>" + rec['schedule'][i].title + "</td>" +
                                "<td>" + rec['schedule'][i].type + "</td>" +
                                "<td>" + rec['schedule'][i].time + "</td>";
                    if (rec['schedule'][i].status == 1) {
                        str += "<td><span class='label label-danger'>" + "Passed" + "</span></td>";
                    }
                    else {
                        str += "<td><span class='label label-info'>" + "Pending" + "</span></td>";
                    }
                    str += "</tr>";
                    return str;
                });
            }
        }
    }

    var devicesearch = function (rec) {
        if (rec.hasOwnProperty('device') == false || rec['device'].length == 0) {
            $("#tab_2_3").hide();
            $("li a[href$='#tab_2_3']").hide();
        }
        else {
            if ($("#tab_2_2").css('display') == 'none') {
                $("#tab_2_3").addClass("active");
            }
            for (var i = 0; i < rec['device'].length; i += 1) {
                $("#tab_2_3 table tbody").append(function () {
                    var str = "<tr>" +
                                "<td>" + (i + 1) + "</td>" +
                                "<td>" + rec['device'][i].asset + "</td>" +
                                "<td>" + rec['device'][i].type + "</td>" +
                                "<td>" + rec['device'][i].owner + "</td>" +
                                "<td>" + rec['device'][i].remark + "</td>";
                              "</tr>";
                    return str;
                });
            }
        }
    }

    var projectsearch = function (rec) {
        if (rec.hasOwnProperty('project') == false || rec['project'].length == 0) {
            $("#tab_2_5").hide();
            $("li a[href$='#tab_2_5']").hide();
        }
        else {
            if ($("#tab_2_2").css('display') == 'none' && $("#tab_2_3").css('display') == 'none') {
                $("#tab_2_5").addClass("active");
            }
            for (var i = 0; i < rec['project'].length; i += 1) {
                $("#tab_2_5 table tbody").append(function () {
                    var str = "<tr>" +
                                "<td>" + (i + 1) + "</td>" +
                                "<td>" + rec['project'][i].name + "</td>" +
                                "<td>" + rec['project'][i].des + "</td>" +
                                "<td>" + rec['project'][i].par + "</td>" +
                                "<td><a href='" + "./project_detail.aspx?id=" + rec['project'][i].id + "'>" + rec['project'][i].name + " detail</a></td>";
                              "</tr>";
                    return str;
                });
            }
        }
    }

    var filesearch = function (rec) {
        if (rec.hasOwnProperty('file') == false || rec['file'].length == 0) {
            $("#tab_1_2").hide();
            $("li a[href$='#tab_1_2']").hide();
        }
        else {
            if ($("#tab_2_2").css('display') == 'none' && $("#tab_2_3").css('display') == 'none' && $("#tab_2_5").css('display') == 'none') {
                $("#tab_1_2").addClass("active");
            }
            for (var i = 0; i < rec['file'].length; i += 1) {
                $("#tab_1_2 table tbody").append(function () {
                    var str = "<tr>" +
                                "<td>" + (i + 1) + "</td>" +
                                "<td>" + rec['file'][i].name + "</td>" +
                                "<td>" + rec['file'][i].owner + "</td>" +
                                "<td>" + rec['file'][i].time + "</td>";
                    "</tr>";
                    return str;
                });
            }
        }
    }

    var accountsearch = function (rec) {
        if (rec.hasOwnProperty('account') == false || rec['account'].length == 0) {
            $("#tab_1_3").hide();
            $("li a[href$='#tab_1_3']").hide();
        }
        else {
            if ($("#tab_2_2").css('display') == 'none' && $("#tab_2_3").css('display') == 'none'
                && $("#tab_2_5").css('display') == 'none' && $("#tab_1_2").css('display') == 'none') {
                $("#tab_1_3").addClass("active");
            }
            for (var i = 0; i < rec['account'].length; i += 1) {
                //$("#tab_1_3").append(function () {
                //    var str = "<div class='search-classic'>" +
                //                "<h4><a href='" + "./user_profile.aspx?id=" + rec['account'][i].sid + "'>" +
                //                    rec['account'][i].name + "Member of " + rec['account'][i].teamname + " Team" + "</a></h4>";
                //    if(rec['account'][i].link.length > 0) {
                //        var htp = rec['account'][i].link;
                //        if (rec['account'][i].link.indexOf("http") < 0) {
                //            htp = "http://" + htp;
                //        }
                //        str += "<a href='" + htp + "'>" + rec['account'][i].link + "</a>";
                //    }
                //    if (rec['account'][i].research.length > 0) {
                //        str += "<p class='account_search user_intro'>" + rec['account'][i].research + "</p>"
                //    }
                //    if (rec['account'][i].paper.length > 0) {
                //        str += "<p class='account_search user_paper'>" + rec['account'][i].paper + "</p>";
                //    }
                //    if (rec['account'][i].award.length > 0) {
                //        str += "<p class='account_search user_award'>" + rec['account'][i].award + "</p>";
                //    }
                //    str += "</div>";
                //    return str;
                //});
                console.log(rec['account'][i].link.length);
                $("#tab_1_3 table tbody").append(function () {
                    var str = "<tr>" +
                                "<td>" + (i + 1) + "</td>" +
                                "<td>" + rec['account'][i].name + "</td>" +
                                "<td>" + rec['account'][i].team + "</td>" +
                                "<td>" + rec['account'][i].email + "</td>" +
                                "<td>" + rec['account'][i].telephone + "</td>";
                    //"<td><a href='" + "./user_profile.aspx?id=" + rec['account'][i].name + "'>" + rec['account'][i].name + " profile</a></td>";
                    if (rec['account'][i].link.length > 0) {
                        var htp = rec['account'][i].link;
                        if (rec['account'][i].link.indexOf("http") < 0) {
                            htp = "http://" + htp;
                        }
                        str += "<td><a href='" + htp + "' target='_blank'>" + rec['account'][i].link + "</a></td>";
                    }
                    else str += "<td></td>"
                    str += "</td>";
                    return str;
                });
            }
        }
    }

    return {

        init: function () {
            var para = getparameter();
            if (para.hasOwnProperty('s') == false || para['s'].length == 0) {
                disableall();
            }
            $.ajax({
                url: "data/search.ashx",
                type: "POST",
                datatype: "json",
                data: {
                    command: "get_search_result",
                    str: para['s']
                },
                success : function (rec) {
                    schedulesearch(rec);
                    devicesearch(rec);
                    projectsearch(rec);
                    filesearch(rec);
                    accountsearch(rec);
                },
                error: function () {
                    alert("load search result error!");
                }
            });

        }

    };
}();

$(document).ready(function () {
});