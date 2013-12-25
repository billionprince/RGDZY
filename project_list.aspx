<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="project_list.aspx.cs" Inherits="RGDZY.project_list" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
	<meta charset="utf-8" />
	<title>SJTU-Joint Laboratory of Cloud Computing | Pages - Project List</title>
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
	<link rel="stylesheet" href="media/css/DT_bootstrap.css" />
	<!-- END PAGE LEVEL STYLES -->
	<link rel="shortcut icon" href="media/image/favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="page-header-fixed">
    <!-- #include file="header.html" -->
	<!-- BEGIN CONTAINER -->
	<div class="page-container row-fluid">
        <uc:Menu id="Menu_Default" 
        runat="server" />    
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
						<h3 class="page-title">Project List</h3>
						<ul class="breadcrumb">
							<li>
								<i class="icon-home"></i>
								<a href="index.html">Home</a> 
								<i class="icon-angle-right"></i>
							</li>
							<li>
								<a href="#">Project Management</a>
								<i class="icon-angle-right"></i>
							</li>
							<li><a href="#">Project List</a></li>
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
								<div class="caption"><i class="icon-edit"></i>List Table</div>
								<div class="tools">
									<a href="javascript:;" class="collapse"></a>
									<a href="#portlet-config" data-toggle="modal" class="config"></a>
									<a href="javascript:;" class="reload"></a>
									<a href="javascript:;" class="remove"></a>
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

										<tr>                                            <th style="display:none">Id</th>

											<th>Brief Name</th>

											<th>Full Name</th>

											<th>Description</th>

											<th>Hyperlink</th>

											<th>Edit</th>

											<th>Delete</th>

										</tr>

									</thead>

									<tbody id="projecttable">

									</tbody>

								</table>                                <div id="form_modal1" onload ="showmodal();" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" >

									<div class="modal-header">

										<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>

										<h3 id="myModalLabel1">Project Edit</h3>

									</div>

									<div class="modal-body">

										<form id="project_form" action="#" class="form-horizontal">

                                            <label id ="projectid" style="display:none"></label>

                                            <div class="control-group">

        										<label  class="control-label">Brief Name</label>

        										<div class="controls">

		    									<input id ="briefname" type="text" class="medium m-wrap" style="margin: 0 auto;" data-provide="typeahead" data-items="4">
                                                    <ul class="typeahead dropdown-menu" style="top: 531px; left: 436px; display: none;">
                                                        <li data-value="Colorado" class="active"><a href="#">Color<strong>ad</strong>o</a></li>
                                                        <li data-value="Nevada"><a href="#">Nev<strong>ad</strong>a</a></li>

                                                    </ul>

										        </div>

									        </div>

                                            <div class="control-group">

        										<label  class="control-label">Full Name</label>

        										<div class="controls">

		    									<input id ="fullname" type="text" class="medium m-wrap" style="margin: 0 auto;" data-provide="typeahead" data-items="4">
                                                    <ul class="typeahead dropdown-menu" style="top: 531px; left: 436px; display: none;">
                                                        <li data-value="Colorado" class="active"><a href="#">Color<strong>ad</strong>o</a></li>
                                                        <li data-value="Nevada"><a href="#">Nev<strong>ad</strong>a</a></li>

                                                    </ul>

										        </div>

									        </div>

                                            <div class="control-group">

        										<label  class="control-label">Description</label>

        										<div class="controls">

		    									<input id ="description" type="text" class="medium m-wrap" style="margin: 0 auto;" data-provide="typeahead" data-items="4">
                                                    <ul class="typeahead dropdown-menu" style="top: 531px; left: 436px; display: none;">
                                                        <li data-value="Colorado" class="active"><a href="#">Color<strong>ad</strong>o</a></li>
                                                        <li data-value="Nevada"><a href="#">Nev<strong>ad</strong>a</a></li>

                                                    </ul>

										        </div>

									        </div>

                                            <div class="control-group">

        										<label  class="control-label">Hyperlink</label>

        										<div class="controls">

		    									<input id ="hyperlink" type="text" class="medium m-wrap" style="margin: 0 auto;" data-provide="typeahead" data-items="4">
                                                    <ul class="typeahead dropdown-menu" style="top: 531px; left: 436px; display: none;">
                                                        <li data-value="Colorado" class="active"><a href="#">Color<strong>ad</strong>o</a></li>
                                                        <li data-value="Nevada"><a href="#">Nev<strong>ad</strong>a</a></li>

                                                    </ul>

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
	<!-- END PAGE LEVEL PLUGINS -->
	<!-- BEGIN PAGE LEVEL SCRIPTS -->
	<script src="media/js/app.js"></script>
	<script src="media/js/table-editable.js"></script>    
	<script src="media/js/project_list.js"></script>
	<script>
	    jQuery(document).ready(function () {

	        App.init();	        PROJECTS.init();

	    });
    </script>
<script type="text/javascript">  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script></body>
<!-- END BODY -->
</html>