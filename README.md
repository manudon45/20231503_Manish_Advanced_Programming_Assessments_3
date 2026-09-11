# Assessment 3: Bank Account Management System (Sprint 3)

**Student Name:** Manish Ray
**Student ID:** 20231503
**Course:** IT7742 Advanced Programming
**Assessment:** Assessment 3 - Sprint 3 (Final Delivery)

---

## 1. Banking System Overview & Sprint 3 Scope

This repository contains the **Sprint 3** delivery of the Banking System Case Study. Sprint 3
integrates the Sprint 1 domain logic and the Sprint 2 MVC structure into the final application and
adds intra-account transfers, a dynamic one-to-many account model, and JSON data persistence.

### Architecture at a glance

- **Model** - `User` (abstract) with `Customer` / `BankStaff`; `Account` (abstract) with
  `EverydayAccount` / `InvestmentAccount` / `OmniAccount`. A user owns `1..*` accounts.
- **Controller** - `BankController` is the facade the forms hold; it owns one `BankRepository`
  and exposes `.Customers`, `.Accounts` and `.Transfers`, plus `LoadData()` / `SaveData()`.
- **View** - `Form1`, `CustomerManagementForm`, `AddAccountForm` and `TransferForm`, all
  inheriting `BaseForm` for consistent branding.
- **Persistence** - `JsonPersistenceService` writes a `BankDataFile` envelope to `bank_data.json`
  using `System.Text.Json` with `[JsonPolymorphic]` type discriminators.

The full class-by-class breakdown (attributes, method signatures, and what changed since Sprint 2)
is in the Class Extensions Report; the UML class diagram and the persistence sequence diagrams are
in the technical report. Both live one level up - see section 5.

---

## 2. Prerequisites & Opening the Solution

### Prerequisites

- **Visual Studio 2026** with the **.NET Desktop Development** workload.
- **.NET 10.0 SDK** (`net10.0-windows` target framework).
- No third-party NuGet packages (built-in .NET SDK + MSTest).

### Open the solution

1. Launch Visual Studio 2026 and choose **Open a project or solution**.
2. Open **`20231503_ManishRay_Assignment3.slnx`** in the repository root.
3. Projects loaded:
   - `20231503_ManishRay_Assignment3` - WinForms GUI application.
   - `20231503_ManishRay_Assignment3.Tests` - MSTest unit tests.

### Build output location

`Directory.Build.props` sets `UseArtifactsOutput`, so **there are no per-project `bin/` or `obj/`
folders**. Every build artefact goes to a single `.artifacts` folder placed one level **above** the
solution folder (outside the Git repository). This is a deliberate workaround for the Windows
260-character path limit - the repo path plus the SDK's generated file names would otherwise
overflow it. To clean, delete that `.artifacts` folder; only `.vs/` needs removing from the
solution folder before zipping.

---

## 3. Running the Application

### Visual Studio

1. Set `20231503_ManishRay_Assignment3` as the **Startup Project**.
2. Press **F5**.
3. On the dashboard:
   - Select an **Active User** from the top-right dropdown.
   - Click account tabs to switch between the customer's accounts.
   - Use **DEPOSIT**, **WITHDRAW** and **CALC INTEREST**.
   - Click **+ ADD ACCOUNT** to attach a new account to the active customer.
   - Click **TRANSFER »** to move funds between the customer's own accounts.
   - Click **MANAGE CUSTOMERS** to add, update or delete customers.

Data is restored automatically when the window opens and saved automatically when it closes -
there is no save button. The store is `bank_data.json`, written next to the executable.

### Command line

```bash
dotnet run --project "20231503_ManishRay_Assignment3/20231503_ManishRay_Assignment3.csproj"
```

---

## 4. Unit Tests

### Visual Studio

1. **Test > Test Explorer**.
2. **Build > Build Solution**.
3. **Run All Tests**.

### Command line

```bash
dotnet test "20231503_ManishRay_Assignment3.Tests/20231503_ManishRay_Assignment3.Tests.csproj"
```

### Current result

- **Total: 54 - Passed: 54 - Failed: 0**
- `TransferControllerTests` follows the Task 1 Gherkin one-for-one (T1-SO1 … T1-S11): every
  successful-transfer example, both overdraft cases, all failure states and boundaries, the
  same-account / non-positive rejections, customer scoping, and all four staff 50%-fee rows.
- `JsonPersistenceServiceTests` (Task 5): polymorphic save/load round-trip, staff role and
  type-specific fields preserved, domain rules still enforced on restored accounts, corrupt file
  quarantined, unknown schema rejected, `BankController` save-then-load, and T1-S11 (balances
  survive a restart).
- `CustomerControllerTests` / `AccountControllerTests`: CRUD, staff creation, last-customer
  guard, 1:N add/remove, unknown-customer / unknown-type handling.
- `EverydayAccountTests` / `InvestmentAccountTests` / `OmniAccountTests`: the Sprint 1 domain
  rules - deposits, withdrawals, overdraft limits, interest and the staff-discounted failed fee.

> If the test host reports that a file was blocked by an Application Control policy, run the tests
> from Visual Studio instead - that is a machine security-policy restriction, not a build failure.

---

## 5. Project Structure

