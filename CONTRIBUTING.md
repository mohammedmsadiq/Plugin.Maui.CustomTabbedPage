# Contributing to Plugin.Maui.CustomTabbedPage

Thank you for your interest in contributing! Contributions of any kind — bug reports, feature requests, documentation improvements, or pull requests — are warmly welcome.

## Getting Started

1. Fork the repository and clone your fork locally.
2. Make sure you have the [.NET 9 SDK](https://dotnet.microsoft.com/download) and the MAUI workload installed:
   ```bash
   dotnet workload install maui
   ```
3. Open `Plugin.Maui.CustomTabbedPage.sln` in Visual Studio or Rider.
4. Build the solution to verify everything compiles.

## Project Structure

```
Plugin.Maui.CustomTabbedPage/
├── src/                          # Library source
│   └── Plugin.Maui.CustomTabbedPage/
│       ├── Controls/             # CustomTabbedPage, CustomTabPage
│       ├── Enums/                # iOSTabBarMode
│       ├── Extensions/           # MauiAppBuilderExtensions
│       └── Platforms/
│           ├── Android/          # Android native mapper
│           └── iOS/              # iOS native mapper
├── samples/                      # Sample application
│   └── Plugin.Maui.CustomTabbedPage.Sample/
└── tests/                        # Unit tests
    └── Plugin.Maui.CustomTabbedPage.Tests/
```

## Running Tests

Unit tests target `net9.0` and do not require any MAUI runtime or device:

```bash
dotnet test tests/Plugin.Maui.CustomTabbedPage.Tests
```

## Coding Guidelines

- Follow the existing code style. An `.editorconfig` is provided at the root.
- Enable nullable reference types (`Nullable=enable`) and resolve all warnings.
- Add XML documentation comments to all public APIs.
- Write or update unit tests for any behavioural changes.

## Submitting a Pull Request

1. Create a branch from `develop` for your change (e.g. `feature/my-feature` or `fix/my-bug`).
2. Write a clear, concise description of your changes in the PR body.
3. Ensure all CI checks pass before requesting a review.
4. Reference any related issue numbers (e.g. `Closes #42`).
5. Keep pull requests focused — one change per PR is easiest to review.

## Reporting Bugs

Use the **Bug report** issue template and include:
- The version of the plugin you are using.
- The target platform and OS version.
- A minimal reproduction case if possible.

## Requesting Features

Use the **Feature request** issue template. Please describe the problem you are trying to solve so we can better understand the use case.

## Code of Conduct

This project follows the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating you agree to abide by its terms.
