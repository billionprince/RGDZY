<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_profile.aspx.cs" Inherits="RGDZY.user_profile" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
	<meta charset="utf-8" />
	<title>SJTU-Joint Laboratory of Cloud Computing | Pages - User Profile</title>
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
	<link href="media/css/bootstrap-fileupload.css" rel="stylesheet" type="text/css" />
	<link href="media/css/chosen.css" rel="stylesheet" type="text/css" />
	<link href="media/css/profile.css" rel="stylesheet" type="text/css" />
	<!-- END PAGE LEVEL STYLES -->
	<link rel="shortcut icon" href="media/image/favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="page-header-fixed">
    <!-- #include file="header.html" -->
	<!-- BEGIN CONTAINER -->   
	<div class="page-container row-fluid">
        <!-- #include file="menu.html" -->
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
							User Profile <small>user profile</small>
						</h3>
						<ul class="breadcrumb">
							<li>
								<i class="icon-home"></i>
								<a href="index.html"><%=get_user().Name %>Home</a> 
								<i class="icon-angle-right"></i>
							</li>
							<li>
								<a href="#">Extra</a>
								<i class="icon-angle-right"></i>
							</li>
							<li><a href="#">User Profile</a></li>
						</ul>
						<!-- END PAGE TITLE & BREADCRUMB-->
					</div>
				</div>
				<!-- END PAGE HEADER-->
				<!-- BEGIN PAGE CONTENT-->
				<div class="row-fluid profile">
					<div class="span12">
						<!--BEGIN TABS-->
						<div class="tabbable tabbable-custom tabbable-full-width">
							<ul class="nav nav-tabs">
								<li class="active"><a href="#tab_1_1" data-toggle="tab">Overview</a></li>
								<li><a href="#tab_1_3" data-toggle="tab">Setting</a></li>
							</ul>
							<div class="tab-content">
								<div class="tab-pane row-fluid active" id="tab_1_1">
									<ul class="unstyled profile-nav span3">
										<li><img src="media/image/profile-img.png" alt="" /></li>
										<li><a href="#">Projects</a></li>
										<li><a href="#">Settings</a></li>
									</ul>
									<div class="span9">
										<div class="row-fluid">
											<div class="span8 profile-info">
												<h1><%=get_user().Name %></h1>
												<p><%=get_user().Introduction %></p>
												<p><a href="<%=get_user().Link %>"><%=get_user().Link %></a></p>
												<ul class="unstyled inline">
													<li><i class="icon-home"></i> <%=get_user().Hometown %></li>
													<li><i class="icon-calendar"></i> <%=get_user().Birthday %></li>                                                    <li><i class="icon-calendar"></i> <%=get_user().University %></li>                                                    <li><i class="icon-envelope-alt"></i> <%=get_user().Email %></li>                                                    <li><i class="icon-th"></i> <%=get_user().Phone %></li>
												</ul>
											</div>
											<!--end span8-->
										</div>
										<!--end row-fluid-->
										<div class="tabbable tabbable-custom tabbable-custom-profile">
											<ul class="nav nav-tabs">
												<li class="active"><a href="#tab_1_11" data-toggle="tab">Research</a></li>
												<li class=""><a href="#tab_1_22" data-toggle="tab">Paper Publications</a></li>
                                                <li class=""><a href="#tab_1_33" data-toggle="tab">Awards</a></li>

											</ul>
											<div class="tab-content">
												<div class="tab-pane active" id="tab_1_11">                                                    <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
															<ul class="feeds">
																<li>
																	<div class="col1">
																		<div class="cont">
																			<div class="cont-col1">
																				<div class="label">                        
																					<i class="icon-bookmark-empty"></i>
																				</div>
																			</div>
																			<div class="cont-col2">
																				<div class="desc">
																					The empty project.
																				</div>
																			</div>
																		</div>
																	</div>
																</li>
																<li>
																	<a href="#">
																		<div class="col1">
																			<div class="cont">
																				<div class="cont-col1">
																					<div class="label">                        
																						<i class="icon-bookmark-empty"></i>
																					</div>
																				</div>
																				<div class="cont-col2">
																					<div class="desc">
																						Program analysis of Java bytecode based on DISL
																					</div>
																				</div>
																			</div>
																		</div>
																	</a>
																</li>
															</ul>
														</div>
												</div>
												<!--tab-pane-->
												<div class="tab-pane" id="tab_1_22">
                                                    <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
															<ul class="feeds">
                                                                <% for (int i = 0; i < get_publication().Count; i++ ) {%>
																<li>
																	<div class="col1">
																		<div class="cont">
																			<div class="cont-col1">
																				<div class="label">                        
																					<i class="icon-file-text"></i>
																				</div>
																			</div>
																			<div class="cont-col2">
																				<div class="desc">
																					<%=(get_publication()[i].PaperName+" "+get_publication()[i].Conference+"'"+get_publication()[i].Year)%>
																				</div>
																			</div>
																		</div>
																	</div>
																</li>
                                                                <% } %>
															</ul>
														</div>
												</div>
												<!--tab-pane-->                                                <div class="tab-pane" id="tab_1_33">
                                                    <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
															<ul class="feeds">
                                                                <% for (int i = 0; i < get_award().Count; i++ ) {%>
																<li>
																	<div class="col1">
																		<div class="cont">
																			<div class="cont-col1">
																				<div class="label">                        
																					<i class="icon-file-text"></i>
																				</div>
																			</div>
																			<div class="cont-col2">
																				<div class="desc">
																					<%=(get_award()[i].Name+" "+get_award()[i].Year)%>
																				</div>
																			</div>
																		</div>
																	</div>
																</li>
                                                                <% } %>
															</ul>
														</div>
												</div>                                                <!--tab-pane-->
											</div>
										</div>
									</div>
									<!--end span9-->
								</div>
								<div class="tab-pane row-fluid profile-account" id="tab_1_3">
									<div class="row-fluid">
										<div class="span12">
											<div class="span3">
												<ul class="ver-inline-menu tabbable margin-bottom-10">
													<li class="active">
														<a data-toggle="tab" href="#tab_1-1">
														<i class="icon-cog"></i> 
														Personal info
														</a> 
														<span class="after"></span>                                    
													</li>
													<li class=""><a data-toggle="tab" href="#tab_2-2"><i class="icon-picture"></i> Change Avatar</a></li>
													<li class=""><a data-toggle="tab" href="#tab_3-3"><i class="icon-lock"></i> Change Password</a></li>
													<li class=""><a data-toggle="tab" href="#tab_4-4"><i class="icon-eye-open"></i> Privacity Settings</a></li>
												</ul>
											</div>
											<div class="span9">
												<div class="tab-content">
													<div id="tab_1-1" class="tab-pane active">
														<div style="height: auto;" id="accordion1-1" class="accordion collapse">
															<form class="profile-form" action="#">
																<label class="control-label">First Name</label>
																<input type="text" id="FirstName" placeholder="John" class="m-wrap span8" />
																<label class="control-label">Last Name</label>
																<input type="text" id="LastName" placeholder="Doe" class="m-wrap span8" />
																<label class="control-label">Mobile Number</label>
																<input type="text" id="MobileNumber" placeholder="+1 646 580 DEMO (6284)" class="m-wrap span8" />
																<label class="control-label">Interests</label>
																<input type="text" id="Interests" placeholder="Design, Web etc." class="m-wrap span8" />
																<label class="control-label">Occupation</label>
																<input type="text" id="Occupation" placeholder="Web Developer" class="m-wrap span8" />
																<label class="control-label">Counrty</label>
																<div class="controls">
																	<input type="text" id="Country" class="span8 m-wrap" style="margin: 0 auto;" data-provide="typeahead" data-items="4" data-source="[&quot;Alabama&quot;,&quot;Alaska&quot;,&quot;Arizona&quot;,&quot;Arkansas&quot;,&quot;US&quot;,&quot;Colorado&quot;,&quot;Connecticut&quot;,&quot;Delaware&quot;,&quot;Florida&quot;,&quot;Georgia&quot;,&quot;Hawaii&quot;,&quot;Idaho&quot;,&quot;Illinois&quot;,&quot;Indiana&quot;,&quot;Iowa&quot;,&quot;Kansas&quot;,&quot;Kentucky&quot;,&quot;Louisiana&quot;,&quot;Maine&quot;,&quot;Maryland&quot;,&quot;Massachusetts&quot;,&quot;Michigan&quot;,&quot;Minnesota&quot;,&quot;Mississippi&quot;,&quot;Missouri&quot;,&quot;Montana&quot;,&quot;Nebraska&quot;,&quot;Nevada&quot;,&quot;New Hampshire&quot;,&quot;New Jersey&quot;,&quot;New Mexico&quot;,&quot;New York&quot;,&quot;North Dakota&quot;,&quot;North Carolina&quot;,&quot;Ohio&quot;,&quot;Oklahoma&quot;,&quot;Oregon&quot;,&quot;Pennsylvania&quot;,&quot;Rhode Island&quot;,&quot;South Carolina&quot;,&quot;South Dakota&quot;,&quot;Tennessee&quot;,&quot;Texas&quot;,&quot;Utah&quot;,&quot;Vermont&quot;,&quot;Virginia&quot;,&quot;Washington&quot;,&quot;West Virginia&quot;,&quot;Wisconsin&quot;,&quot;Wyoming&quot;]" />
																	<p class="help-block"><span class="muted">Start typing to auto complete!. E.g: US</span></p>
																</div>
																<label class="control-label">About</label>
																<textarea id="About" class="span8 m-wrap" rows="3"></textarea>
																<label class="control-label">Website Url</label>
																<input type="text" id="WebsiteUrl" placeholder="http://www.mywebsite.com" class="m-wrap span8" />
																<div class="submit-btn">
																	<a href="#" class="btn green">Save Changes</a>
																	<a href="#" class="btn">Cancel</a>
																</div>
															</form>
														</div>
													</div>
													<div id="tab_2-2" class="tab-pane">
														<div style="height: auto;" id="accordion2-2" class="accordion collapse">
															<form action="#">
																<p>Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod.</p>
																<br />
																<div class="controls">
																	<div class="thumbnail" style="width: 291px; height: 170px;">
																		<img src="media/image/AAAAAA&amp;text=no+image" alt="" />
																	</div>
																</div>
																<div class="space10"></div>
																<div class="fileupload fileupload-new" data-provides="fileupload">
																	<div class="input-append">
																		<div class="uneditable-input">
																			<i class="icon-file fileupload-exists"></i> 
																			<span class="fileupload-preview"></span>
																		</div>
																		<span class="btn btn-file">
																		<span class="fileupload-new">Select file</span>
																		<span class="fileupload-exists">Change</span>
																		<input type="file" class="default" />
																		</span>
																		<a href="#" class="btn fileupload-exists" data-dismiss="fileupload">Remove</a>
																	</div>
																</div>
																<div class="clearfix"></div>
																<div class="controls">
																	<span class="label label-important">NOTE!</span>
																	<span>You can write some information here..</span>
																</div>
																<div class="space10"></div>
																<div class="submit-btn">
																	<a href="#" class="btn green">Submit</a>
																	<a href="#" class="btn">Cancel</a>
																</div>
															</form>
														</div>
													</div>
													<div id="tab_3-3" class="tab-pane">
														<div style="height: auto;" id="accordion3-3" class="accordion collapse">
															<form action="#">
																<label class="control-label">Current Password</label>
																<input type="password" class="m-wrap span8" />
																<label class="control-label">New Password</label>
																<input type="password" class="m-wrap span8" />
																<label class="control-label">Re-type New Password</label>
																<input type="password" class="m-wrap span8" />
																<div class="submit-btn">
																	<a href="#" class="btn green">Change Password</a>
																	<a href="#" class="btn">Cancel</a>
																</div>
															</form>
														</div>
													</div>
													<div id="tab_4-4" class="tab-pane">
														<div style="height: auto;" id="accordion4-4" class="accordion collapse">
															<form action="#">
																<div class="profile-settings row-fluid">
																	<div class="span9">
																		<p>Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus..</p>
																	</div>
																	<div class="control-group span3">
																		<div class="controls">
																			<label class="radio">
																			<input type="radio" name="optionsRadios1" value="option1" />
																			Yes
																			</label>
																			<label class="radio">
																			<input type="radio" name="optionsRadios1" value="option2" checked />
																			No
																			</label>  
																		</div>
																	</div>
																</div>
																<!--end profile-settings-->
																<div class="profile-settings row-fluid">
																	<div class="span9">
																		<p>Enim eiusmod high life accusamus terry richardson ad squid wolf moon</p>
																	</div>
																	<div class="control-group span3">
																		<div class="controls">
																			<label class="checkbox">
																			<input type="checkbox" value="" /> All
																			</label>
																			<label class="checkbox">
																			<input type="checkbox" value="" /> Friends
																			</label>
																		</div>
																	</div>
																</div>
																<!--end profile-settings-->
																<div class="profile-settings row-fluid">
																	<div class="span9">
																		<p>Pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson</p>
																	</div>
																	<div class="control-group span3">
																		<div class="controls">
																			<label class="checkbox">
																			<input type="checkbox" value="" /> Yes
																			</label>
																		</div>
																	</div>
																</div>
																<!--end profile-settings-->
																<div class="profile-settings row-fluid">
																	<div class="span9">
																		<p>Cliche reprehenderit enim eiusmod high life accusamus terry</p>
																	</div>
																	<div class="control-group span3">
																		<div class="controls">
																			<label class="checkbox">
																			<input type="checkbox" value="" /> Yes
																			</label>
																		</div>
																	</div>
																</div>
																<!--end profile-settings-->
																<div class="profile-settings row-fluid">
																	<div class="span9">
																		<p>Oiusmod high life accusamus terry richardson ad squid wolf fwopo</p>
																	</div>
																	<div class="control-group span3">
																		<div class="controls">
																			<label class="checkbox">
																			<input type="checkbox" value="" /> Yes
																			</label>
																		</div>
																	</div>
																</div>
																<!--end profile-settings-->
																<div class="space5"></div>
																<div class="submit-btn">
																	<a href="#" class="btn green">Save Changes</a>
																	<a href="#" class="btn">Cancel</a>
																</div>
															</form>
														</div>
													</div>
												</div>
											</div>
											<!--end span9-->                                   
										</div>
									</div>
								</div>
							</div>
						</div>                            
						<!--END TABS-->
					</div>
				</div>
				<!-- END PAGE CONTENT-->
			</div>
			<!-- END PAGE CONTAINER--> 
		</div>
		<!-- END PAGE -->    
	</div>
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
	<script type="text/javascript" src="media/js/bootstrap-fileupload.js"></script>
	<script type="text/javascript" src="media/js/chosen.jquery.min.js"></script>
	<!-- END PAGE LEVEL PLUGINS -->
	<!-- BEGIN PAGE LEVEL SCRIPTS -->
	<script src="media/js/app.js"></script>          <script src="media/js/user_profile.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->
	<script>
	    jQuery(document).ready(function () {
	        // initiate layout and plugins
	        App.init();
	        Profile.init();

	    });
	</script>
	<!-- END JAVASCRIPTS -->
<script type="text/javascript">  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script></body>
<!-- END BODY -->
</html>
