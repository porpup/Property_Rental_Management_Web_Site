# Property Rental Management Web Site
## I.	PROJECT DESCRIPTION

The purpose of the home Rental Management Website is to make renting a home easier for prospective renters, owners, and managers. With the help of this web-based platform, users can effectively manage properties, look for suitable residences, and connect with one another all in one place. Three main user roles are available in the system: Property Owner, Property Manager, and Potential Tenants. The system provides an easy-to-use interface.

### Users and Operations:
#### Property Owner/Administrator:
•	Create, update, delete, search, and list property manager accounts.\
•	Update, delete, search, and list potential tenant accounts.\
•	Maintain full control over the entire web site.
#### Potential Tenants:
•	Create an online account through the Property Rental Management Web Site.\
•	View apartment listings that suit their requirements.\
•	Schedule appointments with property managers to visit the apartments.\
•	Send messages and inquiries to property managers regarding the listed properties.

### Technologies Used:
#### Client Side:
•	CSS3 for styling and layout.\
•	HTML5 for creating the website's structure, JavaScript for interactive and dynamic elements.\
•	Bootstrap
#### Server Side: 
•	ASP.Net MVC
#### Database: 
•	SQL Server
#### Web Browser: 
•	Microsoft Edge\
•	Google Chrome
#### IDE: 
•	Visual Studio 2022

## II.	DESIGN
### 1.	Connected Services:
•	Design Considerations: The application is integrated with SQLServer Database to manage and store all its data. The choice of SQLServer was driven by its robustness, scalability, and compatibility with ASP.NET applications. Using SQLServer ensures reliable data storage, efficient querying capabilities, and seamless integration with the Entity Framework used in the application. This centralized database system enhances the functionality of the web application by providing a consistent and reliable data source for all application operations, from user authentication to property management. Furthermore, SQLServer's built-in security features ensure that user data and application data are stored securely, adding an additional layer of security to the application.
### 2.	Properties & References
•	Design Considerations: The project heavily relies on the Entity Framework (EF) as its Object-Relational Mapping (ORM) tool. The properties and references within the project are primarily defined and managed through EF, which abstracts the database interactions and allows the application to work with the database using .NET objects. The significance of using Entity Framework lies in its ability to enable developers to work at a higher level of abstraction when dealing with data, reducing the need for most data-access code. This contributes to the overall functionality of the application by ensuring that data operations are consistent, efficient, and maintainable. Moreover, the references in the project, managed through EF, ensure that relationships between different data entities are correctly maintained, providing the application with a structured and relational approach to data management. This design choice not only streamlines database operations but also ensures data integrity and consistency throughout the application.
### 3.	App Data & App Start
•	Design Considerations: The initialization phase of the property rental application is crucial for setting the stage for a seamless user experience. During this phase, essential configurations, settings, and sometimes initial data are loaded to ensure the application runs smoothly. Given the nature of a property rental application, it's imperative that the startup process is efficient to cater to the diverse needs of landlords, tenants, and potential renters.\
•	BundleConfig.cs: In the context of a property rental application, the user interface plays a pivotal role in attracting and retaining users. The BundleConfig.cs file is responsible for bundling and optimizing scripts and stylesheets. Bundling helps in reducing the number of HTTP requests by combining multiple files, while optimization minifies the scripts and stylesheets to reduce their size. This ensures that pages load faster, offering users a smooth browsing experience, which is essential when viewing property listings, images, or navigating through different sections of the application.\
•	FilterConfig.cs: A property rental application deals with sensitive user data, property details, and transactional information. The FilterConfig.cs file is instrumental in setting up application-wide filters. These filters can handle errors gracefully, log critical information, and even manage aspects like user authentication. For instance, if a user tries to access a landlord-only feature, a filter can redirect them to a login page or display an appropriate error message. This design consideration ensures that the application remains robust and user-friendly, minimizing disruptions and enhancing security.\
•	RouteConfig.cs: The routing system, defined in RouteConfig.cs, is the backbone of the MVC architecture in the application. It determines how URLs map to specific controller actions. For a property rental application, this is particularly important. For example, a URL like {controller}/{action}/{id} might be designed to show details of the property with ID 5. The routing system ensures that users can easily navigate to specific properties, view listings by categories, or access their personal dashboards with intuitive URLs. Proper routing design enhances user experience, making it easier for users to find and interact with the content they're interested in.\
•	By ensuring that these configurations are appropriately set up, the property rental application can offer a responsive, secure, and user-friendly platform for all its stakeholders.
### 4.	Controllers
•	Design Considerations: In the MVC pattern, controllers act as intermediaries between the Model (data) and the View (user interface). They process user requests, interact with the data models, and determine which views to render.\
•	ApartmentsController.cs: Manages all operations related to apartment listings, such as adding, editing, or deleting property details.\
•	AppointmentsController.cs: Handles functionalities for booking, viewing, or cancelling appointments between tenants and landlords or property managers.\
•	HomeController.cs: Manages the main landing page of the application, often showcasing featured properties or important announcements.\
•	LoginController.cs: Oversees user authentication processes, ensuring secure login, registration, and session handling.\
•	MessagesController.cs: Facilitates communication between users, enabling them to send, receive, or delete messages within the platform.\
•	TenantController.cs: Manages tenant-specific functionalities, such as viewing available apartments and their information.\
•	UsersController.cs: Handles user account operations, including profile management, password resets, and account settings.\
•	Each controller is designed to streamline specific functionalities, ensuring the application remains modular and maintainable.

