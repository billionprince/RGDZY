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
							<div id="tab_2_2" class="tab-pane active">
								<div class="space40"></div>
								<div class="row-fluid">
									<div class="row-fluid margin-bottom-20">
										<div class="span6 booking-blocks">
											<div class="pull-left booking-img">
												<img src="media/image/image4.jpg" alt="">
												<ul class="unstyled">
													<li><i class="icon-money"></i> From $126</li>
													<li><i class="icon-map-marker"></i> Tioman, Malaysia</li>
												</ul>
											</div>
											<div style="overflow:hidden;">
												<h2><a href="#">Here Any Title</a></h2>
												<ul class="unstyled inline">
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star-empty"></i></li>
												</ul>
												<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum. <a href="#">read more</a></p>
											</div>
										</div>
										<div class="span6 booking-blocks">
											<div class="pull-left booking-img">
												<img src="media/image/image5.jpg" alt="">
												<ul class="unstyled">
													<li><i class="icon-money"></i> From $157</li>
													<li><i class="icon-map-marker"></i> London, UK</li>
												</ul>
											</div>
											<div style="overflow:hidden;">
												<h2><a href="#">Here Any Title</a></h2>
												<ul class="unstyled inline">
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
												</ul>
												<p>Lorem ipsum dolor sit eos et accusamus et iusto odio amet, consectetur adipiscing elit. Ut non libero magna. Sed et quam lacus usce condimentum eleifend enim a sunt in culpa qui officia feugiat. Pellentesque dolores et quas molestias viverra vehicula sem ut volutpat. Integer sed arcu. <a href="#">read more</a></p>
											</div>
										</div>
									</div>
									<div class="row-fluid margin-bottom-20">
										<div class="span6 booking-blocks">
											<div class="pull-left booking-img">
												<img src="media/image/image2.jpg" alt="">
												<ul class="unstyled">
													<li><i class="icon-money"></i> From $126</li>
													<li><i class="icon-map-marker"></i> Tioman, Malaysia</li>
												</ul>
											</div>
											<div style="overflow:hidden;">
												<h2><a href="#">Here Any Title</a></h2>
												<ul class="unstyled inline">
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star-empty"></i></li>
												</ul>
												<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum. <a href="#">read more</a></p>
											</div>
										</div>
										<div class="span6 booking-blocks">
											<div class="pull-left booking-img">
												<img src="media/image/image3.jpg" alt="">
												<ul class="unstyled">
													<li><i class="icon-money"></i> From $157</li>
													<li><i class="icon-map-marker"></i> London, UK</li>
												</ul>
											</div>
											<div style="overflow:hidden;">
												<h2><a href="#">Here Any Title</a></h2>
												<ul class="unstyled inline">
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
													<li><i class="icon-star"></i></li>
												</ul>
												<p>Lorem ipsum dolor sit eos et accusamus et iusto odio amet, consectetur adipiscing elit. Ut non libero magna. Sed et quam lacus usce condimentum eleifend enim a sunt in culpa qui officia feugiat. Pellentesque dolores et quas molestias viverra vehicula sem ut volutpat. Integer sed arcu. <a href="#">read more</a></p>
											</div>
										</div>
									</div>
								</div>
							</div>
							<!--end tab-pane-->                            <div id="tab_2_3" class="tab-pane active">

						        <div class="portlet box">
							        <div class="portlet-body">
								        <table class="table table-hover">
									        <thead>
										        <tr>
											        <th>#</th>
											        <th>First Name</th>
											        <th>Last Name</th>
											        <th class="hidden-480">Username</th>
											        <th>Status</th>
										        </tr>
									        </thead>
									        <tbody>
										        <tr>
											        <td>1</td>
											        <td>Mark</td>
											        <td>Otto</td>
											        <td class="hidden-480">makr124</td>
											        <td><span class="label label-success">Approved</span></td>
										        </tr>
										        <tr>
											        <td>2</td>
											        <td>Jacob</td>
											        <td>Nilson</td>
											        <td class="hidden-480">jac123</td>
											        <td><span class="label label-info">Pending</span></td>
										        </tr>
										        <tr>
											        <td>3</td>
											        <td>Larry</td>
											        <td>Cooper</td>
											        <td class="hidden-480">lar</td>
											        <td><span class="label label-warning">Suspended</span></td>
										        </tr>
										        <tr>
											        <td>3</td>
											        <td>Sandy</td>
											        <td>Lim</td>
											        <td class="hidden-480">sanlim</td>
											        <td><span class="label label-danger">Blocked</span></td>
										        </tr>
									        </tbody>
								        </table>
							        </div>
						        </div>                            </div>
							<!--end tab-pane-->
							<div id="tab_2_5" class="tab-pane">
								<div class="row-fluid search-forms search-default">
									<form class="form-search" action="#">
										<div class="chat-form">
											<div class="input-cont">   
												<input type="text" placeholder="Search..." class="m-wrap" />
											</div>
											<button type="button" class="btn green">
											Search &nbsp; 
											<i class="m-icon-swapright m-icon-white"></i>
											</button>
										</div>
									</form>
								</div>
								<div class="row-fluid search-images">
									<ul class="thumbnails">
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image1.jpg">
											<img src="media/image/image1.jpg" alt="">
											<span><em>600 x 400 - keenthemes.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="Photo" href="media/image/image2.jpg">
											<img src="media/image/image2.jpg" alt="">
											<span><em>220 x 340 - example.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image1.jpg">
											<img src="media/image/image1.jpg" alt="">
											<span><em>800 x 540 - demo.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image5.jpg">
											<img src="media/image/image5.jpg" alt="">
											<span><em>390 x 220 - keenthemes.com</em></span>
											</a>
										</li>
									</ul>
									<ul class="thumbnails">
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image2.jpg">
											<img src="media/image/image2.jpg" alt="">
											<span><em>600 x 400 - keenthemes.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image5.jpg">
											<img src="media/image/image5.jpg" alt="">
											<span><em>220 x 340 - example.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image2.jpg">
											<img src="media/image/image2.jpg" alt="">
											<span><em>800 x 540 - demo.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image1.jpg">
											<img src="media/image/image1.jpg" alt="">
											<span><em>390 x 220 - keenthemes.com</em></span>
											</a>
										</li>
									</ul>
									<ul class="thumbnails">
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image3.jpg">
											<img src="media/image/image3.jpg" alt="">
											<span><em>600 x 400 - keenthemes.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image5.jpg">
											<img src="media/image/image5.jpg" alt="">
											<span><em>220 x 340 - example.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image2.jpg">
											<img src="media/image/image2.jpg" alt="">
											<span><em>800 x 540 - demo.com</em></span>
											</a>
										</li>
										<li class="span3">
											<a class="fancybox-button" data-rel="fancybox-button" title="390 x 220 - keenthemes.com" href="media/image/image1.jpg">
											<img src="media/image/image1.jpg" alt="">
											<span><em>390 x 220 - keenthemes.com</em></span>
											</a>
										</li>
									</ul>
								</div>
								<div class="spac40"></div>
								<div class="pagination pagination-right">
									<ul>
										<li><a href="#">Prev</a></li>
										<li><a href="#">1</a></li>
										<li><a href="#">2</a></li>
										<li class="active"><a href="#">3</a></li>
										<li><a href="#">4</a></li>
										<li><a href="#">5</a></li>
										<li><a href="#">Next</a></li>
									</ul>
								</div>
							</div>
							<!--end tab-pane-->
							<div id="tab_1_2" class="tab-pane">
								<div class="row-fluid search-forms search-default">
									<form class="form-search" action="#">
										<div class="chat-form">
											<div class="input-cont">   
												<input type="text" placeholder="Search..." class="m-wrap" />
											</div>
											<button type="button" class="btn green">Search &nbsp; <i class="m-icon-swapright m-icon-white"></i></button>
										</div>
									</form>
								</div>
								<div class="row-fluid portfolio-block">
									<div class="span5 portfolio-text">
										<img src="media/image/logo_metronic.jpg" alt="" />
										<div class="portfolio-text-info">
											<h4>Metronic - Responsive Template</h4>
											<p>Lorem ipsum dolor sit consectetuer adipiscing elit.</p>
										</div>
									</div>
									<div class="span5">
										<div class="portfolio-info">
											Today Sold
											<span>187</span>
										</div>
										<div class="portfolio-info">
											Total Sold
											<span>1789</span>
										</div>
										<div class="portfolio-info">
											Earnings
											<span>$37.240</span>
										</div>
									</div>
									<div class="span2 portfolio-btn">
										<a href="#" class="btn bigicn-only"><span>View</span></a>                        
									</div>
								</div>
								<div class="row-fluid portfolio-block">
									<div class="span5 portfolio-text">
										<img src="media/image/logo_azteca.jpg" alt="" />
										<div class="portfolio-text-info">
											<h4>Metronic - Responsive Template</h4>
											<p>Lorem ipsum dolor sit consectetuer adipiscing elit.</p>
										</div>
									</div>
									<div class="span5">
										<div class="portfolio-info">
											Today Sold
											<span>24</span>
										</div>
										<div class="portfolio-info">
											Total Sold
											<span>660</span>
										</div>
										<div class="portfolio-info">
											Earnings
											<span>$7.060</span>
										</div>
									</div>
									<div class="span2 portfolio-btn">
										<a href="#" class="btn bigicn-only"><span>View</span></a>                        
									</div>
								</div>
								<div class="row-fluid portfolio-block">
									<div class="span5 portfolio-text">
										<img src="media/image/logo_conquer.jpg" alt="" />
										<div class="portfolio-text-info">
											<h4>Metronic - Responsive Template</h4>
											<p>Lorem ipsum dolor sit consectetuer adipiscing elit.</p>
										</div>
									</div>
									<div class="span5">
										<div class="portfolio-info">
											Today Sold
											<span>24</span>
										</div>
										<div class="portfolio-info">
											Total Sold
											<span>975</span>
										</div>
										<div class="portfolio-info">
											Earnings
											<span>$21.700</span>
										</div>
									</div>
									<div class="span2 portfolio-btn">
										<a href="#" class="btn bigicn-only"><span>View</span></a>                        
									</div>
								</div>
								<div class="row-fluid portfolio-block">
									<div class="span5 portfolio-text">
										<img src="media/image/logo_azteca.jpg" alt="" />
										<div class="portfolio-text-info">
											<h4>Metronic - Responsive Template</h4>
											<p>Lorem ipsum dolor sit consectetuer adipiscing elit.</p>
										</div>
									</div>
									<div class="span5">
										<div class="portfolio-info">
											Today Sold
											<span>24</span>
										</div>
										<div class="portfolio-info">
											Total Sold
											<span>975</span>
										</div>
										<div class="portfolio-info">
											Earnings
											<span>$21.700</span>
										</div>
									</div>
									<div class="span2 portfolio-btn">
										<a href="#" class="btn bigicn-only"><span>View</span></a>                        
									</div>
								</div>
								<div class="row-fluid portfolio-block">
									<div class="span5 portfolio-text">
										<img src="media/image/logo_conquer.jpg" alt="" />
										<div class="portfolio-text-info">
											<h4>Metronic - Responsive Template</h4>
											<p>Lorem ipsum dolor sit consectetuer adipiscing elit .</p>
										</div>
									</div>
									<div class="span5">
										<div class="portfolio-info">
											Today Sold
											<span>24</span>
										</div>
										<div class="portfolio-info">
											Total Sold
											<span>975</span>
										</div>
										<div class="portfolio-info">
											Earnings
											<span>$21.700</span>
										</div>
									</div>
									<div class="span2 portfolio-btn">
										<a href="#" class="btn bigicn-only"><span>View</span></a>                        
									</div>
								</div>
								<div class="row-fluid portfolio-block">
									<div class="span5 portfolio-text">
										<img src="media/image/logo_azteca.jpg" alt="" />
										<div class="portfolio-text-info">
											<h4>Metronic - Responsive Template</h4>
											<p>Lorem ipsum dolor sit consectetuer adipiscing elit.</p>
										</div>
									</div>
									<div class="span5">
										<div class="portfolio-info">
											Today Sold
											<span>24</span>
										</div>
										<div class="portfolio-info">
											Total Sold
											<span>975</span>
										</div>
										<div class="portfolio-info">
											Earnings
											<span>$21.700</span>
										</div>
									</div>
									<div class="span2 portfolio-btn">
										<a href="#" class="btn bigicn-only"><span>View</span></a>                        
									</div>
								</div>
								<div class="space5"></div>
								<div class="pagination pagination-right">
									<ul>
										<li><a href="#">Prev</a></li>
										<li><a href="#">1</a></li>
										<li><a href="#">2</a></li>
										<li class="active"><a href="#">3</a></li>
										<li><a href="#">4</a></li>
										<li><a href="#">5</a></li>
										<li><a href="#">Next</a></li>
									</ul>
								</div>
							</div>
							<!--end tab-pane-->
							<div id="tab_1_3" class="tab-pane">
								<div class="row-fluid search-forms search-default">
									<form class="form-search" action="#">
										<div class="chat-form">
											<div class="input-cont">   
												<input type="text" placeholder="Search..." class="m-wrap" />
											</div>
											<button type="button" class="btn green">Search &nbsp; <i class="m-icon-swapright m-icon-white"></i></button>
										</div>
									</form>
								</div>
								<div class="search-classic">
									<h4><a href="#">Metronic - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Metronic is a responsive admin dashboard template powered with Twitter Bootstrap Framework for admin and backend applications. Metronic has a clean and intuitive metro style design which makes your next project look awesome and yet user friendly..</p>
								</div>
								<div class="search-classic">
									<h4><a href="#">Conquer - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Conquer is a responsive admin dashboard template created mainly for admin and backend applications(CMS, CRM, Custom Admin Application, Admin Dashboard). Conquer template powered with Twitter Bootstrap Framework..</p>
								</div>
								<div class="search-classic">
									<h4><a href="#">Metronic - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Metronic is a responsive admin dashboard template powered with Twitter Bootstrap Framework for admin and backend applications. Metronic has a clean and intuitive metro style design which makes your next project look awesome and yet user friendly..</p>
								</div>
								<div class="search-classic">
									<h4><a href="#">Conquer - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Conquer is a responsive admin dashboard template created mainly for admin and backend applications(CMS, CRM, Custom Admin Application, Admin Dashboard). Conquer template powered with Twitter Bootstrap Framework..</p>
								</div>
								<div class="search-classic">
									<h4><a href="#">Conquer - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Conquer is a responsive admin dashboard template created mainly for admin and backend applications(CMS, CRM, Custom Admin Application, Admin Dashboard). Conquer template powered with Twitter Bootstrap Framework..</p>
								</div>
								<div class="search-classic">
									<h4><a href="#">Metronic - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Metronic is a responsive admin dashboard template powered with Twitter Bootstrap Framework for admin and backend applications. Metronic has a clean and intuitive metro style design which makes your next project look awesome and yet user friendly..</p>
								</div>
								<div class="search-classic">
									<h4><a href="#">Conquer - Responsive Admin Dashboard Template</a></h4>
									<a href="#">http://www.keenthemes.com</a>
									<p>Conquer is a responsive admin dashboard template created mainly for admin and backend applications(CMS, CRM, Custom Admin Application, Admin Dashboard). Conquer template powered with Twitter Bootstrap Framework..</p>
								</div>
								<div class="space5"></div>
								<div class="pagination pagination-centered">
									<ul>
										<li><a href="#">Prev</a></li>
										<li><a href="#">1</a></li>
										<li><a href="#">2</a></li>
										<li class="active"><a href="#">3</a></li>
										<li><a href="#">4</a></li>
										<li><a href="#">5</a></li>
										<li><a href="#">Next</a></li>
									</ul>
								</div>
							</div>
							<!--end tab-pane-->
							<div id="tab_1_4" class="tab-pane">
								<div class="row-fluid search-forms search-default">
									<form class="form-search" action="#">
										<div class="chat-form">
											<div class="input-cont">   
												<input type="text" placeholder="Search..." class="m-wrap" />
											</div>
											<button type="button" class="btn green">Search &nbsp; <i class="m-icon-swapright m-icon-white"></i></button>
										</div>
									</form>
								</div>
								<div class="portlet-body" style="display: block;">
									<table class="table table-striped table-bordered table-advance table-hover">
										<thead>
											<tr>
												<th><i class="icon-briefcase"></i> Company</th>
												<th class="hidden-phone"><i class="icon-question-sign"></i> Descrition</th>
												<th><i class="icon-bookmark"></i> Amount</th>
												<th></th>
											</tr>
										</thead>
										<tbody>
											<tr>
												<td><a href="#">Pixel Ltd</a></td>
												<td class="hidden-phone">Server hardware purchase</td>
												<td>52560.10$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													Smart House
													</a>  
												</td>
												<td class="hidden-phone">Office furniture purchase</td>
												<td>5760.00$ <span class="label label-warning label-mini">Pending</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													FoodMaster Ltd
													</a>
												</td>
												<td class="hidden-phone">Company Anual Dinner Catering</td>
												<td>12400.00$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													WaterPure Ltd
													</a>
												</td>
												<td class="hidden-phone">Payment for Jan 2013</td>
												<td>610.50$ <span class="label label-danger label-mini">Overdue</span></td>
												<td><a class="btn mini red-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													Smart House
													</a>  
												</td>
												<td class="hidden-phone">Office furniture purchase</td>
												<td>5760.00$ <span class="label label-warning label-mini">Pending</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													FoodMaster Ltd
													</a>
												</td>
												<td class="hidden-phone">Company Anual Dinner Catering</td>
												<td>12400.00$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													WaterPure Ltd
													</a>
												</td>
												<td class="hidden-phone">Payment for Jan 2013</td>
												<td>610.50$ <span class="label label-danger label-mini">Overdue</span></td>
												<td><a class="btn mini red-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><a href="#">Pixel Ltd</a></td>
												<td class="hidden-phone">Server hardware purchase</td>
												<td>52560.10$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													Smart House
													</a>  
												</td>
												<td class="hidden-phone">Office furniture purchase</td>
												<td>5760.00$ <span class="label label-warning label-mini">Pending</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													FoodMaster Ltd
													</a>
												</td>
												<td class="hidden-phone">Company Anual Dinner Catering</td>
												<td>12400.00$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><a href="#">Pixel Ltd</a></td>
												<td class="hidden-phone">Server hardware purchase</td>
												<td>52560.10$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													Smart House
													</a>  
												</td>
												<td class="hidden-phone">Office furniture purchase</td>
												<td>5760.00$ <span class="label label-warning label-mini">Pending</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td>
													<a href="#">
													FoodMaster Ltd
													</a>
												</td>
												<td class="hidden-phone">Company Anual Dinner Catering</td>
												<td>12400.00$ <span class="label label-success label-mini">Paid</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
										</tbody>
									</table>
								</div>
								<div class="space5"></div>
								<div class="pagination pagination-right">
									<ul>
										<li><a href="#">Prev</a></li>
										<li><a href="#">1</a></li>
										<li><a href="#">2</a></li>
										<li class="active"><a href="#">3</a></li>
										<li><a href="#">4</a></li>
										<li><a href="#">5</a></li>
										<li><a href="#">Next</a></li>
									</ul>
								</div>
							</div>
							<!--end tab-pane-->
							<div id="tab_1_5" class="tab-pane">
								<div class="row-fluid search-forms search-default">
									<form class="form-search" action="#">
										<div class="chat-form">
											<div class="input-cont">   
												<input type="text" placeholder="Search..." class="m-wrap" />
											</div>
											<button type="button" class="btn green">Search &nbsp; <i class="m-icon-swapright m-icon-white"></i></button>
										</div>
									</form>
								</div>
								<div class="portlet-body">
									<table class="table table-striped table-hover">
										<thead>
											<tr>
												<th>Photo</th>
												<th class="hidden-phone">Fullname</th>
												<th>Username</th>
												<th class="hidden-phone">Joined</th>
												<th class="hidden-phone">Points</th>
												<th>Status</th>
												<th></th>
											</tr>
										</thead>
										<tbody>
											<tr>
												<td><img src="media/image/avatar1.jpg" alt="" /></td>
												<td class="hidden-phone">Mark Nilson</td>
												<td>makr124</td>
												<td class="hidden-phone">19 Jan 2012</td>
												<td class="hidden-phone">1245</td>
												<td><span class="label label-success">Approved</span></td>
												<td><a class="btn mini red-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar2.jpg" alt="" /></td>
												<td class="hidden-phone">Filip Rolton</td>
												<td>jac123</td>
												<td class="hidden-phone">09 Feb 2012</td>
												<td class="hidden-phone">15</td>
												<td><span class="label label-info">Pending</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar3.jpg" alt="" /></td>
												<td class="hidden-phone">Colin Fox</td>
												<td>col</td>
												<td class="hidden-phone">19 Jan 2012</td>
												<td class="hidden-phone">245</td>
												<td><span class="label label-warning">Suspended</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar.png" alt="" /></td>
												<td class="hidden-phone">Nick Stone</td>
												<td>sanlim</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">565</td>
												<td><span class="label label-danger">Blocked</span></td>
												<td><a class="btn mini red-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar1.jpg" alt="" /></td>
												<td class="hidden-phone">Edward Cook</td>
												<td>sanlim</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">45245</td>
												<td><span class="label label-danger">Blocked</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar.png" alt="" /></td>
												<td class="hidden-phone">Nick Stone</td>
												<td>sanlim</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">24512</td>
												<td><span class="label label-danger">Blocked</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar1.jpg" alt="" /></td>
												<td class="hidden-phone">Elis Lim</td>
												<td>makr124</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">145</td>
												<td><span class="label label-success">Approved</span></td>
												<td><a class="btn mini red-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar2.jpg" alt="" /></td>
												<td class="hidden-phone">King Paul</td>
												<td>king123</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">456</td>
												<td><span class="label label-info">Pending</span></td>
												<td><a class="btn mini blue-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar3.jpg" alt="" /></td>
												<td class="hidden-phone">Filip Larson</td>
												<td>fil</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">12453</td>
												<td><span class="label label-warning">Suspended</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar.png" alt="" /></td>
												<td class="hidden-phone">Martin Larson</td>
												<td>larson12</td>
												<td class="hidden-phone">01 Apr 2011</td>
												<td class="hidden-phone">2453</td>
												<td><span class="label label-danger">Blocked</span></td>
												<td><a class="btn mini red-stripe" href="#">View</a></td>
											</tr>
											<tr>
												<td><img src="media/image/avatar1.jpg" alt="" /></td>
												<td class="hidden-phone">King Paul</td>
												<td>sanlim</td>
												<td class="hidden-phone">11 Mar 2012</td>
												<td class="hidden-phone">905</td>
												<td><span class="label label-danger">Blocked</span></td>
												<td><a class="btn mini green-stripe" href="#">View</a></td>
											</tr>
										</tbody>
									</table>
								</div>
								<div class="space5"></div>
								<div class="pagination pagination-right">
									<ul>
										<li><a href="#">Prev</a></li>
										<li><a href="#">1</a></li>
										<li><a href="#">2</a></li>
										<li class="active"><a href="#">3</a></li>
										<li><a href="#">4</a></li>
										<li><a href="#">5</a></li>
										<li><a href="#">Next</a></li>
									</ul>
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