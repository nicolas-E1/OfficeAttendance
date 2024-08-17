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
                    tags "WebAPI"
                }
                infrastructure = container "Infrastructure Layer" "Handles data persistence and external system interactions." ".NET, EF Core and C#" {
                    tags "Infrastructure"
                }
                application = container "Application Layer" "Handles the business logic and application flow." ".NET and C#" {
                    tags "Application"
                }
                core = container "Core Layer" "Defines the core business models and domain logic." ".NET and C#" {
                    tags "Core"
                }
                tests = container "Tests" "Contains all the tests for the Office Attendance System." ".NET, xUnit and C#" {
                    tags "Tests"
                }
                db = container "Database" "Stores attendance, user registration information, access logs, etc." "PostgreSQL" "Database" {
                    tags "Database"
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
    }

    views {
        systemContext officeAttendance "SystemContext" {
            title "System Context diagram for OfficeAttendance System"
            description "The system context diagram for the OfficeAttendance System."
            include *
            autoLayout
            animation {
                user officeAttendance
                admin officeAttendance
            }
        }
        
        container officeAttendance "Container" {
            title "Container diagram for OfficeAttendance System"
            include *
            autoLayout
            animation {
                admin webApp
                user webApp mobileApp
                webApp mobileApp api
                api application 
                application infrastructure core
                infrastructure db
                tests api application core
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
                background #156961
                color #FCD75D
            }
            element "Container" {
                shape roundedBox
                background #156961
                color #FCD75D
            }
            element "MobileApp" {
                shape MobileDeviceLandscape
            }
            element "WebApp" {
                shape WebBrowser
            }
            element "Database" {
                shape cylinder
            }
        }
    }
}
