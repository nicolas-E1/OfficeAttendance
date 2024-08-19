# 1. Record architecture decisions

Date: 2024-06-28

## Status

Accepted

## Context

We need to decide on the architecture style for the new Office Attendance system. The system will need to scale with an increasing number of users and allow independent development of different services.

## Decision

We will base the architecture of the system in the Clean Architecture paradigm. This will facilitate the maintenance of the project and collaboration.

## Consequences

- The different layers of the project will have clear boundaries.
- Some extra configuration files will need to added.
