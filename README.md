# AniX

## Overview

AniX is a centralized platform designed for anime enthusiasts. It allows users to explore trailers, read detailed descriptions, and share reviews about various anime series. The platform aims to provide a comprehensive experience for anime fans, offering curated content and a community-driven review system.

## Links

[AniX Repository on Fontys GIT](https://git.fhict.nl/I499309/anix)

[Figma Main Page Prototype](https://www.figma.com/proto/8dWwVGXPIbtTFzVyaEI6DY/Untitled?type=design&node-id=6-13&t=0UU47rMbw2KYSWoF-1&scaling=min-zoom&page-id=0%3A1&starting-point-node-id=6%3A13&mode=design)

## Main Page Prototype

![Main Page Prototype](https://git.fhict.nl/I499309/anix/-/raw/main/Documentation/Screenshots/main-page.png)

## Anime Page Prototype

![Anime Page Prototype](https://git.fhict.nl/I499309/anix/-/raw/main/Documentation/Screenshots/anime-page.png)

## Features

- **Browse Anime Trailers**: Discover new and trending anime through trailers.
- **Read Detailed Descriptions**: Get in-depth information about each anime series.
- **User Reviews**: Share and read reviews to get community insights.
- **Admin Dashboard**: A separate Windows Forms application for administrative tasks.

## UML

- The UML diagram serves as the foundational architecture for the AniX project. It outlines the classes, methods, and relationships that will be implemented in both the AniXLib class library and the WinForms Application.
- The diagram will be updated as the project evolves to reflect any changes or additions to the architecture.

[AniX UML Diagram](https://svgshare.com/i/yCx.svg)

## Sitemap

1. **Main page**
    - Featured Anime
    - Anime List
    - Trending Anime
    - Anime Updates
2. **Filtered Page**
    - Filtered content
    - Recommendations
3. **Content Page**
    - Trailer
    - Description
    - Reviews
    - Recommendations
4. **Account Page**
    - Dashboard
5. **About Page**
    - Details about the project
6. **Admin Dashboard (Windows Forms App)**
    - CRUD Operations for Anime
    - Review Management Section

## Prerequisites

- **.NET Core SDK**: Required for building and running the .NET Core backend.
- **MSSQL Database**: The application uses Microsoft SQL Server for data persistence.
- **Node.js**: Required for frontend development.
- **Dependency Injection**: Used for better modularity and testability. Integrated into .NET Core.
- **Interface-based Programming**: Utilized for more maintainable and extensible code.
- **SOLID Principles**: The codebase adheres to SOLID principles for better maintainability and readability.

## Additional Software

- **PlantUML**: Optional, used for generating UML diagrams for the project.
