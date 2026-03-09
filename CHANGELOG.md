# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial implementation targeting iOS and Android.
- `CustomTabbedPage` control with per-tab icon, badge, and text visibility support.
- `CustomTabPage` base class for individual tab pages.
- `iOSTabBarMode` enum (`NativeGlass` / `Branded`) to control iOS bar appearance.
- `UseCustomTabbedPage()` `MauiAppBuilder` extension to register native mappers.
- Sample application demonstrating all major features.
- Unit tests for bindable property defaults.
- `Directory.Build.props` for centralised version and build configuration.
- `.editorconfig` for consistent code style across all editors.
- `global.json` to pin the .NET 8 SDK.
- `CONTRIBUTING.md`, `CODE_OF_CONDUCT.md`, and GitHub issue/PR templates.
- Improved CI/CD workflows with test execution and code quality gates.

[Unreleased]: https://github.com/YOUR_ORG/Plugin.Maui.CustomTabbedPage/compare/HEAD
