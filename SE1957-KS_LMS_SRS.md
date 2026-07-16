

![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.001.png)







**SOFTWARE REQUIREMENT SPECIFICATION**

**Cat Ba Booking**









– Hanoi, Jan 2024 –

**Table of Contents**

[I. Record of Changes	3](#_qrjfuaz22yrj)

[II. Software Requirement Specification	4](#_b793bebf4nvm)

[1. Overall Requirements	4](#_k3miclt4hbcm)

[1.1 Context Diagram	4](#_iz9oz5b8vo1s)

[1.2 Main Business Processes	5](#_tj2a80x5ijgq)

[1.3 User Requirements	](#_h56h2x83ijrb)6

[1.4 System Functionalities	](#_n2i4aae5yo4q)11

[1.5 Entity Relationship Diagram	](#_ijceyk25ri2f)14 

[2. Use Case Specifications	](#_2mgqh639rgrh)15

[2.1 ](#_4zv7myyfrq2r)Feature Manage Account[	](#_4zv7myyfrq2r)15

[2.2 ](#_4381u6a1y6t5)Feature Manage Restaurant[	1](#_4381u6a1y6t5)8

[2.3 ](#_4381u6a1y6t5)Feature Manage Homestay[	1](#_4381u6a1y6t5)9

[2.4 ](#_4381u6a1y6t5)Feature Manage Listings[**	](#_4381u6a1y6t5)20

[2.5 ](#_4381u6a1y6t5)Feature Customer Actions[	](#_4381u6a1y6t5)21

[2.6 ](#_4381u6a1y6t5)Feature Payment & Transaction Management[	](#_4381u6a1y6t5)23

[3. Functional Requirements	](#_904fw0m4vgms)25

[3.1 Feature Name1	](#_13a5mmgcely4)25

[3.2 User Authentication	](#_7xi7xvkiqfnl)25

[3.3 System Administration	](#_pltrlfipb7t)25

[4. Non-Functional Requirements	](#_2oy4iff08u11)27

[3.1 External Interfaces	](#_fxl6fctfuhdk)27

[3.2 Quality Attributes	](#_6qzgg1z0a4nw)27

[5. Requirement Appendix	](#_huets3f79vnv)27

[5.1 Business Rules	](#_5m8i3geebwdq)28

[5.2 System Messages	](#_5ysu5vt47rft)28

[5.3 Other Requirements…	](#_98396h44lry2)28



I. Record of Changes

|**Date**|**A\*<br>M, D**|**In charge**|**Change Description**|
| :-: | :-: | :-: | :-: |
|||||
|||||
|||||
|||||
|||||
|||||
|||||
|||||
|||||
|||||
|||||
|||||
|||||
\*A - Added M - Modified D - Deleted


# <a name="_b793bebf4nvm"></a>**II. Software Requirement Specification**
## <a name="_k3miclt4hbcm"></a>**1. Overall Requirements**
### <a name="_iz9oz5b8vo1s"></a>**1.1 Context Diagram**
CatBaBooking is an online travel booking system specialized for Cat Ba Island, enabling customers (Customer and Guest) to easily search for and book entire homestays and dining tables at local restaurants (such as seafood and BBQ), while supporting service owners (Homestay/Restaurant Owner) in managing and updating their information. The system replaces traditional manual processes (phone calls, messaging), reducing processing time and increasing reliability, with a focus on safe and convenient travel experiences on Cat Ba – a renowned destination famous for its beaches, islands, and local cuisine.

![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.002.png)

\>>


### <a name="_tj2a80x5ijgq"></a>**1.2 Main Business Processes**
#### ***1.2.1 Homestay booking process***
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.003.png)
#### ***1.2.2 Restaurant reservation process***
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.004.png)
#### <a name="_2nnmzdw7jpwn"></a>***1.2.3 Service publishing process***
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.005.png)

### <a name="_h56h2x83ijrb"></a>**1.3 User Requirements**
#### ***1.3.1 Actors***

|**#**|**Actor**|**Description**|
| :-: | :- | :- |
|1|Admin|System administrator: manages users, approves/blocks accounts, manages homestay/restaurant listings, manages feedback, and views system reports.|
|2|Homestay Owner|Homestay owner: manages their homestay(s), including adding/updating/deleting listings, handling reservations, updating availability, viewing feedback, and checking revenue statistics.|
|3|Restaurant Owner|Restaurant owner: manages restaurants and tables, handles reservations, confirms/cancels bookings, updates table availability, views feedback, and checks revenue statistics.|
|4|Customer|End-user: searches for homestays/restaurants, makes bookings, payments, updates booking details, and leaves feedback.|
|5|Guest|` `Guest : user who has not logged in or does not have an account. They can access the system to search and view homestays/restaurants, but they cannot perform authenticated actions such as making bookings, payments, or leaving feedback.|
|6|Payment Gateway|External service that processes payments.|

#### ***1.3.2 Use Cases (UC)***

|**ID**|**Use Case**|**Feature**|**Use Case Description**|
| :-: | :- | :- | :- |
|01|Login|Admin, Owner, Customer|Users can log in with valid credentials to access the system.|
|02|Logout|Admin, Owner, Customer|Users can log out to end their session securely.|
|03|Reset Password|Admin, Owner, Customer|Users can reset their password if they forget it or want to change it.|
|04|View Profile|Admin, Owner, Customer|Users can view their profile information stored in the system.|
|05|Update Profile|Admin, Owner, Customer|Users can update their profile information such as name, contact, or password.|
|06|User Management|Admin|Admin can manage users, including viewing user lists, authorizing users, and enabling/disabling accounts.|
|07|Manage Homestay Listings|Admin|Admin can manage homestay listings, including viewing and deleting homestays.|
|08|Manage Restaurant Listings|Admin|Admin can manage restaurant listings, including viewing and deleting restaurants.|
|09|Manage Feedback|Admin|Admin can manage feedback, including viewing and deleting feedback.|
|10|Manage Bookings & Availability|Homestay Owner|Homestay owners can manage bookings and availability, including handling reservations and updating availability.|
|11|View list homestay|Homestay Owner|Homestay owners can view a list of homestays and perform actions such as add, update, or delete homestay.|
|12|Revenue statistics|Homestay Owner|Homestay owners can view revenue statistics of their listings.|
|13|View feedback|Homestay Owner|Homestay owners can view feedback provided by customers.|
|14|View list Restaurant|Restaurant Owner|Restaurant owners can view a list of restaurants and perform actions such as add, update, or delete a restaurant.|
|15|View list Table|Restaurant Owner|Restaurant owners can view tables and manage them (add, update, delete tables).|
|16|View Reservations|Restaurant Owner|Restaurant owners can view reservations and confirm or cancel them, as well as update table availability.|
|17|Revenue statistics|Restaurant Owner|Customers can view a list of homestays and make bookings.|
|18|View list homestay|Customer/Guest|Customers can view a list of tables in restaurants and book tables.|
|19|View list table|Customer/Guest|Customers can view a list of tables in restaurants and book tables.|
|20|View Booking Details|Customer/Guest|Customers can view and update booking details.|
|21|Make Payments|Customer/Guest|Customers can make payments for bookings.|
|22|Leave Feedback|Customer/Guest|Customers can leave feedback after using the services.|
|23|Process Payment|Payment Gateway|Payment Gateway processes customer payments: receives requests, authenticates, authorizes, captures funds, and returns results.|
|24|Send Transaction Status|Payment Gateway|Payment Gateway notifies the booking system of the final transaction status (success/failure/pending).|
#### ***1.3.2 Use Case Diagrams***
##### **1.3.2.1 UCs for Admin**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.006.png)

##### **1.3.2.2 UCs for Homestay Owner**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.007.png)
##### **1.3.2.3 UCs for Restaurant Owner**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.008.png)
##### <a name="_p4433lc9uq6"></a>**1.3.2.4. UCs for  Customer**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.009.png)
##### <a name="_88zmffhduc7h"></a>**1.3.2.5. UCs for  Guest**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.010.png)
##### <a name="_dhwtd6mrqnx9"></a>**1.3.2.6. UCs for Payment Gateway**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.011.png)
### <a name="_n2i4aae5yo4q"></a>**1.4 System Functionalities**
#### ***1.4.1 Screens Flow***
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.012.png)
#### ***1.4.2 Screen Authorization***

|**Screen**|**Admin**|**Homestay Owner**|**Restaurant Owner**|**Customer**|**Guest**|
| :- | :-: | :-: | :-: | :-: | :-: |
|Home/Landing Page|X|X|X|X|X|
|Search Service|`        `X|X|X|X|X|
|View Service Details|<p>X</p><p></p>|X|X|X|X|
|Login|||||X|
|Register |||||X|
|Forgot/Reset Password||||||
|Profile Management|X|X|X|X||
|Log out|X|X|X|X||
|Book Service||||X||
|Payment||||X||
|Booking History|X|X|X|X||
|Feedback||||X||
|Owner Dashboard||X|X|||
|Admin Dashboard|X|||||
|Manage Restaurant (Owner)|||X|||
|Manage Homestay (Owner)||X||||
|Manage Bọoking (Owner)||X|X|||
|View Revenue Statistics||X|X|||
|View Feedback |X|X|X|X|X|
|User Management|X|||||
|Listing Management|X|||||
|Feedback Management|X|||||

#### ***1.4.3 Non-UI Functions***

|**#**|**Feature**|**System Function**|**Description**|
| :- | :- | :- | :- |
|1|Authentication & User|NF-AUTH-01: Validate Credentials|The system must validate the user's email and password against the database. (Based on UserDAO.checkLogin)|
|2|Authentication & User|NF-AUTH-02: Secure Password Hashing|The system must hash the user's password using the Argon2 algorithm before storing it. (Based on PassWordUtil.hashPassword)|
|3|Authentication & User|NF-AUTH-03: Verify Hashed Password|The system must verify the plain-text password provided by the user against the stored hash. (Based on PassWordUtil.verifyPassword)|
|4|Authentication & User|NF-AUTH-04: Check Email Existence|The system must check if an email already exists in the database. (Based on UserDAO.checkUserExist)|
|5|Authentication & User|NF-AUTH-05: Create New User|The system must be able to add a new user record (Customer or Owner) to the database. (Based on UserDAO.insertUser)|
|6|Authentication & User|NF-AUTH-06: Manage User Session|The system must create and store user information (e.g., Users object) in the HTTP Session upon successful login.|
|7|Authentication & User|NF-AUTH-07: Invalidate User Session|The system must invalidate the user's HTTP Session upon receiving a logout request. (Based on LogoutController)|
|8|Authentication & User|NF-AUTH-08: Update User Profile|The system must allow updating a user's personal information (name, phone, avatar) in the database. (Based on UserDAO.updateProfile)|
|9|Authentication & User|NF-AUTH-09: Process Password Reset|The system must support a password reset flow, including finding an account by email and updating the password. (Based on ForgotPasswordController)|
|10|Administrator|NF-ADMIN-01: Retrieve All Users|The system must retrieve a paginated list of all users from the database. (Based on UserDAO.getAllUsers)|
|11|Administrator|NF-ADMIN-02: Update User Status|The system must allow an Admin to update a user's account status (e.g., 'active', 'banned'). (Based on UserDAO.updateUserStatus)|
|12|Administrator|NF-ADMIN-03: Retrieve Pending Applications|The system must retrieve a list of businesses that are in a "pending" status. (Based on BusinessDAO.getPendingBusinesses)|
|13|Administrator|NF-ADMIN-04: Update Application Status|The system must allow an Admin to approve or reject a business registration application. (Based on BusinessDAO.updateBusinessStatus)|
|14|Business Owner|NF-OWNER-01: Retrieve Owner's Businesses|The system must retrieve all businesses associated with a specific Owner ID. (Based on BusinessDAO.getBusinessesByOwnerId)|
|15|Business Owner|NF-OWNER-02: Retrieve Homestay Details|The system must retrieve the detailed information for a specific homestay by its ID. (Based on HomestayDAO.getHomestayById)|
|16|Business Owner|NF-OWNER-03: Update Homestay|The system must allow an owner to update the information for their homestay. (Based on UpdateHomestayController)|
|17|Business Owner|NF-OWNER-04: Retrieve Rooms by Homestay|The system must retrieve a list of rooms associated with a specific homestay. (Based on RoomDAO.getRoomsByHomestayId)|
|18|Business Owner|NF-OWNER-05: Validate Business Settings|The system must validate the integrity of business settings (e.g., opening time < closing time). (Based on RestaurantBusinessSettingsValidator)|
|19|Business Owner|NF-OWNER-06: Update Restaurant Settings|The system must allow an owner to update settings for their restaurant (hours, amenities, cuisine types...). (Based on RestaurantSettingsController)|
|20|Core Business|NF-CORE-01: Retrieve Areas|The system must retrieve a list of all geographical areas (e.g., Cat Ba) from the database. (Based on AreaDAO.getAllAreas)|
|21|Core Business|NF-CORE-02: Process Pagination|The system must calculate and retrieve only a subset of a large dataset based on page number and page size.|
|22|System & Utilities|NF-SYS-01: Manage DB Connection|The system must manage (open, close, and provide) JDBC connections to the database. (Based on DBUtil.java)|
|23|System & Utilities|NF-SYS-02: Send Email Notification|The system must be capable of sending emails (e.g., OTP, notifications) via an SMTP server. (Based on EmailUtil.java)|
|24|System & Utilities|NF-SYS-03: Load Configuration|The system must read configuration files (.properties) to load database and email server information. (Based on DB.properties, Email.properties)|

### <a name="_ijceyk25ri2f"></a>**1.5 Entity Relationship Diagram**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.013.png)

**Entities Description**

|**#**|**Entity**|**Description**|
| :- | :- | :- |
|1|Roles|Defines user access levels and authorization roles within the system.|
|2|Reviewers|Information about users who can review and rate restaurants or homestay.|
|3|Business Occasions|Specifies different types of business occasions or events (e.g., meetings, parties) associated with restaurant or homestay bookings.|
|4|Cuisine Types|<p>Represents categories of cuisine (e.g.grilled, hot pot, vegetarian</p><p>) offered by restaurants.</p>|
|5|Business Restaurant Types|Defines business restaurant categories|
|6|Restaurant Types||
|7|Rooms|Details about homestay rooms|
|8|Booked Rooms|Manages data about rooms that have been reserved or booked.|
|9|Table Availability|Stores availability status of restaurant tables.|
|10|Amenities|Lists available amenities such as Wi-Fi, parking, air conditioning, VIP rooms, etc.|
|11|Dish Categories|Classifies dishes into categories like appetizers, main courses, desserts, etc.|
|12|Dishes|Stores detailed information about dishes in the menu|
|13|Payments|Handles customer payment information and transaction records|
|14|Booking dishes|Links selected dishes to specific table or room bookings.|
|15|Areas|Identify geographical areas such as city center, near the sea, etc.|
|16|Bookings|Manages customer reservations for tables or rooms.|
|17|Booked Tables|Stores detailed data of reserved tables.|
|18|Room Availabilities|Tracks room availability and reservation status.|
|19|Occasions|Stores specific event types customers can book such as parties or meetings|
|20|Business Amenities|Maps restaurants with the amenities they offer.|
|21|Business Cuisines|Associates restaurants with the cuisine types they serve|
|22|Restaurant Tables|Stores detailed information about tables within each restaurant|
|23|Room Images|Stores image references for homestay rooms|
|24|Users|Stores system user information such as customers, admins, or restaurant owners.|

## <a name="_2mgqh639rgrh"></a>**2. Use Case Specifications**
### <a name="_g0gay53exgt4"></a>**2.1 Feature Manage Account**
#### <a name="_vc97d3cnzm3e"></a>***2.1.1 Login System***

|Primary Actors|Customer|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a user, I want to be able to log into the system so that I can use the system’s authenticated features and access my personalized account.|||
|Preconditions|User account has been created & authorized|||
|Postconditions|<p>User logs in the system successfully</p><p>The system tracked successful login into the Activity Log</p>|||
|Normal Sequence/Flow|<p>User clicks Login button from the page header or accesses an authenticated feature (from a link or type the page URL directly into the address bar)</p><p>1. System show the User Login screen</p><p>2. User types in the login details (email, password)</p><p>3. User clicks the Login button</p><p>4. System validates the login details </p><p>5. System allows user to access</p><p>6. System tracks user’s success login to the Activity Log</p><p>7. System directs user to the Home Page (or the previous calling page if any)</p>|||
|Alternative Sequences/Flows|<p>Step 2.1\_Google Login</p><p>1. User clicks Google Login button to login system using Google account</p><p>2. System redirects the user to the Google’s Login screen</p><p>3. User types in the Google account details and chooses to login</p><p>4. Google validates user’s login information successfully and redirect him/her back to the system</p><p>5. Return to step 5 of normal flow.</p><p>Step 4\_System can’t authenticate the user</p><p>User can’t be authenticated & get relevant error message in one of below cases</p><p>1. He/she leaves the Email and/or Password field blank </p><p>2. Input Email or Password are incorrect</p><p>3. Input Email/Password are correct but email has not been verified </p><p>4. The user account is blocked / inactive</p>|||
###
#### <a name="_asp1l1f43nbq"></a><a name="_99x58n5bc61"></a>***2.1.2 Logout System***

|Primary Actors|Administrator|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As an Admin, I want to be able to log out from the system so that I can securely end my session and prevent unauthorized access.|||
|Preconditions|The user is logged into the system.|||
|Postconditions|<p>- User session is terminated.</p><p>- System records logout event in the Activity Log..</p>|||
|Normal Sequence/Flow|<p>1\.User clicks the “Logout**”** button from the system menu or profile section.</p><p>2\.System displays a confirmation dialog (optional).</p><p>3\.User confirms logout.</p><p>4\.System terminates user session and clears authentication tokens.</p><p>5\.System redirects users to the “Home Page” or “Login Page”.</p><p>6\.System logs the logout event into the “Activity Log”.</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Auto Logout</p><p>1\.If the user is inactive for a certain period, the system automatically logs the user out.<br>2\. message “Session expired, please log in again”  is displayed.<br>3\.User is redirected to the Login Page.</p><p></p>|||
####
#### <a name="_wi9ror90n9ys"></a><a name="_rnhf95szucz"></a>***2.1.3 Reset Password***

|Primary Actors|Admin, Owner, Customer|Secondary Actors|Email Servie|
| -: | :- | -: | :- |
|Description|As a user, I want to reset my password if I forget it.|||
|Preconditions|Users must have registered an email.|||
|Postconditions|<p>- Password successfully reset.</p><p>- System updates password in database.</p>|||
|Normal Sequence/Flow|<p>1\.User clicks **“**Forgot Password**”** on the login page.</p><p>2\.System shows password recovery form.</p><p>3\.User enters registered email.</p><p>4\.System validates email and sends reset link via email.</p><p>5\.User clicks the link.</p><p>6\.System displays reset password form.</p><p>7\.User enters a new password and confirms.</p><p>8\.System updates password and displays success message.</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Invalid Email</p><p>1. The system shows “Email not found” .</p><p>Step 7.1 – Password Validation Failed</p><p>1. If password doesn’t meet security policy → “Weak password”.</p>|||
####
#### <a name="_kzo8jt3sn0v6"></a><a name="_5k4v3ctzjyhk"></a>***2.1.4 View Profile***

|Primary Actors|Admin, Owner, Customer|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|` `As a  user, I want to view my profile details stored in the system.|||
|Preconditions|Users must be logged in.|||
|Postconditions|Profile data is displayed successfully..|||
|Normal Sequence/Flow|<p>1\.User clicks Profile from navigation.</p><p>2\.System fetches user profile data.</p><p>3\.System displays user info (name, email, contact, role, etc.).</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – Data Not Found</p><p>1. System displays error message “Profile not found” .</p>|||
####
#### <a name="_jxtn5bute3k3"></a><a name="_p0x7pbqh6t4f"></a>***2.1.5 Update Profile***

|Primary Actors|Admin, Owner, Customer|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a user, I want to update my profile information (name, contact, password, etc.).|||
|Preconditions|User must be logged in|||
|Postconditions|<p>- Profile successfully updated.</p><p>- System logs update activity.</p>|||
|Normal Sequence/Flow|1\.Admin selects homestay/restaurant.<br>2\.System displays feedback about homestay/restaurant .<br>3\.Select offending feedback and delete.<br>4\.System saves changes.|||
|Alternative Sequences/Flows|<p>Step 5.1 – Validation Failed</p><p>1. Invalid input (e.g., email format, missing fields).</p><p>2. The system displays an error message .</p>|||
####
#### <a name="_ioc0omw8l96f"></a><a name="_u06zrqxx0q17"></a>***2.1.6 User Management***

|Primary Actors|Admin|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As an admin, I want to manage system users — view, authorize, enable or disable accounts.|||
|Preconditions|Admin must be logged in.|||
|Postconditions|<p>- User records updated successfully.</p><p>- Changes recorded in system log.</p>|||
|Normal Sequence/Flow|<p>1\.Admin navigates to the User Management section.</p><p>2\.System displays list of users.</p><p>3\.Admin selects a user to view, authorize, enable/disable, or delete.</p><p>4\.Admin confirms action.</p><p>5\.System performs operation and confirms success.</p><p>6\.System updates audit log.</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Invalid Operation</p><p>1\.Admin tries to disable own account → System rejects .</p><p>2\.System shows relevant message.</p>|||
####
### <a name="_1u4u37jhp7zh"></a><a name="_ere4tuapd782"></a>**2.2 Feature Manage Restaurant**
#### <a name="_r1m5kpmg3jrf"></a>***2.2.1 Manage Restaurant Menu***

|Primary Actors|Restaurant Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|restaurant owner, I want to manage my menu items so that I can add, update, or remove dishes.|||
|Preconditions|The Restaurant Owner must be logged in.|||
|Postconditions|<p>-Menu updated successfully.</p><p>-System logs changes.</p>|||
|Normal Sequence/Flow|<p>1\.Owner navigates to the Menu** Management section.</p><p>2\.System displays the current menu list.</p><p>3\.Owner adds, edits, or deletes a dish.</p><p>4\.The system validates information.</p><p>5\.System updates database.</p><p>6\.System displays success message </p>|||
|Alternative Sequences/Flows|<p>Step 4.1 – Invalid Input</p><p>1. The system detects missing or invalid data.</p><p>2. Displays “Invalid menu item details”.</p>|||
####
#### <a name="_3jixz85sn6z5"></a><a name="_lnpyz739bl7j"></a>***2.2.2 Manage Table Booking***

|Primary Actors|Restaurant Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a restaurant owner, I want to manage table bookings to organize reservations effectively..|||
|Preconditions|The owner must be logged in.|||
|Postconditions|<p>- Table bookings updated.</p><p>- Changes recorded in Activity Log.</p>|||
|Normal Sequence/Flow|<p>1Owner accesses Table Booking Management.</p><p>2\.The system displays current table reservations.</p><p>3\.Owner edits booking status or availability.</p><p>4\.System validates and saves changes.</p><p>5\.Confirmation message displayed .</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Conflict Booking</p><p>` `1. The owner attempts to book a table that overlaps with another reservation.<br>` `2.System shows message “Table already reserved”.</p>|||
####
#### <a name="_66d5c8uvtuyy"></a><a name="_5ezzrbeoecu"></a>***2.2.3 View List Restaurant***

|Primary Actors|Restaurant Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a restaurant owner, I want to view the list of my restaurants to manage details and track performance.|||
|Preconditions|The owner is logged in.|||
|Postconditions|Restaurant list displayed successfully.|||
|Normal Sequence/Flow|<p>1\.Owner opens the My Restaurants section.</p><p>2\.The system retrieves the owner's restaurant data.</p><p>3\.Displays the list with name, address, and status.</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – No Restaurants Found</p><p>1. System displays message (“No restaurants available”)</p>|||
####
#### <a name="_es34f0xm2si1"></a><a name="_pcv2nyduyd5"></a>***2.2.4 Revenue Statistics (Restaurant Owner)***

|Primary Actors|Restaurant Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a restaurant owner, I want to view revenue reports to track my restaurant’s financial performance.|||
|Preconditions|The owner must be logged in and have valid restaurant listings.|||
|Postconditions|<p>- Revenue data displayed.</p><p>- System logs report generation.</p>|||
|Normal Sequence/Flow|<p>1\.The owner opens the Revenue Statistics section.</p><p>2\.The system retrieves booking and payment data.</p><p>3\.System calculates revenue and generates charts/tables.</p><p>4\.Owner reviews results by date range or restaurant.</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – No Data Available</p><p>1. The system displays a message (“No revenue data found”).</p>|||
####
###
### <a name="_a3hha6uyentf"></a><a name="_mi8z8ae4bf9c"></a><a name="_vt0w79achlby"></a>**2.3 Feature Homestay Management**
#### <a name="_9bim6wqxgtht"></a>***2.3.1 Manage Bookings & Availability***

|Primary Actors|Admin|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a homestay owner, I want to manage bookings and availability so that I can handle reservations efficiently.|||
|Preconditions|The homestay owner is logged in.|||
|Postconditions|<p>- Booking and availability data updated.</p><p>- System logs updates.</p>|||
|Normal Sequence/Flow|<p>1\.The owner opens the Bookings **& Availability** page.</p><p>2\.The system displays current bookings and room availability.</p><p>3\.Owner reviews or updates availability calendar.</p><p>4\.Owner confirms reservation changes.</p><p>5\.The system validates updates and stores changes.</p><p>6\.Confirmation message displayed .</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Overlapping Reservation</p><p>2. 1System detects double booking conflict.</p><p>3. Displays message (“Booking conflict detected”).</p>|||
###
#### <a name="_939gy9ng7292"></a><a name="_kicb4f1vi2k3"></a>***2.3.2 View List Room (Owner)***

|Primary Actors|Homestay Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a homestay owner, I want to view a list of my homestay room listings and manage them (add, update, delete).|||
|Preconditions|The owner must be logged in.|||
|Postconditions|<p>- Homestay room  list displayed.</p><p>- Changes saved and logged.</p>|||
|Normal Sequence/Flow|<p>1\.Owner opens the My Homestays room section.</p><p>2\.System retrieves and displays homestay list.</p><p>3\.Owner selects a homestay room to edit, delete, or add new.</p><p>4\.System updates database accordingly.</p><p>5\.Displays success message .</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – No Homestays room Found</p><p>1. If no listings exist, the system shows the message “No homestays room available” .</p>|||
###
#### <a name="_g1jtycoqbvbz"></a><a name="_yvhxji7wztxy"></a>***2.3.3 Revenue Statistics (Homestay Owner)***

|Primary Actors|Homestay Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a homestay owner, I want to view revenue statistics to monitor business performance.|||
|Preconditions|The owner must have valid homestay listings.|||
|Postconditions|<p>- Revenue statistics displayed.</p><p>- System logs report generation.</p>|||
|Normal Sequence/Flow|<p>1\.The owner navigates to the Revenue Statistics section.</p><p>2\.The system retrieves booking and payment data.</p><p>3\.The system calculates total revenue and displays charts/tables.</p><p>4\.The owner reviews statistics by date range or property.</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – No Data Available</p><p>1. If no bookings or payments exist, the system displays a message ( “No revenue data found”).</p>|||

#### <a name="_pt5ou2t10ngq"></a>***2.3.4 View Feedback (Homestay Owner)***

|Primary Actors|Homestay Owner|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a homestay owner, I want to view feedback from customers to improve my service quality.|||
|Preconditions|Owner must be logged in|||
|Postconditions|- Feedback displayed successfully.|||
|Normal Sequence/Flow|<p>1\.The owner navigates to the Feedback** Section.</p><p>2\.The system retrieves all feedback related to the owner’s homestays.</p><p>3\.The system displays a feedback list with ratings and comments.</p><p>4\.The owner reviews feedback details.</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – No Data Found</p><p>1. System shows message ( “No feedback available”)</p>|||

### <a name="_bibjlcq2kqx8"></a>**2.4 Feature Manage Listings**
#### <a name="_4lmsy2n5xubf"></a>***2.4.1 Manage Homestay Listings***

|Primary Actors|Admin|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As an Admin, I want to manage homestay listings so that I can view, update, or delete homestays from the system.|||
|Preconditions|Admin is logged in.|||
|Postconditions|<p>- Homestay information is updated or deleted.</p><p>- The system logs the modification activity.</p>|||
|Normal Sequence/Flow|<p>1\.Admin opens the Homestay** Management section.</p><p>2\.System displays the list of homestay listings.</p><p>3\.Admin selects a listing to view, edit, or delete.</p><p>4\.Admin performs the chosen action.</p><p>5\.System validates changes and updates database.</p><p>6\.System displays confirmation message .</p><p>7\.System logs the action in the Activity Log</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Invalid Operation</p><p>2. Admin attempts to delete a non-existing listing.</p><p>3. The system shows an error message (“Listing not found”).</p>|||

#### <a name="_c5u0ca3s8ta6"></a>***2.4.2 Manage Restaurant Listings***

|Primary Actors|Admin|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As an Admin, I want to manage restaurant listings so that I can view, update, or delete restaurants from the system.|||
|Preconditions|Admin must be logged in.|||
|Postconditions|<p>- Restaurant records updated successfully.</p><p>- The system logs the action in the Activity Log.</p>|||
|Normal Sequence/Flow|<p>1\.Admin opens Restaurant Management.</p><p>2\.System displays all restaurant listings.</p><p>3\.Admin selects a restaurant to edit or delete.</p><p>4\.Admin confirms the operation.</p><p>5\.System updates or removes restaurant data.</p><p>6\.The system displays confirmation .</p><p>7\.Activity Log is updated.</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 – Restaurant Not Found. </p><p>1\.Admin selects invalid or deleted restaurant ID.</p><p>2\.System displays a message (“Restaurant not found”).</p>|||
####
#### <a name="_adncjcjmxfzq"></a><a name="_hadg533nms8t"></a>***2.4.3 Manage Feedback***

|Primary Actors|Admin|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As an Admin, I want to manage customer feedback so that I can view and delete inappropriate comments.|||
|Preconditions|Admin is logged in.|||
|Postconditions|<p>- Feedback updated or deleted.</p><p>- System logs feedback management activity..</p>|||
|Normal Sequence/Flow|<p>1\.Admin accesses Feedback Managemen**t** page.</p><p>2\.The system displays all feedback entries.</p><p>3\.Admin reviews feedback content.</p><p>4\.Admin selects and deletes inappropriate feedback.</p><p>5\.System confirms deletion .</p><p>6\.System updates Activity Log.</p>|||
|Alternative Sequences/Flows|<p>Step 4.1 – Feedback Already Removed</p><p>1\.Admin selects feedback that was previously deleted.</p><p>2\.System displays “Feedback not found” .</p>|||

### <a name="_91xe27h6qug8"></a>**2.5 Feature Customer Actions**
#### <a name="_e45ejs4iiutx"></a>***2.5.1 Search & Filter Homestay/Restaurant***

|Primary Actors|Customer|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a customer, I want to search and filter homestays or restaurants by name, price, location, or rating.|||
|Preconditions|None|||
|Postconditions|- Matching search results displayed.|||
|Normal Sequence/Flow|<p>1\.Customer opens a Search** Page.</p><p>2\.Enter keywords or filter criteria.</p><p>3\.The system retrieves matching results.</p><p>4\.Results displayed with pagination and filters applied.</p>|||
|Alternative Sequences/Flows|<p>Step 3.1 –No Results Found</p><p>1. System shows “No matching results”.</p>|||

#### <a name="_wbvw13egyk4j"></a>***2.5.2 Make Booking (Homestay/Restaurant)***

|Primary Actors|Customer|Secondary Actors|Payment Gateway|
| -: | :- | -: | :- |
|Description|As a customer, I want to book a homestay or restaurant table and make payment if required.|||
|Preconditions|Customers must be logged in.|||
|Postconditions|<p>- Booking created successfully.</p><p>- Payment recorded if applicable.</p>|||
|Normal Sequence/Flow|<p>1\.The customer selects a homestay/restaurant.</p><p>2\.The system displays details and booking forms.</p><p>3\.Customer fills booking info and submits.</p><p>4\.The system validates details and checks availability.</p><p>5\.Customer proceeds to payment (if required).</p><p>6\.The system confirms booking and sends notification.</p>|||
|Alternative Sequences/Flows|<p>Step 4.1 – Not Available</p><p>1. System detects no availability.</p><p>2. Displays message ( “Selected date/time unavailable”).</p>|||

#### <a name="_heqc4uff79yo"></a>***2.5.3 Write Feedback***

|Primary Actors|Customer|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a customer, I want to write feedback about my booking experience.|||
|Preconditions|The customer must have completed a booking.|||
|Postconditions|- Feedback submitted successfully.|||
|Normal Sequence/Flow|<p>1\.Customer accesses Booking History.</p><p>2\.Selects a completed booking.</p><p>3\.Clicks Write Feedback.</p><p>4\.The system displays feedback form.</p><p>5\.Customer submits rating and comments.</p><p>6\.The system saves feedback and displays confirmation .</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – Feedback Already Given</p><p>1. System prevents duplicate feedback</p>|||
###
#### <a name="_i35pr6ag7mru"></a><a name="_zct3ibmnsk5d"></a>***2.5.4 View Booking History***

|Primary Actors|Customer|Secondary Actors|None|
| -: | :- | -: | :- |
|Description|As a customer, I want to view my past and current bookings to track my activities.|||
|Preconditions|Customers must be logged in.|||
|Postconditions|- Booking history displayed|||
|Normal Sequence/Flow|<p>1\.Customer opens Booking History.</p><p>2\.The system retrieves past and current bookings.</p><p>3\.Displays booking list with status and details.</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – No History Found</p><p>1. System shows message (“No bookings yet”)</p>|||
###
#### <a name="_zamj8fcikg2c"></a><a name="_6hy8jiljplf5"></a>***2.5.5 Cancel Booking***

|Primary Actors|Customer|Secondary Actors|Payment Gateway|
| -: | :- | -: | :- |
|Description|As a customer, I want to cancel my booking if plans change.|||
|Preconditions|Customers must have a valid booking.|||
|Postconditions|<p>- Booking canceled.</p><p>- Refund processed if applicable.</p>|||
|Normal Sequence/Flow|<p>1\.Customer selects a booking to cancel.</p><p>2\.System checks cancellation policy.</p><p>3\.Customer confirms cancellation.</p><p>4\.System updates booking status to “Cancelled”.</p><p>5\.Refund initiated (if applicable).</p><p>6\.Confirmation message displayed .</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – Late Cancellation</p><p>1. Cancellation after deadline → System denies request.</p>|||

### <a name="_6de2uebnakfk"></a>**2..6 Feature Payment & Transaction Management**
#### <a name="_w62wmlymupz6"></a>***2.6.1 Make Payment***

|Primary Actors|Customer|Secondary Actors|Payment Gateway|
| -: | :- | -: | :- |
|Description|As a customer, I want to make secure online payments for my booking using available methods (e.g., card, e-wallet).|||
|Preconditions|Booking must exist and be confirmed.|||
|Postconditions|<p>- Payment completed successfully.</p><p>- Transactions logged in the system.</p>|||
|Normal Sequence/Flow|<p>1\.The customer chooses a payment method.</p><p>2\.The system connects to Payment Gateway.</p><p>3\.Customer enters payment details.</p><p>4\.Payment Gateway validates and processes transactions.</p><p>5\.The system receives confirmation.</p><p>6\.Booking status changes to “Paid”.</p><p>7\.Receipt displayed and emailed.</p>|||
|Alternative Sequences/Flows|<p>Step 4.1 – Payment Failed</p><p>1. Invalid card or insufficient funds.</p><p>2. System displays message (MSG46: “Payment failed, please try again”).</p>|||
###
#### <a name="_6rnm8h9vjr2q"></a><a name="_m9aw2r52zjry"></a>***2.6.2 Send Transaction Status***

|Primary Actors|Payment Gateway|Secondary Actors|Admin|
| -: | :- | -: | :- |
|Description|As for the payment system, I want to send transaction status updates to notify the booking system of payment results.|||
|Preconditions|Transaction has been processed.|||
|Postconditions|<p>- System updates booking/payment status.</p><p>- Notification sent to user.</p>|||
|Normal Sequence/Flow|<p>1\.Payment Gateway completes processing.</p><p>2\.Sends transaction status (success/failure) to system API.</p><p>3\.System updates database accordingly.</p><p>4\.System sends confirmation message to customer.</p>|||
|Alternative Sequences/Flows|<p>Step 2.1 – Communication Failure</p><p>1. If a callback from Payment Gateway fails, the system retries.</p><p>2. Logs error and notifies admin .</p>|||

## <a name="_lq63d3sfmkrh"></a>**3. Functional Requirements**
### <a name="_13a5mmgcely4"></a>**3.1 Authentication**
#### <a name="_ccaosn3txfm0"></a>***3.1.1 User Authentication***
##### **3.1.1.1 Login**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.014.png)**This screen allows the User to:**

- Authenticate Identity: Enter valid Email and Mật Khẩu (Password) to access the system by clicking the “Login” button.

**On the screen, s/he can also:**

- Recover Password: Go to the password recovery screen by clicking the Quên mật khẩu (Forgot Password) link.
- Register as Customer: Navigate to the customer registration screen by clicking the Đăng kí tài khoản khách hàng (Register Customer Account) button.
- Register as Partner/Host: Navigate to the partner/host registration screen by clicking the Đăng kí tài khoản chủ nhà/homestay (Register Host/Homestay Account) button.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Email|Initial values: blank field. Must contain a valid email address format (e.g., user@domain.com). This field is mandatory for login.|
|Password|Initial values: blank field. Input characters are masked for security (e.g., appear as bullets or asterisks). This field is mandatory for login.|
#####
##### <a name="_6m9r89kcdvzf"></a><a name="_jw1z70trewvb"></a>**3.1.1.2 Forgot password**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.015.png)

![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.016.png)

![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.017.png)

**This screen allows the User to:**

- **Request OTP:** Enter the registered **Email** and click **Gửi Mã OTP** (Send OTP Code).
- **Verify Identity:** Enter the received **Mã OTP** (OTP Code) and click **Xác Thực** (Validate).
- **Reset Password:** Enter and confirm the new password by clicking **Đổi Mật Khẩu** (Change Password).

**On the screen, s/he can also:**

- **Receive Instructions:** Receive an OTP via the registered email address after a successful request.
- **Proceed Step-by-Step:** The **Xác Thực OTP** stage should only become visible/active after successfully sending the OTP. The **Đổi mật khẩu** stage should only become visible/active after successfully validating the OTP.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Email|The registered email address that the user enters to initiate the password recovery process and request the OTP code. This field is mandatory.|
|OTP Code|The one-time authentication code received by the user via email. This code must be entered correctly to verify identity and proceed to the next step.|
|New Password|The new password that the user sets for their account. This password must adhere to the system's security policies (e.g., minimum length, complexity).|
|Confirm Password|The user must re-enter the new password exactly as entered in the New Password box to confirm accuracy.|
##### <a name="_ju44tsgyp4yz"></a>**3.1.1.3 List Booking USer** 
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.018.png)

**This screen allows the User to:**\
` `View and manage all booking information related to their experiences and reservations at Cát Bà.

**On this screen, the User can:**

- **View Total Bookings:** See the total number of bookings across all statuses (confirmed, pending, canceled).
- **Check Booking Status:** Monitor bookings under four categories — *Tất cả (All)*, *Đã xác nhận (Confirmed)*, *Đang chờ (Pending)*, and *Đã hủy (Canceled)*.
- **View Booking Details:** Review specific booking information such as restaurant name, date, time, number of guests, and total price.
- **See Booking Status Indicators:** Identify whether a booking is confirmed (green label), pending, or canceled (red label).
- **Navigate to Booking Details:** Click “Xem chi tiết” to access full details of an individual booking.
- **Return to Home:** Click “Quay lại trang chủ” to go back to the homepage.


**Field Description**

|**Field Name**|**Description**|
| - | - |
|Booking ID|Unique identifier for each booking.|
|`  `Email|The email address associated with the booking.|
|Destination / Venue|Name and address of the restaurant and location.|
|Start Time|An input field or dropdown for the user to specify their current city of residence.|
|End Time|The user's contact phone number.|
|Guest|The total number of guest for the booking.|
|Total Price|The user's detailed residential address.|
|Status|The current status of the booking|
|View Details|A link or button to open more detailed information about that booking.|
#####
### <a name="_5xd8w065sdjz"></a><a name="_7xi7xvkiqfnl"></a>**3.2 User Authentication**
***3.2.1***: **Edit Profile User**
##### <a name="_gjwbltrllkpz"></a>**3.2.1.1 Edit profile**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.019.png)

This screen allows the **User** to:

- **View/Edit Personal Info:** View and modify personal details such as name, gender, date of birth, contact, and address.
- **Return Home:** Navigate back to the home page.
- **Save Changes:** Commit the updated personal information to the system.
- **Cancel Changes:** Discard any modifications made in this section.

On the screen, s/he can also

- **Select Date of Birth:** Input the date of birth via three separate dropdowns (Ngày, Tháng, Năm).
- **Update Contact Info:** Modify their Phone Number and Email address.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Full Name|The user's full name, which can be updated.|
|Gender|A required dropdown selection field allowing the user to choose their gender.|
|Date of Birth|Three combined dropdown fields for selecting the Day, Month, and Year of birth.|
|City of Residence|An input field or dropdown for the user to specify their current city of residence.|
|Phone Number|The user's contact phone number.|
|Email|The user's registered email address. This may be editable or read-only based on system rules.|
|Address|The user's detailed residential address.|
#####
##### <a name="_dssprlyrul3t"></a><a name="_q4m8ywx6skv9"></a>**3.2.1.2 Change Password**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.020.png)

This screen allows the **User** to:

- **Change Password:** Update their account password after successfully verifying the current password.
- **Return Home:** Navigate back to the home page.
- **Save New Password:** Commit the password change to the system.
- **Cancel:** Discard the password change attempt.

On the screen, s/he can also

- **Verify Identity:** Must enter the **Mật khẩu hiện tại** (Current Password) to authenticate the change request.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Current Password|The password currently in use by the user. This is a mandatory verification field.|
|New Password|The new, desired password for the user's account.|
|Confirm Password|Re-enter the new password to ensure it exactly matches the **Mật khẩu mới** field before saving.|
#####
### <a name="_rdiyv17ey73t"></a><a name="_pltrlfipb7t"></a>**3.3 System Administration**
#### <a name="_e7izoed1pcv4"></a>***3.3.2 User Management***
##### **3.3.2.1 User List**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.021.png)

This screen allows the **Administrator** to:

- **View User List:** View a list of all current system users.
- **Search Users:** Enter keywords (likely name, ID, or email) to search the user list.
- **Filter Users by Role:** Filter the user list based on their **Quyền** (Role) using the dropdown selector.
- **Filter Users by Status:** Filter the user list based on their **Trạng thái** (Status) using the dropdown selector.


On the screen, s/he can also

- **View Details:** Click the **Chi tiết** (Details) button in the **Thao Tác** (Action) column to view detailed information for a specific user.
- **Toggle Status:** Change the activation status (e.g., Active/Inactive or ON/OFF) of a specific user using the toggle button in the **Thao Tác** (Action) column.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Search Input Box|Field used to enter keyword(s) to search for users (e.g., by name or email).|
|Role|A filter dropdown allowing the Administrator to view users based on their assigned role (e.g., Admin, Customer, Partner).|
|Status|A filter dropdown allowing the Administrator to view users based on their current status (e.g., Active, Inactive, Pending).|
|List Data Columns|The columns displaying static user information in the table: **STT** (No.), **Họ và tên** (Full Name), **Email**, **Quyền** (Role), and **Trạng thái** (Status).|
|Action|The column containing action buttons/controls for the specific user row.|
|Details Button|Button to navigate to the detailed user information screen..|
|ON/OFF Toggle|Control button used to quickly activate or deactivate the user's account status.|

##### **3.3.2.2 User Details**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.022.png)

This screen allows the **Administrator** to:

- **View User Personal Information:** View all individual details of a selected user.
- **View Business Information (If Applicable):** View the business profile information of the user (e.g., if the user is a Partner/Host).
- **Return to List:** Go back to the **User List** screen by clicking the **Quay lại** (Return) button.

On the screen, s/he can also

- **Review Roles and Status:** Check the assigned **Quyền** (Role) and the current **Trạng thái** (Status) of the user's account.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|List Data Fields|Static fields displaying all data retrieved from the database. This includes personal details (**Họ và tên**, **Email**, **Số điện thoại**, **Số CCCD**, **Địa chỉ cá nhân**, **Quyền**, **Trạng thái**) and business details (**Tên cơ sở kinh doanh**, **Loại Hình**, **Địa Chỉ Kinh Doanh**, **Mô tả**, **Thời gian mở cửa**, **Trạng thái kinh doanh**).|
|Return Button|The button to navigate back to the **User List** screen.|
#####
##### <a name="_ywp0norliuw"></a><a name="_nibtmsxz4cva"></a>**3.3.2.3 Approve Application**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.023.png)

This screen allows the **Administrator** to:

- **View Application List:** View a list of all pending applications or applications requiring review/action.
- **Review Application Status:** Quickly view the current **Trạng thái** (Status) of each application.

On the screen, s/he can also

- **View Details:** Click the **Chi tiết** (Details) button in the **Thao Tác** (Action) column to navigate to the detailed application screen for review and approval/rejection.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|List Data Fields|Static fields displaying key information about the application retrieved from the database: **STT** (No.), **Họ và Tên** (Applicant's Full Name), **Email** (Applicant's Email), **Tên Cơ sở** (Business/Homestay Name), **Loại Hình** (Business Type), and **Trạng Thái** (Application Status).|
|Details Button|IButton to navigate to the detailed application view screen where the Administrator can review, approve, or reject the application..|
#####
##### <a name="_t32j94w7kd99"></a><a name="_d4xcen7blvtx"></a>**3.3.2.4 Approve Application Detail**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.024.png)

This screen allows the **Administrator** to:

- **View Personal Information:** Review the applicant's personal details provided during registration.
- **View Business Information:** Review the business/homestay details provided by the applicant.
- **Approve Application:** Finalize the acceptance of the application by clicking the **Duyệt** (Approve) button.
- **Reject Application:** Finalize the denial of the application by clicking the **Từ Chối** (Reject) button.

On the screen, s/he can also

- **Return to List:** Go back to the **Approve Application** list screen by clicking the **Quay Lại** (Return) button.
- **Make Decision:** Take the definitive action of **Duyệt** or **Từ Chối** based on the reviewed details.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|List Data Fields|**Static fields displaying all data related to the application.** This includes personal details (**Họ và Tên**, **Số điện thoại**, **Số CCCD**, **Địa chỉ cá nhân**) and business details (**Tên cơ sở**, **Loại hình**, **Địa chỉ kinh doanh**, **Miêu tả chi tiết**).|
|Details Button|Button to navigate back to the **Approve Application** list screen (3.3.2.3).|
|Approve Button|Button to formally approve the application, changing its status to 'Approved' or 'Active'.|
|Reject Button|Button to formally reject the application, changing its status to 'Rejected'.|
#####
##### <a name="_513rsnu0qwl1"></a><a name="_50ca4dsb3dxi"></a>**3.3.2.5 Manager Feedback**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.025.png)

This screen allows the **Administrator (Admin System)** to:

- **View Feedback Overview:** View summary statistics such as the **Tổng số đánh giá** (Total Number of Ratings) and the **Điểm trung bình** (Average Score).
- **View Feedback List:** View a list of all submitted user feedback and ratings.
- **Navigate Admin Menu:** Use the sidebar menu to navigate to other Admin System functions (**Dashboard**, **Duyệt yêu cầu**, **Quản lí người dùng**, **Quản lí feedback**, **Cài đặt**, **Đăng xuất**).

On the screen, s/he can also

- **Approve Feedback:** Formalize the visibility of a feedback/rating by clicking the **Duyệt** (Approve) button in the **Thao tác** (Action) column.
- **Delete Feedback:** Remove an inappropriate or redundant feedback/rating by clicking the **Xoá** (Delete) button in the **Thao tác** (Action) column.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Total Ratings|A summary display box showing the total count of feedback/ratings submitted by users.|
|Average Score|A summary display box showing the calculated average rating score (e.g., based on a 5-star scale).|
|List Data Fields|Static fields displaying key information for each feedback entry: **ID**, **Người dùng** (User Name), **Nội dung** (Content/Description of Feedback), **Đánh giá** (Rating Score/Stars), and **Ngày gửi** (Submission Date).|
|Approve Button|Button used by the Administrator to confirm and publish a feedback entry.|
|Delete Button|Button used by the Administrator to remove a feedback entry from the list/system.|
###
### <a name="_xhf0kar90pdv"></a><a name="_nex1asymjcxc"></a>**3.4 Homepage Management**
#### <a name="_97114rw1o2ch"></a>***3.4.1 HomePage***

![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.026.png)

This screen allows the **User** to:

- **Search Services:** Perform a customized search for services (Homestay, Restaurant, etc.) based on various criteria (area, dates, number of people/rooms).
- **View Featured Listings:** View popular or featured listings categorized (e.g., "Homestay nổi bật," "Nhà hàng đặc sắc").
- **Navigate to Details:** Click the **Chi tiết** (Details) button on any listing card to view the specific service details.
- **Filter by Service Type:** Switch between different service categories (**homestay**, **nhà hàng**, **Trang chủ**) using the tabs/buttons at the top right.

On the screen, s/he can also

- **Input Search Criteria:** Utilize the filter dropdowns and input fields (**Khu vực**, **Ngày nhận**, **Ngày trả**, **Số người**, **Số phòng**) before clicking **Tìm** (Search).
- **Browse Categories:** Easily browse through highlighted categories displayed below the search bar.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Service Type Tabs|Buttons/tabs allowing the user to switch the main view/search focus between **homestay**, **nhà hàng** (restaurant), and **Trang chủ** (Home/All).|
|Area|A dropdown filter to select the desired geographic region or area for the search.|
|Check-in Date|A dropdown date selector for the check-in date (for homestay booking).|
|Check-out Date|A dropdown date selector for the check-out date (for homestay booking).|
|Number of People|An input field or dropdown to specify the number of guests.|
|Number of Rooms|An input field or dropdown to specify the number of rooms required.|
|Search Button|The button to execute the search based on the selected criteria..|
|Listing Categories|Headings organizing the featured services (e.g., **Homestay nổi bật**, **Nhà hàng đặc sắc**).|
|Details Button|Button on each listing card that navigates the user to the specific service detail screen.|

#### <a name="_d8jcyiwwufdk"></a>***3.4.2 Homestay*** 
##### <a name="_6x8kgkbed5d8"></a>**3.4.2.1 Homestay list**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.027.png)

` `When the user clicks on “Homestay” in the sidebar, the screen will display a list of all available homestays.

**User Actions:**

**Each homestay in the list includes action buttons:**

- **“Search” button:** Users can **search homestay** using the input field above the list  and the filter panel on the left.
- **“Chi tiết” button:** allowing users to view detailed information and book a room at the homestay.

##### <a name="_udmq0d3cbx5f"></a>**3.4.2.1 Homestay detail & Rooms list**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.028.png)

- When the user clicks on “Chi tiết” on the Homestay List page, the system will display detailed information about that homestay and show the available rooms for it.
- **User Actions:**
+ **“Search”** button: The user can check room availability by entering information in the search bar at the top and then clicking the search button.
+ **“Đặt phòng”** button: allowing users to book a room at that homestay.

#### <a name="_rd6wz1dt3gf8"></a>***3.4.3 Restaurant***
##### <a name="_awezvispv6xn"></a>**3.4.3.1 Restaurant list**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.029.png)

- The screen displays all restaurants that still have available tables.
- Users can **search restaurants** using the input field above the list  and the filter panel on the left.

##### <a name="_tcnp4n2mkbfv"></a>**3.4.3.2 Restaurant detail &  Menu**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.030.png)

##### <a name="_pbbr8k7ltdv9"></a>**3.4.3.3 Checkout  Restaurant**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.031.png)

This screen allows the **User** to:

- **View Restaurant Information:** View static details of the selected restaurant (**Tên nhà hàng**, **Địa chỉ nhà hàng**, **Giờ mở cửa**).
- **View Order Details:** Review the items/services in the order in the **Chi tiết đơn hàng** box and see the **Tổng** (Total) amount.
- **Input Reservation Details:** Enter contact and reservation specifics (**Tên khách hàng**, **Số điện thoại**, **Email**, **Ngày đặt bàn**, **Giờ đặt bàn**, **Số lượng khách**).
- **Confirm Payment:** Finalize the reservation and payment process by clicking the **Xác nhận thanh toán** (Confirm Payment) button.

On the screen, s/he can also

- **Review Billing:** Check the itemized breakdown and final total cost before confirming the payment/reservation.
- **Provide Contact Info:** Ensure accurate details are provided for the reservation confirmation.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Restaurant Info Fields|Static display fields showing details of the selected restaurant: **Tên nhà hàng** (Restaurant Name), **Địa chỉ nhà hàng** (Restaurant Address), and **Giờ mở cửa** (Opening Hours)..|
|Order Details|A display box showing the itemized list of reserved services or items.|
|Total|A display field showing the final total cost of the order.|
|List Data Fields|Input fields for the user to provide reservation and contact details: **Tên khách hàng** (Customer Name), **Số điện thoại** (Phone Number), **Email**, **Ngày Đặt bàn** (Reservation Date), **Giờ đặt bàn** (Reservation Time), and **Số lượng khách** (Number of Guests).|
|Confirm Payment|Button to submit the reservation request and proceed with the payment/confirmation process.|

#### <a name="_39ver1bdvejg"></a>***3.5.4 Payment***
##### <a name="_mdfg1jbpvt2g"></a>**3.5.1.1 Online Payment**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.032.png)

This screen allows the **User** to:

- **View Reservation Summary:** View static details of the reservation, including the **Mã đặt bàn** (Reservation Code), **Nhà hàng** (Restaurant Name), **Ngày Đặt** (Reservation Date), **Số khách** (Number of Guests), and **Số tiền** (Amount Due).
- **Process Payment:** Complete the payment transaction by scanning the provided QR code within the allotted time.

On the screen, s/he can also

- **Scan QR Code:** Use the provided **Mã QR** (QR Code) to initiate the payment via a mobile payment application.
- **Monitor Timeout:** Observe the countdown timer (e.g., **5:00**) to ensure the payment is completed within the specified **5 phút** (5 minutes) limit.
- **Receive Instruction:** View the instruction **"Đang chờ thanh toán... Vui lòng quét mã QR trong vòng 5 phút"** (Awaiting payment... Please scan the QR code within 5 minutes).

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Reservation Summary Fields|Static display fields showing details of the confirmed reservation: **Mã đặt bàn** (Reservation Code), **Nhà hàng** (Restaurant Name), **Ngày Đặt** (Reservation Date), **Số khách** (Number of Guests), and **Số tiền** (Amount Due).|
|Payment Status Message|A static or dynamic field displaying the current status of the payment process and instructions: "Đang chờ thanh toán... Vui lòng quét mã QR trong vòng 5 phút."|
|QR Code|The visual code (QR code) that the user must scan using a banking or payment application to complete the transaction.|
|Timer|A countdown timer indicating the remaining time allowed for the user to complete the payment via QR code scanning.|




### <a name="_tcdhdidoz49k"></a>**3.6 Owner Page**
#### <a name="_3vqulk2hyxgs"></a>***3.6.1 Business information***
##### <a name="_411qfb3th4mb"></a>**3.6.1.1 Restaurant *profile***
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.033.png)

This screen allows the **Restaurant Owner/Administrator** (or relevant user) to:

- **View/Update Basic Info:** View and edit the fundamental details of the restaurant (**Tên Nhà Hàng**, **Địa Chỉ**, **Mô Tả**, etc.).
- **Update Operating Hours:** Modify the restaurant's working schedule (**Giờ mở cửa** and **Giờ đóng cửa**).
- **Update Location:** Select or update the geographical area/location (**Khu Vực**) of the restaurant.
- **Save Changes:** Commit all modifications to the system by clicking the **Lưu Thay Đổi** (Save Changes) button.

On the screen, s/he can also

- **Manage Images:** Upload or view the main image(s) for the business (**Ảnh cơ sở kinh doanh**).

**Field Description**

|**Field Name**|**Description**|
| - | - |
|Business Image|A field/area for uploading or displaying the main image(s) or logo of the restaurant.|
|Area|A dropdown selector or input field used to specify the geographical region or location of the restaurant."|
|List Data Fields|Input fields used to view and update the static restaurant details: **Tên Nhà Hàng** (Restaurant Name), **Địa Chỉ** (Address), **Mô Tả** (Description), **Giờ mở cửa** (Opening Time), and **Giờ đóng cửa** (Closing Time).|
|Save Changes|The button to submit and save all the modified information on the screen..|

#### <a name="_mhzixp6pe6cl"></a>***3.6.2 Owner Homestay***
##### <a name="_o1qwja6twlv4"></a>**3.6.2.1 Homestay Owner Dashboard**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.034.png)	
##### <a name="_eruvzyek58mi"></a>**3.6.2.2 Homestay Rooms Management**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.035.png)
##### <a name="_g7npr2vsrkaz"></a>**3.6.2.3 Homestay Rooms Update**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.036.png)

#### <a name="_ln7fh2glra28"></a>***3.6.3 Business information***


#### <a name="_f9zljvvcnacd"></a>***3.6.4 Owner Restaurant***
##### <a name="_yk9wonbpo5y4"></a>**3.6.3.1 Add Dish**
![ref1]

This screen allows the **Restaurant Owner/Manager** (or relevant user) to:

- **Input Dish Details:** Enter all necessary information for a new menu item, including its name, price, description, and category.
- **Upload Image:** Upload a visual representation (**Ảnh món ăn**) of the new dish.
- **Set Status:** Specify the initial availability or **Trạng Thái** (Status) of the dish (e.g., Available, Out of Stock).
- **Save Dish:** Finalize and save the new dish entry to the restaurant's menu by clicking the **Lưu món** (Save Dish) button.

On the screen, s/he can also

- **Categorize:** Select the **Danh mục** (Category) for the dish (e.g., Appetizers, Main Course, Dessert) via a dropdown or input field.
- **Input Price:** Specify the **Giá** (Price) of the menu item.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|List Data Fields|Input fields used to enter the new dish details: **Tên món** (Dish Name), **Giá** (Price), **Mô tả** (Description), **Danh mục** (Category), and **Trạng Thái** (Status).|
|Dish Image|A field/area for the user to upload an image file of the new dish.|
|Save Dish|The button to submit and save all the entered information, effectively adding the new dish to the restaurant's menu.|

##### <a name="_xhycvk2bjw24"></a>**3.6.3.2 DishsList**
![](Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.038.png)

This screen allows the **Restaurant Owner/Manager** (or relevant user) to:

- **View Dish List:** View a list of all current menu items managed by the restaurant.
- **Review Dish Details:** Quickly review key information for each dish, including its price, category, and status.

On the screen, s/he can also

- **Edit Dish:** Click the **Sửa** (Edit) button in the **Thao tác** (Action) column to navigate to the **Edit Dish** screen (similar to **3.6.3.1 Add Dish**) to modify the details of the selected menu item.
- **Sort List:** Sort the list by clicking on column headers (e.g., **ID**, **Món ăn**, **Giá**, **Danh mục**, **Trạng thái**).

**Field Description**

|**Field Name**|**Description**|
| - | - |
|List Data Fields|Static fields displaying key details for each menu item: **ID**, **Món ăn** (Dish Name), **Giá** (Price), **Danh mục** (Category), and **Trạng thái** (Status).|
|Edit Button|Button to navigate to the edit screen, allowing the user to update the selected dish's details.|
##### <a name="_i3b9463s46oe"></a>**3.6.3.3 Update Dishes**
![ref2]

This screen allows the **Restaurant Owner/Manager** (or relevant user) to:

- **Edit Dish Details:** Modify existing information for a menu item, including its name, price, description, and category.
- **Update Image:** Upload a new or change the existing image (**Ảnh món ăn**) of the dish.
- **Update Status:** Change the availability or **Trạng Thái** (Status) of the dish (e.g., Available, Out of Stock).
- **Save Changes:** Finalize and save the updated dish entry to the restaurant's menu by clicking the **Lưu món** (Save Dish) button.

On the screen, s/he can also

- **Pre-fill Data:** View the current details of the selected dish pre-filled in all input fields upon accessing the screen.
- **Categorize:** Modify the **Danh mục** (Category) for the dish.
- **Adjust Price:** Change the **Giá** (Price) of the menu item.

**Field Description**

|**Field Name**|**Description**|
| - | - |
|List Data Fields|Input fields used to view and update the dish details. Data is pre-filled from the database: **Tên món** (Dish Name), **Giá** (Price), **Mô tả** (Description), **Danh mục** (Category), and **Trạng Thái** (Status).|
|Dish Image|A field/area for the user to upload or replace the current image file of the dish.|
|Save Dish|The button to submit and save all the updated information, modifying the existing dish on the menu.|


## <a name="_2oy4iff08u11"></a>**4. Non-Functional Requirements**
### <a name="_fxl6fctfuhdk"></a>**4.1 External Interfaces**
This system interfaces with several external components to perform its core functions:

**Database Interface:** The system connects to an external MySQL database. All application data, including user accounts, bookings, and business details, is persisted in this database. Configuration (URL, driver, credentials) is managed externally in the DB.properties file.

- **Email Service Interface:** The system interfaces with an external SMTP server (smtp.gmail.com) to send transactional emails to users. This is used for:
  - Registration confirmations.
  - Password reset OTPs.
  - Notifications for owner application status (pending, approved, rejected).
  - Notifications for administrators (e.g., new owner application).
- **Payment Gateway Interface (SePay):** The system receives incoming webhooks from the SePay payment gateway to confirm payments. This interface is responsible for:
  - Listening for POST requests on the /sepay-webhook endpoint.
  - Validating incoming requests using an Authorization token.
  - Parsing payment details (amount, content, transaction ID) from the request body.
  - Updating the booking and payment status in the database upon successful payment confirmation.

### <a name="_6qzgg1z0a4nw"></a>**4.2 Quality Attributes**
#### <a name="_jij3qtacpikj"></a>***4.2.1 Usability*** 
- **User Feedback:** The system shall provide timely and clear feedback to users via email for critical processes, such as account registration, owner application status changes (pending, approved, rejected), and password resets.
- **Role-Based Navigation:** The system shall direct users to the correct interface (dashboard) based on their assigned role (Admin, Owner, Customer) immediately after login.
- **Error Prevention:** The system shall validate user input to prevent common errors. For example, password fields must be validated for strength requirements, and confirmation passwords must match.
- **Account Recovery:** A password reset mechanism using an email-based OTP (One-Time Password) shall be provided for users who forget their password.
#### <a name="_ta5sebvexycc"></a>***4.2.2 Performance***
- **Password Hashing:** User passwords must be hashed using the **Argon2id** algorithm to ensure security. This intentionally resource-intensive operation is calibrated with the following parameters:
  - Iterations: 2
  - Memory Cost: 19456 KB (19MB)
  - Parallelism: 1
- **Connection Pooling (Implicit):** The database utility (DBUtil) loads the driver and properties once using a static initializer. This avoids the performance overhead of reloading resources on every connection request. (Note: A production-grade connection pool is recommended but not explicitly implemented in this class).
- **Resource Management:** The system must provide a mechanism to automatically cancel expired bookings (e.g., those not paid within 5 minutes) to free up inventory (rooms, tables).
- **Webhook Response Time:** The payment webhook endpoint (/sepay-webhook) must process incoming notifications and return a 200 OK status rapidly to prevent retries from the SePay gateway, even if internal processing fails.


#### ***4.2.3 Security***
- **Password Storage:** All user passwords must be securely hashed using Argon2id and never stored in plain text.
- **Password Complexity:** Passwords must adhere to a defined complexity policy: 8-64 characters, containing at least one uppercase letter, one lowercase letter, one number, and one special character.
- **Access Control:** Users must have an "active" status to be authenticated and gain access to the system. Accounts that are "pending" or "rejected" shall be denied login.
- **API Authentication:** The payment webhook endpoint must be secured with an API key (Authorization: Apikey ...) to prevent unauthorized access.

#### <a name="_lmq98l1d0zl3"></a>***4.2.4 Reliability***
- **Fail-Fast Configuration:** The system shall fail at startup if essential configuration files (DB.properties, Email.properties) are missing or unreadable. This prevents the system from running in an invalid state.
- **Graceful Handling of Duplicates:** The payment webhook shall ignore payment notifications for bookings that are not found or have already been processed, returning a 200 OK to the provider to prevent error loops.

#### <a name="_xmz5rk359h68"></a>***4.2.5 Maintainability***
- **Externalized Configuration:** Database credentials, email server credentials, and application-specific settings (e.g., sender name) must be stored in external .properties files (DB.properties, Email.properties). This allows for changes in the environment without recompiling the source code.

## <a name="_huets3f79vnv"></a>**5. Requirement Appendix**
### <a name="_5m8i3geebwdq"></a>**5.1 Business Rules**

|**ID**|**Rule Definition**|
| :-: | :-: |
|BR-01|A user's password must be 8-64 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.|
|BR-02|New owner applications must be manually approved by an administrator. The user's status remains "pending" until approved.|
|BR-03|An administrator must provide a text reason when rejecting an owner's application.|
|BR-04|Only users with an "active" account status are permitted to log in.|
|BR-05|Upon login, users must be redirected to a dashboard corresponding to their role (e.g., Admin, Owner, Customer).|
|BR-06|The system must automatically cancel a booking if payment is not completed within 5 minutes of creation.|
|BR-07|A booking that is already "fully\_paid" or "confirmed" cannot be cancelled due to expiration.|
|BR-08|The payment processing webhook shall only process incoming fund transfers.|
|BR-09|To link a payment, the incoming transaction's content or description field must contain the system-generated booking code (e.g., BK...).|
|BR-10|A new owner registration must trigger two emails: one to the user confirming their application is pending, and one to the admin notifying them of the new application.|
|BR-11|An administrator's decision to approve or reject an owner application must trigger a notification email to that owner.|

### <a name="_5ysu5vt47rft"></a>**5.2 System Messages**

|**#**|**Message code**|**Message Type**|**Context**|**Content**|
| :- | :- | :- | :- | :- |
|1|MSG01|In line|There is not any search result|*No search results.*|
|2|MSG02|In red, under the text box|Input-required fields are empty|*The \* field is required.*|
|3|MSG03|Toast message|Updating asset(s) information successfully|Update asset(s) successfully.|
|4|MSG04|Toast message|Adding new asset successfully|*Add asset successfully.*|
|5|MSG05|Toast message|Confirming email of asset hand-over is sent successfully|*A confirmation email has been sent to {email\_address}.*|
|6|MSG06|Toast message|Resetting asset information successfully|*Return asset(s) successfully.*|
|7|MSG07|Toast message|Deleting asset information successfully|*Delete asset(s) successfully.*|
|8|MSG08|In red, under the text box|Input value length > max length|*Exceed max length of {max\_length}.*|
|9|MSG09|In line|Username or password is not correct when clicking sign-in|*Incorrect user name or password. Please check again.*|
|10|MSG10|In line (Error)|User registration (Owner): Required fields are empty.|*Vui lòng điền đầy đủ thông tin. (Please fill in all information.)*|
|11|MSG11|In line (Error)|User registration (Owner): Passwords do not match.|*Mật khẩu và xác nhận mật khẩu không khớp. (Password and confirmation password do not match.)*|
|12|MSG12|In line (Error)|User registration (Owner): Password does not meet strength requirements.|*Mật khẩu không đủ mạnh! Yêu cầu: 8-64 ký tự, có chữ hoa, chữ thường, số và ký tự đặc biệt. (Password is not strong enough! Requires: 8-64 characters, with uppercase, lowercase, number, and special character.)*|
|13|MSG13|In line (Error)|User registration (Owner): Email already in use or system error.|*Đăng ký thất bại. Email có thể đã được sử dụng hoặc có lỗi hệ thống. (Registration failed. Email may already be in use or there is a system error.)*|
|14|MSG14|In line (Success)|User registration (Owner): Successful submission.|*Đăng ký thành công! Đơn của bạn đã được gửi đến admin và đang chờ duyệt. Vui lòng kiểm tra email. (Registration successful! Your application has been sent to the admin and is awaiting approval. Please check your email.)*|
|15|MSG15|In line (Error)|Login: Email or password fields are empty.|*Vui lòng điền đầy đủ email và mật khẩu. (Please fill in all email and password fields.)*|
|16|MSG16|In line (Error)|Login: User account is not "active" (e.g., pending, rejected, banned).|*Tài khoản của bạn đã bị khóa hoặc đang chờ duyệt. Vui lòng liên hệ quản trị viên. (Your account has been locked or is pending approval. Please contact an administrator.)*|
|17|MSG17|In line (Error)|Login: Invalid credentials.|*Email hoặc mật khẩu không đúng. (Email or password is not correct.)*|
|18|MSG18|In line (Success)|Admin: Application approved successfully.|*Đã duyệt đơn thành công! (Application approved successfully!)*|
|19|MSG19|In line (Error)|Admin: Error during application approval.|*Lỗi khi duyệt đơn. (Error when approving application.)*|
|20|MSG20|In line (Error)|Admin: Reason for rejection is required.|*Vui lòng cung cấp lý do từ chối. (Please provide a reason for rejection.)*|
|21|MSG21|In line (Success)|Admin: Application rejected successfully.|*Đã từ chối đơn thành công! (Application rejected successfully!)*|
|22|MSG22|API Response (JSON)|API: Method Not Allowed (e.g., GET used instead of POST).|*{"error":"Method not allowed"}*|
|23|MSG23|API Response (JSON)|API: Unauthorized access to a secured endpoint (e.g., webhook).|*{"error":"unauthorized"}*|
|24|MSG24|API Response (JSON)|API: A required parameter is missing.|*{"error":"Missing booking code"}*|
|25|MSG25|API Response (JSON)|API: The specified resource (e.g., booking) was not found.|*{"error":"Booking not found"}*|
|26|MSG26|API Response (JSON)|API: Booking cancellation was successful.|*{"status":"cancelled","message":"Booking cancelled successfully"}*|
|27|MSG27|API Response (JSON)|API: Booking is already paid and cannot be cancelled.|*{"status":"already\_paid","message":"Booking already confirmed"}*|
|28|MSG28|API Response (JSON)|API: Booking is not yet expired and cannot be cancelled.|*{"status":"not\_expired","message":"Booking not expired yet"}*|
|29|MSG29|System Exception|System startup: DB.properties file is missing.|*Xin lỗi, không tìm thấy file db.properties (Sorry, cannot find db.properties file)*|
|30|MSG30|System Exception|System startup: Email.properties file is missing.|*Không tìm thấy file cấu hình email. (Cannot find email configuration file.)*|
|31|MSG31|System Exception|System startup: MySQL JDBC driver is missing.|*Không tìm thấy JDBC Driver của MySQL! (Cannot find MySQL JDBC Driver!)*|

### <a name="_98396h44lry2"></a>**5.3 Other Requirements:**
- **REQ-01: Password Hashing Algorithm:** All user passwords must be hashed using the **Argon2id** algorithm.
- **REQ-02: Hashing Parameters:** The Argon2id algorithm must be configured with the following parameters:
  - Iterations: 2
  - Memory Cost: 19456 KB (19MB)
  - Parallelism: 1
- **REQ-03: Secure Memory Wiping:** After hashing or verification, the plain-text password array in memory must be explicitly wiped.
- **REQ-04: Database Configuration File:** The system requires a DB.properties file in the classpath, containing db.driver, db.url, db.username, and db.password.
- **REQ-05: Email Configuration File:** The system requires an Email.properties file in the classpath, containing mail.app.username, mail.app.password, mail.smtp.host, and other JavaMail properties.
- **REQ-06: Webhook Security:** The /sepay-webhook endpoint must be secured. It requires an Authorization header with an API key, which is loaded from a system property or environment variable named SEPAY\_API\_TOKEN.
- **REQ-07: Payment Code Extraction:** The system must be able to extract a booking code (e.g., BK...) from the content or description field of an incoming webhook payload using regular expressions.
- **REQ-08: Character Encoding:** The system must read the webhook request body using StandardCharsets.UTF\_8 to ensure correct parsing of special characters.
- **REQ-09: HTML Email Content:** All user-facing emails (e.g., confirmation, rejection, OTP) must be sent with the content type text/html; charset=UTF-8.
- **REQ-10: HTML Escaping:** Any dynamic user-provided data (like names or business names) inserted into HTML email templates must be properly escaped to prevent HTML injection.


|CatBaBooking- SRS Document|Page |
| :- | -: |


[ref1]: Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.037.png
[ref2]: Aspose.Words.93072086-5489-44e7-b6df-43985b6d0d25.039.png
