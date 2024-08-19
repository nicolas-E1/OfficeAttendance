# Software System Documentation

## Summary

The **Office Attendance** is a tool for announcing employee attendance in an hybrid office setting. This API allows you to track and record employee check-ins, generate attendance reports to have an idea when that person that you want to see in person will be going to the office.
The project is built with a focus on Clean Architecture principles, ensuring separation of concerns, testability, and maintainability. It leverages .NET technologies and follows industry best practices such as Test-Driven Development (TDD).

## Philosophy

This project is built on the foundations of Clean Architecture, Test-Driven Development (TDD), and a strong commitment to best practices in software development. Writing clean code and adhering to these principles not only ensures high-quality work but also makes life easier for your future self and colleagues.

> 🌱  
> I believe that if you want your future self and your colleagues to appreciate your work, writing clean code and following best practices will set you on the right path.

### Clean Architecture

Clean Architecture, as advocated by Robert C. Martin (Uncle Bob), emphasizes the importance of separating the various layers of a software system to improve its modularity and scalability. The core of the application (domain entities, use cases) is kept independent of external frameworks, UI, or databases. This approach allows the application to be more adaptable to changes and easier to test.
If you want to learn more about Clean Architecture, check out my blog post where I cover this topic.

> 💡  
> If you want to learn more about Clean Architecture, check out my **[blog post](https://nicolasbracigliano.com/bit-acora/clean-architecture-building-software-that-lasts/)** where I cover this topic.


### Test-Driven Development (TDD)

TDD is a development methodology where tests are written before the code that fulfils the requirements. This ensures that the software design is guided by tests, resulting in higher code quality, fewer bugs, and a clearer understanding of the software's behaviour.

## Architecture

The project is structured according to Clean Architecture principles, dividing the codebase into distinct layers:

- **Core Layer**: Contains the business logic and domain entities, which are agnostic of external systems.
- **Application Layer**: Coordinates the application's use cases, interacting with the core layer and external interfaces.
- **Infrastructure Layer**: Implements the details of data persistence, repositories, and external services.
- **API Layer**: Provides the entry point for the application, exposing the necessary endpoints and managing user interactions.

This separation ensures that each layer has a clear responsibility, making the system more maintainable and scalable.

## Project Structure

The **OfficeAttendance** solution is composed of five distinct projects, each serving a specific role within the overall architecture. This separation reinforces the principles of Clean Architecture by ensuring clear boundaries and preventing illegal dependencies between layers.

### Projects Overview

- **`OfficeAttendance.Application`**: This project contains the application logic, including Data Transfer Objects (DTOs) and use cases. It is responsible for coordinating the application workflows and interacting with the core domain models while remaining agnostic of the underlying infrastructure and external interfaces.

- **`OfficeAttendance.Core`**: The core domain models and interfaces are housed in this project. It defines the essential business rules and entities that are central to the application. This project is entirely independent of any external frameworks or libraries, adhering to the Clean Architecture principle of keeping the core pure and isolated.

- **`OfficeAttendance.Infrastructure`**: This project is responsible for data persistence and the implementation of repository patterns. It provides the necessary infrastructure to interact with external systems like databases, ensuring that the core and application layers remain decoupled from these concerns.

- **`OfficeAttendance.Tests`**: This project contains unit tests that validate the behaviour of the application. By isolating the tests in a separate project, the codebase remains clean and the tests can be maintained independently from the application code.

- **`OfficeAttendance.WebAPI`**: Serving as the main entry point for the application, this project includes the WebAPI controllers and configuration files. It exposes the necessary endpoints for external interactions and manages the HTTP request/response lifecycle.

## Key Features

- User Management: Users can create and manage their accounts.
- Attendance Tracking: Users can indicate when they plan to be in the office and check how going to the office on a specific day or week.
- Admin Tools: Administrators can manage user roles, attendance records, and system settings.
- Multi-Platform Support: The system includes a web application, a mobile application, and a RESTful API.
