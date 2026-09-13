# Bank Account Management System - Technical Documentation

**Student:** Manish Ray (20231503)
**Course:** IT7742 Advanced Programming - Assessment 3 (Sprint 3)

This document is the API reference for the Bank Account Management System, generated from the
XML documentation comments in the source code using DocFX.

## Namespaces

| Namespace | Purpose |
|-----------|---------|
| `Controllers` | `BankController` facade plus the `Customer`, `Account` and `Transfer` controllers that the forms call. |
| `Data` | `BankRepository` - the shared in-memory model and the single point of load/save. |
| `Models` | `User` / `Customer` / `BankStaff` and `Account` / `EverydayAccount` / `InvestmentAccount` / `OmniAccount`. |
| `Persistence` | `JsonPersistenceService` and the `BankDataFile` envelope written to `bank_data.json`. |
| `Exceptions` | Domain exceptions thrown by the model and controllers. |
| Forms (root namespace) | `BaseForm`, `Form1`, `CustomerManagementForm`, `AddAccountForm`, `TransferForm`. |

See the **API Reference** section for every public type and member.