### 5.	Models
•	Design Considerations: Models in the MVC pattern encapsulate the application's data structures and business logic. They define the shape of the data and the relationships between different data entities.\
•	PropertyRentalModel: This is the primary Entity Framework model, acting as a bridge between the application and the database. It defines the schema, tables, and relationships, ensuring seamless data operations.\
•	Address.cs: Represents location details of properties. Attributes might include street, city, zip code, and state.\
•	Apartment.cs: Defines property listings with attributes like price, size, amenities, and availability status.\
•	Appointment.cs: Manages booking details, including date, time, attendees, and property associated with the appointment.\
•	Message.cs: Facilitates user communication, capturing sender, receiver, content, and timestamps.\
•	Role.cs: Determines user roles (e.g., tenant, owner, admin, manager) and associated permissions.\
•	User.cs: Represents registered users, storing details like username, password, contact info, and associated role.\
•	UserModel.cs: Often an auxiliary model to manage user-specific operations, like authentication or profile updates.\
•	These models collectively shape the application's data landscape, ensuring structured and efficient data management.

### 6.	Scripts
•	Design Considerations: Client-side scripts enhance the application's interactivity and responsiveness. They handle dynamic content updates, form validations, and user interactions without requiring a full page reload.\
•	In the property rental application, scripts might be used for functionalities like dynamic property search filters, interactive property image galleries, real-time availability checks, and form validations. Commonly used libraries or frameworks could include jQuery for general scripting, Bootstrap for responsive design components, and perhaps a date-picker library for appointment scheduling.


### 7.	Views
####	Design Considerations: 
• Views in the MVC pattern represent the user interface. They focus on user experience, layout, and how data is presented to the user.
####	Apartments Views:
•	Create: Interface for adding new apartment listings.\
•	Delete: Allows users to remove apartment listings.\
•	Details: Displays comprehensive information about a specific apartment.\
•	Edit: Provides an interface to update apartment details.\
•	Index: Showcases a list or grid of available apartments.\
####	Appointments Views: 
• Interfaces related to scheduling, viewing, editing, or cancelling appointments between tenants and landlords or property managers.

 

## III. CONCLUSION
As we come to the end of our project on the Property Rental Management Website, it is critical to recognize the practical experience and information we have acquired. We have direct experience with how an idea can develop into a useful online application. We've overcome obstacles in the real world, worked well as a team, and mastered the use of tools and technology. Web development has become more approachable because of our experience with ASP.NET Core MVC, from structuring websites to interacting with databases. We get invaluable skills from this hands-on experience that will be crucial for our next software projects and career aspirations in software development.

![image](https://github.com/porpup/Property_Rental_Management_Web_Site/assets/3512401/87243695-ea01-461c-b524-543b46a10b54)
