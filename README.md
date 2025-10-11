[![.NET MAUI Build](https://github.com/supunsarachitha/SmartShop-FrontEnd-MAUI/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/supunsarachitha/SmartShop-FrontEnd-MAUI/actions/workflows/dotnet-desktop.yml)
# SmartShop Front End with .NET MAUI

SmartShop.MAUI is a cross-platform application built with .NET MAUI, following the MVVM architecture for maintainability and scalability. It provides modular services for API communication and clean ViewModels for robust data binding and testability.

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Configuration](#configuration)
- [Getting Started](#getting-started)
- [Directory Structure](#directory-structure)
- [Usage](#usage)
- [Contributing](#contributing)
- [Troubleshooting](#troubleshooting)
- [License](#license)
- [Contact](#contact)

---

## Features

- **MVVM Architecture**: Ensures a clear separation of concerns for maintainable code.
- **Modular Services**: API endpoints are managed by easily extendable service classes.
- **ViewModels**: Designed for clean data binding, testability, and UI logic separation.
- **Multi-Platform Support**: Runs on Android, iOS, Windows, and macOS.
- **Community Toolkit**: Integrated for enhanced UI and utility features.
- **Authentication Service**: Handles user login with error reporting and validation.
- **Asset Management**: Supports deploying raw assets for use in the application.
- **Font Customization**: Register custom fonts for improved user interface.
- **Dependency Injection**: Uses DI for services and view models, simplifying testing and development.

---

## Architecture

The project is built using the Model-View-ViewModel (MVVM) pattern, which separates UI, business logic, and data management:
- **Models**: Define data structures and API response/request types.
- **Views**: XAML pages for platform-specific user interfaces.
- **ViewModels**: Manage UI logic and interact with services.
- **Services**: Handle API calls, authentication, and reusable logic.

Dependency injection is used for modularity and testability. The main configuration and service registration is in `MauiProgram.cs`.

---

## Configuration

- **API Base URL**: Configured in `SmartShop.MAUI/Helpers/AppConstants.cs` as `http://localhost:5218`.
- **Fonts**: Custom fonts (OpenSans) are registered in `SmartShop.MAUI/MauiProgram.cs`.
- **Dependency Injection**: Services such as `ApiService` and `AuthService` are registered for use across the app.
- **Assets**: Raw assets can be added to `SmartShop.MAUI/Resources/Raw` and are automatically deployed with the app package.

---

## Getting Started

1. **Clone the repository:**
   ```sh
   git clone https://github.com/supunsarachitha/SmartShop-FrontEnd-MAUI.git
   ```
2. **Open the solution:**
   - Use Visual Studio 2022 or later with .NET MAUI workloads installed.

3. **Restore dependencies:**
   - Restore NuGet packages.

4. **Build and run:**
   - Build the solution and run the app on your preferred target platform (Android, iOS, Windows, or macOS).

---

## Directory Structure

- `SmartShop.MAUI/Helpers`: Application constants and utility classes.
- `SmartShop.MAUI/Services`: Modular API and authentication services.
- `SmartShop.MAUI/ViewModels`: MVVM ViewModels for data binding.
- `SmartShop.MAUI/Models`: Data models for requests and responses.
- `SmartShop.MAUI/Platforms`: Platform-specific implementations.
- `SmartShop.MAUI/Resources/Raw`: Raw assets for deployment.
- `SmartShop.MAUI/Views`: XAML pages for UI.

---

## Usage

- **Login Functionality**: Use the login page to authenticate with your backend API. The login logic validates input and handles errors gracefully.
- **API Communication**: All API requests are handled by the `ApiService` and `AuthService` classes, with configuration and error management.
- **Extending Functionality**: To add new pages or services, follow the existing MVVM structure for maintainability.

---

## Contributing

- Fork the repository, create a feature branch, and open a pull request with a clear description of your changes.
- Keep code style consistent and include unit tests where appropriate.
- Document any major changes in the PR description.
- For bugs and feature requests, please use GitHub Issues.

---

## Troubleshooting

- Ensure you are using Visual Studio 2022 or later with the latest .NET MAUI workloads.
- If API requests fail, verify that the backend is running and the `ApiBaseUrl` is correctly configured.
- For asset issues, make sure files are placed in the correct `Resources/Raw` directory with proper build actions.
- Consult the documentation and code comments for further guidance.

---

## License

This project is licensed under the [Apache License 2.0](https://github.com/supunsarachitha/SmartShop-FrontEnd-MAUI/blob/main/LICENSE).

---

## Contact

For questions, suggestions, or support, please open an issue on the [GitHub repository](https://github.com/supunsarachitha/SmartShop-FrontEnd-MAUI) or contact the maintainer via [GitHub profile](https://github.com/supunsarachitha).
