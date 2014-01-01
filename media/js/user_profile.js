var Profile = function () {

    // author: http://www.cnblogs.com/jiekk/archive/2011/06/28/2092444.html
    function GetRequest() {
        var url = location.search; // fetch strings after "?" in url
        var theRequest = new Object();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            }
        }
        return theRequest;
    }

    // original author: http://blog.sina.com.cn/s/blog_5a08ac3d0100dx03.html
    function fillSpanWithTextNode(spanId, text) {
        var tSpan = document.getElementById(spanId);
        // remove all built Text Nodes first
        while (tSpan.hasChildNodes()) {
            tSpan.removeChild(tSpan.childNodes[0]);
        }

        var tNode = document.createTextNode(text);
        tSpan.appendChild(tNode);
    }

    var Request = new Object();
    Request = GetRequest();
    var param1;
    param1 = Request["action"];

    if (param1 == "redirect") {
        fillSpanWithTextNode("responseSpan", "Please Login.");
        $('.alert-login', $('.profile-form')).show();
    }
    else if (param1 == "failed") {
        fillSpanWithTextNode("responseSpan", "Wrong Username/Password Combination. Try again.");
        $('.alert-login', $('.profile-form')).show();
    }

    return {
        //main function to initiate the module
        init: function () {
        }
    };
}();

function ChangeDateFormat(jsondate) {
    if (jsondate == null) {
        jsondate = "";
        return jsondate;
    }
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    }
    else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }

    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}

$(document).ready(function () {
    var myname;
    jcrop_init = false;
    $.ajax({
        url: "data/login.ashx",
        type: "POST",
        dataType: "json",
        data: {
            command: "load_user_settings"
        },
        success: function (rec) {
            $('#RealName').attr("value", rec.RealName);
            $('#StudentId').attr("value", rec.StudentId);
            $('#Email').attr("value", rec.Email);
            $('#Phone').attr("value", rec.Phone);
            $('#Hometown').attr("value", rec.Hometown);
            $('#Birthday').attr("value", ChangeDateFormat(rec.Birthday));
            $('#Link').attr("value", rec.Link);
            $('#University').attr("value", rec.University);
            $('#Introduction').text(rec.Introduction);
            d = new Date();
            myname = rec.Name;
            $('#profile-avatar-img').attr("src", "user_data/" + rec.Name + "/a_" + rec.Name + ".jpg?" + d.getTime());
            $('#header-logo').attr("src", "user_data/" + rec.Name + "/a_" + rec.Name + ".jpg?" + d.getTime());
        },
        error: function () {
            alert("load user error!");
        }
    });
    $(".submit-btn .btn.green.button_changepwd").on('click', function () {
        $.ajax({
            url: "data/login.ashx",
            type: "POST",
            dataType: "json",
            data: {
                command: "change_password",
                oldpwd: CryptoJS.SHA1($('#oldpwd').val()).toString(),
                newpwd1: CryptoJS.SHA1($('#newpwd1').val()).toString(),
                newpwd2: CryptoJS.SHA1($('#newpwd2').val()).toString()
            },
            success: function (rec) {
                alert(rec);
            },
            error: function (rec) {
                alert(rec);
            }
        });
    });
    $(".submit-btn .btn.green.button_submit").on('click', function () {
        $.ajax({
            url: "data/login.ashx",
            type: "POST",
            dataType: "json",
            data: {
                command: "save_user_settings",
                RealName: $('#RealName').val(),
                StudentId: $('#StudentId').val(),
                Email: $('#Email').val(),
                Phone: $('#Phone').val(),
                Hometown: $('#Hometown').val(),
                Birthday: $('#Birthday').val(),
                Link: $('#Link').val(),
                University: $('#University').val(),
                Introduction: $('#Introduction').val()
            },
            success: function (rec) {
                var rtv =
					"<div class=\"span8 profile-info\">" +
                        "<h1>" + rec.RealName + "</h1>" +
                        "<p>" + rec.Introduction + "</p>" +
					    "<p><a href=\"" + rec.Link + "\">" + rec.Link + "</a></p>" +
                        "<ul class=\"unstyled inline\">" +
                            "<li><i class=\"icon-home\"></i>" + rec.Hometown + "</li>" +
                            "<li><i class=\"icon-calendar\"></i>" + ChangeDateFormat(rec.Birthday) + "</li>" +                            "<li><i class=\"icon-calendar\"></i>" + rec.University + "</li>" +                            "<li><i class=\"icon-envelope-alt\"></i>" + rec.Email + "</li>" +                            "<li><i class=\"icon-th\"></i>" + rec.Phone + "</li>" +
                        "</ul>" +
                    "</div>";
                var oc = $("#userinfo").html("");
                $("#userinfo").append(rtv);
            },
            error: function () {
                alert("save user error!");
            }
        });
    });
    $(".submit-btn .btn.green.avatar_submit").on('click', function () {
        if (jcrop_init == false) {
            alert("No Avatar!");
            return;
        }
        $('#avatar-form').submit();
    });
});

