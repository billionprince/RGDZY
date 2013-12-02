var Schedule_Setting = function () {

    var getcalendarlist = function () {
        $.ajax({
            url: "data/calendar.ashx",
            type: "POST",
            datatype: "json",
            data: {
                command: "get_calendar_list"
            },
            success: function (rec) {
                $("#sample_editable_1 tbody").html("");
                for (var i = 0; i < rec.length; i++) {
                    $("#sample_editable_1 tbody").append(function () {
                        return "<tr class=''>" +
                                    "<td>" + rec[i].name + "</td>" +
                                    "<td>" + rec[i].type + "</td>" +
                                    "<td>" + rec[i].group + "</td>" +
                                    "<td class='center'>" + rec[i].start + "</td>" +
                                    "<td class='center'>" + rec[i].end + "</td>" +
                                    "<td>" + rec[i].detail + "</td>" +
                                    "<td><a class='edit' href='#form_modal1' data-toggle='modal'>Edit</a></td>" +
                                    "<td><a class='delete' href='javascript:;'>Delete</a></td>" +
                               "</tr>";
                    });
                }
            },
            error: function () {
                alert("load events error!");
            }
        });
    };

    var getgrouplist = function () {
        $.ajax({
            url: "data/user.ashx",
            type: "POST",
            datatype: "json",
            data: {
                command: "get_user_group"
            },
            success: function (rec) {
                for (var i = 0; i < rec.length; i++) {
                    $(".form-horizontal #my_multi_select2").append(function () {
                        var str = "<optgroup label='" + rec[i].groupname + "'>";
                        for (var j = 0; j < rec[i].username.length; j++) {
                            str += "<option>" + rec[i].username[j] + "</option>";
                        }
                        str += "</optgroup>";
                        return str;
                    });
                }
            },
            error: function () {
                alert("load events error!");
            }
        });
    };

    var handleMultiSelect = function () {
        $('#my_multi_select2').multiSelect({
            selectableOptgroup: true
        });
    };

    var handleDatetimePicker = function () {

        $(".form_datetime").datetimepicker({
            format: "dd MM yyyy - hh:ii",
            pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left")
        });

    };

    var handleTimePickers = function () {

        if (jQuery().timepicker) {
            $('.timepicker-default').timepicker();
            $('.timepicker-24').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false
            });
        }
    }

    var handlePortletTools = function () {
        jQuery('body').on('click', '.portlet .tools a.remove', function (e) {
            e.preventDefault();
            var removable = jQuery(this).parents(".portlet");
            if (removable.next().hasClass('portlet') || removable.prev().hasClass('portlet')) {
                jQuery(this).parents(".portlet").remove();
            } else {
                jQuery(this).parents(".portlet").parent().remove();
            }
        });

        jQuery('body').on('click', '.portlet .tools a.reload', function (e) {
            e.preventDefault();
            var el = jQuery(this).parents(".portlet");
            App.blockUI(el);
            getcalendarlist();
            window.setTimeout(function () {
                App.unblockUI(el);
            }, 1000);
        });

        jQuery('body').on('click', '.portlet .tools .collapse, .portlet .tools .expand', function (e) {
            e.preventDefault();
            var el = jQuery(this).closest(".portlet").children(".portlet-body");
            if (jQuery(this).hasClass("collapse")) {
                jQuery(this).removeClass("collapse").addClass("expand");
                el.slideUp(200);
            } else {
                jQuery(this).removeClass("expand").addClass("collapse");
                el.slideDown(200);
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            getcalendarlist();
            getgrouplist();
            setTimeout(handleMultiSelect, 1000);
            handleDatetimePicker();
            handleTimePickers();
            handlePortletTools();
        }
    };

}();

$(document).ready(function () {

    $(".modal-footer .btn-primary").click(function () {
        var userlist = [];
        $(".ms-elem-selection.ms-selected span").each(function (i, e) {
            userlist.push($(e).html());
        });
        userlist = userlist.join(",");
        var type = $(".form-horizontal select.small.m-wrap.event_type").val();
        var start, end;
        if (type == 0) {
            start = $(".form-horizontal .m-wrap.start_time").val();
            end = $(".form-horizontal .m-wrap.end_time").val();
        }
        else {
            start = $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").val();
            end = $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").val();
        }
        $.ajax({
            url: "data/calendar.ashx",
            type: "POST",
            datatype: "json",
            data: {
                command: "put_calendar_event",
                name: $(".form-horizontal input.m-wrap.small.event_title").val(),
                detail: $(".form-horizontal textarea.medium.m-wrap").val(),
                allday: $(".form-horizontal input[type=checkbox]").is(":checked"),
                type: type,
                start: start,
                end: end,
                user: userlist,
                creator: $(".username").html()
            },
            error: function () {
                alert("set events error!");
            }
        });
    });

    $(".form-horizontal input[type=checkbox]").click(function () {
        if ($(".form-horizontal input[type=checkbox]").is(":checked")) {
            $(".form-horizontal .m-wrap.start_time").parent().parent().parent().hide();
            $(".form-horizontal .m-wrap.end_time").parent().parent().parent().hide();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().hide();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().hide();
        }
        else {
            if ($(".form-horizontal select.small.m-wrap.event_type").val() != 0) {
                $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().show();
                $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().show();
            }
            else {
                $(".form-horizontal .m-wrap.start_time").parent().parent().parent().show();
                $(".form-horizontal .m-wrap.end_time").parent().parent().parent().show();
            }
        }
    });

    $(".form-horizontal select.small.m-wrap.event_type").change(function () {
        var type = $(".form-horizontal select.small.m-wrap.event_type").val();
        if (type == 0) {
            $(".form-horizontal input[type=checkbox]").parent().parent().parent().parent().parent().parent().show();
            $(".form-horizontal .m-wrap.start_time").parent().parent().parent().show();
            $(".form-horizontal .m-wrap.end_time").parent().parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().hide();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_week").parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_month").parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_day").parent().parent().hide();
        }
        else if (type == 1) {
            $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().show();
            $(".form-horizontal select.small.m-wrap.event_week").parent().parent().hide();
            $(".form-horizontal input[type=checkbox]").parent().parent().parent().parent().parent().parent().hide();
            $(".form-horizontal .m-wrap.start_time").parent().parent().parent().hide();
            $(".form-horizontal .m-wrap.end_time").parent().parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_month").parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_day").parent().parent().hide();
        }
        else if (type == 2) {
            $(".form-horizontal input[type=checkbox]").parent().parent().parent().parent().parent().parent().show();
            $(".form-horizontal select.small.m-wrap.event_week").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().show();
            $(".form-horizontal .m-wrap.start_time").parent().parent().parent().hide();
            $(".form-horizontal .m-wrap.end_time").parent().parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_day").parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_month").parent().parent().hide();
        }
        else if (type == 3) {
            $(".form-horizontal input[type=checkbox]").parent().parent().parent().parent().parent().parent().show();
            $(".form-horizontal select.small.m-wrap.event_day").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().show();
            $(".form-horizontal .m-wrap.start_time").parent().parent().parent().hide();
            $(".form-horizontal .m-wrap.end_time").parent().parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_week").parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_month").parent().parent().hide();
        }
        else if (type == 4) {
            $(".form-horizontal input[type=checkbox]").parent().parent().parent().parent().parent().parent().show();
            $(".form-horizontal select.small.m-wrap.event_month").parent().parent().show();
            $(".form-horizontal select.small.m-wrap.event_day").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.start_time").parent().parent().show();
            $(".form-horizontal .input-append.bootstrap-timepicker-component.end_time").parent().parent().show();
            $(".form-horizontal .m-wrap.start_time").parent().parent().parent().hide();
            $(".form-horizontal .m-wrap.end_time").parent().parent().parent().hide();
            $(".form-horizontal select.small.m-wrap.event_week").parent().parent().hide();
        }
    });

});