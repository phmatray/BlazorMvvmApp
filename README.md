# Blazor MVVM Todo App

Welcome to the **Blazor MVVM Todo App**, a modern, scalable, and maintainable web application built using the [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) framework and adhering to the [Model-View-ViewModel (MVVM)](https://en.wikipedia.org/wiki/Model–view–viewmodel) architectural pattern. This application leverages the [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet/tree/main/src/CommunityToolkit.Mvvm) package to streamline MVVM implementations, reducing boilerplate code and enhancing developer productivity.

## 📋 Table of Contents

<!-- TOC -->
* [Blazor MVVM Todo App](#blazor-mvvm-todo-app)
  * [📋 Table of Contents](#-table-of-contents)
  * [🌟 Features](#-features)
  * [🛠 Technologies Used](#-technologies-used)
  * [🗂 Project Structure](#-project-structure)
    * [📄 File Descriptions](#-file-descriptions)
  * [🚀 Getting Started](#-getting-started)
    * [📝 Prerequisites](#-prerequisites)
    * [📥 Installation](#-installation)
    * [▶️ Running the Application](#-running-the-application)
  * [🎮 Usage](#-usage)
    * [**Todo Page (`/todos` or `/`)**](#todo-page-todos-or-)
    * [**Stats Page (`/stats`)**](#stats-page-stats)
  * [🏛 Architecture Overview](#-architecture-overview)
    * [**Models**](#models)
    * [**ViewModels**](#viewmodels)
    * [**Services**](#services)
    * [**Components**](#components)
  * [🤝 Contributing](#-contributing)
    * [📌 Guidelines](#-guidelines)
  * [📄 License](#-license)
  * [📞 Contact](#-contact)
<!-- TOC -->

## 🌟 Features

- **Todo Management:**
    - **Add Todos:** Easily add new todo items with a title.
    - **Toggle Completion:** Mark todos as complete or incomplete.
    - **Load Todos:** Asynchronously load additional todos.

- **Statistics:**
    - **Total Todos:** View the total count of todos, including initial and loaded items.

- **MVVM Architecture:**
    - **Decoupled ViewModels:** `TodoViewModel` and `StatsViewModel` operate independently, interacting through a shared data service.
    - **Observable Properties:** Automatic property change notifications ensure seamless UI updates.

- **CommunityToolkit.Mvvm Integration:**
    - **Simplified Commands:** Utilize `[RelayCommand]` and `[ObservableProperty]` attributes to reduce boilerplate.
    - **Shared Data Service:** A centralized `TodoService` manages todo items, promoting a single source of truth.

## 🛠 Technologies Used

- **Frameworks & Libraries:**
    - [**Blazor**](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor): A framework for building interactive client-side web UI with .NET.
    - [**CommunityToolkit.Mvvm**](https://github.com/CommunityToolkit/dotnet/tree/main/src/CommunityToolkit.Mvvm): Provides utilities and components to implement the MVVM pattern effectively.

- **Languages:**
    - **C#**: Primary programming language.
    - **Razor**: Markup syntax for building Blazor components.

- **Tools:**
    - **.NET SDK**: For building and running the application.
    - **Visual Studio / Visual Studio Code**: Recommended IDEs for development.

## 🗂 Project Structure

```
BlazorMvvmApp/
├── Components/
│   │   App.razor
│   └── ViewModelComponentBase.cs
├── Features/
│   ├── Stats/
│   │   ├── StatsViewModel.cs
│   │   └── Stats.razor
│   └── Todos/
│       ├── TodoItem.cs
│       ├── TodoViewModel.cs
│       └── Todos.razor
├── Services/
│   ├── ITodoService.cs
│   └── TodoService.cs
├── DependencyInjections.cs
└── Program.cs
```

### 📄 File Descriptions

- **Components/App.razor:**
    - Root component of the Blazor application, configuring routing.

- **Components/ViewModelComponentBase.cs:**
    - A base Blazor component that handles `PropertyChanged` events from ViewModels to trigger UI updates.

- **Features/Stats/StatsViewModel.cs:**
    - ViewModel responsible for tracking and updating the total count of todos.

- **Features/Stats/Stats.razor:**
    - Blazor component that displays statistics, specifically the total number of todos.

- **Features/Todos/TodoItem.cs:**
    - Model representing a single todo item with properties like `Title` and `IsComplete`.

- **Features/Todos/TodoViewModel.cs:**
    - ViewModel managing todo items, including adding new todos and loading additional ones asynchronously.

- **Features/Todos/Todos.razor:**
    - Blazor component for displaying and interacting with the todo list.

- **Services/ITodoService.cs & Services/TodoService.cs:**
    - Shared data service managing the collection of todos, ensuring a single source of truth across ViewModels.

- **DependencyInjections.cs:**
    - Extension methods for registering services and ViewModels with the Dependency Injection (DI) container.

- **Program.cs:**
    - Entry point of the Blazor application, configuring services and building the host.

## 🚀 Getting Started

Follow these instructions to set up and run the project locally.

### 📝 Prerequisites

- **.NET SDK 9.0 or later:**  
  Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed.

- **IDE:**
    - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or later with the ASP.NET and web development workload.
    - Alternatively, [Visual Studio Code](https://code.visualstudio.com/) with the C# extension.
    - Or even better, [JetBrains Rider](https://www.jetbrains.com/rider/).

### 📥 Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/phmatray/BlazorMvvmApp.git
   ```

2. **Navigate to the Project Directory:**

   ```bash
   cd BlazorMvvmApp
   ```

3. **Restore Dependencies:**

   ```bash
   dotnet restore
   ```

4. **Install CommunityToolkit.Mvvm Package:**

   The `CommunityToolkit.Mvvm` package is already referenced in the project. If not, install it via the CLI:

   ```bash
   dotnet add package CommunityToolkit.Mvvm
   ```

### ▶️ Running the Application

1. **Build the Project:**

   ```bash
   dotnet build
   ```

2. **Run the Application:**

   ```bash
   dotnet run
   ```

3. **Access the Application:**

   Open your browser and navigate to `https://localhost:5001` or the URL specified in the terminal output.

## 🎮 Usage

### **Todo Page (`/todos` or `/`)**

- **Add a Todo:**
    1. Enter a title in the input field.
    2. Click the "Add" button.
    3. The new todo appears in the list.

- **Toggle Completion:**
    - Click the checkbox next to a todo to mark it as complete or incomplete.

- **Load More Todos:**
    - Click the "Load Todos" button to asynchronously load additional todos.

### **Stats Page (`/stats`)**

- **View Total Todos:**
    - Displays the total count of todos, including both initial and loaded items.

## 🏛 Architecture Overview

The application follows the MVVM pattern, separating concerns into Models, ViewModels, and Views (Blazor components).

### **Models**

- **TodoItem:**
    - Represents the data structure for a todo item.

### **ViewModels**

- **TodoViewModel:**
    - Manages the collection of todos, handles adding new todos, and loading additional todos asynchronously.

- **StatsViewModel:**
    - Monitors the total count of todos by subscribing to changes in the `ITodoService`.

### **Services**

- **ITodoService & TodoService:**
    - A shared service that manages the `ObservableCollection<TodoItem>`. Acts as a single source of truth for the todo items, allowing ViewModels to interact with it without direct dependencies on each other.

### **Components**

- **ViewModelComponentBase:**
    - A base component that subscribes to `PropertyChanged` events from ViewModels to trigger UI updates (`StateHasChanged`).

- **Todos.razor:**
    - UI component for displaying and interacting with the todo list.

- **Stats.razor:**
    - UI component for displaying statistics related to todos.

## 🤝 Contributing

Contributions are welcome! Follow these steps to contribute to the project:

1. **Fork the Repository:**

   Click the "Fork" button at the top-right corner of the repository page.

2. **Clone Your Fork:**

   ```bash
   git clone https://github.com/phmatray/BlazorMvvmApp.git
   cd BlazorMvvmApp
   ```

3. **Create a Feature Branch:**

   ```bash
   git checkout -b feature/YourFeatureName
   ```

4. **Commit Your Changes:**

   ```bash
   git commit -m "Add your detailed description of changes"
   ```

5. **Push to Your Fork:**

   ```bash
   git push origin feature/YourFeatureName
   ```

6. **Create a Pull Request:**

   Navigate to your fork on GitHub and click the "Compare & pull request" button.

### 📌 Guidelines

- **Code Quality:**  
  Ensure your code follows best practices and is well-documented.

- **Testing:**  
  Include unit tests for new features or significant changes.

- **Commit Messages:**  
  Write clear and descriptive commit messages.

## 📄 License

This project is licensed under the [MIT License](LICENSE).  
See the [LICENSE](LICENSE) file for details.

---

## 📞 Contact

For any questions, suggestions, or support, please open an issue in the [GitHub repository](https://github.com/phmatray/BlazorMvvmApp/issues).

---

Thank you for checking out the **Blazor MVVM Todo App**! We hope it serves as a solid foundation for your Blazor projects using the MVVM pattern.