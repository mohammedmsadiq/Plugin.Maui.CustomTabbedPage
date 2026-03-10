// Tests for CustomTabbedPage BindableProperty defaults have been removed.
// BindableProperty requires the MAUI runtime to initialise, which is not
// available in a plain net9.0 test project.  These tests should be run as
// part of an instrumented UI-test suite on a real device or emulator.
//
// Only pure-C# (enum, value-type) tests belong in this project.

namespace Plugin.Maui.CustomTabbedPage.Tests;