```text
20231503_Manish_Advanced_Programming_Assessments_3/
├── 20231503_ManishRay_Assignment3/                  # WinForms GUI project
│   ├── Controllers/
│   │   ├── BankController.cs                        # Facade: owns repository + segregated controllers
│   │   ├── CustomerController.cs                    # Customer CRUD
│   │   ├── AccountController.cs                     # Account add / remove / list (1:N)
│   │   └── TransferController.cs                    # Intra-account transfers
│   ├── Data/
│   │   └── BankRepository.cs                        # Shared in-memory model + Load/Save
│   ├── Exceptions/
│   │   ├── BankingException.cs
│   │   ├── InsufficientFundsException.cs
│   │   └── PersistenceException.cs                  # JSON save/load failure (Task 5)
│   ├── Persistence/
│   │   ├── BankDataFile.cs                          # Serialized root envelope (Task 5)
│   │   └── JsonPersistenceService.cs                # System.Text.Json save / load layer (Task 5)
│   ├── Models/
│   │   ├── Account.cs                               # Abstract account base
│   │   ├── EverydayAccount.cs
│   │   ├── InvestmentAccount.cs
│   │   ├── OmniAccount.cs
│   │   ├── User.cs                                  # Abstract user base (AddAccount / RemoveAccount)
│   │   ├── Customer.cs                              # IsStaff => false
│   │   └── BankStaff.cs                             # IsStaff => true, StaffId
│   ├── BaseForm.cs / BaseForm.Designer.cs           # Visual inheritance base (Task 4)
│   ├── Form1.cs / Form1.Designer.cs                 # Main dashboard (dynamic account tabs)
│   ├── CustomerManagementForm.cs / .Designer.cs     # Customer management modal
│   ├── AddAccountForm.cs / .Designer.cs             # Add-account modal (Sprint 3)
│   ├── TransferForm.cs / .Designer.cs               # Intra-account transfer modal (Task 4)
│   └── Program.cs
│
├── 20231503_ManishRay_Assignment3.Tests/            # MSTest project (54 tests)
│   ├── EverydayAccountTests.cs
│   ├── InvestmentAccountTests.cs
│   ├── OmniAccountTests.cs
│   ├── CustomerControllerTests.cs
│   ├── AccountControllerTests.cs
│   ├── TransferControllerTests.cs                   # Task 6 - the Task 1 Gherkin, scenario by scenario
│   └── JsonPersistenceServiceTests.cs               # Task 5 - JSON round-trip / edge cases
│
├── VSdoc/                                           # Task 7 - generated API documentation (VSdocman)
│   └── index.html                                   # Entry point; one topic page per namespace / type / member
│
├── AdvancedProgrammingUMLDiagrams.drawio            # Task 3 - updated UML class diagram (source)
├── Directory.Build.props                            # Redirects build output to ../.artifacts
├── 20231503_ManishRay_Assignment3.slnx              # Solution file
├── .gitignore
└── README.md
```

Planning and hand-over documents live one level up and are submitted to Canvas separately:

| File | Task |
|------|------|
| `20231503_Manish_Kumar_Ray_Report.docx` | 3, 4, 5 - UML class diagram, UI wireframes, persistence sequence diagrams |
| `20231503_Manish_Kumar_Ray_Gherkin_Scenarios.docx` | 1 - Gherkin feature file |
| `20231503_Manish_Kumar_Ray_Class_Extensions_Report.docx` | 3 - attributes and method signatures |
| `20231503_Manish_Kumar_Ray_Test_Results.docx` | 6 - test plan, results table, debugging log |
| `20231503_Manish_Kumar_Ray_User_Guide.docx` | 8 - 3-page bank staff user guide |
| `Generated references/` | Source material and the generated Task 7 documentation report |
| `img/`, `sequence diagram/` | Screenshots and exported diagram images used in the documents |

---

## 6. Version Control

- Development branch: **`feature/sprint3-integration`** - all Sprint 3 work was committed here, with
  no development on `main`.
- `.gitignore` excludes `bin/`, `obj/`, `.vs/` and other IDE/build artefacts, plus the runtime data
  files (`bank_data.json`, `bank_data.json.tmp`, `bank_data.json.corrupt-*`). Builds are
  additionally redirected out of the repo via `Directory.Build.props` (`UseArtifactsOutput`) - see
  "Build output location" in section 2.
- Commit history on the feature branch, oldest first:

  | Commit | Message |
  |--------|---------|
  | `da247f3` | feat: Adding new account, updated models and UML |
  | `fcf67a1` | feat: Updated UML |
  | `5dcba50` | feat: Transfer feature and UI revamp |
  | `af87893` | feat: JSON persistence implemented, controllers seggregated |
  | `1276830` | feat: XML comments added for document generation, test cases updated |
  | `4780474` | feat: VSdocman used for XML document generation |
  | `d2da755` | feat: XML documentation generated to html |
  | `ef5cfb4` | feat: Updated UML |

- **Release:** `feature/sprint3-integration` was merged into `main` through **pull request #1**
  (merge commit `da60a68`), marking the Sprint 3 release. It is a true merge commit rather than a
  fast-forward or a squash, so the feature branch and the point where it rejoined `main` both stay
  visible in `git log --graph --all`.
