﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_management.aspx.cs" Inherits="RGDZY.user_management" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
	<meta charset="utf-8" />
	<title>SJTU-Joint Laboratory of Cloud Computing | Pages - User Management</title>
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
	<link rel="stylesheet" type="text/css" href="media/css/select2_metro.css" />
	<link rel="stylesheet" type="text/css" href="media/css/DT_bootstrap.css" />    <link rel="stylesheet" type="text/css" href="media/css/bootstrap-fileupload.css" />

	<link rel="stylesheet" type="text/css" href="media/css/jquery.gritter.css" />

	<link rel="stylesheet" type="text/css" href="media/css/chosen.css" />

	<link rel="stylesheet" type="text/css" href="media/css/select2_metro.css" />

	<link rel="stylesheet" type="text/css" href="media/css/jquery.tagsinput.css" />

	<link rel="stylesheet" type="text/css" href="media/css/bootstrap-wysihtml5.css" />

	<link rel="stylesheet" type="text/css" href="media/css/datepicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/bootstrap-toggle-buttons.css" />

	<link rel="stylesheet" type="text/css" href="media/css/daterangepicker.css" />

	<link rel="stylesheet" type="text/css" href="media/css/bootstrap-modal.css" />    <style type="text/css">
        <!-- 
        .Absolute-Center.is-Image { height: auto; margin:10px}  
        .Absolute-Center.is-Image img { width: 100%; height: auto; } 
        .datatable-scroll { max-height: 800px; overflow-x: auto; overflow-y: visible; }
        .datacell-scroll { max-height: 100px; overflow-x: auto; overflow-y: visible; }
        --> 
    </style>
	<!-- END PAGE LEVEL STYLES -->
	<link rel="shortcut icon" href="media/image/favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="page-header-fixed">
    <uc:Header id="Header_Default" 
        runat="server" /> 
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
							User Management&nbsp<small> user administration</small>
						</h3>
						<ul class="breadcrumb">
							<li>
								<i class="icon-home"></i>
								<a href="index.html">Home</a> 
								<i class="icon-angle-right"></i>
							</li>
							<li>
								<a href="#">Account Management</a>
								<i class="icon-angle-right"></i>
							</li>
							<li><a href="#">User Management</a></li>
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
								<div class="caption"><i class="icon-edit"></i>User Table</div>
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
								<table class="table table-striped table-hover table-bordered" id="sample_editable_1" >
									<thead>
										<tr>

                                            <th style="display:none">Id</th>

											<th>User Name</th>

                                            <th style="display:none">Authority</th>

                                            <th>Group</th>

											<th>Real Name</th>

											<th>Student ID</th>

											<th>Email</th>

                                            <th>Phone</th>

											<th>Hometown</th>

											<th>Birthday</th>

											<th>University</th>

                                            <th>Introduction</th>                                            <th>Edit</th>                                            <th>Delete</th>
										</tr>
									</thead>
									<tbody>
									</tbody>
								</table>                                <div id="form_modal1" onload ="showmodal();" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" >

									<div class="modal-header">

										<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>

										<h3 id="myModalLabel1">User</h3>

									</div>

									<div class="modal-body">

										<form id="u_form" action="#" class="form-horizontal">

                                            <label style="color: dimgray;"> * are mandatory fields</label>
                                            <label id ="BlankId" style="display:none"></label>
                                            <div class="control-group">

        										    <label class="control-label">User Name*</label>

        										    <div class="controls">

		    									    <input id ="UserName" type="text" class="not-nullable medium m-wrap" style="margin: 0 auto;" disabled="disabled">
                                                    <!--ul class="typeahead dropdown-menu" style="top: 531px; left: 436px; display: none;">
                                                        <li data-value="Colorado" class="active"><a href="#">Color<strong>ad</strong>o</a></li>
                                                        <li data-value="Nevada"><a href="#">Nev<strong>ad</strong>a</a></li>
                                                    </ul-->

										        </div>

									        </div>

                                            <div class="control-group">

        										    <label class="control-label">Password</label>

        										    <div class="controls">

		    									        <input id ="Password" type="password" class="not-new-nullable medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

        										    <label class="control-label">Retype Password</label>

        										    <div class="controls">

		    									        <input id ="RetypePassword" type="password" class="not-new-nullable medium m-wrap" style="margin: 0 auto;">
                                                    <img id ="Retype-info" class="Absolute-Center is-Image" src="/media/image/hor-menu-search-close.png" data-dismiss="Inconsistent"/>

										        </div>

									        </div>

                                            <div class="control-group">

        										<label class="control-label">Authority</label>

        										<div class="controls">
                                                    <input type="checkbox" id="checkbox1" value="checkbox"> Schedule 
                                                    <input type="checkbox" id="checkbox2" value="checkbox"> Device&nbsp;&nbsp;  
                                                    <input type="checkbox" id="checkbox3" value="checkbox"> Project&nbsp;<br/>
                                                    <input type="checkbox" id="checkbox4" value="checkbox"> Account&nbsp;
                                                    <input type="checkbox" id="checkbox5" value="checkbox"> Seminar&nbsp;
                                                    <input type="checkbox" id="checkbox6" value="checkbox"> File&nbsp;&nbsp;&nbsp;&nbsp; <br/>
                                                    <input type="checkbox" id="checkbox7" value="checkbox"> Supervisor
                                                    <br/> 

		    									<input id ="Authority" type="number" min="1" value="1" max="511" class="not-nullable medium m-wrap" style="display: none; margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

        										    <label class="control-label">Group*</label>

        										    <div class="controls">

		    									    <input id ="Group" type="text" class="not-nullable medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

        										<label class="control-label">Real Name*</label>

        										<div class="controls">

		    									<input id ="RealName" type="text" class="not-nullable medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

        										    <label class="control-label">Student ID</label>

        										    <div class="controls">

		    									    <input id ="StudentId" type="text" class="medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

										        <label class="control-label">E-Mail*</label>

										        <div class="controls">

											         <input id ="Email" type="text" class="not-nullable medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

										        <label class="control-label">Phone</label>

										        <div class="controls">

											         <input id ="Phone" type="text" class="medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

										        <label class="control-label">Home Town</label>

										        <div class="controls">

											         <input id ="Hometown" type="text" class="medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

										        <label class="control-label">Birthday</label>

										        <div class="controls">

											         <input id ="Birthday" class="medium m-wrap m-ctrl-medium date-picker" readonly="" size="16" type="text" value="">

										        </div>

									        </div>

                                            <div class="control-group">

										        <label class="control-label">University</label>

										        <div class="controls">

											         <input id ="University" type="text" class="medium m-wrap" style="margin: 0 auto;">

										        </div>

									        </div>

                                            <div class="control-group">

										        <label class="control-label">Introduction</label>

										        <div class="controls">

											         <textarea id ="Introduction" class="medium m-wrap" rows="5"></textarea>

										        </div>

									        </div>

										</form>

									</div>

									<div class="modal-footer">

										<button id="close" class="btn" data-dismiss="modal" aria-hidden="true">Close</button>

										<button id="save" class="btn green btn-primary" data-dismiss="modal">Save changes</button>

									</div>

								</div>

							</div>
						</div>
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
	<script type="text/javascript" src="media/js/select2.min.js"></script>
	<script type="text/javascript" src="media/js/jquery.dataTables.js"></script>
	<script type="text/javascript" src="media/js/DT_bootstrap.js"></script>
    
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

	<script type="text/javascript" src="media/js/date.js"></script>

	<script type="text/javascript" src="media/js/jquery.inputmask.bundle.min.js"></script>   

	<script type="text/javascript" src="media/js/jquery.input-ip-address-control-1.0.min.js"></script>

	<script type="text/javascript" src="media/js/jquery.multi-select.js"></script>   

	<script src="media/js/bootstrap-modal.js" type="text/javascript" ></script>

	<script src="media/js/bootstrap-modalmanager.js" type="text/javascript" ></script>         <script src="media/js/crypton-js-3.1.2-sha1.js" type="text/javascript"></script>  
	<!-- END PAGE LEVEL PLUGINS -->
	<!-- BEGIN PAGE LEVEL SCRIPTS -->
	<script src="media/js/app.js"></script>
	<script src ="media/js/user_management.js"></script>

	<script src="media/js/form-components.js"></script>     
	<script>
	    jQuery(document).ready(function () {

	        App.init();	        FormComponents.init();
	        UserMan.init();

	    });
	</script>
<script type="text/javascript">  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script></body>
<!-- END BODY -->
</html>
