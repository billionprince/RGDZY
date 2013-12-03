var Login = function () {
    
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
        $('.alert-login', $('.login-form')).show();
    }
    else if (param1 == "failed") {
        fillSpanWithTextNode("responseSpan", "Wrong Username/Password Combination. Try again.");
        $('.alert-login', $('.login-form')).show();
    }

    return {
        //main function to initiate the module
        init: function () {
        	
           $('.login-form').validate({
	            errorElement: 'label', //default input error message container
	            errorClass: 'help-inline', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            rules: {
	                username: {
	                    required: true
	                },
	                password: {
	                    required: true
	                },
	                remember: {
	                    required: false
	                }
	            },

	            messages: {
	                username: {
	                    required: "Username is required."
	                },
	                password: {
	                    required: "Password is required."
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   
	                $('.alert-error', $('.login-form')).show();
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
	                    //$('#add--response').html('<div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">¡Á</button><strong>Well done!</strong> You successfully read this important alert message.</div>');
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

	        $('.login-form input').keypress(function (e) {
	            if (e.which == 13) {
	                if ($('.login-form').validate().form()) {
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
	                        //$('#add--response').html('<div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">¡Á</button><strong>Well done!</strong> You successfully read this important alert message.</div>');
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
	                    //window.location.href = "wrong_enter.html";
	                }
	                return false;
	            }
	        });

	        $('.forget-form').validate({
	            errorElement: 'label', //default input error message container
	            errorClass: 'help-inline', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            ignore: "",
	            rules: {
	                email: {
	                    required: true,
	                    email: true
	                }
	            },

	            messages: {
	                email: {
	                    required: "Email is required."
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   

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
	                window.location.href = "index.html";
	            }
	        });

	        $('.forget-form input').keypress(function (e) {
	            if (e.which == 13) {
	                if ($('.forget-form').validate().form()) {
	                    window.location.href = "index.html";
	                }
	                return false;
	            }
	        });

	        jQuery('#forget-password').click(function () {
	            jQuery('.login-form').hide();
	            jQuery('.forget-form').show();
	        });

	        jQuery('#back-btn').click(function () {
	            jQuery('.login-form').show();
	            jQuery('.forget-form').hide();
	        });

	        $('.register-form').validate({
	            errorElement: 'label', //default input error message container
	            errorClass: 'help-inline', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            ignore: "",
	            rules: {
	                username: {
	                    required: true
	                },
	                password: {
	                    required: true
	                },
	                rpassword: {
	                    equalTo: "#register_password"
	                },
	                email: {
	                    required: true,
	                    email: true
	                },
	                tnc: {
	                    required: true
	                }
	            },

	            messages: { // custom messages for radio buttons and checkboxes
	                tnc: {
	                    required: "Please accept TNC first."
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   

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
	                if (element.attr("name") == "tnc") { // insert checkbox errors after the container                  
	                    error.addClass('help-small no-left-padding').insertAfter($('#register_tnc_error'));
	                } else {
	                    error.addClass('help-small no-left-padding').insertAfter(element.closest('.input-icon'));
	                }
	            },

	            submitHandler: function (form) {
	                window.location.href = "index.html";
	            }
	        });

	        jQuery('#register-btn').click(function () {
	            jQuery('.login-form').hide();
	            jQuery('.register-form').show();
	        });

	        jQuery('#register-back-btn').click(function () {
	            jQuery('.login-form').show();
	            jQuery('.register-form').hide();
	        });

	        $.backstretch([
		        "media/image/bg/1.jpg",
		        "media/image/bg/2.jpg",
		        "media/image/bg/3.jpg",
		        "media/image/bg/4.jpg"
		        ], {
		          fade: 1000,
		          duration: 8000
		      });
        }

    };

}();