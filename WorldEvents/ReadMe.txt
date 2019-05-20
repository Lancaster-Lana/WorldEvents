To install DBs
1. in Package Manager Console run 
   PM> Update-Database -project WorldEvents.EntityFramework -context SatteliteDbContext 
    NOTE: to add your own migration 
   PM> Add-Migration <MigrationName2> -project WorldEvents.EntityFramework -context SatteliteDbContext
	 And then call 
2. run application WorldEvents.Web. If it is running, click 'Login' on the right top corner of the site
2.1 to login as administrator enter
  Login: Admin
  Password: Admin
2.2 to login with social network, click twitter, etc/ then - confirm

3. There are 2 boards 
3.1 on Admin Board can be
 - created\removed categories
 - created\removed projects
 - created\removed users, and added\removed Category subscriptions per user
 
3.2 On main board User may see all news\projects he\she is subscribed (via admin board).
But he\she can subscibe himself by clicking on site botton links (with categories names)