# Assessment 2: Customer Information Management Prototype (Sprint 2)

**Student Name:** Manish Ray  
**Student ID:** 20231503  
**Course:** IT7742 Advanced Programming  
**Assessment:** Assessment 2 - Sprint 2 (30% Weighting)  

---

## 1. Banking System Overview & Sprint 2 Features

This repository contains the **Sprint 2** prototype for the Banking System Case Study. Sprint 2 extends the financial foundation built in Sprint 1 by implementing structural architectural patterns, custom exception handling, and automated unit testing.

### Key Features Added in Sprint 2:
- **Model-View-Controller (MVC) Architecture**: Decouples customer management from the user interface. A dedicated `CustomerController` handles customer state, customer creation, detail updates, deletion, and customer seeding (`C-2026-001` and `C-2026-002`).
- **Separate Customer Management GUI View**: Customer management is hosted in a dedicated form (`CustomerManagementForm`), accessible via the **"MANAGE CUSTOMERS"** button on the main dashboard. Supports Visual Studio WinForms Drag-and-Drop Designer editing.
- **Custom Exception Handling**: Custom exception hierarchy (`BankingException` base and `InsufficientFundsException` subclass) enforcing tailored error messages per account type (Everyday, Investment fee deductions, and Omni overdraft limits).
- **Hardened Model Encapsulation**: Balance mutations are locked behind a `private` field with a `protected AdjustBalance` mutator in `Account.cs`. Account list composition is explicitly managed by concrete subclasses (`Customer` and `BankStaff`).
- **Automated MSTest Unit Test Suite**: Comprehensive unit testing covering happy paths, failure exception paths, fee discounts for bank staff, interest calculations, and controller CRUD operations.

---

## 2. Prerequisites & Opening the Solution in Visual Studio

### Prerequisites / Dependencies Required:
- **Visual Studio 2022** (version 17.0 or later) with the **.NET Desktop Development** workload installed.
- **.NET 10.0 SDK** (or .NET 8.0/9.0 SDK matching installed .NET environment).
- No external third-party NuGet packages are required (uses standard built-in .NET SDK and MSTest framework).

### Step-by-Step Instructions to Open the Solution:
1. Launch **Visual Studio 2022**.
2. Click **Open a project or solution**.
3. Navigate to the repository directory:  
   `Assignment2\Assignment2\`
4. Select and open **`20231503_ManishRay_Assignment2.sln`** (or `20231503_ManishRay_Assignment2.slnx`).
5. Wait for Visual Studio to load the solution projects:
   - `20231503_ManishRay_Assignment2` (Main WinForms GUI Application)
   - `20231503_ManishRay_Assignment2.Tests` (MSTest Unit Test Project)

---

## 3. Explicit Instructions to Run the GUI Application

### Running via Visual Studio:
1. Set `20231503_ManishRay_Assignment2` as the **Startup Project** (right-click the project in **Solution Explorer** $\rightarrow$ select **Set as Startup Project**).
2. Press **`F5`** (or click the green **Play / Start** button in the top toolbar marked `20231503_ManishRay_Assignment2`).
3. The main Banking Dashboard window (`Form1`) will launch:
   - Select an **Active User** from the top right dropdown.
   - Switch between **Everyday Account**, **Investment Account**, and **Omni Account** tabs.
   - Enter an amount to test **Deposit**, **Withdraw**, or **Calculate Interest**.
   - Click **"MANAGE CUSTOMERS"** in the top navigation bar to launch the modal form (`CustomerManagementForm`) for adding, updating, or deleting customer records.

### Running via Command Line (Terminal / PowerShell):
From the repository root folder, run:
```bash
dotnet run --project "20231503_ManishRay_Assignment2/20231503_ManishRay_Assignment2.csproj"
```

---

## 4. Locating, Building, and Executing the Unit Test Suite

### Executing via Visual Studio Test Explorer:
1. In Visual Studio, open the top menu bar and select **Test** $\rightarrow$ **Test Explorer** (or press `Ctrl + E, T`).
2. The **Test Explorer** panel will open on the side.
3. Click the **Build** menu $\rightarrow$ select **Build Solution** (or press `Ctrl + Shift + B`) to ensure test binaries are built.
4. In **Test Explorer**, click the **Run All Tests in View** button (or press `Ctrl + R, A`).
5. All 17 test cases under `20231503_ManishRay_Assignment2.Tests` will execute, displaying green checkmarks for passed tests.

### Executing via Command Line:
From the repository root folder, run:
```bash
dotnet test "20231503_ManishRay_Assignment2.Tests/20231503_ManishRay_Assignment2.Tests.csproj"
```

### Test Suite Execution Summary:
- **Total Tests**: 17
- **Passed**: 17
- **Failed**: 0

---

## Project File Structure

```text
Assignment2/
├── 20231503_ManishRay_Assignment2/             # Main WinForms GUI Project
│   ├── Controllers/
│   │   └── CustomerController.cs                # MVC Controller
│   ├── Exceptions/
│   │   ├── BankingException.cs                  # Base Custom Exception
│   │   └── InsufficientFundsException.cs        # Insufficient Funds Exception
│   ├── Models/
│   │   ├── Account.cs                           # Abstract Account Base
│   │   ├── EverydayAccount.cs                   # Everyday Account
│   │   ├── InvestmentAccount.cs                 # Investment Account
│   │   ├── OmniAccount.cs                       # Omni Account
│   │   ├── User.cs                              # Abstract User Base
│   │   ├── Customer.cs                          # Customer Model
│   │   └── BankStaff.cs                         # Bank Staff Model
│   ├── Form1.cs & Form1.Designer.cs             # Main Dashboard View
│   ├── CustomerManagementForm.cs & Designer.cs  # Customer Management Modal View
│   └── Program.cs                               # Entry Point
│
├── 20231503_ManishRay_Assignment2.Tests/       # Unit Test Project
│   ├── EverydayAccountTests.cs                  # Everyday Account Tests
│   ├── InvestmentAccountTests.cs                # Investment Account Tests
│   ├── OmniAccountTests.cs                      # Omni Account Tests
│   └── CustomerControllerTests.cs               # Controller CRUD Tests
│
├── AdvancedProgrammingUMLDiagrams.drawio        # Draw.io UML Class Diagram
└── README.md                                    # Comprehensive Documentation
```