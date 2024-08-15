workspace {

    model {
        user = person "User" "An Office Attendance user with an active account."
        softwareSystem = softwareSystem "Office Attendance" "Enables users to see who’s going to the office and share their own plans."

        user -> softwareSystem "Uses"
    }

    views {
        systemContext softwareSystem "SystemContext" {
            include *
            autoLayout
        }
        
        container softwareSystem "Container" {
            include *
            autoLayout
        }

        styles {
            element "Software System" {
                background #156961
                color #FCD75D
            }
            element "Person" {
                shape person
                background #156961
                color #FCD75D
            }
        }
    }
}
