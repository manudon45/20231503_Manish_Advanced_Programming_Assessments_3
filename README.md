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

### Feature status

| Task | Feature | Status |
|------|---------|--------|
| 1 | Gherkin scenarios for intra-account transfers and staff fee logic | Complete (`Gherkin-Scenarios.docx`) |
| 2 | `feature/sprint3-integration` branch + `.gitignore` | Complete |
| 3 | One-to-many account model, dynamic "Add New Account", Bank Staff distinction | Complete |
| 4 | Form inheritance (`BaseForm`), dedicated intra-account transfer screen, wireframes | Complete |
| 5 | JSON serialization with polymorphic account types, auto save/load | In progress |
| 6 | Manual test plan and results table | In progress |
| 7 | Controller XML documentation + generated report | In progress |
| 8 | 3-page bank staff user guide | In progress |

---

## 2. Sprint 3 - Task 3: Architectural Evolution (One-to-Many)

Sprint 1 and Sprint 2 gave every customer exactly three accounts created in the constructor. Sprint 3
turns the `List<Account>` into a true one-to-many relationship that can grow at runtime through the
Controller.

### Model changes

- **`User`** (abstract base)
  - `AddAccount(Account account)` - attaches a new account to the `Accounts` collection.
  - `RemoveAccount(Account account)` - detaches an account, always keeping a minimum of one.
- **`Customer` / `BankStaff`** - unchanged construction, but the account list is no longer fixed in size.

### Bank Staff distinction

The architectural difference between a regular customer and a staff member is modelled with
**inheritance plus a polymorphic flag**:

- `User.IsStaff` is an `abstract bool` property.
- `Customer` overrides it to return `false`; `BankStaff` overrides it to return `true` and adds a
  `StaffId` attribute.
- `Account.Deposit` / `Account.Withdraw` accept an `isStaff` argument; when `true` the failed
  transaction fee on Investment and Omni accounts is halved (the 50% Bank Staff Benefit).

### Controller changes (`CustomerController`)

| Method | Purpose |
|--------|---------|
| `AddAccountToCustomer(customerNumber, accountType, initialBalance, extraParameter)` | Creates an `EverydayAccount`, `InvestmentAccount` or `OmniAccount` and adds it to the customer. `extraParameter` is the interest rate (Investment) or overdraft limit (Omni). Returns `bool`. |
| `RemoveAccountFromCustomer(customerNumber, accountIndex)` | Removes one account, keeping a minimum of one. |
| `GetAccountsForCustomer(customerNumber)` | Returns the customer's `List<Account>`. |

### UI changes

- The main dashboard account tabs are now generated dynamically from `currentUser.Accounts`
  inside a horizontally scrolling `FlowLayoutPanel`, so any number of accounts is supported.
- A new **"+ ADD ACCOUNT"** button opens `AddAccountForm`, a modal dialog that uses `ComboBox`
  selection for the account type and adds the account through the Controller (not the Form).
- Switching the active user rebuilds the tab strip.

### Supporting documents

- `Task3-Class-Extensions-Report.md` - full attribute and method-signature report for the
  `Customer`, `Account` and `Controller` classes.
- `AdvancedProgrammingUMLDiagrams.drawio` - updated class diagram showing the `1 --- 0..*`
  Customer/Account association and the Bank Staff distinction.

---

## 2b. Sprint 3 - Task 4: High-Fidelity UI & Form Inheritance

### Visual inheritance - `BaseForm`

`BaseForm : Form` is the single source of visual truth. Every window inherits it:
`Form1`, `CustomerManagementForm`, `AddAccountForm` and `TransferForm`.

`BaseForm` provides:

- the palette constants (`NavyDark`, `Gold`, `TextGray`, ...) as `protected` members;
- a constructor that applies `BackColor` / `ForeColor` / `Font` / `AutoScaleMode`;
- `CreateBrandBar(subtitle)` - the gold "MB" logo + "ManishRayyy Bank" title strip;
- control factories: `CreatePrimaryButton` / `CreateAccentButton` / `CreateNeutralButton` /
  `CreateDangerButton`, `CreateFieldLabel`, `CreateStyledTextBox`, `CreateStyledComboBox`.

### Intra-account transfer

- **Controller logic** (`CustomerController.TransferFunds(customerNumber, sourceIndex,
  destinationIndex, amount)`) lives in the Controller, not the Form. It validates the customer,
  the account indices, that source and destination differ, and a positive amount, then reuses
  `Account.Withdraw` / `Account.Deposit` so overdraft limits, balance checks and the
  staff-discounted failed-transaction fee all apply. A failed withdrawal aborts the transfer
  before any money reaches the destination (no partial transfers).
