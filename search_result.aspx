<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search_result.aspx.cs" Inherits="RGDZY.search_result" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
	<meta charset="utf-8" />
	<title>SJTU-Joint Laboratory of Cloud Computing | Pages - Search Result</title>
	<meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	<!-- BEGIN GLOBAL MANDATORY STYLES -->
	<link href="media/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
	<link href="media/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css"/>
	<link href="media/css/font-awesome.css" rel="stylesheet" type="text/css"/>
	<link href="media/css/style-metro.css" rel="stylesheet" type="text/css"/>
	<link href="media/css/style.css" rel="stylesheet" type="text/css"/>
	<link href="media/css/style-responsive.css" rel="stylesheet" type="text/css"/>
	<link href="media/css/default.css" rel="stylesheet" type="text/css" id="style_color"/>
	<link href="media/css/uniform.default.css" rel="stylesheet" type="text/css"/>
	<!-- END GLOBAL MANDATORY STYLES -->
	<!-- BEGIN PAGE LEVEL STYLES --> 
	<link rel="stylesheet" type="text/css" href="media/css/datepicker.css" />
	<link href="media/css/jquery.fancybox.css" rel="stylesheet" />
	<link href="media/css/search.css" rel="stylesheet" type="text/css"/>
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
			<!-- BEGIN PAGE CONTAINER-->
			<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->
				<div class="row-fluid">
					<div class="span12">
						<!-- BEGIN PAGE TITLE & BREADCRUMB-->
						<h3 class="page-title">
							Search Results <small>search results</small>
						</h3>
						<ul class="breadcrumb">
							<li>
								<i class="icon-home"></i>
								<a href="index.html">Home</a> 
								<i class="icon-angle-right"></i>
							</li>
							<li><a href="#">Search Results</a></li>
						</ul>
						<!-- END PAGE TITLE & BREADCRUMB-->
					</div>
				</div>
				<!-- END PAGE HEADER-->
				<!-- BEGIN PAGE CONTENT-->
				<div class="row-fluid">
					<div class="tabbable tabbable-custom tabbable-full-width">
						<ul class="nav nav-tabs">
							<li class="active"><a data-toggle="tab" href="#tab_2_2">Schedule Search</a></li>
							<li><a data-toggle="tab" href="#tab_2_3">Device Search</a></li>
							<li><a data-toggle="tab" href="#tab_2_5">Project Search</a></li>
							<li ><a data-toggle="tab" href="#tab_1_2">File Search</a></li>
							<li><a data-toggle="tab" href="#tab_1_3">Account Search</a></li>
						</ul>
						<div class="tab-content">
							<div id="tab_2_2" class="tab-pane active">                                <div class="portlet box">
							        <div class="portlet-body">
								        <table class="table table-hover">
									        <thead>
										        <tr>
											        <th>#</th>
											        <th>Title</th>
											        <th>Type</th>
											        <th class="hidden-480">Time</th>
											        <th>Status</th>
										        </tr>
									        </thead>
									        <tbody>
									        </tbody>
								        </table>
							        </div>
						        </div>

							</div>
							<!--end tab-pane-->                            <div id="tab_2_3" class="tab-pane">

						        <div class="portlet box">
							        <div class="portlet-body">
								        <table class="table table-hover">
									        <thead>
										        <tr>
											        <th>#</th>
											        <th>Asset No.</th>
											        <th>Type</th>
											        <th class="hidden-480">Owner</th>
											        <th>Remark</th>
										        </tr>
									        </thead>
									        <tbody>
									        </tbody>
								        </table>
							        </div>
						        </div>                            </div>
							<!--end tab-pane-->
							<div id="tab_2_5" class="tab-pane">                                <div class="portlet box">
							        <div class="portlet-body">
								        <table class="table table-hover">
									        <thead>
										        <tr>
											        <th>#</th>
											        <th>Project Name</th>
											        <th>Description</th>                                                    <th>Members</th>
											        <th>Project Detail</th>
										        </tr>
									        </thead>
									        <tbody>
									        </tbody>
								        </table>
							        </div>
						        </div>

							</div>
							<!--end tab-pane-->
							<div id="tab_1_2" class="tab-pane">                                <div class="portlet box">
							        <div class="portlet-body">
								        <table class="table table-hover">
									        <thead>
										        <tr>
											        <th>#</th>
											        <th>File Name</th>
											        <th>Owner</th>                                                    <th>Print Time</th>
										        </tr>
									        </thead>
									        <tbody>
									        </tbody>
								        </table>
							        </div>
						        </div>
							</div>
							<!--end tab-pane-->
							<div id="tab_1_3" class="tab-pane">                                <div class="portlet box">
							        <div class="portlet-body">
								        <table class="table table-hover">
									        <thead>
										        <tr>
											        <th>#</th>
											        <th>Real Name</th>                                                    <th>Team</th>
											        <th>Email</th>                                                    <th>Telephone</th>                                                    <th>Link</th>

										        </tr>
									        </thead>
									        <tbody>
									        </tbody>
								        </table>
							        </div>
						        </div>

							</div>
							<!--end tab-pane-->
						</div>
					</div>
					<!--end tabbable-->           
				</div>
				<!-- END PAGE CONTENT-->
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
	<script type="text/javascript" src="media/js/bootstrap-datepicker.js"></script>
	<script src="media/js/jquery.fancybox.pack.js"></script>
	<script src="media/js/app.js"></script>
	<script src="media/js/search_result.js"></script>      
	<script>
	    jQuery(document).ready(function () {

	        App.init();

	        SearchResult.init();

	    });
	</script>
	<!-- END JAVASCRIPTS -->
<script type="text/javascript">  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script></body>
<!-- END BODY -->
</html>