<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="RGDZY.control.Menu" %>

<!-- BEGIN SIDEBAR -->

<div class="page-sidebar nav-collapse collapse">
      
			<ul class="page-sidebar-menu">
				<li>
					<div class="sidebar-toggler hidden-phone"></div>
				</li>

				<li>
					<form class="sidebar-search">
						<div class="input-box">
							<a href="javascript:;" class="remove"></a>
							<input type="text" placeholder="Search..." />
							<input type="button" class="submit" value=" " />
						</div>
					</form>
				</li>

				<li class="start active ">
					<a href="default.aspx">
					    <i class="icon-home"></i> 
					    <span class="title"><%=getUsername()%>'s Timeline</span>
					    <span class="selected"></span>
					</a>
				</li>

                <li class="">
                    <a href="javascript:;">
					    <i class="icon-calendar"></i> 
					    <span class="title">Schedule Management</span>
					    <span class="arrow"></span>
					</a>
					<ul class="sub-menu">
						<li >
							<a href="page_calendar.aspx">My Calendar</a>
						</li>
                        <% if((getAuthority() & getFlag("A_SCHEDULE")) != 0x0) { %>
                        <li>
							<a href="page_schedule_setting.aspx">Setting</a>
						</li>
                        <%} %>
					</ul>
				</li>

                <li class="">
                    <a href="javascript:;">
					    <i class="icon-cogs"></i> 
					    <span class="title">Device</span>
					    <span class="arrow"></span>
					</a>
					<ul class="sub-menu">
						<li >
							<a href="user_device.aspx">My Device</a>
						</li>
                        <% if((getAuthority() & getFlag("A_DEVICE")) != 0x0) { %>
                        <li >
							<a href="device_list.aspx">Device List</a>
						</li>
                        <%} %>
					</ul>
				</li>

                <li class="">
                    <a href="javascript:;">
					    <i class="icon-briefcase"></i> 
					    <span class="title">Project & Seminar</span>
					    <span class="arrow"></span>
					</a>
					<ul class="sub-menu">
						<li >
                            <a href="project_list.aspx">Project List</a>
						</li>
                        <li >
                            <a href="user_project.aspx">User Project</a>
						</li>
                        <li >
                            <a href="seminar_table.aspx">Seminar List</a>
						</li>
                        <li >
                            <a href="user_seminar.aspx">User Seminar</a>
						</li>
					</ul>
				</li>

                <li class="">
                    <a href="javascript:;">
					    <i class="icon-bolt"></i> 
					    <span class="title">Print</span>
					    <span class="arrow"></span>
					</a>
					<ul class="sub-menu">
						<li >
							<a href="file_print.aspx">Print</a>
						</li>
                        <% if((getAuthority() & getFlag("A_ADMIN")) != 0x0) { %>
						<li >
							<a href="file_record.aspx">Files Record</a>
						</li>
                        <%} %>
					</ul>
				</li>

                <li class="">
                    <a href="javascript:;">
					    <i class="icon-user"></i> 
					    <span class="title">Account Management</span>
					    <span class="arrow"></span>
					</a>
					<ul class="sub-menu">
                        <% if((getAuthority() & getFlag("A_ACCOUNT")) != 0x0) { %>
						<li >
							<a href="svnftp_account.aspx">SVN & FTP Management</a>
						</li>
                        <%} %>
                        <li >
							<a href="user_profile.aspx">User profile</a>
						</li>
                        <li >
							<a href="lab_member.aspx">Lab Member</a>
						</li>
                        <% if((getAuthority() & getFlag("A_ACCOUNT")) != 0x0) { %>
                        <li >
							<a href="user_management.aspx">User Management</a>
						</li>
                        <li >
							<a href="group_management.aspx">Group</a>
						</li>
                        <%} %>
					</ul>
				</li>
			</ul>

		</div>

<!-- END SIDEBAR -->