- **`TransferForm`** is the dedicated screen: a customer `ComboBox`, plus **Source** and
  **Destination** `ComboBox` pickers populated from that customer's `Accounts` (no free-text
  entry), a live balance caption under each, an amount field, and a green/red result line.
  Opened from the dashboard's **TRANSFER »** button, primed with the active user.

### Navigation / wireframes

- `Task4-UI-Wireframes-and-Sitemap.md` - sitemap (Mermaid), navigation flow, ASCII wireframes for
  every screen (with a detailed layout for the new Intra-Account Transfer interface), and the
  form-inheritance tree.

---

## 3. Prerequisites & Opening the Solution

### Prerequisites

- **Visual Studio 2022** (17.8 or later) with the **.NET Desktop Development** workload.
- **.NET 10.0 SDK** (`net10.0-windows` target framework).
- No third-party NuGet packages (built-in .NET SDK + MSTest).

### Open the solution

1. Launch Visual Studio 2022 and choose **Open a project or solution**.
2. Open **`20231503_ManishRay_Assignment3.slnx`** in the repository root.
3. Projects loaded:
   - `20231503_ManishRay_Assignment3` - WinForms GUI application.
   - `20231503_ManishRay_Assignment3.Tests` - MSTest unit tests.

---

## 4. Running the Application

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

### Command line

```bash
dotnet run --project "20231503_ManishRay_Assignment3/20231503_ManishRay_Assignment3.csproj"
```

---

## 5. Unit Tests

### Visual Studio

1. **Test > Test Explorer**.
2. **Build > Build Solution**.
3. **Run All Tests**.

### Command line

```bash
dotnet test "20231503_ManishRay_Assignment3.Tests/20231503_ManishRay_Assignment3.Tests.csproj"
```

### Current result

- **Total: 23 - Passed: 23 - Failed: 0**
- Includes `AddAccountToCustomer` (one-to-many) and `TransferFunds` coverage: valid transfer,
  same-account rejection, insufficient-funds rollback, and the staff 50% failed-fee discount.

---

## 6. Project Structure

```text
20231503_Manish_Advanced_Programming_Assessments_3/
├── 20231503_ManishRay_Assignment3/                  # WinForms GUI project
│   ├── Controllers/
│   │   └── CustomerController.cs                    # MVC Controller (customer + account CRUD)
│   ├── Exceptions/
│   │   ├── BankingException.cs
│   │   └── InsufficientFundsException.cs
│   ├── Models/
│   │   ├── Account.cs                               # Abstract account base
│   │   ├── EverydayAccount.cs
│   │   ├── InvestmentAccount.cs
│   │   ├── OmniAccount.cs
│   │   ├── User.cs                                  # Abstract user base (AddAccount / RemoveAccount)
│   │   ├── Customer.cs                              # IsStaff => false
│   │   └── BankStaff.cs                             # IsStaff => true, StaffId
│   ├── BaseForm.cs                                  # Visual inheritance base (Task 4)
│   ├── Form1.cs / Form1.Designer.cs                 # Main dashboard (dynamic account tabs)
│   ├── CustomerManagementForm.cs / .Designer.cs     # Customer management modal
│   ├── AddAccountForm.cs                            # Add-account modal (Sprint 3)
│   ├── TransferForm.cs                              # Intra-account transfer modal (Task 4)
│   └── Program.cs
│
├── 20231503_ManishRay_Assignment3.Tests/           # MSTest project
│   ├── EverydayAccountTests.cs
│   ├── InvestmentAccountTests.cs
│   ├── OmniAccountTests.cs
│   └── CustomerControllerTests.cs
│
├── AdvancedProgrammingUMLDiagrams.drawio           # Updated UML class diagram
├── Gherkin-Scenarios.docx                          # Task 1 BDD scenarios
├── Task3-Class-Extensions-Report.md                # Task 3 class extensions report
├── Task4-UI-Wireframes-and-Sitemap.md              # Task 4 wireframes, sitemap, form-inheritance tree
└── README.md
```

---

## 7. Version Control

- Development branch: **`feature/sprint3-integration`** (no work on `main`).
- `.gitignore` excludes `bin/`, `obj/`, `.vs/` and other IDE/build artefacts.
- The feature branch is merged into `main` once Task 5 is functional to mark the project release.
