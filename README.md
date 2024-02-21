User Story
Description:
As a user, I need a basic people registration system where I can perform operations to add, delete, update, and delete people's records. The information to be registered includes name, email, and date of birth. To ensure data security, it is necessary for the user to be authenticated to access the registration functionalities. Additionally, it is essential to have the possibility for users to register in the system to perform people registrations.

Acceptance Criteria:

Implement an authentication system to ensure that only authenticated users can access the people registration functionalities.
Develop an intuitive and user-friendly user interface that allows adding, deleting, updating, and deleting people's records.
Include mandatory fields such as name, email, and date of birth in the people registration form.
Validate user-entered data to ensure the integrity and consistency of the information.
Implement automated tests to ensure the robustness and reliability of the code.
Allow new users to register in the system by providing necessary information such as username, password, and email.
Store people's records in a secure and reliable database, ensuring data integrity and privacy.
Ensure that adding, deleting, updating, and deleting people's records are performed securely and efficiently without compromising the system's performance.
Document the code and installation and configuration procedures to facilitate maintenance and future use of the system.


----
Architecture Description

I chose to use DDD due to its ability to model and organize the problem domain clearly and cohesively. With DDD, we can map the central concepts of our project's domain, such as Person and User, into entities, aggregates, services, and value objects, facilitating the understanding and maintenance of the code over time.

The Command pattern was chosen to separate actions that need to be performed into distinct commands. Each command represents a single action in the system, such as "Create Person" or "Update User". This promotes modularity.

MongoDB was a suitable choice for this project due to its flexibility and ability to store data in the JSON document format, which aligns well with the object-oriented structure in an application.

----

How run the project

In the project root, open CMD and run this command to run mongoDB -> docker-compose up --build. 
After finishing the execution/installation, open the build folder and run the bat file

In the root project there is a postman file, to help you run some tests, or you can use swagger. Remembering, you need to add bearer token to reach some endpoints