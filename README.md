# Office Attendance API

The Office Attendance API is a tool for announcing employee attendance in an hybrid office setting. This API allows you to track and record employee check-ins, generate attendance reports to have an idea when that person that you want to see in person will be going to the office.

## Features

- Employee check-in tracking
- Attendance report generation
- Administrative tasks (e.g., adding/removing employees)
- Integration with other office management systems

## Installation

To use the Office Attendance API, follow these steps:

1. Clone the repository: `git clone https://github.com/your-username/OfficeAttendanceAPI.git`
2. Spin up the container `docker compose up --build`

## Usage

Once the container is up and running, you can start making API requests to manage employee attendance. Here are some examples:

- Retrieve attendance report for a particular day:
    ```
    GET /attendance/reports/attendance/2024-05-25
    ```

- Retrieve attendance report for the current week:
    ```
    GET /attendance/reports/week
    ```

## License

This project is licensed under the [MIT License](/LICENSE).
