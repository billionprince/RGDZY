<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page_schedule_setting.aspx.cs" Inherits="RGDZY.page_schedule_setting" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
	<meta charset="utf-8" />
	<title>SJTU-Joint Laboratory of Cloud Computing | Pages - Calendar Setting</title>
	<meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	<!-- BEGIN GLOBAL MANDATORY STYLES -->
	<link href="media/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>

	<link href="media/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css"/>

	<link href="media/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

	<link href="media/css/style-metro.css" rel="stylesheet" type="text/css"/>

	<link href="media/css/style.css" rel="stylesheet" type="text/css"/>

	<link href="media/css/style-responsive.css" rel="stylesheet" type="text/css"/>

	<link href="media/css/default.css" rel="stylesheet" type="text/css" id="style_color"/>

	<link href="media/css/uniform.default.css" rel="stylesheet" type="text/css"/>
	<!-- END GLOBAL MANDATORY STYLES -->
	<!-- BEGIN PAGE LEVEL STYLES -->
	<link href="media/css/fullcalendar.css" rel="stylesheet" />    <link rel="stylesheet" type="text/css" href="media/css/bootstrap-fileupload.css" />

	<link rel="stylesheet" type="text/css" href="media/css/jquery.gritter.css" />

	<link rel="stylesheet" type="text/css" href="media/css/chosen.css" />

	<link rel="stylesheet" type="text/css" href="media/css/select2_metro.css" />

	<link rel="stylesheet" type="text/css" href="media/css/jquery.tagsinput.css" />

	<link rel="stylesheet" type="text/css" href="media/css/clockface.css" />

	<link rel="stylesheet" type="text/css" href="media/css/bootstrap-wysihtml5.css" />

	<link rel="stylesheet" type="text/css" href="media/css/datepicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/timepicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/colorpicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/bootstrap-toggle-buttons.css" />

	<link rel="stylesheet" type="text/css" href="media/css/daterangepicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/datetimepicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/multi-select-metro.css" />

	<link href="media/css/bootstrap-modal.css" rel="stylesheet" type="text/css"/>    <link href="media/css/schedule.css" rel="stylesheet" type="text/css"/>
	<!-- END PAGE LEVEL STYLES -->
	<link rel="shortcut icon" href="media/image/favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="page-header-fixed">
    <!-- #include file="header.html" -->
    <uc:Menu id="Menu_Default" 
        runat="server" />
	<!-- BEGIN CONTAINER -->   
	<div class="page-container row-fluid">
		<!-- BEGIN PAGE -->
		<div class="page-content">
			<!-- BEGIN SAMPLE PORTLET CONFIGURATION MODAL FORM-->
			<div id="portlet-config" class="modal hide">
				<div class="modal-header">
					<button data-dismiss="modal" class="close" type="button"></button>
					<h3>portlet Settings</h3>
				</div>
				<div class="modal-body">
					<p>Here will be a configuration form</p>
				</div>
			</div>
			<!-- END SAMPLE PORTLET CONFIGURATION MODAL FORM-->
			<!-- BEGIN PAGE CONTAINER-->
			<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->
				<div class="row-fluid">
					<div class="span12">
						<!-- BEGIN PAGE TITLE & BREADCRUMB-->
						<h3 class="page-title">
							Schedule <small>setting</small>
						</h3>
						<ul class="breadcrumb">
							<li>
								<i class="icon-home"></i>
								<a href="default.aspx">Home</a> 
								<i class="icon-angle-right"></i>
							</li>
							<li>
								<a href="#">Schedule Management</a>
								<i class="icon-angle-right"></i>
							</li>
							<li><a href="#">Setting</a></li>
						</ul>
						<!-- END PAGE TITLE & BREADCRUMB-->
					</div>
				</div>
				<!-- END PAGE HEADER-->
				<!-- BEGIN PAGE CONTENT-->
				<div class="row-fluid">
					<div class="span12">
						<!-- BEGIN EXAMPLE TABLE PORTLET-->
						<div class="portlet box blue">
							<div class="portlet-title">
								<div class="caption"><i class="icon-edit"></i>Schedule Setting</div>
								<div class="tools">
									<a href="javascript:;" class="collapse"></a>
									<a href="javascript:;" class="reload"></a>
								</div>
							</div>
							<div class="portlet-body">
								<div class="clearfix">
									<div class="btn-group">
										<a id="sample_editable_1_new" class="btn green" href="#form_modal1" data-toggle="modal">
										Add New <i class="icon-plus"></i>
										</a>
									</div>
								</div>

								<table class="table table-striped table-hover table-bordered" id="sample_editable_1">

									<thead>

										<tr>

											<th>Name</th>

                                            <th>Type</th>

                                            <th>Participant</th>

                                            <th>Begin Time</th>

											<th>End Time</th>

                                            <th>Detail</th>

											<th>Edit</th>

											<th>Delete</th>

                                            <th style="display:none">id</th>

                                            <th style="display:none">allday</th>
										</tr>

									</thead>

                                    <tbody>
                                    </tbody>

								</table>
                                
                                <div id="form_modal1" onload ="showmodal();" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" >

									<div class="modal-header">

										<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>

										<h3 id="myModalLabel1">Event</h3>

									</div>

									<div class="modal-body">

										<form action="#" class="form-horizontal">

                                            <div class="control-group">

										        <label class="control-label">Name</label>

										        <div class="controls">

											        <input type="text" class="m-wrap medium event_title" />

                                                    <span style ="display:none" class="help-inline">Schedule Name</span>

										        </div>

									        </div>

                                            <div class="control-group">

													<label class="control-label">Details</label>

													<div class="controls">

														<textarea class="medium m-wrap event_detail" rows="3"></textarea>

													</div>

											</div>

                                            <div class="control-group">

                                                <label class="control-label">Type</label>
                                                
                                                <div class="controls">
                                                
                                                	<select class="medium m-wrap event_type" tabindex="1">
                                                
                                                        <option value="0">once</option>
                                                
                                                		<option value="1">daily</option>
                                                
                                                		<option value="2">weekly</option>
                                                
                                                		<option value="3">monthly</option>
                                                
                                                		<option value="4">yearly</option>
                                                
                                                	</select>
                                                
                                                </div>

                                            </div>

                                            <div class="control-group">

										        <label class="control-label">Time Scope</label>

										        <div class="controls">

											        <label class="checkbox line"><span><input value="" type="checkbox"></span>Anytime</label>
										        </div>

									        </div>

                                            <!--weekly-->
                                            <div class="control-group" style="display:none">

                                                <label class="control-label">Week</label>
                                                
                                                <div class="controls">
                                                
                                                	<select class="medium m-wrap event_week" tabindex="2">

                                                        <option value="0">MON</option>
                                                
                                                		<option value="1">TUE</option>
                                                
                                                		<option value="2">WED</option>
                                                
                                                		<option value="3">THUR</option>
                                                
                                                		<option value="4">FRI</option>

                                                        <option value="5">SAT</option>

                                                        <option value="6">SUN</option>
                                                
                                                	</select>
                                                
                                                </div>

                                            </div>

                                            <!--yearly-->
                                            <div class="control-group" style="display:none">

                                                <label class="control-label">Month</label>
                                                
                                                <div class="controls">
                                                
                                                	<select class="medium m-wrap event_month" tabindex="4">

                                                        <option value="0">JAN</option>
                                                
                                                		<option value="1">FEB</option>
                                                
                                                		<option value="2">MAR</option>
                                                
                                                		<option value="3">APR</option>
                                                
                                                		<option value="4">MAY</option>

                                                        <option value="5">JUN</option>

                                                        <option value="6">JUL</option>
                                                        
                                                        <option value="7">AUG</option>
                                                
                                                		<option value="8">SEP</option>
                                                
                                                		<option value="9">OCT</option>

                                                        <option value="10">NOV</option>

                                                        <option value="11">DEC</option>
                                                
                                                	</select>
                                                
                                                </div>

                                            </div>

                                            <!--monthly-->
                                            <div class="control-group" style="display:none">

                                                <label class="control-label">Day</label>
                                                
                                                <div class="controls">
                                                
                                                	<select class="medium m-wrap event_day" tabindex="3">

                                                        <option value="0">1</option>
                                                
                                                		<option value="1">2</option>
                                                
                                                		<option value="2">3</option>
                                                
                                                		<option value="3">4</option>
                                                
                                                		<option value="4">5</option>

                                                        <option value="5">6</option>

                                                        <option value="6">7</option>
                                                        
                                                        <option value="7">8</option>
                                                
                                                		<option value="8">9</option>
                                                
                                                		<option value="9">10</option>

                                                        <option value="10">11</option>

                                                        <option value="11">12</option>
                                                        
                                                        <option value="12">13</option>
                                                
                                                		<option value="13">14</option>
                                                
                                                		<option value="14">15</option>
                                                
                                                		<option value="15">16</option>
                                                
                                                		<option value="16">17</option>

                                                        <option value="17">18</option>

                                                        <option value="18">19</option>
                                                        
                                                        <option value="19">20</option>
                                                
                                                		<option value="20">21</option>
                                                
                                                		<option value="21">22</option>

                                                        <option value="22">23</option>

                                                        <option value="23">24</option>

                                                        <option value="24">25</option>
                                                
                                                		<option value="25">26</option>

                                                        <option value="26">27</option>

                                                        <option value="27">28</option>

                                                        <option value="28">29</option>

                                                        <option value="29">30</option>

                                                        <option value="30">31</option>                                
                                                	</select>
                                                
                                                </div>

                                            </div>

                                            <div class="control-group" style="display:none">

										        <label class="control-label">Time start</label>

										        <div class="controls">

											        <div class="input-append bootstrap-timepicker-component start_time">

												        <input class="m-wrap m-ctrl-small timepicker-24" type="text">

												        <span class="add-on"><i class="icon-time"></i></span>

											        </div>

										        </div>

									        </div>

                                            <div class="control-group" style="display:none">

										        <label class="control-label">Time end</label>

										        <div class="controls">

											        <div class="input-append bootstrap-timepicker-component end_time">

												        <input class="m-wrap m-ctrl-small timepicker-24" type="text">

												        <span class="add-on"><i class="icon-time"></i></span>

											        </div>

										        </div>

									        </div>

											<div class="control-group">

												<label class="control-label">Begin Time</label>

												<div class="controls">

													<div class="input-append date form_datetime">

														<input size="16" type="text" value="" readonly class="m-wrap start_time">

														<span class="add-on"><i class="icon-calendar"></i></span>

													</div>

												</div>

											</div>

                                            <div class="control-group">

												<label class="control-label">End Time</label>

												<div class="controls">

													<div class="input-append date form_datetime">

														<input size="16" type="text" value="" readonly class="m-wrap end_time">

														<span class="add-on"><i class="icon-calendar"></i></span>

													</div>

												</div>

											</div>

                                            <div class="control-group">

										        <label class="control-label">Grouped Options</label>

										        <div class="controls">

											        <select multiple="multiple" id="my_multi_select2" name="my_multi_select2[]">

											        </select>

										        </div>

									        </div>

										</form>

									</div>

									<div class="modal-footer">

										<button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>

										<button class="btn green btn-primary" data-dismiss="modal">Save changes</button>

									</div>

								</div>

						</div>                        </div>

						<!-- END EXAMPLE TABLE PORTLET-->
					</div>
				</div>
				<!-- END PAGE CONTENT -->
			</div>
			<!-- END PAGE CONTAINER--> 
		</div>
		<!-- END PAGE -->    
	</div>
	<!-- END CONTAINER -->
    <!-- #include file="footer.html" -->
	<!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->

	<!-- BEGIN CORE PLUGINS -->

	<script src="media/js/jquery-1.10.1.min.js" type="text/javascript"></script>

	<script src="media/js/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>

	<!-- IMPORTANT! Load jquery-ui-1.10.1.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->

	<script src="media/js/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>      

	<script src="media/js/bootstrap.min.js" type="text/javascript"></script>

	<!--[if lt IE 9]>

	<script src="media/js/excanvas.min.js"></script>

	<script src="media/js/respond.min.js"></script>  

	<![endif]-->   

	<script src="media/js/jquery.slimscroll.min.js" type="text/javascript"></script>

	<script src="media/js/jquery.blockui.min.js" type="text/javascript"></script>  

	<script src="media/js/jquery.cookie.min.js" type="text/javascript"></script>

	<script src="media/js/jquery.uniform.min.js" type="text/javascript" ></script>

	<!-- END CORE PLUGINS -->

	<!-- BEGIN PAGE LEVEL PLUGINS -->

	<script type="text/javascript" src="media/js/ckeditor.js"></script>  

	<script type="text/javascript" src="media/js/bootstrap-fileupload.js"></script>

	<script type="text/javascript" src="media/js/chosen.jquery.min.js"></script>

	<script type="text/javascript" src="media/js/select2.min.js"></script>

	<script type="text/javascript" src="media/js/wysihtml5-0.3.0.js"></script> 

	<script type="text/javascript" src="media/js/bootstrap-wysihtml5.js"></script>

	<script type="text/javascript" src="media/js/jquery.tagsinput.min.js"></script>

	<script type="text/javascript" src="media/js/jquery.toggle.buttons.js"></script>

	<script type="text/javascript" src="media/js/bootstrap-datepicker.js"></script>

	<script type="text/javascript" src="media/js/bootstrap-datetimepicker.js"></script>

	<script type="text/javascript" src="media/js/clockface.js"></script>

	<script type="text/javascript" src="media/js/date.js"></script>

	<script type="text/javascript" src="media/js/daterangepicker.js"></script> 

	<script type="text/javascript" src="media/js/bootstrap-colorpicker.js"></script>  

	<script type="text/javascript" src="media/js/bootstrap-timepicker.js"></script>

	<script type="text/javascript" src="media/js/jquery.inputmask.bundle.min.js"></script>   

	<script type="text/javascript" src="media/js/jquery.input-ip-address-control-1.0.min.js"></script>

	<script type="text/javascript" src="media/js/jquery.multi-select.js"></script>   

	<script src="media/js/bootstrap-modal.js" type="text/javascript" ></script>

	<script src="media/js/bootstrap-modalmanager.js" type="text/javascript" ></script> 

	<!-- END PAGE LEVEL PLUGINS -->

	<!-- BEGIN PAGE LEVEL SCRIPTS -->

	<script src="media/js/app.js"></script>

    <script src="media/js/schedule_setting.js"></script>

	<!-- END PAGE LEVEL SCRIPTS -->

	<script>

	    jQuery(document).ready(function () {

	        // initiate layout and plugins

	        App.init();
	        Schedule_Setting.init();

	    });

	</script>

	<!-- END JAVASCRIPTS -->   

<script type="text/javascript">  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script></body>

<!-- END BODY -->
</html>