jQuery(function ($) {

    // Create variables (in this scope) to hold the API and image size
    var jcrop_api,
        boundx,
        boundy;

    var $preview,
        $pcnt,
        $pimg,
        xsize,
        ysize;
        jcrop_init = false;
   
    $('#avatar-preview').change(function () {
        $('#avatar-preview').show();
        $('#avatar-submit').show();
        initJcrop();
        $('.avatar_submit').show();
        $('.avatar_cancel').show();
    });

    //initJcrop();
    function initJcrop()//{{{
    {
        if (jcrop_init == true) {
            destroyJcrop();
        }
        //$(".jcrop-assist-avatar-preview img").Jcrop({
        $("#target-avatar").Jcrop({
            onChange: updateCoord,
            onSelect: updateCoord,
            //aspectRatio: xsize / ysize
            aspectRatio: 1
        }, function () {
            // Use the API to get the real image size
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            jcrop_api = this;

            jcrop_api.setOptions({ allowResize: true });
            jcrop_api.setOptions({ allowSelect: true });
            jcrop_api.setOptions({ aspectRatio: 1 / 1 });
            jcrop_api.animateTo([0, 0, 200, 200]);
            jcrop_api.focus();
            jcrop_init = true;
        });
    };

    function destroyJcrop() {
        jcrop_api.destroy();
        jcrop_init = false;
    }

    function updateCoord(c) {
        $('#x1').val(c.x);
        $('#y1').val(c.y);
        $('#x2').val(c.x2);
        $('#y2').val(c.y2);
        $('#w').val(c.w);
        $('#h').val(c.h);
    };
});

$(document).ready(function () {
    var opts = {
        success: function (name) {
            $("#avatar_save_info").html("Avatar Uploaded Successfully.");
            $("#Info-Show").click();
            setTimeout("$('#Info-Close').click();", 1000);
            // force image reload
            d = new Date();
            $('#profile-avatar-img').attr("src", "user_data/" + name + "/a_" + name + ".jpg?" + d.getTime());
            $('#header-logo').attr("src", "user_data/" + name + "/a_" + name + ".jpg?" + d.getTime());
            $('#tab-Overview').click();
        },
        error: function (data) {
            $("#avatar_save_info").html("Oops.. Avatar Uploading Failed!");
            $("#Info-Show").click();
            setTimeout("$('#Info-Close').click();", 1000);
        }
    };
    $('.avatar_submit').hide();
    $('.avatar_cancel').hide();
    $('#Info-Show-Div').hide();
    $('#avatar-preview').hide();
    $('#avatar-submit').hide();
    $('#avatar-upload-info').hide();
    $('#avatar-form').ajaxForm(opts);

    $('#avatar-input').change(function () {
        //alert("avatar-input changed!");
        var file = this.files[0];
        var name = file.name;
        var size = file.size;
        var type = file.type;
        //validation
    });
});
