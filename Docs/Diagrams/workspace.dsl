workspace {
    model {
        user = person "User" "An Office Attendance user with an active account." "User"
        admin = person "Admin" "An Office Attendance user with admin privileges." "Admin"

        group "Office Attendance" {
            officeAttendance = softwareSystem "Office Attendance" "Enables users to see who’s going to the office and share their own plans." {
                webApp = container "Web Application" "Allows users to see who’s going to the office and share their own plans." "Typescript and React" "Web Browser" {
                    tags "WebApp"
                }

                mobileApp = container "Mobile Application" "Allows users to see who’s going to the office and share their own plans." "React Native" "Mobile Device" {
                    tags "MobileApp"
                }

                api = container "Web API" "Exposes RESTful API endpoints for managing and viewing attendance records." ".NET and C#" {
                    attendanceController = component "AttendanceController" "Handles HTTP requests related to attendance management." ".NET and C#"
                    tags "WebAPI"
                }

                infrastructure = container "Infrastructure Layer" "Handles data persistence and external system interactions." ".NET, EF Core and C#" {
                    attendanceRepository = component "AttendanceRepository" "Handles data operations for attendance records." "EF Core"
                    employeeRepository = component "EmployeeRepository" "Handles data operations for employee records." "EF Core"
                    appDbContext = component "AppDbContext" "Manages the database connection and data model." "EF Core"
                    appDbContextFactory = component "AppDbContextFactory" "Creates instances of the AppDbContext." "EF Core"
                    tags "Infrastructure"
                }

                application = container "Application Layer" "Handles the business logic and application flow." ".NET and C#" {
                    getAttendanceByDayUseCase = component "GetAttendanceByDayUseCase" "Handles the logic for retrieving attendance records for a specific day, ensuring correct business rules are applied." ".NET and C#"
                    getAttendanceByWeekUseCase = component "GetAttendanceByWeekUseCase" "Handles the logic for retrieving attendance records for the current week, ensuring correct business rules are applied." ".NET and C#"
                    tags "Application"
                }

                core = container "Core Layer" "Defines the core business models and domain logic." ".NET and C#" {
                    attendance = component "Attendance" "The attendance entity" ".NET and C#"
                    employee = component "Employee" "The employee entity" ".NET and C#"
                    errorHandling = component "ErrorHandling" "Handles all domain-specific exceptions." ".NET and C#"
                    tags "Core"
                }

                db = container "Database" "Stores attendance, user registration information, access logs, etc." "PostgreSQL" "Database" {
                    tags "Database"
                }

                tests = container "Tests" "Contains all the tests for the Office Attendance System." ".NET, xUnit and C#" {
                    unitTests = component "Unit Tests" "Validates individual components within the core and application layers." ".NET, xUnit"
                    integrationTests = component "Integration Tests" "Validates interactions between multiple components, particularly across layers." ".NET, xUnit"
                    endToEndTests = component "End-to-End Tests" "Validates the complete flow of the system from API to database." ".NET, xUnit"
                    tags "Tests"
                }
            }
        }
        
        # Relationships between people and software systems
        admin -> officeAttendance "Manages users and attendance."
        user -> officeAttendance "Checks who’s going to the office and shares their own plans."
        # Relationships between people and containers
        admin -> webApp "Manages users and attendance using the web app."
        user -> webApp "Checks who’s going to the office and shares their own plans using the web app."
        user -> mobileApp "Checks who’s going to the office and shares their own plans using the mobile app."
        webApp -> api "Makes API requests to"
        mobileApp -> api "Makes API requests to"
        api -> application "Invokes use cases via"
        application -> infrastructure "Persists and retrieves data via"
        infrastructure -> db "Reads from and writes to"
        application -> core "Interacts with code domain via"
        tests -> api "Validates endpoints via"
        tests -> application "Validates use cases via"
        tests -> core "Validates domain logic via"
        # Relationships between containers
        // API
        webApp -> attendanceController "Sends HTTP requests to"
        mobileApp -> attendanceController "Sends HTTP requests to"
        // Application
        api -> getAttendanceByDayUseCase "Executes use case logic via"
        api -> getAttendanceByWeekUseCase "Executes use case logic via"
        // Infrastructure
        application -> attendanceRepository "Reads and writes data via"
        application -> employeeRepository "Reads and writes data via"
        attendanceRepository -> appDbContext "Uses for data operations"
        employeeRepository -> appDbContext "Uses for data operations"
        appDbContext -> appDbContextFactory "Uses"
        // Core
        attendanceRepository -> attendance "Uses"
        employeeRepository -> employee "Uses"
        getAttendanceByDayUseCase -> attendance "Uses"
        getAttendanceByWeekUseCase -> attendance "Uses"
        attendance -> errorHandling "Throws"
        employee -> errorHandling "Throws"
        // Tests
        unitTests -> api "Validates"
        integrationTests -> api "Validates"
        endToEndTests -> api "Validates"
        unitTests -> application "Validates"
        unitTests -> core "Validates"
    }

    views {
        systemContext officeAttendance "SystemContext" {
            title "System Context diagram for OfficeAttendance System"
            description "The system context diagram for the OfficeAttendance System."
            include *
            autoLayout
            animation {
                officeAttendance
                user officeAttendance
                admin officeAttendance
            }
        }
        
        container officeAttendance "Container" {
            title "Container diagram for OfficeAttendance System"
            description "Shows the main containers and their interactions to provide a high-level overview."
            include *
            exclude "Tests"
            autoLayout
            animation {
                admin user webApp mobileApp
                webApp mobileApp api
                api application 
                application core
                application infrastructure db
            }
        }

        container officeAttendance "Container-Tests" {
            title "Container diagram for the Tests container"
            description "Shows the tests container and its interactions to provide a high-level overview."
            include *
            autoLayout
            animation {
                user admin webApp mobileApp
                tests api
                tests application infrastructure db
                tests core
            }
        }

        component api "WebAPI-Component" {
            title "Component diagram for the WebAPI container"
            description "Shows the components of the WebAPI container and their interactions."
            include "->element.parent==api->"
            autoLayout
            animation {
                attendanceController
                webApp attendanceController
                mobileApp attendanceController
            }
        }

        component application "Application-Component" {
            title "Component diagram for the Application container"
            description "Shows the components of the Application container and their interactions."
            include "->element.parent==application->"
            autoLayout
            animation {
                getAttendanceByDayUseCase getAttendanceByWeekUseCase
                api getAttendanceByDayUseCase getAttendanceByWeekUseCase
                core getAttendanceByDayUseCase getAttendanceByWeekUseCase
            }
        }

        component infrastructure "Infrastructure-Component" {
            title "Component diagram for the Infrastructure container"
            description "Shows the components of the Infrastructure container and their interactions."
            include "->element.parent==infrastructure->"
            autoLayout
            animation {
                attendanceRepository employeeRepository
                application
                appDbContext
                appDbContextFactory
            }
        }

        component core "Core-Component" {
            title "Component diagram for the Core container"
            description "Shows the components of the Core container and their interactions."
            include "->element.parent==core->"
            autoLayout
            animation {
                employee attendance
                infrastructure application
                errorHandling
            }
        }

        component tests "Tests-Component" {
            title "Component diagram for the Tests container"
            description "Shows the components of the Tests container and their interactions."
            include "->element.parent==tests->"
            autoLayout
            animation {
                unitTests
                api application core
                integrationTests api
                endToEndTests api
            }
        }

        styles {
            element "Software System" {
                shape roundedBox
                background #156961
                color #FCD75D
            }
            element "Person" {
                shape person
                background #4A4A4A
                color #FCD75D
            }
            element "Container" {
                shape roundedBox
                background #156961
                color #FCD75D
            }
            element "Component" {
                shape component
                background #FCD75D
                color #156961
            }
            element "MobileApp" {
                shape MobileDeviceLandscape
                background #93C5A5
                color #4A4A4A
            }
            element "WebApp" {
                shape WebBrowser
                background #93C5A5
                color #4A4A4A
            }
            element "Database" {
                shape cylinder
                background #1F8A70
            }
        }
    }
}
