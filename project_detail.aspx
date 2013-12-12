<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="project_detail.aspx.cs" Inherits="RGDZY.project_detail" %>

<!-- <%@ Register TagPrefix="uc" TagName="Menu" Src="~/control/Menu.ascx" %>-->
<!DOCTYPE html>

<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->

<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->

<!--[if !IE]><!--> <html lang="en" class="no-js"> <!--<![endif]-->

<!-- BEGIN HEAD -->

<head>

	<meta charset="utf-8" />

	<title>SJTU-Joint Laboratory of Cloud Computing | Pages - Project Detail</title>

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

	<!-- END PAGE LEVEL STYLES -->

	<link rel="shortcut icon" href="media/image/favicon.ico" />

</head>

<!-- END HEAD -->

<!-- BEGIN BODY -->

<body class="page-header-fixed">

    <!-- #include file="header.html" -->

	<!-- BEGIN CONTAINER -->

	<div class="page-container">

        <uc:Menu id="Menu_Default" runat="server" MinValue="1" MaxValue="10" />

		<!-- BEGIN PAGE -->

		<div class="page-content">

			<!-- BEGIN PAGE CONTAINER-->

			<div class="container-fluid">

				<!-- BEGIN PAGE HEADER-->

				<div class="row-fluid">

					<div class="span12">

						<!-- BEGIN PAGE TITLE & BREADCRUMB-->

						<h3 class="page-title">

							Project Detail <small>project</small>

						</h3>

						<ul class="breadcrumb">

							<li>

								<i class="icon-home"></i>

								<a href="default.aspx">Home</a> 

								<i class="icon-angle-right"></i>

							</li>

							<li><a href="project_list.aspx">Project List</a></li>

							<li class="pull-right no-text-shadow">

							</li>

						</ul>

						<!-- END PAGE TITLE & BREADCRUMB-->

					</div>

                    <div class="span6">

							<!-- BEGIN PORTLET-->

							<div class="portlet">

								<div class="portlet-title line">

									<div class="caption"><i class="icon-comments"></i>Chats</div>

								</div>

								<div class="portlet-body" id="chats">

									<div class="scroller" data-height="435px" data-always-visible="1" data-rail-visible1="1">

										<ul class="chats">

											<li class="in">

												<img class="avatar" alt="" src="media/image/avatar1.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Bob Nilson</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="out">

												<img class="avatar" alt="" src="media/image/avatar2.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Lisa Wong</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="in">

												<img class="avatar" alt="" src="media/image/avatar1.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Bob Nilson</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="out">

												<img class="avatar" alt="" src="media/image/avatar3.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Richard Doe</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="in">

												<img class="avatar" alt="" src="media/image/avatar3.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Richard Doe</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="out">

												<img class="avatar" alt="" src="media/image/avatar1.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Bob Nilson</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="in">

												<img class="avatar" alt="" src="media/image/avatar3.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Richard Doe</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, 

													sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.

													</span>

												</div>

											</li>

											<li class="out">

												<img class="avatar" alt="" src="media/image/avatar1.jpg" />

												<div class="message">

													<span class="arrow"></span>

													<a href="#" class="name">Bob Nilson</a>

													<span class="datetime">at Jul 25, 2012 11:09</span>

													<span class="body">

													Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. sed diam nonummy nibh euismod tincidunt ut laoreet.

													</span>

												</div>

											</li>

										</ul>

									</div>

									<div class="chat-form">

										<div class="input-cont">   

											<input class="m-wrap" type="text" placeholder="Type a message here..." />

										</div>

										<div class="btn-cont"> 

											<span class="arrow"></span>

											<a href="" class="btn blue icn-only"><i class="icon-ok icon-white"></i></a>

										</div>

									</div>

								</div>

							</div>

							<!-- END PORTLET-->

						</div>

				</div>

				<!-- END PAGE HEADER-->

			</div>

			<!-- END PAGE CONTAINER-->    

		</div>

		<!-- END PAGE -->

	</div>

    <!-- #include file="footer.html" -->

	<!-- END CONTAINER -->

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

	<script src="media/js/jquery.vmap.js" type="text/javascript"></script>   

	<script src="media/js/jquery.vmap.russia.js" type="text/javascript"></script>

	<script src="media/js/jquery.vmap.world.js" type="text/javascript"></script>

	<script src="media/js/jquery.vmap.europe.js" type="text/javascript"></script>

	<script src="media/js/jquery.vmap.germany.js" type="text/javascript"></script>

	<script src="media/js/jquery.vmap.usa.js" type="text/javascript"></script>

	<script src="media/js/jquery.vmap.sampledata.js" type="text/javascript"></script>  

	<script src="media/js/jquery.flot.js" type="text/javascript"></script>

	<script src="media/js/jquery.flot.resize.js" type="text/javascript"></script>

	<script src="media/js/jquery.pulsate.min.js" type="text/javascript"></script>

	<script src="media/js/date.js" type="text/javascript"></script>

	<script src="media/js/daterangepicker.js" type="text/javascript"></script>     

	<script src="media/js/jquery.gritter.js" type="text/javascript"></script>

	<script src="media/js/fullcalendar.min.js" type="text/javascript"></script>

	<script src="media/js/jquery.easy-pie-chart.js" type="text/javascript"></script>

	<script src="media/js/jquery.sparkline.min.js" type="text/javascript"></script>  

	<!-- END PAGE LEVEL PLUGINS -->

	<!-- BEGIN PAGE LEVEL SCRIPTS -->

	<script src="media/js/app.js" type="text/javascript"></script>
    <script src="media/js/project_detail.js" type="text/javascript"></script>

	<!-- END PAGE LEVEL SCRIPTS -->  

	<script>

	    jQuery(document).ready(function () {

	        App.init(); // initlayout and core plugins

	        Project_Detail.init();

	    });

	</script>

	<!-- END JAVASCRIPTS -->

    <script type="text/javascript">  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script></body>

<!-- END BODY -->

</html>