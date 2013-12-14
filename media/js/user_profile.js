﻿var Profile = function () {

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
    console.warn(param1);
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

            $('.profile-form').validate({
                errorElement: 'label', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    $('.alert-error', $('.profile-form')).show();
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
	                    .closest('.control-group').addClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label.closest('.control-group').removeClass('error');
                    label.remove();
                },

                errorPlacement: function (error, element) {
                    error.addClass('help-small no-left-padding').insertAfter(element.closest('.input-icon'));
                },

                submitHandler: function (form) {
                    // crypton-js-3.1.2-sha1.js is required for SHA1 hashing (see login.aspx)
                    var pass = $("input[name='password']").val();
                    var passhash = CryptoJS.SHA1(pass).toString();
                    request = $.ajax({
                        type: "POST",
                        url: "data/login.ashx",
                        dataType: 'text',
                        data: {
                            command: 'get_validate',
                            username: $("input[name='username']").val(),
                            password: passhash
                        }
                        /*,
	                    success: function (rec) {
	                        alert('Suc:' + rec);
	                    },

	                    error: function (rec) {
	                        alert('Failed:' + rec.responseText);
	                    }*/
                    });

                    // callback handler that will be called on success
                    request.done(function (response, textStatus, jqXHR) {
                        // log a message to the console
                        console.log("Hooray, it worked!");
                        //alert("success" + response);
                        //$('#add--response').html('<div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">×</button><strong>Well done!</strong> You successfully read this important alert message.</div>');
                        if (response == "Success")
                            window.location.href = "default.aspx";
                        else
                            window.location.href = ("login.aspx?action=failed&resp=" + response);
                    });

                    // callback handler that will be called on failure
                    request.fail(function (jqXHR, textStatus, errorThrown) {
                        // log the error to the console
                        console.error(
                            "The following error occured: " + textStatus, errorThrown);
                    });

                    // callback handler that will be called regardless
                    // if the request failed or succeeded
                    request.always(function () {
                        // reenable the inputs
                        //$inputs.prop("disabled", false);
                    });
                }
            });

            $('.profile-form input').keypress(function (e) {
                if (e.which == 13) {
                    if ($('.profile-form').validate().form()) {
                        // crypton-js-3.1.2-sha1.js is required for SHA1 hashing (see login.aspx)
                        var pass = $("input[name='password']").val();
                        var passhash = CryptoJS.SHA1(pass).toString();
                        request = $.ajax({
                            type: "POST",
                            url: "data/login.ashx",
                            dataType: 'text',
                            data: {
                                command: 'profile_update',
                                username: $("input[name='username']").val(),
                                password: passhash
                            }
                            /*,
                            success: function (rec) {
                                alert('Suc:' + rec);
                            },
    
                            error: function (rec) {
                                alert('Failed:' + rec.responseText);
                            }*/
                        });

                        // callback handler that will be called on success
                        request.done(function (response, textStatus, jqXHR) {
                            // log a message to the console
                            console.log("Hooray, it worked!");
                            //alert("success" + response);
                            //$('#add--response').html('<div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">×</button><strong>Well done!</strong> You successfully read this important alert message.</div>');
                            if (response == "Success")
                                window.location.href = "yes.aspx";
                            else
                                window.location.href = "failed.aspx";
                        });

                        // callback handler that will be called on failure
                        request.fail(function (jqXHR, textStatus, errorThrown) {
                            // log the error to the console
                            console.error(
                                "The following error occured: " + textStatus, errorThrown);
                        });

                        // callback handler that will be called regardless
                        // if the request failed or succeeded
                        request.always(function () {
                            // reenable the inputs
                            //$inputs.prop("disabled", false);
                        });
                        //window.location.href = "wrong_enter.html";
                    }
                    return false;
                }
            });
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
});